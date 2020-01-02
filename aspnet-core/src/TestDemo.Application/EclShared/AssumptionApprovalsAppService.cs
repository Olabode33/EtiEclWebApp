using TestDemo.Authorization.Users;

using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.Organizations;

namespace TestDemo.EclShared
{
    [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals)]
    public class AssumptionApprovalsAppService : TestDemoAppServiceBase, IAssumptionApprovalsAppService
    {
        private readonly IRepository<AssumptionApproval, Guid> _assumptionApprovalRepository;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<Assumption, Guid> _assumptionsRepository;
        private readonly IRepository<EadInputAssumption, Guid> _eadInputAssumptionsRepository;
        private readonly IRepository<LgdInputAssumption, Guid> _lgdInputAssumptionsRepository;


        public AssumptionApprovalsAppService(
            IRepository<AssumptionApproval, Guid> assumptionApprovalRepository,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<Assumption, Guid> assumptionsRepository,
            IRepository<EadInputAssumption, Guid> eadInputAssumptionsRepository,
            IRepository<LgdInputAssumption, Guid> lgdInputAssumptionsRepository,
            IRepository<User, long> lookup_userRepository
            )
        {
            _assumptionApprovalRepository = assumptionApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _assumptionsRepository = assumptionsRepository;
            _eadInputAssumptionsRepository = eadInputAssumptionsRepository;
            _lgdInputAssumptionsRepository = lgdInputAssumptionsRepository;
        }

        public async Task<PagedResultDto<GetAssumptionApprovalForViewDto>> GetAll(GetAllAssumptionApprovalsInput input)
        {
            var frameworkFilter = (FrameworkEnum)input.FrameworkFilter;
            var assumptionTypeFilter = (AssumptionTypeEnum)input.AssumptionTypeFilter;

            var filteredAssumptionApprovals = _assumptionApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AssumptionGroup.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.OldValue.Contains(input.Filter) || e.NewValue.Contains(input.Filter) || e.ReviewComment.Contains(input.Filter))
                        .WhereIf(input.OrganizationUnitIdFilter != null, e => e.OrganizationUnitId == input.OrganizationUnitIdFilter)
                        .WhereIf(input.FrameworkFilter > -1, e => e.Framework == frameworkFilter)
                        .WhereIf(input.AssumptionTypeFilter > -1, e => e.AssumptionType == assumptionTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AssumptionGroupFilter), e => e.AssumptionGroup == input.AssumptionGroupFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name == input.UserNameFilter);

            var pagedAndFilteredAssumptionApprovals = filteredAssumptionApprovals
                .OrderBy(input.Sorting ?? "status, creationTime desc")
                .PageBy(input);

            var assumptionApprovals = from o in pagedAndFilteredAssumptionApprovals
                                      join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                                      from s1 in j1.DefaultIfEmpty()
                                      join ou in _organizationUnitRepository.GetAll() on o.OrganizationUnitId equals ou.Id into ou1
                                      from ou2 in ou1.DefaultIfEmpty()

                                      select new GetAssumptionApprovalForViewDto()
                                      {
                                          AssumptionApproval = new AssumptionApprovalDto
                                          {
                                              OrganizationUnitId = o.OrganizationUnitId,
                                              Framework = o.Framework,
                                              AssumptionType = o.AssumptionType,
                                              AssumptionGroup = o.AssumptionGroup,
                                              InputName = o.InputName,
                                              OldValue = o.OldValue,
                                              NewValue = o.NewValue,
                                              DateReviewed = o.DateReviewed,
                                              Status = o.Status,
                                              Id = o.Id,
                                              AssumptionId = o.AssumptionId,
                                              AssumptionEntity = o.AssumptionEntity,
                                              ReviewComment = o.ReviewComment
                                          },
                                          UserName = s1 == null ? "" : s1.FullName.ToString(),
                                          OrganizationUnitName = ou2 == null ? "" : ou2.DisplayName
                                      };

            var totalCount = await filteredAssumptionApprovals.CountAsync();

            return new PagedResultDto<GetAssumptionApprovalForViewDto>(
                totalCount,
                await assumptionApprovals.ToListAsync()
            );
        }

        public async Task<GetAssumptionApprovalForViewDto> GetAssumptionApprovalForView(Guid id)
        {
            var assumptionApproval = await _assumptionApprovalRepository.GetAsync(id);

            var output = new GetAssumptionApprovalForViewDto { AssumptionApproval = ObjectMapper.Map<AssumptionApprovalDto>(assumptionApproval) };

            if (output.AssumptionApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.AssumptionApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals_Edit)]
        public async Task<GetAssumptionApprovalForEditOutput> GetAssumptionApprovalForEdit(EntityDto<Guid> input)
        {
            var assumptionApproval = await _assumptionApprovalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAssumptionApprovalForEditOutput { AssumptionApproval = ObjectMapper.Map<CreateOrEditAssumptionApprovalDto>(assumptionApproval) };

            if (output.AssumptionApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.AssumptionApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAssumptionApprovalDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals_Create)]
        protected virtual async Task Create(CreateOrEditAssumptionApprovalDto input)
        {
            var assumptionApproval = ObjectMapper.Map<AssumptionApproval>(input);



            await _assumptionApprovalRepository.InsertAsync(assumptionApproval);
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals_Edit)]
        protected virtual async Task Update(CreateOrEditAssumptionApprovalDto input)
        {
            var assumptionApproval = await _assumptionApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, assumptionApproval);
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _assumptionApprovalRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_AssumptionApprovals)]
        public async Task<PagedResultDto<AssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<AssumptionApprovalUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new AssumptionApprovalUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<AssumptionApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task ApproveReject(AssumptionApprovalDto input)
        {
            var assumption = await _assumptionApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            assumption.Status = input.Status;
            assumption.ReviewComment = input.ReviewComment;
            assumption.ReviewedByUserId = AbpSession.UserId;
            assumption.DateReviewed = DateTime.Now;

            //await UpdateAssumptionEntity(input);

            ObjectMapper.Map(assumption, assumption);
        }

        private async Task UpdateAssumptionEntity(AssumptionApprovalDto input)
        {
            UpdateAssumptionStatusDto statusDto = new UpdateAssumptionStatusDto() { Id = input.Id, Status = input.Status };

            //switch (input.AssumptionEntity)
            //{
            //    case EclEnums.Assumption:
            //        await _assumptionsAppService.UpdateStatus(statusDto);
            //        break;
            //    case EclEnums.EadInputAssumption:
            //        await _eadInputAssumptionsAppService.UpdateStatus(statusDto);
            //        break;
            //    case EclEnums.LgdInputAssumption:
            //        await _lgdInputAssumptionsAppService.UpdateStatus(statusDto);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}