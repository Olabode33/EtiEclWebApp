using TestDemo.Authorization.Users;
using TestDemo.Wholesale;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Wholesale.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.Wholesale
{
    public class WholesaleEclApprovalsAppService : TestDemoAppServiceBase, IWholesaleEclApprovalsAppService
    {
        private readonly IRepository<WholesaleEclApproval, Guid> _wholesaleEclApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<WholesaleEcl, Guid> _lookup_wholesaleEclRepository;


        public WholesaleEclApprovalsAppService(IRepository<WholesaleEclApproval, Guid> wholesaleEclApprovalRepository, IRepository<User, long> lookup_userRepository, IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository)
        {
            _wholesaleEclApprovalRepository = wholesaleEclApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;

        }

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredWholesaleEclApprovals = _wholesaleEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.WholesaleEclFk)
                        .Where(e => e.WholesaleEclId == input.Id);

            var wholesaleEclApprovals = from o in filteredWholesaleEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = o.WholesaleEclId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var ecl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = "";
            if (ecl.CreatorUserId != null)
            {
                createdBy = _lookup_userRepository.FirstOrDefault((long)ecl.CreatorUserId).FullName;
            }
            string updatedBy = "";
            if (ecl.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)ecl.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await wholesaleEclApprovals.ToListAsync(),
                DateCreated = ecl.CreationTime,
                LastUpdated = ecl.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        public async Task<PagedResultDto<GetWholesaleEclApprovalForViewDto>> GetAll(GetAllWholesaleEclApprovalsInput input)
        {
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredWholesaleEclApprovals = _wholesaleEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.WholesaleEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ReviewComment.Contains(input.Filter))
                        .WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
                        .WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

            var pagedAndFilteredWholesaleEclApprovals = filteredWholesaleEclApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var wholesaleEclApprovals = from o in pagedAndFilteredWholesaleEclApprovals
                                        join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                                        from s1 in j1.DefaultIfEmpty()

                                        join o2 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o2.Id into j2
                                        from s2 in j2.DefaultIfEmpty()

                                        select new GetWholesaleEclApprovalForViewDto()
                                        {
                                            WholesaleEclApproval = new WholesaleEclApprovalDto
                                            {
                                                ReviewedDate = o.ReviewedDate,
                                                Status = o.Status,
                                                Id = o.Id
                                            },
                                            UserName = s1 == null ? "" : s1.Name.ToString(),
                                            WholesaleEclTenantId = s2 == null ? "" : s2.TenantId.ToString()
                                        };

            var totalCount = await filteredWholesaleEclApprovals.CountAsync();

            return new PagedResultDto<GetWholesaleEclApprovalForViewDto>(
                totalCount,
                await wholesaleEclApprovals.ToListAsync()
            );
        }

        public async Task<GetWholesaleEclApprovalForEditOutput> GetWholesaleEclApprovalForEdit(EntityDto<Guid> input)
        {
            var wholesaleEclApproval = await _wholesaleEclApprovalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetWholesaleEclApprovalForEditOutput { WholesaleEclApproval = ObjectMapper.Map<CreateOrEditWholesaleEclApprovalDto>(wholesaleEclApproval) };

            if (output.WholesaleEclApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEclApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            if (output.WholesaleEclApproval.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclApproval.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditWholesaleEclApprovalDto input)
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

        protected virtual async Task Create(CreateOrEditWholesaleEclApprovalDto input)
        {
            var wholesaleEclApproval = ObjectMapper.Map<WholesaleEclApproval>(input);


            if (AbpSession.TenantId != null)
            {
                wholesaleEclApproval.TenantId = (int?)AbpSession.TenantId;
            }


            await _wholesaleEclApprovalRepository.InsertAsync(wholesaleEclApproval);
        }

        protected virtual async Task Update(CreateOrEditWholesaleEclApprovalDto input)
        {
            var wholesaleEclApproval = await _wholesaleEclApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, wholesaleEclApproval);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _wholesaleEclApprovalRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<WholesaleEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<WholesaleEclApprovalUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new WholesaleEclApprovalUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<WholesaleEclApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<PagedResultDto<WholesaleEclApprovalWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.TenantId.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<WholesaleEclApprovalWholesaleEclLookupTableDto>();
            foreach (var wholesaleEcl in wholesaleEclList)
            {
                lookupTableDtoList.Add(new WholesaleEclApprovalWholesaleEclLookupTableDto
                {
                    Id = wholesaleEcl.Id.ToString(),
                    DisplayName = wholesaleEcl.TenantId?.ToString()
                });
            }

            return new PagedResultDto<WholesaleEclApprovalWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}