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
    public class ImportCalibrationPdCommsConsFromExcelJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IPdCommsConsExcelDataReader _pdCommsConsExcelDataReader;
        private readonly IInvalidPdCommsConsExporter _invalidExporter;
        private readonly IRepository<CalibrationPdCommsCons, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputPdCommsCon> _pdCrDrRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<TrackCalibrationUploadSummary> _uploadSummaryRepository;
        private readonly IEclCustomRepository _customRepository;
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public ImportCalibrationPdCommsConsFromExcelJob(
            IPdCommsConsExcelDataReader pdCrDreExcelDataReader,
            IInvalidPdCommsConsExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputPdCommsCon> pdCrDrRepository,
            IRepository<CalibrationPdCommsCons, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<TrackCalibrationUploadSummary> uploadSummaryRepository,
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
            IObjectMapper objectMapper)
        {
            _pdCommsConsExcelDataReader = pdCrDreExcelDataReader;
            _invalidExporter = invalidExporter;
            _pdCrDrRepository = pdCrDrRepository;
            _calibrationRepository = calibrationRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _uploadSummaryRepository = uploadSummaryRepository;
            _customRepository = customRepository;
            _backgroundJobManager = backgroundJobManager;
        }

        [UnitOfWork]
        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
            var pdCommsCons = GetPdCommsConsFromExcelOrNull(args);
            if (pdCommsCons == null || !pdCommsCons.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            UpdateCalibrationTableToDraftAsync(args);

            var jobs = pdCommsCons.Count / 5000;
            jobs += 1;
            AddToUploadSummaryTable(args, jobs);                                           

            for (int i = 0; i < jobs; i++)
            {
                var sub_pdCommsCons = pdCommsCons.Skip(i * 5000).Take(5000).ToList();
                _backgroundJobManager.Enqueue<SaveCalibrationPdCommsConsFromExcelJob, SaveCalibrationPdCommsConsFromExcelJobArgs>(new SaveCalibrationPdCommsConsFromExcelJobArgs
                {
                    Args = args,
                    UploadedRecords = sub_pdCommsCons
                });
            }

            _backgroundJobManager.Enqueue<TrackCalibrationPdCommsConsUploadJob, ImportCalibrationDataFromExcelJobArgs>(args, delay: TimeSpan.FromSeconds(30));
        }

        private List<ImportCalibrationPdCommsConsAsStringDto> GetPdCommsConsFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _pdCommsConsExcelDataReader.GetImportPdCommsConsFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreatePdCrDr(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCommsConsDto> inputs)
        {
            var invalids = new List<ImportCalibrationPdCommsConsDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreatePdCommsConsAsync(input, args));
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

            AsyncHelper.RunSync(() => ProcessImportPdCommsConsResultAsync(args, invalids));
        }

        private async Task CreatePdCommsConsAsync(ImportCalibrationPdCommsConsDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _pdCrDrRepository.InsertAsync(new CalibrationInputPdCommsCon()
            {
                Customer_No = input.Customer_No,
                Account_No = input.Account_No,
                Contract_No = input.Contract_No,
                Product_Type = input.Product_Type,
                Days_Past_Due = input.Days_Past_Due,
                Classification = input.Classification,
                Outstanding_Balance_Lcy = input.Outstanding_Balance_Lcy,
                Contract_Start_Date = input.Contract_Start_Date,
                Contract_End_Date = input.Contract_End_Date,
                Snapshot_Date = input.Snapshot_Date,
                Segment = input.Segment,
                Current_Rating = input.Current_Rating,
                WI = input.WI,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now,
                Serial=input.Serial
            });
        }

        private async Task ProcessImportPdCommsConsResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCommsConsDto> invalids)
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
                    _localizationSource.GetString("AllCalibrationRecoveryRateSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationPdCommsConsList"),
                Abp.Notifications.NotificationSeverity.Warn));
            UpdateUploadSummaryTable(args, GeneralStatusEnum.Failed, _localizationSource.GetString("FileCantBeConvertedToCalibrationPdCommsConsList"));
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
        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputPdCommsCons, DbHelperConst.COL_CalibrationId, args.CalibrationId.ToString()));
            //_pdCrDrRepository.Delete(x => x.CalibrationId == args.CalibrationId);
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

        private void AddToUploadSummaryTable(ImportCalibrationDataFromExcelJobArgs args, int allJobs)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.CalibrationId);
            if(uploadSummary == null)
            {
                _uploadSummaryRepository.Insert(new TrackCalibrationUploadSummary
                {
                    RegisterId = args.CalibrationId,
                    AllJobs = allJobs,
                    CompletedJobs = 0,
                    Status = GeneralStatusEnum.Processing
                });
            }
            else
            {
                uploadSummary.RegisterId = args.CalibrationId;
                uploadSummary.AllJobs = allJobs;
                uploadSummary.CompletedJobs = 0;
                uploadSummary.Status = GeneralStatusEnum.Processing;
                _uploadSummaryRepository.Update(uploadSummary);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        private void SendEmailAlert(ImportCalibrationDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/pdcommscons/view/" + args.CalibrationId;
            var type = "PD COMMS CONS calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportCalibrationDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var type = "PD COMMS CONS calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
