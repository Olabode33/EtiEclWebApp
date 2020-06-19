using Abp.Application.Services.Dto;
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
using TestDemo.CalibrationResult;
using TestDemo.Configuration;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared.Emailer;
using TestDemo.EclShared.Importing.Assumptions.Dto;
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
    public class ImportMacroProjectionsDataFromExcelJob : BackgroundJob<ImportAssumptionDataFromExcelJobArgs>, ITransientDependency
    {
        private readonly IMacroProjectionDataExcelDataReader _excelDataReader;
        private readonly IInvalidMacroProjectionDataExporter _invalidExporter; 
        private readonly IRepository<AffiliateAssumption, Guid> _affiliateAssumptionsRepository;
        private readonly IRepository<AssumptionApproval, Guid> _affiliateAssumptionsApprovalRepository;
        private readonly IRepository<PdInputAssumptionMacroeconomicProjection, Guid> _dataRepository;
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroVariableRepository;
        private readonly IRepository<MacroResult_SelectedMacroEconomicVariables> _selectedMacroVariableRepository;
        private readonly IRepository<MacroeconomicVariable> _macroVariableRepository;
        private readonly IAppNotifier _appNotifier;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ILocalizationSource _localizationSource;
        private readonly IObjectMapper _objectMapper;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<OrganizationUnit, long> _ouRepository;

        public ImportMacroProjectionsDataFromExcelJob(
            IMacroProjectionDataExcelDataReader excelDataReader,
            IInvalidMacroProjectionDataExporter invalidExporter,
            IAppNotifier appNotifier,
            IBinaryObjectManager binaryObjectManager,
            ILocalizationManager localizationManager,
            IRepository<AffiliateAssumption, Guid> affiliateAssumptionRepository,
            IRepository<AssumptionApproval, Guid> affiliateAssumptionsApprovalRepository,
            IRepository<PdInputAssumptionMacroeconomicProjection, Guid> dataRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IRepository<MacroResult_SelectedMacroEconomicVariables> selectedMacroVariableRepository,
            IRepository<MacroeconomicVariable> macroVariableRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IRepository<User, long> userRepository,
            IRepository<OrganizationUnit, long> ouRepository,
            IObjectMapper objectMapper)
        {
            _excelDataReader = excelDataReader;
            _invalidExporter = invalidExporter;
            _dataRepository = dataRepository;
            _affiliateAssumptionsRepository = affiliateAssumptionRepository;
            _affiliateAssumptionsApprovalRepository = affiliateAssumptionsApprovalRepository;
            _affiliateMacroVariableRepository = affiliateMacroVariableRepository;
            _selectedMacroVariableRepository = selectedMacroVariableRepository;
            _macroVariableRepository = macroVariableRepository;
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
        public override void Execute(ImportAssumptionDataFromExcelJobArgs args)
        {
            var macroAnalysis = GetMacroProjectionDataFromExcelOrNull(args);
            if (macroAnalysis == null || !macroAnalysis.Any())
            {
                SendInvalidExcelNotification(args);
                return;
            }

            DeleteExistingDataAsync(args);
            CreateMacroProjection(args, macroAnalysis);
            SubmitForApproval(args);
            SendEmailAlert(args);
        }

        private List<ImportMacroProjectionDataDto> GetMacroProjectionDataFromExcelOrNull(ImportAssumptionDataFromExcelJobArgs args)
        {
            try
            {
                var file = AsyncHelper.RunSync(() => _binaryObjectManager.GetOrNullAsync(args.BinaryObjectId));

                var filter = _selectedMacroVariableRepository.GetAll().Where(x => x.AffiliateId == args.AffiliateId);
                var query = from s in filter
                            join m in _macroVariableRepository.GetAll() on s.MacroeconomicVariableId equals m.Id into m1
                            from m2 in m1.DefaultIfEmpty()
                            select new NameValueDto<int> { 
                                Value = s.MacroeconomicVariableId,
                                Name = m2 == null ? "" : m2.Name };

                var items = query.OrderBy(e => e.Name).ToList();


                return _excelDataReader.GetImportMacroProjectionDataFromExcel(file.Bytes, items);
            }
            catch (Exception e)
            {
                Logger.Debug("Error importing MacroProjectionDataFromExcel: " + e.Message);
                return null;
            }
        }

        private void CreateMacroProjection(ImportAssumptionDataFromExcelJobArgs args, List<ImportMacroProjectionDataDto> inputs)
        {
            var invalids = new List<ImportMacroProjectionDataDto>();

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

        private async Task CreateMacroAnalysisAsync(ImportMacroProjectionDataDto input, ImportAssumptionDataFromExcelJobArgs args)
        {
            await _dataRepository.InsertAsync(new PdInputAssumptionMacroeconomicProjection()
            {
                Key = "MacroeconomicProjectionInputs" + input.MacroeconomicVariableId.ToString() + input.Date.Value.ToString("yyyy-mm-dd"),
                Date = (DateTime)input.Date,
                InputName = input.InputName,
                BestValue = (double)input.BestValue,
                OptimisticValue = (double)input.OptimisticValue,
                DownturnValue = (double)input.DownturnValue,
                MacroeconomicVariableId = (int)input.MacroeconomicVariableId,
                IsComputed = false,
                CanAffiliateEdit = false,
                RequiresGroupApproval = true,
                Status = GeneralStatusEnum.Approved,
                Framework = args.Framework,
                OrganizationUnitId = args.AffiliateId,
                CreatorUserId = args.User.UserId,
                LastModificationTime = DateTime.Now,
                LastModifierUserId = args.User.UserId
            });
        }

        private async Task ProcessImportMacroAnalysisDataResultAsync(ImportAssumptionDataFromExcelJobArgs args, List<ImportMacroProjectionDataDto> invalids)
        {
            if (invalids.Any())
            {

                var filter = _selectedMacroVariableRepository.GetAll().Where(x => x.AffiliateId == args.AffiliateId);
                var query = from s in filter
                            join m in _macroVariableRepository.GetAll() on s.MacroeconomicVariableId equals m.Id into m1
                            from m2 in m1.DefaultIfEmpty()
                            select new NameValueDto<int>
                            {
                                Value = s.MacroeconomicVariableId,
                                Name = m2 == null ? "" : m2.Name
                            };

                var items = query.OrderBy(e => e.Name).ToList();

                var file = _invalidExporter.ExportToFile(invalids, items);
                await _appNotifier.SomeUsersCouldntBeImported(args.User, file.FileToken, file.FileType, file.FileName);
            }
            else
            {
                await _appNotifier.SendMessageAsync(
                    args.User,
                    _localizationSource.GetString("AllMacroProjectionDataSuccessfullyImportedFromExcel"),
                    Abp.Notifications.NotificationSeverity.Success);
            }
        }

        private void SendInvalidExcelNotification(ImportAssumptionDataFromExcelJobArgs args)
        {
            _appNotifier.SendMessageAsync(
                args.User,
                _localizationSource.GetString("FileCantBeConvertedToMacroProjectionDataList"),
                Abp.Notifications.NotificationSeverity.Warn);
        }

        private void DeleteExistingDataAsync(ImportAssumptionDataFromExcelJobArgs args)
        {
            var ids = _dataRepository.GetAllList(x => x.Framework == args.Framework && x.OrganizationUnitId == args.AffiliateId);
            foreach (var item in ids)
            {
                _dataRepository.HardDelete(e => e.Id == item.Id);
            }
        }

        private void SubmitForApproval(ImportAssumptionDataFromExcelJobArgs args)
        {
            var assumption = _affiliateAssumptionsRepository.FirstOrDefault(x => x.OrganizationUnitId == args.AffiliateId);
            _affiliateAssumptionsApprovalRepository.Insert(new AssumptionApproval()
            {
                OrganizationUnitId = args.AffiliateId,
                Framework = args.Framework,
                AssumptionType = AssumptionTypeEnum.PdSnPAssumption,
                AssumptionGroup = "SnP Cummulative Default Rate",
                InputName = "SnP Cummulative Default Rate",
                OldValue = "-",
                NewValue = _localizationSource.GetString("CheckAssumptionsPage"),
                AssumptionId = assumption.Id,
                AssumptionEntity = EclEnums.PdSnpAssumption,
                Status = GeneralStatusEnum.Submitted
            });
        }

        private void SendEmailAlert(ImportAssumptionDataFromExcelJobArgs args)
        {
            var user = _userRepository.FirstOrDefault(args.User.UserId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/assumption/affiliates/view/" + args.AffiliateId;
            var type = args.Framework.ToString() + " Macro-economic projection";
            var ou = _ouRepository.FirstOrDefault(args.AffiliateId);
            _emailer.SendEmailDataUploadCompleteAsync(user, type, ou.DisplayName, link);
        }

    }
}
