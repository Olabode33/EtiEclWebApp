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

namespace TestDemo.Calibration
{
    [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms)]
    public class CalibrationLgdRecoveryRateAppService : TestDemoAppServiceBase, ICalibrationsAppService
    {
        private readonly IRepository<CalibrationLgdRecoveryRate, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationLgdRecoveryRateApproval, Guid> _calibrationApprovalRepository;
        private readonly IRepository<CalibrationInputLgdRecoveryRate> _calibrationInputRepository;
        private readonly IRepository<CalibrationResultLgdRecoveryRate> _calibrationResultRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public CalibrationLgdRecoveryRateAppService(
            IRepository<CalibrationLgdRecoveryRate, Guid> calibrationRepository, 
            IRepository<User, long> lookup_userRepository,
            IRepository<CalibrationLgdRecoveryRateApproval, Guid> calibrationApprovalRepository,
            IRepository<CalibrationInputLgdRecoveryRate> calibrationInputRepository,
            IRepository<CalibrationResultLgdRecoveryRate> calibrationResultRepository)
        {
            _calibrationRepository = calibrationRepository;
            _lookup_userRepository = lookup_userRepository;
            _calibrationApprovalRepository = calibrationApprovalRepository;
            _calibrationInputRepository = calibrationInputRepository;
            _calibrationResultRepository = calibrationResultRepository;
        }

        public async Task<PagedResultDto<GetCalibrationRunForViewDto>> GetAll(GetAllCalibrationRunInput input)
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
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var calibrationEadBehaviouralTerms = from o in pagedAndFilteredCalibrationEadBehaviouralTerms
                                                 join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                                 from s1 in j1.DefaultIfEmpty()

                                                 select new GetCalibrationRunForViewDto()
                                                 {
                                                     Calibration = new CalibrationRunDto
                                                     {
                                                         ClosedDate = o.ClosedDate,
                                                         Status = o.Status,
                                                         Id = o.Id
                                                     },
                                                     ClosedBy = o.CloseByUserFk == null || o.CloseByUserFk.FullName == null ? "" : o.CloseByUserFk.FullName,
                                                     DateCreated = o.CreationTime,
                                                     CreatedBy = s1 == null ? "" : s1.FullName
                                                 };

            var totalCount = await filteredCalibrationEadBehaviouralTerms.CountAsync();

            return new PagedResultDto<GetCalibrationRunForViewDto>(
                totalCount,
                await calibrationEadBehaviouralTerms.ToListAsync()
            );
        }

        [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms_Edit)]
        public async Task<GetCalibrationRunForEditOutput> GetCalibrationForEdit(EntityDto<Guid> input)
        {
            var calibrationEadBehaviouralTerm = await _calibrationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCalibrationRunForEditOutput { Calibration = ObjectMapper.Map<CreateOrEditCalibrationRunDto>(calibrationEadBehaviouralTerm) };

            if (output.Calibration.CloseByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.Calibration.CloseByUserId);
                output.ClosedByUserName = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        public async Task<Guid> CreateOrEdit(CreateOrEditCalibrationRunDto input)
        {
            if (input.Id == null)
            {
                return await Create(input);
            }
            else
            {
                await Update(input);
                return (Guid)input.Id;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms_Create)]
        protected virtual async Task<Guid> Create(CreateOrEditCalibrationRunDto input)
        {
            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                long ouId = userSubsidiaries[0].Id;
                Guid id = await _calibrationRepository.InsertAndGetIdAsync(new CalibrationLgdRecoveryRate()
                {
                    OrganizationUnitId = ouId,
                    Status = CalibrationStatusEnum.Draft
                });
                return id;
            }
            else
            {
                throw new UserFriendlyException(L("UserDoesNotBelongToAnyAffiliateError"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms_Edit)]
        protected virtual async Task Update(CreateOrEditCalibrationRunDto input)
        {
            var calibrationEadBehaviouralTerm = await _calibrationRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, calibrationEadBehaviouralTerm);
        }

        [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _calibrationRepository.DeleteAsync(input.Id);
        }
        [AbpAuthorize(AppPermissions.Pages_CalibrationEadBehaviouralTerms)]
        public async Task<List<CalibrationEadBehaviouralTermUserLookupTableDto>> GetAllUserForTableDropdown()
        {
            return await _lookup_userRepository.GetAll()
                .Select(user => new CalibrationEadBehaviouralTermUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user == null || user.Name == null ? "" : user.Name.ToString()
                }).ToListAsync();
        }

        public virtual async Task SubmitForApproval(EntityDto<Guid> input)
        {
            var validation = await ValidateForSubmission(input.Id);
            if (validation.Status)
            {
                var calibration = await _calibrationRepository.FirstOrDefaultAsync((Guid)input.Id);
                calibration.Status = CalibrationStatusEnum.Submitted;
                ObjectMapper.Map(calibration, calibration);
            }
            else
            {
                throw new UserFriendlyException(L("ValidationError") + validation.Message);
            }
        }

        public virtual async Task ApproveReject(CreateOrEditEclApprovalDto input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync((Guid)input.EclId);

            await _calibrationApprovalRepository.InsertAsync(new CalibrationLgdRecoveryRateApproval
            {
                CalibrationId = input.EclId,
                ReviewComment = input.ReviewComment,
                ReviewedByUserId = AbpSession.UserId,
                ReviewedDate = DateTime.Now,
                Status = input.Status
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            if (input.Status == GeneralStatusEnum.Approved)
            {
                var requiredApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals);
                var eclApprovals = await _calibrationApprovalRepository.GetAllListAsync(x => x.CalibrationId == input.EclId && x.Status == GeneralStatusEnum.Approved);
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

        public async Task RunCalibration(EntityDto<Guid> input)
        {
            var calibration = await _calibrationRepository.FirstOrDefaultAsync((Guid)input.Id);
            if (calibration.Status == CalibrationStatusEnum.Approved)
            {
                calibration.Status = CalibrationStatusEnum.Processing;
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

        public async Task GenerateReport(EntityDto<Guid> input)
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

        public async Task ApplyToEcl(EntityDto<Guid> input)
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

        public async Task CloseCalibration(EntityDto<Guid> input)
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



        protected virtual async Task<ValidationMessageDto> ValidateForSubmission(Guid calibrationId)
        {
            ValidationMessageDto output = new ValidationMessageDto();

            var uploads = await _calibrationInputRepository.GetAllListAsync(x => x.CalibrationId == calibrationId);
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