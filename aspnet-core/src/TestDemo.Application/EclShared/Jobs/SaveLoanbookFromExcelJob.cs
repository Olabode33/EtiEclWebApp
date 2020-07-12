using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Organizations;
using Abp.Threading;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.BatchEcls.BatchEclInput;
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.EclShared.Importing.Utils;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;
using TestDemo.OBE;
using TestDemo.ObeInputs;
using TestDemo.Retail;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class SaveLoanbookFromExcelJob : BackgroundJob<SaveEclLoanbookDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly ILoanbookExcelDataReader _loanbookExcelDataReader;
        private readonly IInvalidLoanbookExporter _invalidLoanbookExporter;
        private readonly IRepository<RetailEclDataLoanBook, Guid> _retailEclDataLoanbookRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _wholesaleEclDataLoanbookRepository;
        private readonly IRepository<ObeEclDataLoanBook, Guid> _obeEclDataLoanbookRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadSummaryRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadSummaryRepository;
        private readonly IRepository<ObeEclUpload, Guid> _obeUploadSummaryRepository;
        private readonly IRepository<BatchEclUpload, Guid> _batchUploadSummaryRepository;
        private readonly IRepository<BatchEclDataLoanBook, Guid> _batchDataLoanbookRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
        private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
        private readonly IRepository<WholesaleEcl, Guid> _wholesaleEclRepository;
        private readonly IRepository<TrackEclDataLoanBookException> _loanbookExceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IEclCustomRepository _customRepository;
        private readonly IValidationUtil _validator;

        public SaveLoanbookFromExcelJob(
            ILoanbookExcelDataReader loanbookExcelDataReader, 
            IInvalidLoanbookExporter invalidLoanbookExporter, 
            IRepository<RetailEclDataLoanBook, Guid> retailEclDataLoanbookRepository, 
            IRepository<WholesaleEclDataLoanBook, Guid> wholesaleEclDataLoanbookRepository, 
            IRepository<ObeEclDataLoanBook, Guid> obeEclDataPaymentScheduleRepository, 
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository, 
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository, 
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository,
            IRepository<BatchEclUpload, Guid> batchUploadSummaryRepository,
            IRepository<BatchEclDataLoanBook, Guid> batchDataLoanbookRepository,
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<RetailEcl, Guid> retailEclRepository,
            IRepository<ObeEcl, Guid> obeEclRepository,
            IRepository<WholesaleEcl, Guid> wholesaleEclRepository,
            IRepository<TrackEclDataLoanBookException> loanbookExceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IEclCustomRepository customRepository,
            IValidationUtil validator,
            IObjectMapper objectMapper)
        {
            _loanbookExcelDataReader = loanbookExcelDataReader;
            _invalidLoanbookExporter = invalidLoanbookExporter;
            _retailEclDataLoanbookRepository = retailEclDataLoanbookRepository;
            _wholesaleEclDataLoanbookRepository = wholesaleEclDataLoanbookRepository;
            _obeEclDataLoanbookRepository = obeEclDataPaymentScheduleRepository;
            _retailUploadSummaryRepository = retailUploadSummaryRepository;
            _wholesaleUploadSummaryRepository = wholesaleUploadSummaryRepository;
            _obeUploadSummaryRepository = obeUploadSummaryRepository;
            _batchUploadSummaryRepository = batchUploadSummaryRepository;
            _batchDataLoanbookRepository = batchDataLoanbookRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _retailEclRepository = retailEclRepository;
            _obeEclRepository = obeEclRepository;
            _wholesaleEclRepository = wholesaleEclRepository;
            _objectMapper = objectMapper;
            _customRepository = customRepository;
            _loanbookExceptionTrackerRepository = loanbookExceptionTrackerRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _validator = validator;
        }

        [UnitOfWork]
        public override void Execute(SaveEclLoanbookDataFromExcelJobArgs args)
        {
            var loanbooks = args.Loanbook;
            //if (loanbooks == null || !loanbooks.Any())
            //{
            //    SendInvalidExcelNotification(args);
            //    return;
            //}
            var validatedLoanbooks = ValidateLoanBook(loanbooks);
            var EclId = GetEclId(args.Args);
            if (EclId != null)
            {
                CreateLoanbook(args.Args, validatedLoanbooks, (Guid)EclId);
            }
            else
            {
                Logger.Debug("ImportLoanbookFromExcelJob: Error Retrieving ECL Id, " + DateTime.Now.ToString());
            }
        }

        private List<ImportLoanbookDto> ValidateLoanBook(List<ImportLoanbookDtoNew> loanbooks)
        {
            var invalidLoanbook = new List<ImportLoanbookDto>();
            var validLoanbook = new List<ImportLoanbookDto>();
            //var loanbookArray = loanbooks.ToArray();

            foreach(var loanbookNew in loanbooks)
            {
                var exceptionMessage = new StringBuilder();
                var loanbook = new ImportLoanbookDto();

                try
                {
                    loanbook.CustomerNo = loanbookNew.CustomerNo;
                    loanbook.AccountNo = loanbookNew.AccountNo;
                    loanbook.ContractNo = loanbookNew.ContractNo;
                    loanbook.CustomerName = loanbookNew.CustomerName;
                    loanbook.SnapshotDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.SnapshotDate, nameof(loanbook.SnapshotDate), exceptionMessage);
                    loanbook.Segment = loanbookNew.Segment;
                    loanbook.Sector = loanbookNew.Sector;
                    loanbook.Currency = loanbookNew.Currency;
                    loanbook.ProductType = loanbookNew.ProductType;
                    loanbook.ProductMapping = loanbookNew.ProductMapping;
                    loanbook.SpecialisedLending = loanbookNew.SpecialisedLending;
                    loanbook.RatingModel = loanbookNew.RatingModel;
                    loanbook.OriginalRating = _validator.ValidateIntegerValueFromRowOrNull(loanbookNew.OriginalRating, nameof(loanbook.OriginalRating), exceptionMessage);
                    loanbook.CurrentRating = _validator.ValidateIntegerValueFromRowOrNull(loanbookNew.CurrentRating, nameof(loanbook.CurrentRating), exceptionMessage);
                    loanbook.LifetimePD = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.LifetimePD, nameof(loanbook.LifetimePD), exceptionMessage);
                    loanbook.Month12PD = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.Month12PD, nameof(loanbook.Month12PD), exceptionMessage);
                    loanbook.DaysPastDue = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.DaysPastDue, nameof(loanbook.DaysPastDue), exceptionMessage);
                    loanbook.WatchlistIndicator = _validator.ValidateBooleanValueFromRowOrNull(loanbookNew.WatchlistIndicator, nameof(loanbook.WatchlistIndicator), exceptionMessage);
                    loanbook.Classification = loanbookNew.Classification;
                    loanbook.ImpairedDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.ImpairedDate, nameof(loanbook.ImpairedDate), exceptionMessage);
                    loanbook.DefaultDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.DefaultDate, nameof(loanbook.DefaultDate), exceptionMessage);
                    loanbook.CreditLimit = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CreditLimit, nameof(loanbook.CreditLimit), exceptionMessage);
                    loanbook.OriginalBalanceLCY = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.OriginalBalanceLCY, nameof(loanbook.OriginalBalanceLCY), exceptionMessage);
                    loanbook.OutstandingBalanceLCY = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.OutstandingBalanceLCY, nameof(loanbook.OutstandingBalanceLCY), exceptionMessage);
                    loanbook.OutstandingBalanceACY = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.OutstandingBalanceACY, nameof(loanbook.OutstandingBalanceACY), exceptionMessage);
                    loanbook.ContractStartDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.ContractStartDate, nameof(loanbook.ContractStartDate), exceptionMessage);
                    loanbook.ContractEndDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.ContractEndDate, nameof(loanbook.ContractEndDate), exceptionMessage);
                    loanbook.RestructureIndicator = _validator.ValidateBooleanValueFromRowOrNull(loanbookNew.RestructureIndicator, nameof(loanbook.RestructureIndicator), exceptionMessage);
                    loanbook.RestructureRisk = loanbookNew.RestructureRisk;
                    loanbook.RestructureType = loanbookNew.RestructureType;
                    loanbook.RestructureStartDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.RestructureStartDate, nameof(loanbook.RestructureStartDate), exceptionMessage);
                    loanbook.RestructureEndDate = _validator.ValidateDateTimeValueFromRowOrNull(loanbookNew.RestructureEndDate, nameof(loanbook.RestructureEndDate), exceptionMessage);
                    loanbook.PrincipalPaymentTermsOrigination = loanbookNew.PrincipalPaymentTermsOrigination;
                    loanbook.PPTOPeriod = _validator.ValidateIntegerValueFromRowOrNull(loanbookNew.PPTOPeriod, nameof(loanbook.PPTOPeriod), exceptionMessage);
                    loanbook.InterestPaymentTermsOrigination = loanbookNew.InterestPaymentTermsOrigination;
                    loanbook.IPTOPeriod = _validator.ValidateIntegerValueFromRowOrNull(loanbookNew.IPTOPeriod, nameof(loanbook.IPTOPeriod), exceptionMessage);
                    loanbook.PrincipalPaymentStructure = loanbookNew.PrincipalPaymentStructure;
                    loanbook.InterestPaymentStructure = loanbookNew.InterestPaymentStructure;
                    loanbook.InterestRateType = loanbookNew.InterestRateType;
                    loanbook.BaseRate = loanbookNew.BaseRate;
                    loanbook.OriginationContractualInterestRate = loanbookNew.OriginationContractualInterestRate;
                    loanbook.IntroductoryPeriod = _validator.ValidateIntegerValueFromRowOrNull(loanbookNew.IntroductoryPeriod, nameof(loanbook.IntroductoryPeriod), exceptionMessage);
                    loanbook.PostIPContractualInterestRate = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.PostIPContractualInterestRate, nameof(loanbook.PostIPContractualInterestRate), exceptionMessage);
                    loanbook.CurrentContractualInterestRate = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CurrentContractualInterestRate, nameof(loanbook.CurrentContractualInterestRate), exceptionMessage);
                    loanbook.EIR = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.EIR, nameof(loanbook.EIR), exceptionMessage);
                    loanbook.DebentureOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.DebentureOMV, nameof(loanbook.DebentureOMV), exceptionMessage);
                    loanbook.DebentureFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.DebentureFSV, nameof(loanbook.DebentureFSV), exceptionMessage);
                    loanbook.CashOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CashOMV, nameof(loanbook.CashOMV), exceptionMessage);
                    loanbook.CashFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CashFSV, nameof(loanbook.CashFSV), exceptionMessage);
                    loanbook.InventoryOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.InventoryOMV, nameof(loanbook.InventoryOMV), exceptionMessage);
                    loanbook.InventoryFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.InventoryFSV, nameof(loanbook.InventoryFSV), exceptionMessage);
                    loanbook.PlantEquipmentOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.PlantEquipmentOMV, nameof(loanbook.PlantEquipmentOMV), exceptionMessage);
                    loanbook.PlantEquipmentFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.PlantEquipmentFSV, nameof(loanbook.PlantEquipmentFSV), exceptionMessage);
                    loanbook.ResidentialPropertyOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.ResidentialPropertyOMV, nameof(loanbook.ResidentialPropertyOMV), exceptionMessage);
                    loanbook.ResidentialPropertyFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.ResidentialPropertyFSV, nameof(loanbook.ResidentialPropertyFSV), exceptionMessage);
                    loanbook.CommercialPropertyOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CommercialPropertyOMV, nameof(loanbook.CommercialPropertyOMV), exceptionMessage);
                    loanbook.CommercialPropertyFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CommercialPropertyFSV, nameof(loanbook.CommercialPropertyFSV), exceptionMessage);
                    loanbook.ReceivablesOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.ReceivablesOMV, nameof(loanbook.ReceivablesOMV), exceptionMessage);
                    loanbook.ReceivablesFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.ReceivablesFSV, nameof(loanbook.ReceivablesFSV), exceptionMessage);
                    loanbook.SharesOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.SharesOMV, nameof(loanbook.SharesOMV), exceptionMessage);
                    loanbook.SharesFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.SharesFSV, nameof(loanbook.SharesFSV), exceptionMessage);
                    loanbook.VehicleOMV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.VehicleOMV, nameof(loanbook.VehicleOMV), exceptionMessage);
                    loanbook.VehicleFSV = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.VehicleFSV, nameof(loanbook.VehicleFSV), exceptionMessage);
                    loanbook.CureRate = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.CureRate, nameof(loanbook.CureRate), exceptionMessage);
                    loanbook.GuaranteeIndicator = _validator.ValidateBooleanValueFromRowOrNull(loanbookNew.GuaranteeIndicator, nameof(loanbook.GuaranteeIndicator), exceptionMessage);
                    loanbook.GuarantorPD = loanbookNew.GuarantorPD;
                    loanbook.GuarantorLGD = loanbookNew.GuarantorLGD;
                    loanbook.GuaranteeValue = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.GuaranteeValue, nameof(loanbook.GuaranteeValue), exceptionMessage);
                    loanbook.GuaranteeLevel = _validator.ValidateDoubleValueFromRowOrNull(loanbookNew.GuaranteeLevel, nameof(loanbook.GuaranteeLevel), exceptionMessage);

                    if (exceptionMessage.Length > 0)
                    {
                        loanbook.Exception = exceptionMessage.ToString();
                    }

                    validLoanbook.Add(loanbook);
                }
                catch (Exception e)
                {
                    loanbook.Exception = e.Message;
                }
            }

            return validLoanbook;
        }

        private Guid? GetEclId(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return retailSummary == null ? (Guid?)null : retailSummary.RetailEclId;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return wholesaleSummary == null ? (Guid?)null : wholesaleSummary.WholesaleEclId;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return obeSummary == null ? (Guid?)null : (Guid)obeSummary.ObeEclId;

                case FrameworkEnum.Batch:
                    var batchSummary = _batchUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    return batchSummary == null ? (Guid?)null : (Guid)batchSummary.BatchId;
                default:
                    return null;
            }
        }

        private List<ImportLoanbookDtoNew> GetLoanbookListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                Logger.Debug("ImportLoanbookFromExcelJobFileToken: " + file.Id);
                return _loanbookExcelDataReader.GetImportLoanbookFromExcel(file.Bytes);
            }
            catch (Exception e)
            {
                Logger.Debug("ImportLoanbookFromExcelJobError: " + e.Message);
                Logger.Debug("ImportLoanbookFromExcelJobErrorInnerException: " + e.InnerException);
                Logger.Debug("ImportLoanbookFromExcelJobErrorStackTrack: " + e.StackTrace);
                return null;
            }
        }

        [UnitOfWork]
        private void CreateLoanbook(ImportEclDataFromExcelJobArgs args, List<ImportLoanbookDto> loanbooks, Guid eclId)
        {
            var invalidLoanbook = new List<ImportLoanbookDto>();

            foreach (var loanbook in loanbooks)
            {
                if (loanbook.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateLoanbookAsync(loanbook, args, eclId));
                    }
                    catch (UserFriendlyException exception)
                    {
                        loanbook.Exception = exception.Message;
                        invalidLoanbook.Add(loanbook);
                    }
                    catch (Exception exception)
                    {
                        loanbook.Exception = exception.ToString();
                        invalidLoanbook.Add(loanbook);
                    }
                }
                else
                {
                    invalidLoanbook.Add(loanbook);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportLoanbookResultAsync(args, invalidLoanbook, eclId));
        }

        private async Task CreateLoanbookAsync(ImportLoanbookDto input, ImportEclDataFromExcelJobArgs args, Guid eclId)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    await CreateForRetail(input, eclId);
                    break;

                case FrameworkEnum.Wholesale:
                    await CreateForWholesale(input, eclId);
                    break;

                case FrameworkEnum.OBE:
                    await CreateForObe(input, eclId);
                    break;

                case FrameworkEnum.Batch:
                    await SplitLoanbook(input, eclId);
                    break;
            }
        }
        
        private async Task SplitLoanbook(ImportLoanbookDto input, Guid batchId)
        {
            //Product Type Seperator
            List<string> nonObeSeperator = new List<string>();
            nonObeSeperator.Add("loan");
            nonObeSeperator.Add("lease");
            nonObeSeperator.Add("od");
            nonObeSeperator.Add("morgage");

            //Segment Seperator
            List<string> wholesaleSeperator = new List<string>();
            wholesaleSeperator.Add("corporate");
            wholesaleSeperator.Add("commercial");

            List<string> retailSeperator = new List<string>();
            retailSeperator.Add("consumer");

            if (!nonObeSeperator.Any(e => e.ToLower() == input.ProductType.ToLower()))
            {
                var obeEcl = await _obeEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
                if(obeEcl != null)
                {
                    await CreateForObe(input, obeEcl.Id);
                    return;
                }
                else
                {
                    Logger.Debug("SaveLoanbookFromExeclJob.SplitLoanbook: Error: No OBE Ecl found");
                    return;
                }
            }
            else if(retailSeperator.Any(e => e.ToLower() == input.Segment.ToLower()))
            {
                var rEcl = await _retailEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
                if (rEcl != null)
                {
                    await CreateForRetail(input, rEcl.Id);
                    return;
                }
                else
                {
                    Logger.Debug("SaveLoanbookFromExeclJob.SplitLoanbook: Error: No Retail Ecl found");
                    return;
                }
            }
            else if (wholesaleSeperator.Any(e => e.ToLower() == input.Segment.ToLower()))
            {
                var wEcl = await _wholesaleEclRepository.FirstOrDefaultAsync(e => e.BatchId == batchId);
                if (wEcl != null)
                {
                    await CreateForWholesale(input, wEcl.Id);
                    return;
                }
                else
                {
                    Logger.Debug("SaveLoanbookFromExeclJob.SplitLoanbook: Error: No Wholesale Ecl found");
                    return;
                }
            }
            else
            {
                Logger.Debug("SaveLoanbookFromExeclJob.SplitLoanbook: Error: Record cannot be categorized");
                return;
            }
        }

        private async Task CreateForObe(ImportLoanbookDto input, Guid eclId)
        {
            await _obeEclDataLoanbookRepository.InsertAsync(new ObeEclDataLoanBook()
            {
                CustomerNo = input.CustomerNo,
                AccountNo = input.AccountNo,
                ContractNo = input.ContractNo,
                CustomerName = input.CustomerName,
                SnapshotDate = input.SnapshotDate,
                Segment = input.Segment,
                Sector = input.Sector,
                Currency = input.Currency,
                ProductType = input.ProductType,
                ProductMapping = input.ProductMapping,
                SpecialisedLending = input.SpecialisedLending,
                RatingModel = input.RatingModel,
                OriginalRating = input.OriginalRating,
                CurrentRating = input.CurrentRating,
                LifetimePD = input.LifetimePD,
                Month12PD = input.Month12PD,
                DaysPastDue = input.DaysPastDue,
                WatchlistIndicator = input.WatchlistIndicator,
                Classification = input.Classification,
                ImpairedDate = input.ImpairedDate,
                DefaultDate = input.DefaultDate,
                CreditLimit = input.CreditLimit,
                OriginalBalanceLCY = input.OriginalBalanceLCY,
                OutstandingBalanceLCY = input.OutstandingBalanceLCY,
                OutstandingBalanceACY = input.OutstandingBalanceACY,
                ContractStartDate = input.ContractStartDate,
                ContractEndDate = input.ContractEndDate,
                RestructureIndicator = input.RestructureIndicator,
                RestructureRisk = input.RestructureRisk,
                RestructureType = input.RestructureType,
                RestructureStartDate = input.RestructureStartDate,
                RestructureEndDate = input.RestructureEndDate,
                PrincipalPaymentTermsOrigination = input.PrincipalPaymentTermsOrigination,
                PPTOPeriod = input.PPTOPeriod,
                InterestPaymentTermsOrigination = input.InterestPaymentTermsOrigination,
                IPTOPeriod = input.IPTOPeriod,
                PrincipalPaymentStructure = input.PrincipalPaymentStructure,
                InterestPaymentStructure = input.InterestPaymentStructure,
                InterestRateType = input.InterestRateType,
                BaseRate = input.BaseRate,
                OriginationContractualInterestRate = input.OriginationContractualInterestRate,
                IntroductoryPeriod = input.IntroductoryPeriod,
                PostIPContractualInterestRate = input.PostIPContractualInterestRate,
                CurrentContractualInterestRate = input.CurrentContractualInterestRate,
                EIR = input.EIR,
                DebentureOMV = input.DebentureOMV,
                DebentureFSV = input.DebentureFSV,
                CashOMV = input.CashOMV,
                CashFSV = input.CashFSV,
                InventoryOMV = input.InventoryOMV,
                InventoryFSV = input.InventoryFSV,
                PlantEquipmentOMV = input.PlantEquipmentOMV,
                PlantEquipmentFSV = input.PlantEquipmentFSV,
                ResidentialPropertyOMV = input.ResidentialPropertyOMV,
                ResidentialPropertyFSV = input.ResidentialPropertyFSV,
                CommercialPropertyOMV = input.CommercialPropertyOMV,
                CommercialProperty = input.CommercialPropertyFSV,
                ReceivablesOMV = input.ReceivablesOMV,
                ReceivablesFSV = input.ReceivablesFSV,
                SharesOMV = input.SharesOMV,
                SharesFSV = input.SharesFSV,
                VehicleOMV = input.VehicleOMV,
                VehicleFSV = input.VehicleFSV,
                CureRate = input.CureRate,
                GuaranteeIndicator = input.GuaranteeIndicator,
                GuarantorPD = input.GuarantorPD,
                GuarantorLGD = input.GuarantorLGD,
                GuaranteeValue = input.GuaranteeValue,
                GuaranteeLevel = input.GuaranteeLevel,
                ObeEclUploadId = eclId
            });
        }

        private async Task CreateForWholesale(ImportLoanbookDto input, Guid eclId)
        {
            await _wholesaleEclDataLoanbookRepository.InsertAsync(new WholesaleEclDataLoanBook()
            {
                CustomerNo = input.CustomerNo,
                AccountNo = input.AccountNo,
                ContractNo = input.ContractNo,
                CustomerName = input.CustomerName,
                SnapshotDate = input.SnapshotDate,
                Segment = input.Segment,
                Sector = input.Sector,
                Currency = input.Currency,
                ProductType = input.ProductType,
                ProductMapping = input.ProductMapping,
                SpecialisedLending = input.SpecialisedLending,
                RatingModel = input.RatingModel,
                OriginalRating = input.OriginalRating,
                CurrentRating = input.CurrentRating,
                LifetimePD = input.LifetimePD,
                Month12PD = input.Month12PD,
                DaysPastDue = input.DaysPastDue,
                WatchlistIndicator = input.WatchlistIndicator,
                Classification = input.Classification,
                ImpairedDate = input.ImpairedDate,
                DefaultDate = input.DefaultDate,
                CreditLimit = input.CreditLimit,
                OriginalBalanceLCY = input.OriginalBalanceLCY,
                OutstandingBalanceLCY = input.OutstandingBalanceLCY,
                OutstandingBalanceACY = input.OutstandingBalanceACY,
                ContractStartDate = input.ContractStartDate,
                ContractEndDate = input.ContractEndDate,
                RestructureIndicator = input.RestructureIndicator,
                RestructureRisk = input.RestructureRisk,
                RestructureType = input.RestructureType,
                RestructureStartDate = input.RestructureStartDate,
                RestructureEndDate = input.RestructureEndDate,
                PrincipalPaymentTermsOrigination = input.PrincipalPaymentTermsOrigination,
                PPTOPeriod = input.PPTOPeriod,
                InterestPaymentTermsOrigination = input.InterestPaymentTermsOrigination,
                IPTOPeriod = input.IPTOPeriod,
                PrincipalPaymentStructure = input.PrincipalPaymentStructure,
                InterestPaymentStructure = input.InterestPaymentStructure,
                InterestRateType = input.InterestRateType,
                BaseRate = input.BaseRate,
                OriginationContractualInterestRate = input.OriginationContractualInterestRate,
                IntroductoryPeriod = input.IntroductoryPeriod,
                PostIPContractualInterestRate = input.PostIPContractualInterestRate,
                CurrentContractualInterestRate = input.CurrentContractualInterestRate,
                EIR = input.EIR,
                DebentureOMV = input.DebentureOMV,
                DebentureFSV = input.DebentureFSV,
                CashOMV = input.CashOMV,
                CashFSV = input.CashFSV,
                InventoryOMV = input.InventoryOMV,
                InventoryFSV = input.InventoryFSV,
                PlantEquipmentOMV = input.PlantEquipmentOMV,
                PlantEquipmentFSV = input.PlantEquipmentFSV,
                ResidentialPropertyOMV = input.ResidentialPropertyOMV,
                ResidentialPropertyFSV = input.ResidentialPropertyFSV,
                CommercialPropertyOMV = input.CommercialPropertyOMV,
                CommercialProperty = input.CommercialPropertyFSV,
                ReceivablesOMV = input.ReceivablesOMV,
                ReceivablesFSV = input.ReceivablesFSV,
                SharesOMV = input.SharesOMV,
                SharesFSV = input.SharesFSV,
                VehicleOMV = input.VehicleOMV,
                VehicleFSV = input.VehicleFSV,
                CureRate = input.CureRate,
                GuaranteeIndicator = input.GuaranteeIndicator,
                GuarantorPD = input.GuarantorPD,
                GuarantorLGD = input.GuarantorLGD,
                GuaranteeValue = input.GuaranteeValue,
                GuaranteeLevel = input.GuaranteeLevel,
                WholesaleEclUploadId = eclId
            });
        }

        private async Task CreateForRetail(ImportLoanbookDto input, Guid eclId)
        {
            await _retailEclDataLoanbookRepository.InsertAsync(new RetailEclDataLoanBook()
            {
                CustomerNo = input.CustomerNo,
                AccountNo = input.AccountNo,
                ContractNo = input.ContractNo,
                CustomerName = input.CustomerName,
                SnapshotDate = input.SnapshotDate,
                Segment = input.Segment,
                Sector = input.Sector,
                Currency = input.Currency,
                ProductType = input.ProductType,
                ProductMapping = input.ProductMapping,
                SpecialisedLending = input.SpecialisedLending,
                RatingModel = input.RatingModel,
                OriginalRating = input.OriginalRating,
                CurrentRating = input.CurrentRating,
                LifetimePD = input.LifetimePD,
                Month12PD = input.Month12PD,
                DaysPastDue = input.DaysPastDue,
                WatchlistIndicator = input.WatchlistIndicator,
                Classification = input.Classification,
                ImpairedDate = input.ImpairedDate,
                DefaultDate = input.DefaultDate,
                CreditLimit = input.CreditLimit,
                OriginalBalanceLCY = input.OriginalBalanceLCY,
                OutstandingBalanceLCY = input.OutstandingBalanceLCY,
                OutstandingBalanceACY = input.OutstandingBalanceACY,
                ContractStartDate = input.ContractStartDate,
                ContractEndDate = input.ContractEndDate,
                RestructureIndicator = input.RestructureIndicator,
                RestructureRisk = input.RestructureRisk,
                RestructureType = input.RestructureType,
                RestructureStartDate = input.RestructureStartDate,
                RestructureEndDate = input.RestructureEndDate,
                PrincipalPaymentTermsOrigination = input.PrincipalPaymentTermsOrigination,
                PPTOPeriod = input.PPTOPeriod,
                InterestPaymentTermsOrigination = input.InterestPaymentTermsOrigination,
                IPTOPeriod = input.IPTOPeriod,
                PrincipalPaymentStructure = input.PrincipalPaymentStructure,
                InterestPaymentStructure = input.InterestPaymentStructure,
                InterestRateType = input.InterestRateType,
                BaseRate = input.BaseRate,
                OriginationContractualInterestRate = input.OriginationContractualInterestRate,
                IntroductoryPeriod = input.IntroductoryPeriod,
                PostIPContractualInterestRate = input.PostIPContractualInterestRate,
                CurrentContractualInterestRate = input.CurrentContractualInterestRate,
                EIR = input.EIR,
                DebentureOMV = input.DebentureOMV,
                DebentureFSV = input.DebentureFSV,
                CashOMV = input.CashOMV,
                CashFSV = input.CashFSV,
                InventoryOMV = input.InventoryOMV,
                InventoryFSV = input.InventoryFSV,
                PlantEquipmentOMV = input.PlantEquipmentOMV,
                PlantEquipmentFSV = input.PlantEquipmentFSV,
                ResidentialPropertyOMV = input.ResidentialPropertyOMV,
                ResidentialPropertyFSV = input.ResidentialPropertyFSV,
                CommercialPropertyOMV = input.CommercialPropertyOMV,
                CommercialProperty = input.CommercialPropertyFSV,
                ReceivablesOMV = input.ReceivablesOMV,
                ReceivablesFSV = input.ReceivablesFSV,
                SharesOMV = input.SharesOMV,
                SharesFSV = input.SharesFSV,
                VehicleOMV = input.VehicleOMV,
                VehicleFSV = input.VehicleFSV,
                CureRate = input.CureRate,
                GuaranteeIndicator = input.GuaranteeIndicator,
                GuarantorPD = input.GuarantorPD,
                GuarantorLGD = input.GuarantorLGD,
                GuaranteeValue = input.GuaranteeValue,
                GuaranteeLevel = input.GuaranteeLevel,
                RetailEclUploadId = eclId

            });
        }

        private async Task ProcessImportLoanbookResultAsync(ImportEclDataFromExcelJobArgs args, List<ImportLoanbookDto> invalidLoanbook, Guid eclId)
        {
            if (invalidLoanbook.Any())
            {
                foreach (var invalid in invalidLoanbook)
                {
                    await _loanbookExceptionTrackerRepository.InsertAsync(new TrackEclDataLoanBookException
                    {
                        CustomerNo = invalid.CustomerNo,
                        AccountNo = invalid.AccountNo,
                        ContractNo = invalid.ContractNo,
                        CustomerName = invalid.CustomerName,
                        SnapshotDate = invalid.SnapshotDate,
                        Segment = invalid.Segment,
                        Sector = invalid.Sector,
                        Currency = invalid.Currency,
                        ProductType = invalid.ProductType,
                        ProductMapping = invalid.ProductMapping,
                        SpecialisedLending = invalid.SpecialisedLending,
                        RatingModel = invalid.RatingModel,
                        OriginalRating = invalid.OriginalRating,
                        CurrentRating = invalid.CurrentRating,
                        LifetimePD = invalid.LifetimePD,
                        Month12PD = invalid.Month12PD,
                        DaysPastDue = invalid.DaysPastDue,
                        WatchlistIndicator = invalid.WatchlistIndicator,
                        Classification = invalid.Classification,
                        ImpairedDate = invalid.ImpairedDate,
                        DefaultDate = invalid.DefaultDate,
                        CreditLimit = invalid.CreditLimit,
                        OriginalBalanceLCY = invalid.OriginalBalanceLCY,
                        OutstandingBalanceLCY = invalid.OutstandingBalanceLCY,
                        OutstandingBalanceACY = invalid.OutstandingBalanceACY,
                        ContractStartDate = invalid.ContractStartDate,
                        ContractEndDate = invalid.ContractEndDate,
                        RestructureIndicator = invalid.RestructureIndicator,
                        RestructureRisk = invalid.RestructureRisk,
                        RestructureType = invalid.RestructureType,
                        RestructureStartDate = invalid.RestructureStartDate,
                        RestructureEndDate = invalid.RestructureEndDate,
                        PrincipalPaymentTermsOrigination = invalid.PrincipalPaymentTermsOrigination,
                        PPTOPeriod = invalid.PPTOPeriod,
                        InterestPaymentTermsOrigination = invalid.InterestPaymentTermsOrigination,
                        IPTOPeriod = invalid.IPTOPeriod,
                        PrincipalPaymentStructure = invalid.PrincipalPaymentStructure,
                        InterestPaymentStructure = invalid.InterestPaymentStructure,
                        InterestRateType = invalid.InterestRateType,
                        BaseRate = invalid.BaseRate,
                        OriginationContractualInterestRate = invalid.OriginationContractualInterestRate,
                        IntroductoryPeriod = invalid.IntroductoryPeriod,
                        PostIPContractualInterestRate = invalid.PostIPContractualInterestRate,
                        CurrentContractualInterestRate = invalid.CurrentContractualInterestRate,
                        EIR = invalid.EIR,
                        DebentureOMV = invalid.DebentureOMV,
                        DebentureFSV = invalid.DebentureFSV,
                        CashOMV = invalid.CashOMV,
                        CashFSV = invalid.CashFSV,
                        InventoryOMV = invalid.InventoryOMV,
                        InventoryFSV = invalid.InventoryFSV,
                        PlantEquipmentOMV = invalid.PlantEquipmentOMV,
                        PlantEquipmentFSV = invalid.PlantEquipmentFSV,
                        ResidentialPropertyOMV = invalid.ResidentialPropertyOMV,
                        ResidentialPropertyFSV = invalid.ResidentialPropertyFSV,
                        CommercialPropertyOMV = invalid.CommercialPropertyOMV,
                        CommercialProperty = invalid.CommercialPropertyFSV,
                        ReceivablesOMV = invalid.ReceivablesOMV,
                        ReceivablesFSV = invalid.ReceivablesFSV,
                        SharesOMV = invalid.SharesOMV,
                        SharesFSV = invalid.SharesFSV,
                        VehicleOMV = invalid.VehicleOMV,
                        VehicleFSV = invalid.VehicleFSV,
                        CureRate = invalid.CureRate,
                        GuaranteeIndicator = invalid.GuaranteeIndicator,
                        GuarantorPD = invalid.GuarantorPD,
                        GuarantorLGD = invalid.GuarantorLGD,
                        GuaranteeValue = invalid.GuaranteeValue,
                        GuaranteeLevel = invalid.GuaranteeLevel,
                        EclId = eclId,
                        Exception = invalid.Exception
                    });
                }
            }
            _uploadJobsTrackerRepository.Insert(new TrackRunningUploadJobs
            {
                RegisterId = args.UploadSummaryId
            });
        }

        private async Task ExportInvalids(ImportEclDataFromExcelJobArgs args)
        {
            Guid? eclId = null;
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        eclId = retailSummary.RetailEclId;
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        eclId = wholesaleSummary.WholesaleEclId;
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        eclId = (Guid)obeSummary.ObeEclId;
                    }
                    break;
            }
            
            if(eclId != null)
            {
                var invalids = _loanbookExceptionTrackerRepository.GetAll()
                                                                  .Where(e => e.EclId == eclId)
                                                                  .Select(e => _objectMapper.Map<ImportLoanbookDto>(e))
                                                                  .ToList();
                var file = _invalidLoanbookExporter.ExportToFile(invalids);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
                DeleteExistingExceptions(args, (Guid)eclId);
            } 
        }

        [UnitOfWork]
        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        var allJobs = retailSummary.AllJobs;
                        var completed = retailSummary.CompletedJobs;
                        if ( allJobs <= (completed + 1))
                        {
                            retailSummary.CompletedJobs = retailSummary.AllJobs;
                            retailSummary.Status = GeneralStatusEnum.Completed;
                            AsyncHelper.RunSync(() => ExportInvalids(args));
                            SendEmailAlert(args);
                        }
                        else
                        {
                            retailSummary.CompletedJobs += 1;
                            _retailUploadSummaryRepository.Update(retailSummary);
                        }
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        var allJobs = wholesaleSummary.AllJobs;
                        var completed = wholesaleSummary.CompletedJobs;
                        if (allJobs <= (completed + 1))
                        {
                            wholesaleSummary.CompletedJobs = wholesaleSummary.AllJobs;
                            wholesaleSummary.Status = GeneralStatusEnum.Completed;
                            AsyncHelper.RunSync(() => ExportInvalids(args));
                            SendEmailAlert(args);
                        }
                        else
                        {
                            wholesaleSummary.CompletedJobs += 1;
                            _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                        }
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        var allJobs = obeSummary.AllJobs;
                        var completed = obeSummary.CompletedJobs;
                        if (allJobs <= (completed + 1))
                        {
                            obeSummary.CompletedJobs = obeSummary.AllJobs;
                            obeSummary.Status = GeneralStatusEnum.Completed;
                            AsyncHelper.RunSync(() => ExportInvalids(args));
                            SendEmailAlert(args);
                        }
                        else
                        {
                            obeSummary.CompletedJobs += 1;
                            _obeUploadSummaryRepository.Update(obeSummary);
                        }
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void DeleteExistingExceptions(ImportEclDataFromExcelJobArgs args, Guid EclId)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_TrackEclDataLoanBookException, DbHelperConst.COL_EclId, EclId.ToString()));
        }

        private void SendEmailAlert(ImportEclDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var frameworkId = (int)args.Framework;
            string link = baseUrl + "/app/main/ecl/view/" + frameworkId.ToString() + "/";
            long ouId = 0;

            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var retailEcl = _retailEclRepository.FirstOrDefault(retailSummary.RetailEclId);
                    link += retailEcl.Id;
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wEcl = _wholesaleEclRepository.FirstOrDefault(wholesaleSummary.WholesaleEclId);
                    link += wEcl.Id;
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var oEcl = _obeEclRepository.FirstOrDefault((Guid)obeSummary.ObeEclId);
                    link += oEcl.Id;
                    ouId = oEcl.OrganizationUnitId;
                    break;
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " loanbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportEclDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId); 
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            long ouId = 0;

            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var retailEcl = _retailEclRepository.FirstOrDefault(retailSummary.RetailEclId);
                    ouId = retailEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wEcl = _wholesaleEclRepository.FirstOrDefault(wholesaleSummary.WholesaleEclId);
                    ouId = wEcl.OrganizationUnitId;
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var oEcl = _obeEclRepository.FirstOrDefault((Guid)obeSummary.ObeEclId);
                    ouId = oEcl.OrganizationUnitId;
                    break;
            }

            var ou = _ouRepository.FirstOrDefault(ouId);
            var type = args.Framework.ToString() + " loanbook";
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private string GetLocalizedExceptionMessagePart(string parameter, string required)
        {
            return _localizationSource.GetString("{0}IsInvalid", parameter) + " " + _localizationSource.GetString(required) + "; ";
        }

    }
}
