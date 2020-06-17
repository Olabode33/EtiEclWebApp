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
using TestDemo.Configuration;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing;
using TestDemo.EclShared.Importing.Dto;
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
        }

        [UnitOfWork]
        public override void Execute(ImportEclDataFromExcelJobArgs args)
        {
            var loanbooks = GetLoanbookListFromExcelOrNull(args);
            if (loanbooks == null || !loanbooks.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateLoanbook(args, loanbooks);
            UpdateSummaryTableToCompletedAsync(args);
            SendEmailAlert(args);
        }

        private List<ImportLoanbookDto> GetLoanbookListFromExcelOrNull(ImportEclDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _loanbookExcelDataReader.GetImportLoanbookFromExcel(file.Bytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

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
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllLoanbookSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportEclDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToLoanbookList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void UpdateSummaryTableToCompletedAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (retailSummary != null)
                    {
                        retailSummary.Status = GeneralStatusEnum.Completed;
                        _retailUploadSummaryRepository.Update(retailSummary);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (wholesaleSummary != null)
                    {
                        wholesaleSummary.Status = GeneralStatusEnum.Completed;
                        _wholesaleUploadSummaryRepository.Update(wholesaleSummary);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    if (obeSummary != null)
                    {
                        obeSummary.Status = GeneralStatusEnum.Completed;
                        _obeUploadSummaryRepository.Update(obeSummary);
                    }
                    break;
            }
        }

        private void DeleteExistingDataAsync(ImportEclDataFromExcelJobArgs args)
        {
            switch (args.Framework)
            {
                case FrameworkEnum.Retail:
                    var retailSummary = _retailUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var rlb = _retailEclDataLoanbookRepository.Count(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    if (retailSummary != null && rlb > 0)
                    {
                        _retailEclDataLoanbookRepository.HardDelete(x => x.RetailEclUploadId == retailSummary.RetailEclId);
                    }
                    break;

                case FrameworkEnum.Wholesale:
                    var wholesaleSummary = _wholesaleUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var wlb = _wholesaleEclDataLoanbookRepository.Count(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    if (wholesaleSummary != null && wlb > 0)
                    {
                        _wholesaleEclDataLoanbookRepository.HardDelete(x => x.WholesaleEclUploadId == wholesaleSummary.WholesaleEclId);
                    }
                    break;

                case FrameworkEnum.OBE:
                    var obeSummary = _obeUploadSummaryRepository.FirstOrDefault((Guid)args.UploadSummaryId);
                    var obelb = _obeEclDataLoanbookRepository.Count(x => x.ObeEclUploadId == obeSummary.ObeEclId);
                    if (obeSummary != null && obelb > 0)
                    {
                        _obeEclDataLoanbookRepository.HardDelete(x => x.ObeEclUploadId == obeSummary.ObeEclId);
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
            _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }
    }
}
