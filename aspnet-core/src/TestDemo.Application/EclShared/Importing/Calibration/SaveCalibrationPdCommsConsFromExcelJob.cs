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
    public class SaveCalibrationPdCommsConsFromExcelJob : BackgroundJob<SaveCalibrationPdCommsConsFromExcelJobArgs>, ITransientDependency
    {
        private readonly IPdCommsConsExcelDataReader _pdCommsConsExcelDataReader;
        private readonly IInvalidPdCrDrExporter _invalidExporter;
        private readonly IRepository<CalibrationPdCommsCons, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputPdCommsCon> _pdCommsConsRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IEclCustomRepository _customRepository;
        private readonly IRepository<TrackCalibrationPdCommsConsException> _exceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IValidationUtil _validator;

        public SaveCalibrationPdCommsConsFromExcelJob(
            IPdCommsConsExcelDataReader pdCrDreExcelDataReader,
            IInvalidPdCrDrExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputPdCommsCon> pdCrDrRepository,
            IRepository<CalibrationPdCommsCons, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IEclCustomRepository customRepository,
            IRepository<TrackCalibrationPdCommsConsException> exceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IValidationUtil validator,
            IObjectMapper objectMapper)
        {
            _pdCommsConsExcelDataReader = pdCrDreExcelDataReader;
            _invalidExporter = invalidExporter;
            _pdCommsConsRepository = pdCrDrRepository;
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
        public override void Execute(SaveCalibrationPdCommsConsFromExcelJobArgs args)
        {
            var records = args.UploadedRecords;
            var validatedRecords = ValidateRecords(records);

            CreatePdCommsCons(args.Args, validatedRecords);
        }

        private List<ImportCalibrationPdCommsConsDto> ValidateRecords(List<ImportCalibrationPdCommsConsAsStringDto> inputs)
        {
            var records = new List<ImportCalibrationPdCommsConsDto>();
            //var loanbookArray = loanbooks.ToArray();

            foreach (var item in inputs)
            {
                var exceptionMessage = new StringBuilder();
                var record = new ImportCalibrationPdCommsConsDto();

                try
                {
                    record.Customer_No = item.Customer_No;
                    record.Account_No = item.Account_No;
                    record.Contract_No = item.Contract_No;
                    record.Product_Type = item.Product_Type;
                    record.Days_Past_Due = _validator.ValidateIntegerValueFromRowOrNull(item.Days_Past_Due, nameof(record.Days_Past_Due), exceptionMessage);
                    record.Classification = item.Classification;
                    record.Outstanding_Balance_Lcy = _validator.ValidateDoubleValueFromRowOrNull(item.Outstanding_Balance_Lcy, nameof(record.Outstanding_Balance_Lcy), exceptionMessage);
                    record.Contract_Start_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Contract_Start_Date, nameof(record.Contract_Start_Date), exceptionMessage);
                    record.Contract_End_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Contract_End_Date, nameof(record.Contract_End_Date), exceptionMessage);
                    record.Snapshot_Date = _validator.ValidateDateTimeValueFromRowOrNull(item.Snapshot_Date, nameof(record.Snapshot_Date), exceptionMessage);
                    record.Current_Rating = _validator.ValidateIntegerValueFromRowOrNull(item.Current_Rating, nameof(record.Current_Rating), exceptionMessage);
                    record.Serial = item.Serial;
                    record.Segment = item.Segment;
                    record.WI = item.WI;

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

        private void CreatePdCommsCons(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCommsConsDto> inputs)
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

            AsyncHelper.RunSync(() => ProcessImportPdCrDrResultAsync(args, invalids));
        }

        private async Task CreatePdCommsConsAsync(ImportCalibrationPdCommsConsDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _pdCommsConsRepository.InsertAsync(new CalibrationInputPdCommsCon()
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
                Current_Rating = input.Current_Rating,
                Segment = input.Segment,
                Serial=input.Serial,
                WI = input.WI,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportPdCrDrResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCommsConsDto> invalids)
        {
            if (invalids.Any())
            {
                foreach (var item in invalids)
                {
                    await _exceptionTrackerRepository.InsertAsync(new TrackCalibrationPdCommsConsException
                    {
                        Account_No = item.Account_No,
                        Snapshot_Date = item.Snapshot_Date,
                        CalibrationId = args.CalibrationId,
                        Classification = item.Classification,
                        Contract_End_Date = item.Contract_End_Date,
                        Contract_No = item.Contract_No,
                        Contract_Start_Date = item.Contract_Start_Date,
                        Current_Rating = item.Current_Rating,
                        Customer_No = item.Customer_No,
                        Days_Past_Due = item.Days_Past_Due,
                        Exception = item.Exception,
                        Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                        Product_Type = item.Product_Type,
                        Segment = item.Segment,
                        WI = item.WI,
                        Serial = item.Serial                        
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
