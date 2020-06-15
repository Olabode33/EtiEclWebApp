using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
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

        public ImportCalibrationBehaviouralFromExcelJob(
            IEadBehaviouralTermExcelDataReader eadBehaviouralTermExcelDataReader,
            IInvalidEadBehaviouralTermExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputEadBehaviouralTerms> behaviouralTermsRepository,
            IRepository<CalibrationEadBehaviouralTerm, Guid> calibrationRepository,
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
        }

        [UnitOfWork]
        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
            var paymentSchedules = GeBehaviouralTermsListFromExcelOrNull(args);
            if (paymentSchedules == null || !paymentSchedules.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateBehaviouralTerm(args, paymentSchedules);
            UpdateCalibrationTableToDraftAsync(args);
        }

        private List<ImportCalibrationBehaviouralTermDto> GeBehaviouralTermsListFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
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
                DateCreated = DateTime.Now,
                Assumption_NonExpired = input.Assumption_NonExpired,
                Freq_NonExpired = input.Freq_NonExpired,
                Assumption_Expired = input.Assumption_Expired,
                Freq_Expired = input.Freq_Expired,
                Comment = input.Comment
            });
        }

        private async Task ProcessImportBehaviouralTermResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationBehaviouralTermDto> invalidBehaviouralTerm)
        {
            if (invalidBehaviouralTerm.Any())
            {
                var file = _invalidExporter.ExportToFile(invalidBehaviouralTerm);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllCalibrationBehaviouralTermSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationBehaviouralTermList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            _behaviouralTermsRepository.Delete(x => x.CalibrationId == args.CalibrationId);
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
