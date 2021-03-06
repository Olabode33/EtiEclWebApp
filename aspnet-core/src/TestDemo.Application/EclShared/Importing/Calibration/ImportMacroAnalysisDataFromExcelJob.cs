﻿using Abp.Application.Services.Dto;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TestDemo.AffiliateMacroEconomicVariable;
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
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;
        private readonly IRepository<TrackMacroUploadSummary> _uploadSummaryRepository;
        private readonly IEclCustomRepository _customRepository;

        public ImportMacroAnalysisDataFromExcelJob(
            IMacroAnalysisDataExcelDataReader excelDataReader,
            IInvalidMacroAnalysisDataExporter invalidExporter,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<MacroeconomicData> dataRepository,
            IRepository<MacroAnalysis> calibrationRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IEclCustomRepository customRepository, 
            IRepository<TrackMacroUploadSummary> uploadSummaryRepository,
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
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _userRepository = userRepository;
            _ouRepository = ouRepository;
            _customRepository = customRepository;
            _uploadSummaryRepository = uploadSummaryRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            AddToUploadSummaryTable(args);

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
                    _localizationSource.GetString("AllMacroAnalysisDataSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
                SendEmailAlert(args);
                UpdateUploadSummaryTable(args, GeneralStatusEnum.Completed, "");
            }
        }

        private void SendInvalidExcelNotification(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToMacroAnalysisDataList"),
                Abp.Notifications.NotificationSeverity.Warn));
            UpdateUploadSummaryTable(args, GeneralStatusEnum.Failed, _localizationSource.GetString("FileCantBeConvertedToMacroAnalysisDataList"));
        }

        [UnitOfWork]
        private void DeleteExistingDataAsync(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputMacroeconomicData, DbHelperConst.COL_MacroId, args.MacroId.ToString()));

            //_dataRepository.Delete(x => x.MacroId == args.MacroId);
        }

        [UnitOfWork]
        private void UpdateCalibrationTableToDraftAsync(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
            if (calibration != null)
            {
                calibration.Status = CalibrationStatusEnum.Draft;
                _calibrationRepository.Update(calibration);
            }
        }

        private void AddToUploadSummaryTable(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.MacroId);
            if (uploadSummary == null)
            {
                _uploadSummaryRepository.Insert(new TrackMacroUploadSummary
                {
                    RegisterId = args.MacroId,
                    AllJobs = 1,
                    CompletedJobs = 0,
                    Status = GeneralStatusEnum.Processing
                });
            }
            else
            {
                uploadSummary.RegisterId = args.MacroId;
                uploadSummary.AllJobs = 1;
                uploadSummary.CompletedJobs = 0;
                uploadSummary.Status = GeneralStatusEnum.Processing;
                _uploadSummaryRepository.Update(uploadSummary);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        private void UpdateUploadSummaryTable(ImportMacroAnalysisDataFromExcelJobArgs args, GeneralStatusEnum status, string comment)
        {
            var uploadSummary = _uploadSummaryRepository.FirstOrDefault(e => e.RegisterId == args.MacroId);
            if (uploadSummary != null)
            {
                uploadSummary.RegisterId = args.MacroId;
                uploadSummary.CompletedJobs = uploadSummary.AllJobs;
                uploadSummary.Status = status;
                uploadSummary.Comment = status == GeneralStatusEnum.Failed ? comment : "";
                _uploadSummaryRepository.Update(uploadSummary);
            }
            CurrentUnitOfWork.SaveChanges();
        }

        private void SendEmailAlert(ImportMacroAnalysisDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/macroAnalysis/view/" + args.MacroId;
            var type = "Macro analysis";
            var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

        private void SendInvalidEmailAlert(ImportMacroAnalysisDataFromExcelJobArgs args, FileDto file)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ServerRootAddress"];
            var link = baseUrl + "file/DownloadTempFile?fileType=" + file.FileType + "&fileToken=" + file.FileToken + "&fileName=" + file.FileName;

            var type = "Macro analysis";
            var calibration = _calibrationRepository.FirstOrDefault(args.MacroId);
            var ou = _ouRepository.FirstOrDefault(calibration.OrganizationUnitId);
            AsyncHelper.RunSync(() => _emailer.SendEmailInvalidDataUploadCompleteAsync(user, type, ou.DisplayName, link));
        }

    }
}
