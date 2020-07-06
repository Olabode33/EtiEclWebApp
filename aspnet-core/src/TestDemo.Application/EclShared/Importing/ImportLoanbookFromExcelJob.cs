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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Authorization.Users;
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing;
using TestDemo.EclShared.Importing.Dto;
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
    public class ImportLoanbookFromExcelJob : BackgroundJob<ImportEclDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly ILoanbookExcelDataReader _loanbookExcelDataReader;
        private readonly IInvalidLoanbookExporter _invalidLoanbookExporter;
        private readonly IRepository<RetailEclDataLoanBook, Guid> _retailEclDataLoanbookRepository;
        private readonly IRepository<WholesaleEclDataLoanBook, Guid> _wholesaleEclDataLoanbookRepository;
        private readonly IRepository<ObeEclDataLoanBook, Guid> _obeEclDataLoanbookRepository;
        private readonly IRepository<RetailEclUpload, Guid> _retailUploadSummaryRepository;
        private readonly IRepository<WholesaleEclUpload, Guid> _wholesaleUploadSummaryRepository;
        private readonly IRepository<ObeEclUpload, Guid> _obeUploadSummaryRepository;
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
        private readonly IEclCustomRepository _customRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public ImportLoanbookFromExcelJob(
            ILoanbookExcelDataReader loanbookExcelDataReader, 
            IInvalidLoanbookExporter invalidLoanbookExporter, 
            IRepository<RetailEclDataLoanBook, Guid> retailEclDataLoanbookRepository, 
            IRepository<WholesaleEclDataLoanBook, Guid> wholesaleEclDataLoanbookRepository, 
            IRepository<ObeEclDataLoanBook, Guid> obeEclDataPaymentScheduleRepository, 
            IRepository<RetailEclUpload, Guid> retailUploadSummaryRepository, 
            IRepository<WholesaleEclUpload, Guid> wholesaleUploadSummaryRepository, 
            IRepository<ObeEclUpload, Guid> obeUploadSummaryRepository, 
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
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
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
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            //UpdateSummaryTableToFileUploaded(args);

            var loanbooks = GetLoanbookListFromExcelOrNull(args);
            if (loanbooks == null || !loanbooks.Any())
            {
                SendInvalidExcelNotification(args);
                UpdateSummaryTableToFailed(args);
                return;
            }
            //var validatedLoanbooks = ValidateLoanBook(args, loanbooks);
            //Logger.Debug("ImportLoanbookFromExcelJob: Finish Reading Data from Excel, " + DateTime.Now.ToString());
            DeleteExistingDataAsync(args);
            //Logger.Debug("ImportLoanbookFromExcelJob: Finish Deleting Existing Data, " + DateTime.Now.ToString());
            //CreateLoanbook(args, loanbooks);
            //Logger.Debug("ImportLoanbookFromExcelJob: Finish Inserting new Data, " + DateTime.Now.ToString());
            
            var jobs = loanbooks.Count / 5000;
            jobs += 1;
            UpdateSummaryTableToCompletedAsync(args, jobs);

            for (int i = 0; i < jobs; i++)
            {
                var sub_loanbook = loanbooks.Skip(i * 5000).Take(5000).ToList();
                _backgroundJobManager.Enqueue<SaveLoanbookFromExcelJob, SaveEclLoanbookDataFromExcelJobArgs>(new SaveEclLoanbookDataFromExcelJobArgs
                {
                    Args = args,
                    Loanbook = sub_loanbook
                });
            }
            //if ((jobs * 10000) < loanbooks.Count )
            //{
            //    var sub_loanbook = loanbooks.Skip(1 * 10000).Take(10000).ToList();
            //    _backgroundJobManager.Enqueue<SaveLoanbookFromExcelJob, SaveEclLoanbookDataFromExcelJobArgs>(new SaveEclLoanbookDataFromExcelJobArgs
            //    {
            //        Args = args,
            //        Loanbook = sub_loanbook
            //    });
            //}

            _backgroundJobManager.Enqueue<TrackLoanbookUploadJob, ImportEclDataFromExcelJobArgs>(args, delay: TimeSpan.FromSeconds(30));
        
        }

        private List<ImportLoanbookDto> ValidateLoanBook(ImportEclDataFromExcelJobArgs args, List<ImportLoanbookDtoNew> loanbooks)
        {
            var invalidLoanbook = new List<ImportLoanbookDto>();
            var validLoanbook = new List<ImportLoanbookDto>();
            var loanbookArray = loanbooks.ToArray();

            for(var i = 0; i < loanbookArray.Length; i++)
            {
                var exceptionMessage = new StringBuilder();
                var loanbook = new ImportLoanbookDto();

                try
                {
                    loanbook.CustomerNo = loanbookArray[i].CustomerNo;
                    loanbook.AccountNo = loanbookArray[i].AccountNo;
                    loanbook.ContractNo = loanbookArray[i].ContractNo;
                    loanbook.CustomerName = loanbookArray[i].CustomerName;
                    loanbook.SnapshotDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].SnapshotDate, nameof(loanbook.SnapshotDate), exceptionMessage);
                    loanbook.Segment = loanbookArray[i].Segment;
                    loanbook.Sector = loanbookArray[i].Sector;
                    loanbook.Currency = loanbookArray[i].Currency;
                    loanbook.ProductType = loanbookArray[i].ProductType;
                    loanbook.ProductMapping = loanbookArray[i].ProductMapping;
                    loanbook.SpecialisedLending = loanbookArray[i].SpecialisedLending;
                    loanbook.RatingModel = loanbookArray[i].RatingModel;
                    loanbook.OriginalRating = ValidateIntegerValueFromRowOrNull(loanbookArray[i].OriginalRating, nameof(loanbook.OriginalRating), exceptionMessage);
                    loanbook.CurrentRating = ValidateIntegerValueFromRowOrNull(loanbookArray[i].CurrentRating, nameof(loanbook.CurrentRating), exceptionMessage);
                    loanbook.LifetimePD = ValidateDoubleValueFromRowOrNull(loanbookArray[i].LifetimePD, nameof(loanbook.LifetimePD), exceptionMessage);
                    loanbook.Month12PD = ValidateDoubleValueFromRowOrNull(loanbookArray[i].Month12PD, nameof(loanbook.Month12PD), exceptionMessage);
                    loanbook.DaysPastDue = ValidateIntegerValueFromRowOrNull(loanbookArray[i].DaysPastDue, nameof(loanbook.DaysPastDue), exceptionMessage);
                    loanbook.WatchlistIndicator = ValidateBooleanValueFromRowOrNull(loanbookArray[i].WatchlistIndicator, nameof(loanbook.WatchlistIndicator), exceptionMessage);
                    loanbook.Classification = loanbookArray[i].Classification;
                    loanbook.ImpairedDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].ImpairedDate, nameof(loanbook.ImpairedDate), exceptionMessage);
                    loanbook.DefaultDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].DefaultDate, nameof(loanbook.DefaultDate), exceptionMessage);
                    loanbook.CreditLimit = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CreditLimit, nameof(loanbook.CreditLimit), exceptionMessage);
                    loanbook.OriginalBalanceLCY = ValidateDoubleValueFromRowOrNull(loanbookArray[i].OriginalBalanceLCY, nameof(loanbook.OriginalBalanceLCY), exceptionMessage);
                    loanbook.OutstandingBalanceLCY = ValidateDoubleValueFromRowOrNull(loanbookArray[i].OutstandingBalanceLCY, nameof(loanbook.OutstandingBalanceLCY), exceptionMessage);
                    loanbook.OutstandingBalanceACY = ValidateDoubleValueFromRowOrNull(loanbookArray[i].OutstandingBalanceACY, nameof(loanbook.OutstandingBalanceACY), exceptionMessage);
                    loanbook.ContractStartDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].ContractStartDate, nameof(loanbook.ContractStartDate), exceptionMessage);
                    loanbook.ContractEndDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].ContractEndDate, nameof(loanbook.ContractEndDate), exceptionMessage);
                    loanbook.RestructureIndicator = ValidateBooleanValueFromRowOrNull(loanbookArray[i].RestructureIndicator, nameof(loanbook.RestructureIndicator), exceptionMessage);
                    loanbook.RestructureRisk = loanbookArray[i].RestructureRisk;
                    loanbook.RestructureType = loanbookArray[i].RestructureType;
                    loanbook.RestructureStartDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].RestructureStartDate, nameof(loanbook.RestructureStartDate), exceptionMessage);
                    loanbook.RestructureEndDate = ValidateDateTimeValueFromRowOrNull(loanbookArray[i].RestructureEndDate, nameof(loanbook.RestructureEndDate), exceptionMessage);
                    loanbook.PrincipalPaymentTermsOrigination = loanbookArray[i].PrincipalPaymentTermsOrigination;
                    loanbook.PPTOPeriod = ValidateIntegerValueFromRowOrNull(loanbookArray[i].PPTOPeriod, nameof(loanbook.PPTOPeriod), exceptionMessage);
                    loanbook.InterestPaymentTermsOrigination = loanbookArray[i].InterestPaymentTermsOrigination;
                    loanbook.IPTOPeriod = ValidateIntegerValueFromRowOrNull(loanbookArray[i].IPTOPeriod, nameof(loanbook.IPTOPeriod), exceptionMessage);
                    loanbook.PrincipalPaymentStructure = loanbookArray[i].PrincipalPaymentStructure;
                    loanbook.InterestPaymentStructure = loanbookArray[i].InterestPaymentStructure;
                    loanbook.InterestRateType = loanbookArray[i].InterestRateType;
                    loanbook.BaseRate = loanbookArray[i].BaseRate;
                    loanbook.OriginationContractualInterestRate = loanbookArray[i].OriginationContractualInterestRate;
                    loanbook.IntroductoryPeriod = ValidateIntegerValueFromRowOrNull(loanbookArray[i].IntroductoryPeriod, nameof(loanbook.IntroductoryPeriod), exceptionMessage);
                    loanbook.PostIPContractualInterestRate = ValidateDoubleValueFromRowOrNull(loanbookArray[i].PostIPContractualInterestRate, nameof(loanbook.PostIPContractualInterestRate), exceptionMessage);
                    loanbook.CurrentContractualInterestRate = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CurrentContractualInterestRate, nameof(loanbook.CurrentContractualInterestRate), exceptionMessage);
                    loanbook.EIR = ValidateDoubleValueFromRowOrNull(loanbookArray[i].EIR, nameof(loanbook.EIR), exceptionMessage);
                    loanbook.DebentureOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].DebentureOMV, nameof(loanbook.DebentureOMV), exceptionMessage);
                    loanbook.DebentureFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].DebentureFSV, nameof(loanbook.DebentureFSV), exceptionMessage);
                    loanbook.CashOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CashOMV, nameof(loanbook.CashOMV), exceptionMessage);
                    loanbook.CashFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CashFSV, nameof(loanbook.CashFSV), exceptionMessage);
                    loanbook.InventoryOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].InventoryOMV, nameof(loanbook.InventoryOMV), exceptionMessage);
                    loanbook.InventoryFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].InventoryFSV, nameof(loanbook.InventoryFSV), exceptionMessage);
                    loanbook.PlantEquipmentOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].PlantEquipmentOMV, nameof(loanbook.PlantEquipmentOMV), exceptionMessage);
                    loanbook.PlantEquipmentFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].PlantEquipmentFSV, nameof(loanbook.PlantEquipmentFSV), exceptionMessage);
                    loanbook.ResidentialPropertyOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].ResidentialPropertyOMV, nameof(loanbook.ResidentialPropertyOMV), exceptionMessage);
                    loanbook.ResidentialPropertyFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].ResidentialPropertyFSV, nameof(loanbook.ResidentialPropertyFSV), exceptionMessage);
                    loanbook.CommercialPropertyOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CommercialPropertyOMV, nameof(loanbook.CommercialPropertyOMV), exceptionMessage);
                    loanbook.CommercialPropertyFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CommercialPropertyFSV, nameof(loanbook.CommercialPropertyFSV), exceptionMessage);
                    loanbook.ReceivablesOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].ReceivablesOMV, nameof(loanbook.ReceivablesOMV), exceptionMessage);
                    loanbook.ReceivablesFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].ReceivablesFSV, nameof(loanbook.ReceivablesFSV), exceptionMessage);
                    loanbook.SharesOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].SharesOMV, nameof(loanbook.SharesOMV), exceptionMessage);
                    loanbook.SharesFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].SharesFSV, nameof(loanbook.SharesFSV), exceptionMessage);
                    loanbook.VehicleOMV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].VehicleOMV, nameof(loanbook.VehicleOMV), exceptionMessage);
                    loanbook.VehicleFSV = ValidateDoubleValueFromRowOrNull(loanbookArray[i].VehicleFSV, nameof(loanbook.VehicleFSV), exceptionMessage);
                    loanbook.CureRate = ValidateDoubleValueFromRowOrNull(loanbookArray[i].CureRate, nameof(loanbook.CureRate), exceptionMessage);
                    loanbook.GuaranteeIndicator = ValidateBooleanValueFromRowOrNull(loanbookArray[i].GuaranteeIndicator, nameof(loanbook.GuaranteeIndicator), exceptionMessage);
                    loanbook.GuarantorPD = loanbookArray[i].GuarantorPD;
                    loanbook.GuarantorLGD = loanbookArray[i].GuarantorLGD;
                    loanbook.GuaranteeValue = ValidateDoubleValueFromRowOrNull(loanbookArray[i].GuaranteeValue, nameof(loanbook.GuaranteeValue), exceptionMessage);
                    loanbook.GuaranteeLevel = ValidateDoubleValueFromRowOrNull(loanbookArray[i].GuaranteeLevel, nameof(loanbook.GuaranteeLevel), exceptionMessage);

                    validLoanbook.Add(loanbook);
                }
                catch (Exception e)
                {
                    loanbook.Exception = e.Message;
                }
            }

            return validLoanbook;
        }

        private List<ImportLoanbookDtoNew> GetLoanbookListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                //Logger.Debug("ImportLoanbookFromExcelJobFileToken: " + file.Id);
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
        private void CreateLoanbook(ImportEclDataFromExcelJobArgs args, List<ImportLoanbookDto> loanbooks)
        {
            var invalidLoanbook = new List<ImportLoanbookDto>();

            foreach (var loanbook in loanbooks)
            {
                if (loanbook.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateLoanbookAsync(loanbook, args));
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

            AsyncHelper.RunSync(() => ProcessImportLoanbookResultAsync(args, invalidLoanbook));
        }

        private async Task CreateLoanbookAsync(ImportLoanbookDto input, ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
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
                        RetailEclUploadId = retailSummary.RetailEclId
                       
                    });
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
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
                        WholesaleEclUploadId = wholesaleSummary.WholesaleEclId
                    });
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
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
                        ObeEclUploadId = obeSummary.ObeEclId
                    });
                    break;
            }
        }

        private async Task ProcessImportLoanbookResultAsync(ImportEclDataFromExcelJobArgs args, List<ImportLoanbookDto> invalidLoanbook)
        {
            if (invalidLoanbook.Any())
            {
                var file = _invalidLoanbookExporter.ExportToFile(invalidLoanbook);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllLoanbookSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToLoanbookList"),
                Abp.Notifications.NotificationSeverity.Warn));
        }

        [UnitOfWork]
        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args, int allJobs)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.AllJobs = allJobs;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.AllJobs = allJobs;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.AllJobs = allJobs;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void UpdateSummaryTableToFailed(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.FileUploaded = false;
                        retailSummary.Status = GeneralStatusEnum.Failed;
                        retailSummary.UploadComment = _localizationSource.GetString("FileCantBeConvertedToLoanbookList");
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.FileUploaded = false;
                        wholesaleSummary.Status = GeneralStatusEnum.Failed;
                        wholesaleSummary.UploadComment = _localizationSource.GetString("FileCantBeConvertedToLoanbookList");
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.FileUploaded = false;
                        obeSummary.Status = GeneralStatusEnum.Failed;
                        obeSummary.UploadComment = _localizationSource.GetString("FileCantBeConvertedToLoanbookList");
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
            CurrentUnitOfWork.SaveChanges();
        }


        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var rlb = _retailEclDataLoanbookRepository.Count(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    if (retailSummary != null && rlb > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclLoanBookRetail, DbHelperConst.COL_RetailEclUploadId, retailSummary.RetailEclId.ToString()));
                        //_retailEclDataLoanbookRepository.HardDelete(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wlb = _wholesaleEclDataLoanbookRepository.Count(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    if (wholesaleSummary != null && wlb > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclLoanBookWholesale, DbHelperConst.COL_WholesaleEclUploadId, wholesaleSummary.WholesaleEclId.ToString()));
                        //_wholesaleEclDataLoanbookRepository.HardDelete(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var obelb = _obeEclDataLoanbookRepository.Count(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    if (obeSummary != null && obelb > 0)
                    {
                        AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_EclLoanBookObe, DbHelperConst.COL_ObeEclUploadId, obeSummary.ObeEclId.ToString()));
                        //_obeEclDataLoanbookRepository.HardDelete(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    }
                    break;
            }
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

        private int? ValidateIntegerValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            int returnValue;

            if (value == null)
            {
                return null;
            }
            else if (int.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private double? ValidateDoubleValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            double returnValue;
            if (value == null)
            {
                return null;
            }
            else if (double.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private DateTime? ValidateDateTimeValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            DateTime returnValue;

            if (value == null)
            {
                return null;
            }
            else if (DateTime.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private bool ValidateBooleanValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            bool returnValue;
            if (value == null)
            {
                return false;
            }

            if (bool.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return false;
        }

        private string GetLocalizedExceptionMessagePart(string parameter)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
        }

    }
}
