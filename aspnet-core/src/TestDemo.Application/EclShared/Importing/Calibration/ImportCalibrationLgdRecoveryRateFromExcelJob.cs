﻿using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Threading;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Calibration;
using TestDemo.CalibrationInput;
using TestDemo.EclShared.Dtos;
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

        public ImportCalibrationLgdRecoveryRateFromExcelJob(
            ILgdRecoveryRateExcelDataReader lgdRecoveryRateExcelDataReader,
            IInvalidLgdRecoveryRateExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputLgdRecoveryRate> recoveryRateRepository,
            IRepository<CalibrationLgdRecoveryRate, Guid> calibrationRepository,
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
        }

        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
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
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllCalibrationRecoveryRateSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationLgdRecoveryRateList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            _recoveryRateRepository.Delete(x => x.CalibrationId == args.CalibrationId);
        }

        private void UpdateCalibrationTableToDraftAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
            }
        }

    }
}
