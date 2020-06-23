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
using TestDemo.EclShared.Importing.Calibration;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Notifications;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class ImportCalibrationCcfSummaryFromExcelJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IEadCcfSummaryExcelDataReader _eadCcfSummaryExcelDataReader;
        private readonly IInvalidEadCcfSummaryExporter _invalidExporter;
        private readonly IRepository<CalibrationEadCcfSummary, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputEadCcfSummary> _ccfSummaryRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;

        public ImportCalibrationCcfSummaryFromExcelJob(
            IEadCcfSummaryExcelDataReader eadCcfSummaryExcelDataReader,
            IInvalidEadCcfSummaryExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputEadCcfSummary> ccfSummaryRepository,
            IRepository<CalibrationEadCcfSummary, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IObjectMapper objectMapper)
        {
            _eadCcfSummaryExcelDataReader = eadCcfSummaryExcelDataReader;
            _invalidExporter = invalidExporter;
            _ccfSummaryRepository = ccfSummaryRepository;
            _calibrationRepository = calibrationRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
            var ccfSummary = GetCcfSummaryListFromExcelOrNull(args);
            if (ccfSummary == null || !ccfSummary.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateCcfSummary(args, ccfSummary);
            UpdateCalibrationTableToDraftAsync(args);
        }

        private List<ImportCalibrationCcfSummaryDto> GetCcfSummaryListFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _eadCcfSummaryExcelDataReader.GetImportCcfSummaryFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreateCcfSummary(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationCcfSummaryDto> ccfSummaries)
        {
            var invalidCcfSummary = new List<ImportCalibrationCcfSummaryDto>();

            foreach (var ccfSummary in ccfSummaries)
            {
                if (ccfSummary.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateCcfSummaryAsync(ccfSummary, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        ccfSummary.Exception = exception.Message;
                        invalidCcfSummary.Add(ccfSummary);
                    }
                    catch(Exception exception)
                    {
                        ccfSummary.Exception = exception.ToString();
                        invalidCcfSummary.Add(ccfSummary);
                    }
                }
                else
                {
                    invalidCcfSummary.Add(ccfSummary);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportCcfSummaryResultAsync(args, invalidCcfSummary));
        }

        private async Task CreateCcfSummaryAsync(ImportCalibrationCcfSummaryDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _ccfSummaryRepository.InsertAsync(new CalibrationInputEadCcfSummary()
            {
                Customer_No = input.Customer_No,
                Account_No = input.Account_No,
                Product_Type = input.Product_Type,
                Settlement_Account = input.Settlement_Account,
                Snapshot_Date = input.Snapshot_Date,
                Contract_Start_Date = input.Contract_Start_Date,
                Contract_End_Date = input.Contract_End_Date,
                Limit = input.Limit,
                Outstanding_Balance = input.Outstanding_Balance,
                Classification = input.Classification,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportCcfSummaryResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationCcfSummaryDto> invalidBehaviouralTerm)
        {
            if (invalidBehaviouralTerm.Any())
            {
                var file = _invalidExporter.ExportToFile(invalidBehaviouralTerm);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllCalibrationCcfSummarySuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationBehaviouralTermList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            _ccfSummaryRepository.Delete(x => x.CalibrationId == args.CalibrationId);
        }

        [UnitOfWork]
        private void UpdateCalibrationTableToDraftAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
                CurrentUnitOfWork.SaveChanges();
            }
        }

        private void SendEmailAlert(ImportCalibrationDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/ccfSummary/view/" + args.CalibrationId;
            var type = "CCF summary calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }

        private void SendInvalidEmailAlert(ImportCalibrationDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;
            var type = "CCF summary calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }

    }
}
