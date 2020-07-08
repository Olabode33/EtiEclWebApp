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
    public class ImportCalibrationBehaviouralFromExcelJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IEadBehaviouralTermExcelDataReader _eadBehaviouralTermExcelDataReader;
        private readonly IInvalidEadBehaviouralTermExporter _invalidExporter;
        private readonly IRepository<CalibrationEadBehaviouralTerm, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputEadBehaviouralTerms> _behaviouralTermsRepository;
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
        protected readonly IBackgroundJobManager _backgroundJobManager;

        public ImportCalibrationBehaviouralFromExcelJob(
            IEadBehaviouralTermExcelDataReader eadBehaviouralTermExcelDataReader,
            IInvalidEadBehaviouralTermExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputEadBehaviouralTerms> behaviouralTermsRepository,
            IRepository<CalibrationEadBehaviouralTerm, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IRepository<TrackCalibrationUploadSummary> uploadSummaryRepository,
            IEclCustomRepository customRepository,
            IBackgroundJobManager backgroundJobManager,
            IObjectMapper objectMapper)
        {
            _eadBehaviouralTermExcelDataReader = eadBehaviouralTermExcelDataReader;
            _invalidExporter = invalidExporter;
            _behaviouralTermsRepository = behaviouralTermsRepository;
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
            var behavioural = GeBehaviouralTermsListFromExcelOrNull(args);
            if (behavioural == null || !behavioural.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            UpdateCalibrationTableToDraftAsync(args);

            var jobs = behavioural.Count / 5000;
            jobs += 1;
            AddToUploadSummaryTable(args, jobs);

            for (int i = 0; i < jobs; i++)
            {
                var sub_ = behavioural.Skip(i * 5000).Take(5000).ToList();
                _backgroundJobManager.Enqueue<SaveCalibrationBehaviouralFromExcelJob, SaveCalibrationBehaviouralTermExcelJobArgs>(new SaveCalibrationBehaviouralTermExcelJobArgs
                {
                    Args = args,
                    UploadedRecords = sub_
                });
            }

            _backgroundJobManager.Enqueue<TrackCalibrationBehaviouralUploadJob, ImportCalibrationDataFromExcelJobArgs>(args, delay: TimeSpan.FromSeconds(30));

        }

        private List<ImportCalibrationBehaviouralTermAsStringDto> GeBehaviouralTermsListFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _eadBehaviouralTermExcelDataReader.GetImportBehaviouralTermFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreateBehaviouralTerm(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationBehaviouralTermDto> behaviouralTerms)
        {
            var invalidBehaviouralTerm = new List<ImportCalibrationBehaviouralTermDto>();

            foreach (var behaviouralTerm in behaviouralTerms)
            {
                if (behaviouralTerm.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateBehaviouralTermAsync(behaviouralTerm, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        behaviouralTerm.Exception = exception.Message;
                        invalidBehaviouralTerm.Add(behaviouralTerm);
                    }
                    catch(Exception exception)
                    {
                        behaviouralTerm.Exception = exception.ToString();
                        invalidBehaviouralTerm.Add(behaviouralTerm);
                    }
                }
                else
                {
                    invalidBehaviouralTerm.Add(behaviouralTerm);
                }
            }

            AsyncHelper.RunSync(() => ProcessImportBehaviouralTermResultAsync(args, invalidBehaviouralTerm));
        }

        private async Task CreateBehaviouralTermAsync(ImportCalibrationBehaviouralTermDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _behaviouralTermsRepository.InsertAsync(new CalibrationInputEadBehaviouralTerms()
            {
                Customer_No = input.Customer_No,
                Account_No = input.Account_No,
                Contract_No = input.Contract_No,
                Customer_Name = input.Customer_Name,
                Snapshot_Date = input.Snapshot_Date,
                Classification = input.Classification,
                Original_Balance_Lcy = input.Original_Balance_Lcy,
                Outstanding_Balance_Lcy = input.Outstanding_Balance_Lcy,
                Outstanding_Balance_Acy = input.Outstanding_Balance_Acy,
                Contract_Start_Date = input.Contract_Start_Date,
                Contract_End_Date = input.Contract_End_Date,
                Restructure_Indicator = input.Restructure_Indicator,
                Restructure_Type = input.Restructure_Type,
                Restructure_Start_Date = input.Restructure_Start_Date,
                Restructure_End_Date = input.Restructure_End_Date,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportBehaviouralTermResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationBehaviouralTermDto> invalidBehaviouralTerm)
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
                    _localizationSource.GetString("AllCalibrationBehaviouralTermSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);

                SendEmailAlert(args);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationBehaviouralTermList"),
                Abp.Notifications.NotificationSeverity.Warn));
            UpdateUploadSummaryTable(args, GeneralStatusEnum.Failed, _localizationSource.GetString("FileCantBeConvertedToCalibrationBehaviouralTermList"));
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputBehaviouralTerm, DbHelperConst.COL_CalibrationId, args.CalibrationId.ToString()));

            //_behaviouralTermsRepository.Delete(x => x.CalibrationId == args.CalibrationId);
            //CurrentUnitOfWork.SaveChanges();
        }

        [UnitOfWork]
        private void UpdateCalibrationTableToDraftAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
                //CurrentUnitOfWork.SaveChanges();
            }
        }

        private void AddToUploadSummaryTable(ImportCalibrationDataFromExcelJobArgs args, int allJobs)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.CalibrationId);
            if (uploadSummary == null)
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

        private void SendEmailAlert(ImportCalibrationDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/behavioralTerms/view/" + args.CalibrationId;
            var type = "Behavioural terms calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportCalibrationDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var type = "Behavioural terms calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
