using Abp.Application.Services.Dto;
using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Abp.ObjectMapping;
using Abp.Threading;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TestDemo.AffiliateMacroEconomicVariable;
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
    public class ImportMacroAnalysisDataFromExcelJob : BackgroundJob<ImportMacroAnalysisDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IMacroAnalysisDataExcelDataReader _excelDataReader;
        private readonly IInvalidMacroAnalysisDataExporter _invalidExporter;
        private readonly IRepository<MacroAnalysis> _calibrationRepository;
        private readonly IRepository<MacroeconomicData> _dataRepository;
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroVariableRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;

        public ImportMacroAnalysisDataFromExcelJob(
            IMacroAnalysisDataExcelDataReader excelDataReader,
            IInvalidMacroAnalysisDataExporter invalidExporter,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<MacroeconomicData> dataRepository,
            IRepository<MacroAnalysis> calibrationRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IObjectMapper objectMapper)
        {
            _excelDataReader = excelDataReader;
            _invalidExporter = invalidExporter;
            _dataRepository = dataRepository;
            _calibrationRepository = calibrationRepository;
            _affiliateMacroVariableRepository = affiliateMacroVariableRepository;
            _appNotifier = appNotifier;
            _binaryObjectManager = binaryObjectManager;
            _objectMapper = objectMapper;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        [UnitOfWork]
        public override void Execute(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            var macroAnalysis = GetMacroAnalysisDataFromExcelOrNull(args);
            if (macroAnalysis == null || !macroAnalysis.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateMacroAnalysis(args, macroAnalysis);
            UpdateCalibrationTableToDraftAsync(args);
        }

        private List<ImportMacroAnalysisDataDto> GetMacroAnalysisDataFromExcelOrNull(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));
                var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
                var all = _affiliateMacroVariableRepository.GetAllList(x => x.AffiliateId == calibration.OrganizationUnitId);
                var affiliateMacroVariables = all.Where(x => x.AffiliateId == calibration.OrganizationUnitId)
                                                    .Select(x => new NameValueDto<int>
                                                    {
                                                        Value = x.MacroeconomicVariableId,
                                                        Name = x.MacroeconomicVariableFk == null ? "" : x.MacroeconomicVariableFk.Name
                                                    }).ToList();
                return _excelDataReader.GetImportMacroAnalysisDataFromExcel(file.Bytes, affiliateMacroVariables);
            }
            catch (Exception e)
            {
                Logger.Debug("Error imporint MacroAnalysisDataFromExcel: " + e.Message);
                return null;
            }
        }

        private void CreateMacroAnalysis(ImportMacroAnalysisDataFromExcelJobArgs args, List<ImportMacroAnalysisDataDto> inputs)
        {
            var invalids = new List<ImportMacroAnalysisDataDto>();

            foreach (var input in inputs)
            {
                if (input.CanBeImported())
                {
                    try
                    {
                        AsyncHelper.RunSync(() => CreateMacroAnalysisAsync(input, args));
                    }
                    catch (UserFriendlyException exception)
                    {
                        input.Exception = exception.Message;
                        invalids.Add(input);
                    }
                    catch (Exception exception)
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

            AsyncHelper.RunSync(() => ProcessImportMacroAnalysisDataResultAsync(args, invalids));
        }

        private async Task CreateMacroAnalysisAsync(ImportMacroAnalysisDataDto input, ImportMacroAnalysisDataFromExcelJobArgs args)
        {

            var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
            await _dataRepository.InsertAsync(new MacroeconomicData()
            {
                MacroeconomicId = input.MacroeconomicId,
                Value = input.Value,
                Period = input.Period,
                AffiliateId = calibration.OrganizationUnitId,
                MacroId = args.MacroId
            });
        }

        private async Task ProcessImportMacroAnalysisDataResultAsync(ImportMacroAnalysisDataFromExcelJobArgs args, List<ImportMacroAnalysisDataDto> invalids)
        {
            if (invalids.Any())
            {

                var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
                var affiliateMacroVariables = _affiliateMacroVariableRepository.GetAll()
                                                                               .Include(x => x.MacroeconomicVariableFk)
                                                                               .Where(x => x.AffiliateId == calibration.OrganizationUnitId)
                                                                               .Select(x => new NameValueDto<int>
                                                                               {
                                                                                   Value = x.MacroeconomicVariableId,
                                                                                   Name = x.MacroeconomicVariableFk == null ? "" : x.MacroeconomicVariableFk.Name
                                                                               })
                                                                               .ToList();
                var file = _invalidExporter.ExportToFile(invalids, affiliateMacroVariables);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllMacroAnalysisDataSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToMacroAnalysisDataList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void DeleteExistingDataAsync(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            _dataRepository.Delete(x => x.MacroId == args.MacroId);
        }

        private void UpdateCalibrationTableToDraftAsync(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
            }
        }

    }
}
