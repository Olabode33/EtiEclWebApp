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
    public class ImportCalibrationLgdHaircutFromExcelJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly ILgdHaircutExcelDataReader _lgdHaircutExcelDataReader;
        private readonly IInvalidLgdHaircutExporter _invalidExporter;
        private readonly IRepository<CalibrationLgdHairCut, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputLgdHairCut> _haircutRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;

        public ImportCalibrationLgdHaircutFromExcelJob(
            ILgdHaircutExcelDataReader lgdHaircutExcelDataReader,
            IInvalidLgdHaircutExporter invalidExporter, 
            IAppNotifier appNotifier, 
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<CalibrationInputLgdHairCut> haircutRepository,
            IRepository<CalibrationLgdHairCut, Guid> calibrationRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IObjectMapper objectMapper)
        {
            _lgdHaircutExcelDataReader = lgdHaircutExcelDataReader;
            _invalidExporter = invalidExporter;
            _haircutRepository = haircutRepository;
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
            var ccfSummary = GetHaircutListFromExcelOrNull(args);
            if (ccfSummary == null || !ccfSummary.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateHaircut(args, ccfSummary);
            UpdateCalibrationTableToDraftAsync(args);
            SendEmailAlert(args);
        }

        private List<ImportCalibrationLgdHaircutDto> GetHaircutListFromExcelOrNull(ImportCalibrationDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                return _lgdHaircutExcelDataReader.GetImportLgdHaircutFromExcel(file.Bytes);
            }
            catch(Exception)
            {
                return null;
            }
        }

        private void CreateHaircut(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationLgdHaircutDto> inputs)
        {
            var invalids = new List<ImportCalibrationLgdHaircutDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateHaircutAsync(input, args));
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

            AsyncHelper.RunSync(() => ProcessImportHaircutResultAsync(args, invalids));
        }

        private async Task CreateHaircutAsync(ImportCalibrationLgdHaircutDto input, ImportCalibrationDataFromExcelJobArgs args)
        {
            await _haircutRepository.InsertAsync(new CalibrationInputLgdHairCut()
            {
                Customer_No = input.Customer_No,
                Account_No = input.Account_No,
                Contract_No = input.Contract_No,
                Snapshot_Date = input.Snapshot_Date,
                Period = input.Period,
                Outstanding_Balance_Lcy = input.Outstanding_Balance_Lcy, 
                Debenture_OMV = input.Debenture_OMV,
                Debenture_FSV = input.Debenture_FSV,
                Cash_OMV = input.Cash_OMV,
                Cash_FSV = input.Cash_FSV,
                Inventory_OMV = input.Inventory_OMV,
                Inventory_FSV = input.Inventory_FSV,
                Plant_And_Equipment_OMV = input.Plant_And_Equipment_OMV,
                Plant_And_Equipment_FSV = input.Plant_And_Equipment_FSV,
                Residential_Property_OMV = input.Residential_Property_OMV,
                Residential_Property_FSV = input.Residential_Property_FSV,
                Commercial_Property_OMV = input.Commercial_Property_OMV,
                Commercial_Property_FSV = input.Commercial_Property_FSV,
                Receivables_OMV = input.Receivables_OMV,
                Receivables_FSV = input.Receivables_FSV,
                Shares_OMV = input.Shares_OMV,
                Shares_FSV = input.Shares_FSV,
                Vehicle_OMV = input.Vehicle_OMV,
                Vehicle_FSV = input.Vehicle_FSV,
                CalibrationId = args.CalibrationId,
                DateCreated = DateTime.Now
            });
        }

        private async Task ProcessImportHaircutResultAsync(ImportCalibrationDataFromExcelJobArgs args, List<ImportCalibrationLgdHaircutDto> invalids)
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
                    _localizationSource.GetString("AllCalibrationCcfSummarySuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportCalibrationDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToCalibrationLgdHaircutList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void DeleteExistingDataAsync(ImportCalibrationDataFromExcelJobArgs args)
        {
            _haircutRepository.Delete(x => x.CalibrationId == args.CalibrationId);
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

        private void SendEmailAlert(ImportCalibrationDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/haircut/view/" + args.CalibrationId;
            var type = "LGD haircut calibration";
            var calibration = _calibrationRepository.FirstOrDefault((Guid)args.CalibrationId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }

    }
}
