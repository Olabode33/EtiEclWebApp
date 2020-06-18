using TestDemo.Authorization.Users;
using TestDemo.Retail;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.Retail
{
    public class RetailEclApprovalsAppService : TestDemoAppServiceBase, IRetailEclApprovalsAppService
    {
        private readonly IRepository<RetailEclApproval, Guid> _retailEclApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<RetailEcl, Guid> _lookup_retailEclRepository;


        public RetailEclApprovalsAppService(IRepository<RetailEclApproval, Guid> retailEclApprovalRepository, IRepository<User, long> lookup_userRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository)
        {
            _retailEclApprovalRepository = retailEclApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_retailEclRepository = lookup_retailEclRepository;

        }

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredRetailEclApprovals = _retailEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.RetailEclFk)
                        .Where(e => e.RetailEclId == input.Id);

            var retailEclApprovals = from o in filteredRetailEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = (Guid)o.RetailEclId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var ecl = await _lookup_retailEclRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)ecl.CreatorUserId).FullName;
            string updatedBy = "";
            if (ecl.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)ecl.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await retailEclApprovals.ToListAsync(),
                DateCreated = ecl.CreationTime,
                LastUpdated = ecl.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        public async Task<PagedResultDto<GetRetailEclApprovalForViewDto>> GetAll(GetAllRetailEclApprovalsInput input)
        {
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredRetailEclApprovals = _retailEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.RetailEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ReviewComment.Contains(input.Filter))
                        .WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
                        .WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter), e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredRetailEclApprovals = filteredRetailEclApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var retailEclApprovals = from o in pagedAndFilteredRetailEclApprovals
                                     join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                                     from s1 in j1.DefaultIfEmpty()

                                     join o2 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o2.Id into j2
                                     from s2 in j2.DefaultIfEmpty()

                                     select new GetRetailEclApprovalForViewDto()
                                     {
                                         RetailEclApproval = new RetailEclApprovalDto
                                         {
                                             ReviewedDate = o.ReviewedDate,
                                             ReviewComment = o.ReviewComment,
                                             Status = o.Status,
                                             Id = o.Id
                                         },
                                         UserName = s1 == null ? "" : s1.Name.ToString(),
                                         RetailEclTenantId = s2 == null ? "" : s2.TenantId.ToString()
                                     };

            var totalCount = await filteredRetailEclApprovals.CountAsync();

            return new PagedResultDto<GetRetailEclApprovalForViewDto>(
                totalCount,
                await retailEclApprovals.ToListAsync()
            );
        }

        public async Task<GetRetailEclApprovalForEditOutput> GetRetailEclApprovalForEdit(EntityDto<Guid> input)
        {
            var retailEclApproval = await _retailEclApprovalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRetailEclApprovalForEditOutput { RetailEclApproval = ObjectMapper.Map<CreateOrEditRetailEclApprovalDto>(retailEclApproval) };

            if (output.RetailEclApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEclApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            if (output.RetailEclApproval.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclApproval.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditRetailEclApprovalDto input)
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

        protected virtual async Task Create(CreateOrEditRetailEclApprovalDto input)
        {
            var retailEclApproval = ObjectMapper.Map<RetailEclApproval>(input);


            if (AbpSession.TenantId != null)
            {
                retailEclApproval.TenantId = (int?)AbpSession.TenantId;
            }


            await _retailEclApprovalRepository.InsertAsync(retailEclApproval);
        }

        protected virtual async Task Update(CreateOrEditRetailEclApprovalDto input)
        {
            var retailEclApproval = await _retailEclApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, retailEclApproval);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _retailEclApprovalRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<RetailEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclApprovalUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new RetailEclApprovalUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<RetailEclApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<PagedResultDto<RetailEclApprovalRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_retailEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<RetailEclApprovalRetailEclLookupTableDto>();
            foreach (var retailEcl in retailEclList)
            {
                lookupTableDtoList.Add(new RetailEclApprovalRetailEclLookupTableDto
                {
                    Id = retailEcl.Id.ToString(),
                    DisplayName = retailEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<RetailEclApprovalRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}