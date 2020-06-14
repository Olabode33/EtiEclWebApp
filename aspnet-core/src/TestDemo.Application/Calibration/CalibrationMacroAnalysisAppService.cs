﻿using TestDemo.Authorization.Users;
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

namespace TestDemo.Calibration
{
    [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms)]
    public class CalibrationMacroAnalysisAppService : TestDemoAppServiceBase
    {
        private readonly IRepository<MacroAnalysis> _calibrationRepository;
        private readonly IRepository<MacroAnalysisApproval> _calibrationApprovalRepository;
        private readonly IRepository<MacroeconomicData> _calibrationInputRepository;
        private readonly IRepository<MacroResult_PrincipalComponent> _principalComponentResultRepository;
        private readonly IRepository<MacroResult_Statistics> _statisticsResultRepository;
        private readonly IRepository<MacroResult_CorMat> _corMatResultRepository;
        private readonly IRepository<MacroResult_IndexData> _indexDataResultRepository;
        private readonly IRepository<MacroResult_PrincipalComponentSummary> _principalSummayrResultRepository;
        private readonly IRepository<MacroeconomicVariable> _macroeconomicVariableRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IInputPdCrDrExporter _inputDataExporter;


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
        IInputPdCrDrExporter inputDataExporter)
        {
            _calibrationRepository = calibrationRepository;
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
            _inputDataExporter = inputDataExporter;
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

            var filteredCalibrationEadBehaviouralTerms = _calibrationRepository.GetAll()
                        .Include(e => e.CloseByUserFk)
                        .WhereIf(userOrganizationUnitIds.Count() > 0, x => userOrganizationUnitIds.Contains(x.OrganizationUnitId))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false)
                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(input.AffiliateIdFilter.HasValue && input.AffiliateIdFilter > -1, e => e.OrganizationUnitId == input.AffiliateIdFilter);

            var pagedAndFilteredCalibrationEadBehaviouralTerms = filteredCalibrationEadBehaviouralTerms
                .OrderBy(input.Sorting ?? "creationTime desc")
                .PageBy(input);

            var calibrationEadBehaviouralTerms = from o in pagedAndFilteredCalibrationEadBehaviouralTerms
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
                                                         Id = o.Id
                                                     },
                                                     ClosedBy = o.CloseByUserFk == null || o.CloseByUserFk.FullName == null ? "" : o.CloseByUserFk.FullName,
                                                     DateCreated = o.CreationTime,
                                                     CreatedBy = s1 == null ? "" : s1.FullName,
                                                     AffiliateName = ou2 == null ? "" : ou2.DisplayName
                                                 };

            var totalCount = await filteredCalibrationEadBehaviouralTerms.CountAsync();

            return new PagedResultDto<GetMacroAnalysisRunForViewDto>(
                totalCount,
                await calibrationEadBehaviouralTerms.ToListAsync()
            );
        }

        public async Task<GetCalibrationRunForEditOutput> GetCalibrationForEdit(EntityDto input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);

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

        public async Task<CalibrationInputSummaryDto<InputMacroAnalysisDataDto>> GetInputSummary(EntityDto input)
        {
            var total = await _calibrationInputRepository.CountAsync(x => x.MacroId == input.Id);
            var filter = _calibrationInputRepository.GetAll().Where(x => x.MacroId == input.Id && x.MacroeconomicId != -1).Take(10);

            var items = from f in filter
                        join mv in _macroeconomicVariableRepository.GetAll() on f.MacroeconomicId equals mv.Id into mv1
                        from mv2 in mv1.DefaultIfEmpty()

                        select new InputMacroAnalysisDataDto
                        {
                            MacroeconomicId = f.MacroeconomicId,
                            MacroeconomicVariableName = f.MacroeconomicId == -1 ? "NPL Percentage Ratio" : (mv2 == null ? "" : mv2.Name),
                            Period = f.Period,
                            Value = f.Value
                        };


            return new CalibrationInputSummaryDto<InputMacroAnalysisDataDto>
            {
                Total = total,
                Items = await items.ToListAsync()
            };
        }

        //public async Task<GetAllResultPdCrDrDto> GetResult(EntityDto<Guid> input)
        //{
        //    var summary = await _calibrationResultRepository.FirstOrDefaultAsync(x => x.CalibrationId == input.Id);
        //    var items = await _pd12MonthsResultRepository.GetAll().Where(x => x.CalibrationId == input.Id)
        //                                                .Select(x => ObjectMapper.Map<ResultPd12MonthsDto>(x))
        //                                                .ToListAsync();

        //    return new GetAllResultPdCrDrDto
        //    {
        //        Pd12Months = items,
        //        Pd12MonthsSummary = ObjectMapper.Map<ResultPd12MonthsSummaryDto>(summary)
        //    };
        //}

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
                int id = await _calibrationRepository.InsertAndGetIdAsync(new MacroAnalysis()
                {
                    OrganizationUnitId = ouId,
                    Status = CalibrationStatusEnum.Draft
                });
                return id;
            }
            else
            {
                int id = await _calibrationRepository.InsertAndGetIdAsync(new MacroAnalysis()
                {
                    OrganizationUnitId = (long)input.AffiliateId,
                    Status = CalibrationStatusEnum.Draft
                });
                return id;
            }
        }

        protected virtual async Task Update(CreateOrEditMacroAnalysisRunDto input)
        {
            var calibrationEadBehaviouralTerm = await _calibrationRepository.FirstOrDefaultAsync(input.Id);
            ObjectMapper.Map(input, calibrationEadBehaviouralTerm);
        }

        public async Task Delete(EntityDto input)
        {
            await _calibrationRepository.DeleteAsync(input.Id);
        }

        public virtual async Task SubmitForApproval(EntityDto input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);
                calibration.Status = CalibrationStatusEnum.Submitted;
                ObjectMapper.Map(calibration, calibration);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditMacroAnalysisApprovalDto input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.MacroId);

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
                }
                else
                {
                    calibration.Status = CalibrationStatusEnum.AwaitngAdditionApproval;
                }
            }
            else
            {
                calibration.Status = CalibrationStatusEnum.Draft;
            }

            ObjectMapper.Map(calibration, calibration);
        }

        public async Task RunCalibration(EntityDto input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);
            if (calibration.Status == CalibrationStatusEnum.Approved)
            {
                calibration.Status = CalibrationStatusEnum.Completed;
                await _calibrationRepository.UpdateAsync(calibration);
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
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);

            if (calibration.Status == CalibrationStatusEnum.Completed || calibration.Status == CalibrationStatusEnum.AppliedToEcl )
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
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);

            if (calibration.Status == CalibrationStatusEnum.Completed)
            {
                //Call apply to ecl job
                var old = await _calibrationRepository.FirstOrDefaultAsync(x => x.Status == CalibrationStatusEnum.AppliedToEcl);
                if (old != null)
                {
                    old.Status = CalibrationStatusEnum.Completed;
                    await _calibrationRepository.UpdateAsync(old);
                }

                calibration.Status = CalibrationStatusEnum.AppliedToEcl;
                await _calibrationRepository.UpdateAsync(calibration);
            }
            else
            {
                throw new UserFriendlyException(L("ApplyCalibrationToEclError"));
            }
        }

        public async Task CloseCalibration(EntityDto input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync(input.Id);

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

            var items = await _calibrationInputRepository.GetAll().Where(x => x.MacroId == input.Id)
                                                         .Select(x => ObjectMapper.Map<InputPdCrDrDto>(x))
                                                         .ToListAsync();

            return _inputDataExporter.ExportToFile(items);
        }

        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(int calibrationId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _calibrationInputRepository.GetAllListAsync(x => x.MacroId == calibrationId);
            if (uploads.Count > 0)
            {
                var calibration = await _calibrationRepository.FirstOrDefaultAsync(calibrationId);
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

    }
}