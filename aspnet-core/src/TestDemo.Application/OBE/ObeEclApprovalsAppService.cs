using TestDemo.OBE;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.OBE.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.OBE
{
    public class ObeEclApprovalsAppService : TestDemoAppServiceBase, IObeEclApprovalsAppService
    {
        private readonly IRepository<ObeEclApproval, Guid> _obeEclApprovalRepository;
        private readonly IRepository<ObeEcl, Guid> _lookup_obeEclRepository;
        private readonly IRepository<User, long> _lookup_userRepository;


        public ObeEclApprovalsAppService(IRepository<ObeEclApproval, Guid> obeEclApprovalRepository, IRepository<ObeEcl, Guid> lookup_obeEclRepository, IRepository<User, long> lookup_userRepository)
        {
            _obeEclApprovalRepository = obeEclApprovalRepository;
            _lookup_obeEclRepository = lookup_obeEclRepository;
            _lookup_userRepository = lookup_userRepository;

        }

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredObeEclApprovals = _obeEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.ObeEclFk)
                        .Where(e => e.ObeEclId == input.Id);

            var obeEclApprovals = from o in filteredObeEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = (Guid)o.ObeEclId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var ecl = await _lookup_obeEclRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)ecl.CreatorUserId).FullName;
            string updatedBy = "";
            if (ecl.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)ecl.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await obeEclApprovals.ToListAsync(),
                DateCreated = ecl.CreationTime,
                LastUpdated = ecl.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        public async Task<PagedResultDto<GetObeEclApprovalForViewDto>> GetAll(GetAllObeEclApprovalsInput input)
        {
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredObeEclApprovals = _obeEclApprovalRepository.GetAll()
                        .Include(e => e.ObeEclFk)
                        .Include(e => e.ReviewedByUserFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ReviewComment.Contains(input.Filter))
                        .WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
                        .WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter), e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredObeEclApprovals = filteredObeEclApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var obeEclApprovals = from o in pagedAndFilteredObeEclApprovals
                                  join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                                  from s1 in j1.DefaultIfEmpty()

                                  join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                                  from s2 in j2.DefaultIfEmpty()

                                  select new GetObeEclApprovalForViewDto()
                                  {
                                      ObeEclApproval = new ObeEclApprovalDto
                                      {
                                          ReviewedDate = o.ReviewedDate,
                                          ReviewComment = o.ReviewComment,
                                          Status = o.Status,
                                          Id = o.Id
                                      },
                                      ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                                      UserName = s2 == null ? "" : s2.Name.ToString()
                                  };

            var totalCount = await filteredObeEclApprovals.CountAsync();

            return new PagedResultDto<GetObeEclApprovalForViewDto>(
                totalCount,
                await obeEclApprovals.ToListAsync()
            );
        }

        public async Task<GetObeEclApprovalForEditOutput> GetObeEclApprovalForEdit(EntityDto<Guid> input)
        {
            var obeEclApproval = await _obeEclApprovalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetObeEclApprovalForEditOutput { ObeEclApproval = ObjectMapper.Map<CreateOrEditObeEclApprovalDto>(obeEclApproval) };

            if (output.ObeEclApproval.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclApproval.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }

            if (output.ObeEclApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.ObeEclApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditObeEclApprovalDto input)
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

        protected virtual async Task Create(CreateOrEditObeEclApprovalDto input)
        {
            var obeEclApproval = ObjectMapper.Map<ObeEclApproval>(input);


            if (AbpSession.TenantId != null)
            {
                obeEclApproval.TenantId = (int?)AbpSession.TenantId;
            }


            await _obeEclApprovalRepository.InsertAsync(obeEclApproval);
        }

        protected virtual async Task Update(CreateOrEditObeEclApprovalDto input)
        {
            var obeEclApproval = await _obeEclApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, obeEclApproval);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _obeEclApprovalRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<ObeEclApprovalObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_obeEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ObeEclApprovalObeEclLookupTableDto>();
            foreach (var obeEcl in obeEclList)
            {
                lookupTableDtoList.Add(new ObeEclApprovalObeEclLookupTableDto
                {
                    Id = obeEcl.Id.ToString(),
                    DisplayName = obeEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<ObeEclApprovalObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<PagedResultDto<ObeEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ObeEclApprovalUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new ObeEclApprovalUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<ObeEclApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}