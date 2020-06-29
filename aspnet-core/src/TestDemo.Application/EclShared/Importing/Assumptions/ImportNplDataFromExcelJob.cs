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
using TestDemo.Calibration;
using TestDemo.CalibrationInput;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing.Assumptions.Dto;
using TestDemo.EclShared.Importing.Calibration;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class ImportNplDataFromExcelJob : BackgroundJob<ImportAssumptionDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly INplDataExcelDataReader _excelDataReader;
        private readonly IInvalidNplExporter _invalidExporter;
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionsRepository;
        private readonly IRepository<AssumptionApproval, Guid> _affiliateAssumptionsApprovalRepository;
        private readonly IRepository<PdInputAssumptionNplIndex, Guid> _dataRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IEclCustomRepository _customRepository;

        public ImportNplDataFromExcelJob(
            INplDataExcelDataReader excelDataReader,
            IInvalidNplExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<PdInputAssumptionNplIndex, Guid> dataRepository,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
            IRepository<AssumptionApproval, Guid> affiliateAssumptionsApprovalRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IEclCustomRepository customRepository,
            IObjectMapper objectMapper)
        {
            _excelDataReader = excelDataReader;
            _invalidExporter = invalidExporter;
            _dataRepository = dataRepository;
            _affiliateAssumptionsRepository = affiliateAssumptionRepository;
            _affiliateAssumptionsApprovalRepository = affiliateAssumptionsApprovalRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _customRepository = customRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportAssumptionDataFromExcelJobArgs args)
        {
            var npl = GetNplFromExcelOrNull(args);
            if (npl == null || !npl.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateNpl(args, npl);
        }

        private List<ImportNplDataDto> GetNplFromExcelOrNull(ImportAssumptionDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _excelDataReader.GetImportDataFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreateNpl(ImportAssumptionDataFromExcelJobArgs args, List<ImportNplDataDto> inputs)
        {
            var invalids = new List<ImportNplDataDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateSnPAsync(input, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        input.Exception = exception.Message;
                        invalids.Add(input);
                    }
                    catch(Exception exception)
                    {
                        input.Exception = exception.ToString();
                        invalids.Add(input);
                    }
                }
                else
                {
                    invalids.Add(input);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportPdCrDrResultAsync(args, invalids));
        }

        private async Task CreateSnPAsync(ImportNplDataDto input, ImportAssumptionDataFromExcelJobArgs args)
        {
            await _dataRepository.InsertAsync(new PdInputAssumptionNplIndex()
            {
                Key = "HistoricalIndexQ" + input.KeyRow,
                Date = input.Date == null ? DateTime.Now : (DateTime)input.Date,
                Actual = input.Actual == null ? 0 : (double)input.Actual,
                Standardised = input.Standardised == null ? 0 : (double)input.Standardised,
                EtiNplSeries = input.EtiNplSeries == null ? 0 : (double)input.EtiNplSeries,
                IsComputed = true,
                CanAffiliateEdit = false,
                RequiresGroupApproval = true,
                Status = GeneralStatusEnum.Approved,
                Framework = args.Framework,
                OrganizationUnitId = args.AffiliateId,
                CreatorUserId = args.User.UserId,
                LastModificationTime = DateTime.Now,
                LastModifierUserId = args.User.UserId
            });
        }

        private async Task ProcessImportPdCrDrResultAsync(ImportAssumptionDataFromExcelJobArgs args, List<ImportNplDataDto> invalids)
        {
            if (invalids.Any())
            {
                var file = _invalidExporter.ExportToFile(invalids);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllSnPSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
                SubmitForApproval(args);
            }
        }

        private void SendInvalidExcelNotification(ImportAssumptionDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToSnPList"),
                Abp.Notifications.NotificationSeverity.Warn));
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportAssumptionDataFromExcelJobArgs args)
        {
            var ids = _dataRepository.GetAllList(x => x.Framework == args.Framework && x.OrganizationUnitId == args.AffiliateId);
            foreach (var item in ids)
            {
                _dataRepository.HardDelete(e => e.Id == item.Id);
            }
        }

        private void SubmitForApproval(ImportAssumptionDataFromExcelJobArgs args)
        {
            var assumption = _affiliateAssumptionsRepository.FirstOrDefault(x => x.OrganizationUnitId == args.AffiliateId);
            _affiliateAssumptionsApprovalRepository.Insert(new AssumptionApproval()
            {
                OrganizationUnitId = args.AffiliateId,
                Framework = args.Framework,
                AssumptionType = AssumptionTypeEnum.PdNplIndex,
                AssumptionGroup = "NPL",
                InputName = "NPL Index",
                OldValue = "-",
                NewValue = _localizationSource.GetString("CheckAssumptionsPage"),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdNplIndexAssumption,
                Status = GeneralStatusEnum.Submitted,
                CreationTime = DateTime.Now,
                CreatorUserId = args.User.UserId,
            });
        }

        private void SendEmailAlert(ImportAssumptionDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/assumption/affiliates/view/" + args.AffiliateId;
            var type = args.Framework.ToString() + " NPL";
            var ou = _ouRepository.FirstOrDefault(args.AffiliateId);
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportAssumptionDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var type = args.Framework.ToString() + " NPL";
            var ou = _ouRepository.FirstOrDefault(args.AffiliateId);
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
