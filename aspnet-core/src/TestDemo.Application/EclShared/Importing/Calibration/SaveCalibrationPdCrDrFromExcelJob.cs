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
    public class SaveCalibrationPdCrDrFromExcelJob : BackgroundJob<SaveCalibrationPdCrDrFromExcelJobArgs>, ITransientDependency
    {
        private readonly IPdCrDrExcelDataReader _pdCrDrExcelDataReader;
        private readonly IInvalidPdCrDrExporter _invalidExporter;
        private readonly IRepository<CalibrationPdCrDr, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputPdCrDr> _pdCrDrRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IEclCustomRepository _customRepository;
        private readonly IRepository<TrackCalibrationPdCrDrException> _exceptionTrackerRepository;
        private readonly IRepository<TrackRunningUploadJobs> _uploadJobsTrackerRepository;
        private readonly IValidationUtil _validator;

        public SaveCalibrationPdCrDrFromExcelJob(
            IPdCrDrExcelDataReader pdCrDreExcelDataReader,
            IInvalidPdCrDrExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputPdCrDr> pdCrDrRepository,
            IRepository<CalibrationPdCrDr, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IEclCustomRepository customRepository,
            IRepository<TrackCalibrationPdCrDrException> exceptionTrackerRepository,
            IRepository<TrackRunningUploadJobs> uploadJobsTrackerRepository,
            IValidationUtil validator,
            IObjectMapper objectMapper)
        {
            _pdCrDrExcelDataReader = pdCrDreExcelDataReader;
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
            _customRepository = customRepository;
            _exceptionTrackerRepository = exceptionTrackerRepository;
            _uploadJobsTrackerRepository = uploadJobsTrackerRepository;
            _validator = validator;
        }

        [UnitOfWork]
        public override void Execute(SaveCalibrationPdCrDrFromExcelJobArgs args)
        {
            var records = args.UploadedRecords;
            var validatedRecords = ValidateRecords(records);

            CreatePdCrDr(args.Args, validatedRecords);
        }

        private List<ImportCalibrationPdCrDrDto> ValidateRecords(List<ImportCalibrationPdCrDrAsStringDto> inputs)
        {
            var records = new List<ImportCalibrationPdCrDrDto>();
            //var loanbookArray = loanbooks.ToArray();

            foreach (var item in inputs)
            {
                var exceptionMessage = new StringBuilder();
                var record = new ImportCalibrationPdCrDrDto();

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
                    record.RAPP_Date = _validator.ValidateIntegerValueFromRowOrNull(item.RAPP_Date, nameof(record.RAPP_Date), exceptionMessage);
                    record.Current_Rating = item.Current_Rating;

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

        private void CreatePdCrDr(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCrDrDto> inputs)
        {
            var invalids = new List<ImportCalibrationPdCrDrDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreatePdCrDrAsync(input, args));
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

        private async Task CreatePdCrDrAsync(ImportCalibrationPdCrDrDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _pdCrDrRepository.InsertAsync(new CalibrationInputPdCrDr()
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
                RAPP_Date = input.RAPP_Date,
                Current_Rating = input.Current_Rating,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportPdCrDrResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationPdCrDrDto> invalids)
        {
            if (invalids.Any())
            {
                foreach (var item in invalids)
                {
                    await _exceptionTrackerRepository.InsertAsync(new TrackCalibrationPdCrDrException
                    {
                        Account_No = item.Account_No,
                        RAPP_Date = item.RAPP_Date,
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
                        Product_Type = item.Product_Type
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
