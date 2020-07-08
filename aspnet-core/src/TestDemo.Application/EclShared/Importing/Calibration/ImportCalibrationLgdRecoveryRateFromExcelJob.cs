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
using TestDemo.Common;
using TestDemo.Configuration;
using TestDemo.Dto;
using TestDemo.EclLibrary.Workers.Trackers;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
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
    public class ImportCalibrationLgdRecoveryRateFromExcelJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly ILgdRecoveryRateExcelDataReader _lgdRecoveryRateExcelDataReader;
        private readonly IInvalidLgdRecoveryRateExporter _invalidExporter;
        private readonly IRepository<CalibrationLgdRecoveryRate, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputLgdRecoveryRate> _recoveryRateRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IEclCustomRepository _customRepository;
        private readonly IRepository<TrackCalibrationUploadSummary> _uploadSummaryRepository;

        public ImportCalibrationLgdRecoveryRateFromExcelJob(
            ILgdRecoveryRateExcelDataReader lgdRecoveryRateExcelDataReader,
            IInvalidLgdRecoveryRateExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputLgdRecoveryRate> recoveryRateRepository,
            IRepository<CalibrationLgdRecoveryRate, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IEclCustomRepository customRepository,
            IRepository<TrackCalibrationUploadSummary> uploadSummaryRepository,
            IObjectMapper objectMapper)
        {
            _lgdRecoveryRateExcelDataReader = lgdRecoveryRateExcelDataReader;
            _invalidExporter = invalidExporter;
            _recoveryRateRepository = recoveryRateRepository;
            _calibrationRepository = calibrationRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _customRepository = customRepository;
            _uploadSummaryRepository = uploadSummaryRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
            AddToUploadSummaryTable(args);

            var recoveryRate = GetRecoveryRateFromExcelOrNull(args);
            if (recoveryRate == null || !recoveryRate.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateRecoveryRate(args, recoveryRate);
            UpdateCalibrationTableToDraftAsync(args);
        }

        private List<ImportCalibrationLgdRecoveryRateDto> GetRecoveryRateFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _lgdRecoveryRateExcelDataReader.GetImportLgdRecoveryRateFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreateRecoveryRate(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationLgdRecoveryRateDto> inputs)
        {
            var invalids = new List<ImportCalibrationLgdRecoveryRateDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateRecoveryRateAsync(input, args));
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

            AsyncHelper.RunSync(() => ProcessImportRecoveryRateResultAsync(args, invalids));
        }

        private async Task CreateRecoveryRateAsync(ImportCalibrationLgdRecoveryRateDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _recoveryRateRepository.InsertAsync(new CalibrationInputLgdRecoveryRate()
            {
                Customer_No = input.Customer_No,
                Account_No = input.Account_No,
                Contract_No = input.Contract_No,
                Account_Name = input.Account_Name,
                Segment = input.Segment,
                Days_Past_Due = input.Days_Past_Due,
                Classification = input.Classification,
                Default_Date = input.Default_Date,
                Outstanding_Balance_Lcy = input.Outstanding_Balance_Lcy,
                Contractual_Interest_Rate = input.Contractual_Interest_Rate,
                Amount_Recovered = input.Amount_Recovered,
                Date_Of_Recovery = input.Date_Of_Recovery,
                Type_Of_Recovery = input.Type_Of_Recovery,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportRecoveryRateResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationLgdRecoveryRateDto> invalids)
        {
            if (invalids.Any())
            {
                var file = _invalidExporter.ExportToFile(invalids);
                await _appNotifier.SomeDataCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
                SendInvalidEmailAlert(args, file);

                var baseUrl = _appConfiguration["App:ServerRootAddress"];
                var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;
                UpdateUploadSummaryTable(args, GeneralStatusEnum.Failed, _localizationSource.GetString("CompletedWithErrorsCheckEmail") + " &nbsp;<a href='" + link + "'> Download</a>");
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllCalibrationRecoveryRateSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
                UpdateUploadSummaryTable(args, GeneralStatusEnum.Completed, "");
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationLgdRecoveryRateList"),
                Abp.Notifications.NotificationSeverity.Warn));
            UpdateUploadSummaryTable(args, GeneralStatusEnum.Failed, _localizationSource.GetString("FileCantBeConvertedToCalibrationLgdRecoveryRateList"));
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputRecoveryRate, DbHelperConst.COL_CalibrationId, args.CalibrationId.ToString()));

            //_recoveryRateRepository.Delete(x => x.CalibrationId == args.CalibrationId);
        }

        private void AddToUploadSummaryTable(ImportCalibrationDataFromExcelJobArgs args)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.CalibrationId);
            if (uploadSummary == null)
            {
                _uploadSummaryRepository.Insert(new TrackCalibrationUploadSummary
                {
                    RegisterId = args.CalibrationId,
                    AllJobs = 1,
                    CompletedJobs = 0,
                    Status = GeneralStatusEnum.Processing
                });
            }
            else
            {
                uploadSummary.RegisterId = args.CalibrationId;
                uploadSummary.AllJobs = 1;
                uploadSummary.CompletedJobs = 0;
                uploadSummary.Status = GeneralStatusEnum.Processing;
                _uploadSummaryRepository.Update(uploadSummary);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        private void UpdateUploadSummaryTable(ImportCalibrationDataFromExcelJobArgs args, GeneralStatusEnum status, string comment)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.CalibrationId);
            if (uploadSummary != null)
            {
                uploadSummary.RegisterId = args.CalibrationId;
                uploadSummary.CompletedJobs = uploadSummary.AllJobs;
                uploadSummary.Status = status;
                uploadSummary.Comment = status == GeneralStatusEnum.Failed ? comment : "";
                _uploadSummaryRepository.Update(uploadSummary);
            }
            CurrentUnitOfWork.SaveChanges();
        }


        [UnitOfWork]
        private void UpdateCalibrationTableToDraftAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
            }
        }

        private void SendEmailAlert(ImportCalibrationDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/recovery/view/" + args.CalibrationId;
            var type = "LGD recovery rate calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportCalibrationDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var type = "LGD recovery rate calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
