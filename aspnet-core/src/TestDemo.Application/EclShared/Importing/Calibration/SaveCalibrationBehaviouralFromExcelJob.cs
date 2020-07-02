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
using TestDemo.EclShared.Importing.Utils;
using TestDemo.InvestmentComputation;
using TestDemo.Notifications;
using TestDemo.ObeInputs;
using TestDemo.RetailInputs;
using TestDemo.Storage;
using TestDemo.WholesaleInputs;

namespace TestDemo.EclShared.Importing
{
    public class SaveCalibrationBehaviouralFromExcelJob : BackgroundJob<SaveCalibrationBehaviouralTermExcelJobArgs>, ITransientDependency
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
        private readonly IRepository<TrackCalibrationBehaviouralTermException> _exceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IValidationUtil _validator;

        public SaveCalibrationBehaviouralFromExcelJob(
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
            IEclCustomRepository customRepository,
            IRepository<TrackCalibrationBehaviouralTermException> exceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IValidationUtil validator,
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
            _customRepository = customRepository;
            _exceptionTrackerRepository = exceptionTrackerRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _validator = validator;
        }

        [UnitOfWork]
        public override void Execute(SaveCalibrationBehaviouralTermExcelJobArgs args)
        {
            var records = args.UploadedRecords;
            var validatedRecords = ValidateRecords(records);

            CreateBehaviouralTerm(args.Args, validatedRecords);
        }


        private List<ImportCalibrationBehaviouralTermDto> ValidateRecords(List<ImportCalibrationBehaviouralTermAsStringDto> inputs)
        {
            var records = new List<ImportCalibrationBehaviouralTermDto>();
            //var loanbookArray = loanbooks.ToArray();

            foreach (var item in inputs)
            {
                var exceptionMessage = new StringBuilder();
                var record = new ImportCalibrationBehaviouralTermDto();

                try
                {
                    record.Customer_No = item.Customer_No;
                    record.Account_No = item.Account_No;
                    record.Contract_No = item.Contract_No;
                    record.Customer_Name = item.Customer_Name;
                    record.Snapshot_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Snapshot_Date, nameof(record.Snapshot_Date), exceptionMessage);
                    record.Classification = item.Classification;
                    record.Original_Balance_Lcy = _validator.ValidateDoubleValueFromRowOrNull(item.Original_Balance_Lcy, nameof(record.Original_Balance_Lcy), exceptionMessage);
                    record.Outstanding_Balance_Lcy = _validator.ValidateDoubleValueFromRowOrNull(item.Outstanding_Balance_Lcy, nameof(record.Outstanding_Balance_Lcy), exceptionMessage);
                    record.Outstanding_Balance_Acy = _validator.ValidateDoubleValueFromRowOrNull(item.Outstanding_Balance_Acy, nameof(record.Outstanding_Balance_Acy), exceptionMessage);
                    record.Contract_Start_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Contract_Start_Date, nameof(record.Contract_Start_Date), exceptionMessage);
                    record.Contract_End_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Contract_End_Date, nameof(record.Contract_End_Date), exceptionMessage);
                    record.Restructure_Indicator = item.Restructure_Indicator;
                    record.Restructure_Type = item.Restructure_Type;
                    record.Restructure_Start_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Restructure_Start_Date, nameof(record.Restructure_Start_Date), exceptionMessage);
                    record.Restructure_End_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Restructure_End_Date, nameof(record.Restructure_End_Date), exceptionMessage);

                    if (exceptionMessage.Length > 0)
                    {
                        record.Exception = exceptionMessage.ToString();
                    }
                }
                catch (Exception e)
                {
                    record.Exception = e.Message;
                }

                records.Add(record);
            }

            return records;
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
                DateCreated = DateTime.Now,
            });
        }

        private async Task ProcessImportBehaviouralTermResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationBehaviouralTermDto> invalidBehaviouralTerm)
        {
            if (invalidBehaviouralTerm.Any())
            {
                foreach (var item in invalidBehaviouralTerm)
                {
                    await _exceptionTrackerRepository.InsertAsync(new TrackCalibrationBehaviouralTermException
                    {
                        Customer_No = item.Customer_No,
                        Account_No = item.Account_No,
                        Contract_No = item.Contract_No,
                        Customer_Name = item.Customer_Name,
                        Snapshot_Date = item.Snapshot_Date,
                        Classification = item.Classification,
                        Original_Balance_Lcy = item.Original_Balance_Lcy,
                        Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                        Outstanding_Balance_Acy = item.Outstanding_Balance_Acy,
                        Contract_Start_Date = item.Contract_Start_Date,
                        Contract_End_Date = item.Contract_End_Date,
                        Restructure_Indicator = item.Restructure_Indicator,
                        Restructure_Type = item.Restructure_Type,
                        Restructure_Start_Date = item.Restructure_Start_Date,
                        Restructure_End_Date = item.Restructure_End_Date,
                        CalibrationId = args.CalibrationId,
                        Exception = item.Exception
                    });
                }
            }
            _uploadJobsTrackerRepository.Insert(new TrackRunningUploadJobs
            {
                RegisterId = args.CalibrationId
            });
        }

    }
}
