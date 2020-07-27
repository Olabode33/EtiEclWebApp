using TestDemo.Authorization.Users;
using System.Collections.Generic;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Calibration.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.Calibration.Approvals;
using TestDemo.CalibrationInput;
using TestDemo.CalibrationResult;
using Abp.UI;
using TestDemo.Dto.Approvals;
using TestDemo.EclConfig;
using Abp.Configuration;
using TestDemo.EclShared.Dtos;
using Abp.Organizations;
using TestDemo.Calibration.Exporting;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.EclShared.Emailer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using TestDemo.Configuration;
using Abp.AutoMapper;
using TestDemo.Calibration.Jobs;
using Abp.BackgroundJobs;
using TestDemo.EclShared.Jobs;

namespace TestDemo.Calibration
{
    [AbpAuthorize(AppPermissions.Pages_Calibration)]
    public class CalibrationMacroAnalysisAppService : TestDemoAppServiceBase
    {
        private readonly IRepository<MacroAnalysis> _macroAnalysisRepository;
        private readonly IRepository<MacroAnalysisApproval> _calibrationApprovalRepository;
        private readonly IRepository<MacroeconomicData> _calibrationInputRepository;
        private readonly IRepository<MacroResult_PrincipalComponent> _principalComponentResultRepository;
        private readonly IRepository<MacroResult_Statistics> _statisticsResultRepository;
        private readonly IRepository<MacroResult_CorMat> _corMatResultRepository;
        private readonly IRepository<MacroResult_IndexData> _indexDataResultRepository;
        private readonly IRepository<MacroResult_PrincipalComponentSummary> _principalSummayrResultRepository;
        private readonly IRepository<MacroeconomicVariable> _macroeconomicVariableRepository;
        private readonly IRepository<AffiliateMacroEconomicVariableOffset> _affiliateMacroVariableRepository;
        private readonly IRepository<MacroResult_SelectedMacroEconomicVariables> _macroResultEconomicVariableRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IMacroAnalysisDataTemplateExporter _templateExporter;
        private readonly IEclEngineEmailer _emailer;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly IBackgroundJobManager _backgroundJobManager;


        public CalibrationMacroAnalysisAppService(
            IRepository<MacroAnalysis> calibrationRepository,
            IRepository<User, long> lookup_userRepository,
            IRepository<MacroAnalysisApproval> calibrationApprovalRepository,
            IRepository<MacroeconomicData> calibrationInputRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<MacroResult_PrincipalComponent> principalComponentResultRepository,
            IRepository<MacroResult_Statistics> statisticsResultRepository,
            IRepository<MacroResult_CorMat> corMatResultRepository,
            IRepository<MacroResult_IndexData> indexDataResultRepository,
            IRepository<MacroResult_PrincipalComponentSummary> principalSummayrResultRepository,
            IRepository<MacroeconomicVariable> macroeconomicVariableRepository,
            IRepository<AffiliateMacroEconomicVariableOffset> affiliateMacroVariableRepository,
            IRepository<MacroResult_SelectedMacroEconomicVariables> macroResultEconomicVariableRepository,
            IEclEngineEmailer emailer,
            IHostingEnvironment env,
            IBackgroundJobManager backgroundJobManager,
        IMacroAnalysisDataTemplateExporter templateeExporter)
        {
            _macroAnalysisRepository = calibrationRepository;
            _lookup_userRepository = lookup_userRepository;
            _calibrationApprovalRepository = calibrationApprovalRepository;
            _calibrationInputRepository = calibrationInputRepository;
            _principalComponentResultRepository = principalComponentResultRepository;
            _statisticsResultRepository = statisticsResultRepository;
            _corMatResultRepository = corMatResultRepository;
            _indexDataResultRepository = indexDataResultRepository;
            _principalSummayrResultRepository = principalSummayrResultRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _macroeconomicVariableRepository = macroeconomicVariableRepository;
            _affiliateMacroVariableRepository = affiliateMacroVariableRepository;
            _macroResultEconomicVariableRepository = macroResultEconomicVariableRepository;
            _templateExporter = templateeExporter;
            _emailer = emailer;
            _appConfiguration = env.GetAppConfiguration();
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task<PagedResultDto<GetMacroAnalysisRunForViewDto>> GetAll(GetAllCalibrationRunInput input)
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userOrganizationUnit = await UserManager.GetOrganizationUnitsAsync(user);
            //var userSubsChildren = _organizationUnitRepository.GetAll().Where(ou => userSubsidiaries.Any(uou => ou.Code.StartsWith(uou.Code)));
            var userOrganizationUnitIds = userOrganizationUnit.Select(ou => ou.Id);

            var statusFilter = input.StatusFilter.HasValue
                        ? (CalibrationStatusEnum)input.StatusFilter
                        : default;

            var filteredMacroAnalysisData = _macroAnalysisRepository.GetAll()
                        .Include(e => e.CloseByUserFk)
                        .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(input.AffiliateIdFilter.HasValue && input.AffiliateIdFilter > -1, e => e.OrganizationUnitId == input.AffiliateIdFilter);

            var pagedAndFilteredMacroAnalysis = filteredMacroAnalysisData
                .OrderBy(input.Sorting ?? "creationTime desc")
                .PageBy(input);

            var calibrationEadBehaviouralTerms = from o in pagedAndFilteredMacroAnalysis
                                                 join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                                 from s1 in j1.DefaultIfEmpty()

                                                 join ou in _organizationUnitRepository.GetAll() on o.OrganizationUnitId equals ou.Id into ou1
                                                 from ou2 in ou1.DefaultIfEmpty()

                                                 select new GetMacroAnalysisRunForViewDto()
                                                 {
                                                     Calibration = new MacroAnalysisRunDto
                                                     {
                                                         ClosedDate = o.ClosedDate,
                                                         Status = o.Status,
                                                         ModelType=o.ModelType,
                                                         Id = o.Id
                                                     },
                                                     ClosedBy = o.CloseByUserFk == null || o.CloseByUserFk.FullName == null ? "" : o.CloseByUserFk.FullName,
                                                     DateCreated = o.CreationTime,
                                                     CreatedBy = s1 == null ? "" : s1.FullName,
                                                     AffiliateName = ou2 == null ? "" : ou2.DisplayName
                                                 };

            var totalCount = await filteredMacroAnalysisData.CountAsync();

            return new PagedResultDto<GetMacroAnalysisRunForViewDto>(
                totalCount,
                await calibrationEadBehaviouralTerms.ToListAsync()
            );
        }

        public async Task<GetCalibrationRunForEditOutput> GetCalibrationForEdit(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCalibrationRunForEditOutput { Calibration = ObjectMapper.Map<CreateOrEditCalibrationRunDto>(calibration) };

            if (output.Calibration.CloseByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Calibration.CloseByUserId);
                output.ClosedByUserName = _lookupUser?.Name?.ToString();
            }

            if (calibration.OrganizationUnitId != null)
            {
                var ou = await _organizationUnitRepository.FirstOrDefaultAsync((calibration.OrganizationUnitId));
                output.AffiliateName = ou.DisplayName;
            }

            string createdBy = _lookup_userRepository.FirstOrDefault((long)calibration.CreatorUserId).FullName;
            string updatedBy = "";
            if (calibration.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)calibration.LastModifierUserId).FullName;
            }

            output.AuditInfo = new EclAuditInfoDto()
            {
                Approvals = await GetApprovalAudit(input),
                DateCreated = calibration.CreationTime,
                LastUpdated = calibration.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };

            return output;
        }

        protected async Task<List<EclApprovalAuditInfoDto>> GetApprovalAudit(EntityDto input)
        {

            var filteredApprovals = _calibrationApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.CalibrationFk)
                        .Where(e => e.MacroId == input.Id);

            var approvals = from o in filteredApprovals
                            join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new EclApprovalAuditInfoDto()
                            {
                                //EclId = (Guid)o.MacroId,
                                ReviewedDate = o.CreationTime,
                                Status = o.Status,
                                ReviewComment = o.ReviewComment,
                                ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                            };

            return await approvals.ToListAsync();
        }

        public async Task<GetMacroAnalysisDataDto> GetInputSummary(EntityDto input)
        {
            var total = await _calibrationInputRepository.CountAsync(x => x.MacroId == input.Id && x.MacroeconomicId == -1);
            var macro = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
            var items = await _affiliateMacroVariableRepository.GetAll()
                                                               .Include(e => e.MacroeconomicVariableFk)
                                                               .Where(e => e.AffiliateId == macro.OrganizationUnitId)
                                                               .Select(e => new NameValueDto<int>
                                                               {
                                                                   Value = e.MacroeconomicVariableId,
                                                                   Name = e.MacroeconomicVariableFk == null ? "" : e.MacroeconomicVariableFk.Name
                                                               })
                                                               .ToListAsync();
            var inputs = await _calibrationInputRepository.GetAllListAsync(e => e.MacroId == input.Id);


            List<string> columns = new List<string>();
            List<List<double?>> values = new List<List<double?>>();

            var npls = inputs.Where(e => e.MacroeconomicId == -1).Select(e => e.Value == null ? 0 : e.Value).ToList();
            var periods = inputs.Where(e => e.MacroeconomicId == -1).Select(e => e.Period).ToList();

            columns.Add("NPL_Percentage_Ratio");
            values.Add(npls);

            foreach (var item in items)
            {
                var value = inputs.Where(e => e.MacroeconomicId == item.Value).Select(e => e.Value == null ? 0 : e.Value).ToList();

                columns.Add(item.Name);
                values.Add(value);
            }


            return new GetMacroAnalysisDataDto
            {
                Columns = columns, 
                Values = values,
                Periods = periods
            };
        }

        public async Task<GetAllMacroResultDto> GetResult(EntityDto input)
        {
            var principal = await _principalComponentResultRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                        .Select(x => ObjectMapper.Map<MacroResultPrincipalComponentDto>(x))
                                                        .ToListAsync();
            var statistics = await _statisticsResultRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                        .Select(x => ObjectMapper.Map<MacroResultStatisticsDto>(x))
                                                        .ToListAsync();
            var indexData = await _indexDataResultRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                        .Select(x => ObjectMapper.Map<MacroResultIndexDataDto>(x))
                                                        .ToListAsync();
            var cor = await _corMatResultRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                        .Select(x => ObjectMapper.Map<MacroResultCorMatDto>(x))
                                                        .ToListAsync();
            var principalSummary = await _principalSummayrResultRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                        .Select(x => ObjectMapper.Map<MacroResultPrincipalComponentSummaryDto>(x))
                                                        .ToListAsync();


            return new GetAllMacroResultDto
            {
                PrincipalComponent = principal,
                Statistics = statistics,
                IndexData = indexData,
                CorMat = cor,
                PrincipalComponentSummary = principalSummary
            };
        }

        public async Task<int> CreateOrEdit(CreateOrEditMacroAnalysisRunDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                await Update(input);
                return (int)input.Id;
            }
        }

        protected virtual async Task<int> Create(CreateOrEditMacroAnalysisRunDto input)
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                long ouId = userSubsidiaries[0].Id;
                int id = await _macroAnalysisRepository.InsertAndGetIdAsync(new MacroAnalysis()
                {
                    OrganizationUnitId = ouId,
                    Status = CalibrationStatusEnum.Draft,
                    ModelType = input.ModelType
                });
                return id;
            }
            else
            {
                int id = await _macroAnalysisRepository.InsertAndGetIdAsync(new MacroAnalysis()
                {
                    OrganizationUnitId = (long)input.AffiliateId,
                    Status = CalibrationStatusEnum.Draft,
                    ModelType = input.ModelType
                });
                return id;
            }
        }

        protected virtual async Task Update(CreateOrEditMacroAnalysisRunDto input)
        {
            var calibrationEadBehaviouralTerm = await _macroAnalysisRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, calibrationEadBehaviouralTerm);
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Override)]
        public async Task UpdateStatisticsResult(List<MacroResultStatisticsDto> input)
        {
            var result = ObjectMapper.Map<List<MacroResult_Statistics>>(input);

            var mac = _macroAnalysisRepository.FirstOrDefault(input[0].MacroId);
            mac.Status = CalibrationStatusEnum.AppliedOverride;
            await _macroAnalysisRepository.UpdateAsync(mac);

            foreach (var item in result)
            {
                await _statisticsResultRepository.UpdateAsync(item);
            }

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input[0].MacroId,
                ReviewComment = "",
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = GeneralStatusEnum.Override
            });
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Override)]
        public async Task UpdatePrincipalSummaryResult(List<MacroResultPrincipalComponentSummaryDto> input)
        {
            var result = ObjectMapper.Map<List<MacroResult_PrincipalComponentSummary>>(input);

            var mac = _macroAnalysisRepository.FirstOrDefault(input[0].MacroId);
            mac.Status = CalibrationStatusEnum.AppliedOverride;
            await _macroAnalysisRepository.UpdateAsync(mac);

            foreach (var item in result)
            {
                await _principalSummayrResultRepository.UpdateAsync(item);
            }

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input[0].MacroId,
                ReviewComment = "",
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = GeneralStatusEnum.Override
            });
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Override)]
        public async Task UpdatePrincipalComponentResult(List<MacroResultPrincipalComponentDto> input)
        {
            var result = ObjectMapper.Map<List<MacroResult_PrincipalComponent>>(input);

            var mac = _macroAnalysisRepository.FirstOrDefault(input[0].MacroId);
            mac.Status = CalibrationStatusEnum.AppliedOverride;
            await _macroAnalysisRepository.UpdateAsync(mac);

            foreach (var item in result)
            {
                await _principalComponentResultRepository.UpdateAsync(item);
            }

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input[0].MacroId,
                ReviewComment = "",
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = GeneralStatusEnum.Override
            });
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Override)]
        public async Task UpdateIndexResult(List<MacroResultIndexDataDto> input)
        {
            var result = ObjectMapper.Map<List<MacroResult_IndexData>>(input);

            var mac = _macroAnalysisRepository.FirstOrDefault(input[0].MacroId);
            mac.Status = CalibrationStatusEnum.AppliedOverride;
            await _macroAnalysisRepository.UpdateAsync(mac);

            foreach (var item in result)
            {
                await _indexDataResultRepository.UpdateAsync(item);
            }

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input[0].MacroId,
                ReviewComment = "",
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = GeneralStatusEnum.Override
            });
        }

        public List<GetSelectedMacroeconomicVariableDto> GetMacroEconomicVariables(EntityDto input)
        {
            var output = new List<GetSelectedMacroeconomicVariableDto>();
            var macroResult_SelectedMacros = _macroResultEconomicVariableRepository.GetAll().Where(r => r.AffiliateId == input.Id).ToList();

            foreach (var item in macroResult_SelectedMacros)
            {
                output.Add(new GetSelectedMacroeconomicVariableDto
                {
                    MacroeconomicVariable = ObjectMapper.Map<MacroeconomicVariableDto>(_macroeconomicVariableRepository.FirstOrDefault(i => i.Id == item.MacroeconomicVariableId)),
                    SelectedMacroeconomicVariable = ObjectMapper.Map<OverrideSelectedMacroeconomicVariableDto>(item)
                });
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Override)]
        public async Task UpdateSelectedMacroVariablesResult(OverrideMacroeconomicVariableDto input)
        {
            var result = ObjectMapper.Map<List<MacroResult_SelectedMacroEconomicVariables>>(input.MacroeconomicVariables);

            var mac = _macroAnalysisRepository.FirstOrDefault(input.MacroId);
            mac.Status = CalibrationStatusEnum.AppliedOverride;
            await _macroAnalysisRepository.UpdateAsync(mac);

            foreach (var item in result)
            {
                await _macroResultEconomicVariableRepository.UpdateAsync(item);
            }

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input.MacroId,
                ReviewComment = "",
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = GeneralStatusEnum.Override
            });
        }

        public async Task Delete(EntityDto input)
        {
            await _macroAnalysisRepository.DeleteAsync(input.Id);
        }

        public virtual async Task SubmitForApproval(EntityDto input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
                calibration.Status = CalibrationStatusEnum.Submitted;
                ObjectMapper.Map(calibration, calibration);
                await SendSubmittedEmail(input.Id);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditMacroAnalysisApprovalDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.MacroId);

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input.MacroId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _calibrationApprovalRepository.GetAllListAsync(x => x.MacroId == input.MacroId && x.Status == GeneralStatusEnum.Approved);
                if (eclApprovals.Count(x => x.Status == GeneralStatusEnum.Approved) >= requiredApprovals)
                {
                    calibration.Status = CalibrationStatusEnum.Approved;
                    await SendApprovedEmail(calibration.Id);
                }
                else
                {
                    calibration.Status = CalibrationStatusEnum.AwaitngAdditionApproval;
                    await SendAdditionalApprovalEmail(calibration.Id);
                }
            }
            else
            {
                calibration.Status = CalibrationStatusEnum.Draft;
            }

            ObjectMapper.Map(calibration, calibration);
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_ReviewOverride)]
        public async Task ApproveRejectCalibrationResult(CreateOrEditMacroAnalysisApprovalDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.MacroId);

            await _calibrationApprovalRepository.InsertAsync(new MacroAnalysisApproval
            {
                MacroId = input.MacroId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });

            if (input.Status == GeneralStatusEnum.Approved)
            {
                calibration.Status = CalibrationStatusEnum.Completed;
            }
            else
            {
                calibration.Status = CalibrationStatusEnum.Draft;
            }

            ObjectMapper.Map(calibration, calibration);
        }

        public async Task RunCalibration(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
            if (calibration.Status == CalibrationStatusEnum.Approved)
            {
                calibration.Status = CalibrationStatusEnum.Completed;
                await _macroAnalysisRepository.UpdateAsync(calibration);
                //await _backgroundJobManager.EnqueueAsync<RunInvestmentEclJob, RunEclJobArgs>(new RunEclJobArgs
                //{
                //    EclId = input.Id,
                //    UserIdentifier = AbpSession.ToUserIdentifier()
                //});
            }
            else
            {
                throw new UserFriendlyException(L("CalibrationMustBeApprovedBeforeRunning"));
            }
        }

        public async Task GenerateReport(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);

            if (calibration.Status == CalibrationStatusEnum.Completed || calibration.Status == CalibrationStatusEnum.AppliedToEcl)
            {
                //await _backgroundJobManager.EnqueueAsync<GenerateEclReportJob, GenerateReportJobArgs>(new GenerateReportJobArgs()
                //{
                //    eclId = input.Id,
                //    eclType = EclType.Investment,
                //    userIdentifier = AbpSession.ToUserIdentifier()
                //});
            }
            else
            {
                throw new UserFriendlyException(L("GenerateReportErrorCalibrationNotRun"));
            }
        }

        public async Task ApplyToEcl(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
            var selectedMacro = await _macroResultEconomicVariableRepository.GetAllListAsync(e => e.AffiliateId == calibration.OrganizationUnitId);
            if (selectedMacro.Count <= 0)
            {
                throw new UserFriendlyException(L("NoSelectedMacroVariablesFromResultError"));
            }

            if (calibration.Status == CalibrationStatusEnum.Completed)
            {
                //Call apply to ecl job
                var old = await _macroAnalysisRepository.FirstOrDefaultAsync(x => x.Status == CalibrationStatusEnum.AppliedToEcl && x.OrganizationUnitId == calibration.OrganizationUnitId);
                if (old != null)
                {
                    old.Status = CalibrationStatusEnum.Completed;
                    await _macroAnalysisRepository.UpdateAsync(old);
                }

                calibration.Status = CalibrationStatusEnum.AppliedToEcl;
                await _macroAnalysisRepository.UpdateAsync(calibration);
            }
            else
            {
                throw new UserFriendlyException(L("ApplyCalibrationToEclError"));
            }
        }

        public async Task CloseCalibration(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);

            if (calibration.Status == CalibrationStatusEnum.Completed)
            {
                //Call apply to ecl job

            }
            else
            {
                throw new UserFriendlyException(L("ApplyCalibrationToEclError"));
            }
        }

        public async Task<FileDto> ExportToExcel(EntityDto input)
        {

            var macro = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
            var items = await _affiliateMacroVariableRepository.GetAll()
                                                               .Include(e => e.MacroeconomicVariableFk)
                                                               .Where(e => e.AffiliateId == macro.OrganizationUnitId)
                                                               .Select(e => new NameValueDto<int> {
                                                                    Value = e.MacroeconomicVariableId,
                                                                    Name = e.MacroeconomicVariableFk == null ? "" : e.MacroeconomicVariableFk.Name
                                                               })
                                                               .ToListAsync();
            var inputs = await _calibrationInputRepository.GetAllListAsync(e => e.MacroId == input.Id);

            
            List<string> columns = new List<string>();
            List<List<double?>> values = new List<List<double?>>();

            var npls = inputs.Where(e => e.MacroeconomicId == -1).Select(e => e.Value == null ? 0 : e.Value).ToList();
            var periods = inputs.Where(e => e.MacroeconomicId == -1).Select(e => e.Period).ToList();

            columns.Add("NPL_Percentage_Ratio");
            values.Add(npls);

            foreach (var item in items)
            {
                var value = inputs.Where(e => e.MacroeconomicId == item.Value).Select(e => e.Value == null ? 0 : e.Value).ToList();

                columns.Add(item.Name);
                values.Add(value);
            }


            return _templateExporter.ExportInputToFile(columns, values, periods);
        }

        public async Task<FileDto> GetInputTemplate(EntityDto input)
        {
            var macro = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);
            var items = await _affiliateMacroVariableRepository.GetAll()
                                                               .Include(e => e.MacroeconomicVariableFk)
                                                               .Where(e => e.AffiliateId == macro.OrganizationUnitId)
                                                               .Select(e => e.MacroeconomicVariableFk == null ? "" : e.MacroeconomicVariableFk.Name)
                                                               .ToListAsync();

            return _templateExporter.ExportTemplateToFile(items);
        }

        [AbpAuthorize(AppPermissions.Pages_Calibration_Erase)]
        public async Task Erase(EntityDto input)
        {
            var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(input.Id);

            if (calibration.Status != CalibrationStatusEnum.AppliedToEcl)
            {
                //Call apply to ecl job
                await _macroAnalysisRepository.DeleteAsync(input.Id);
                await _backgroundJobManager.EnqueueAsync<EraseCalibrationJob, EraserJobArgs>(new EraserJobArgs
                {
                    EraseType = TrackTypeEnum.MacroAnalysis,
                    IntId = input.Id
                });
            }
            else
            {
                throw new UserFriendlyException(L("ApplyCalibrationToEclError"));
            }
        }

        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(int calibrationId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _calibrationInputRepository.GetAllListAsync(x => x.MacroId == calibrationId);
            if (uploads.Count > 0)
            {
                var calibration = await _macroAnalysisRepository.FirstOrDefaultAsync(calibrationId);
                var notCompleted = calibration.Status == CalibrationStatusEnum.Uploading;
                output.Status = !notCompleted;
                output.Message = notCompleted == true ? L("UploadInProgressError") : "";
            }
            else
            {
                output.Status = false;
                output.Message = L("NoUploadedRecordFoundForEcl");
            }

            return output;
        }

        public async Task SendSubmittedEmail(int calibrationId)
        {
            var calibration = _macroAnalysisRepository.FirstOrDefault(calibrationId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/macroAnalysis/view/" + calibrationId;
            var type = "Macro analysis";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = calibration.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = calibration.CreatorUserId == null ? (long)AbpSession.UserId : (long)calibration.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.CalibrationSubmittedEmail
            });
        }

        public async Task SendAdditionalApprovalEmail(int calibrationId)
        {
            var calibration = _macroAnalysisRepository.FirstOrDefault(calibrationId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/macroAnalysis/view/" + calibrationId;
            var type = "Macro analysis";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = calibration.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = calibration.CreatorUserId == null ? (long)AbpSession.UserId : (long)calibration.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.CalibrationAwaitingApprovalEmail
            });
        }

        public async Task SendApprovedEmail(int calibrationId)
        {
            var calibration = _macroAnalysisRepository.FirstOrDefault(calibrationId);
            var baseUrl = _appConfiguration["App:ClientRootAddress"];
            var link = baseUrl + "/app/main/calibration/macroAnalysis/view/" + calibrationId;
            var type = "Macro analysis";

            await _backgroundJobManager.EnqueueAsync<SendEmailJob, SendEmailJobArgs>(new SendEmailJobArgs()
            {
                AffiliateId = calibration.OrganizationUnitId,
                Link = link,
                Type = type,
                UserId = calibration.CreatorUserId == null ? (long)AbpSession.UserId : (long)calibration.CreatorUserId,
                SendEmailType = SendEmailTypeEnum.CalibrationApprovedEmail
            });
        }

    }
}