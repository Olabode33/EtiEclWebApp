using TestDemo.Authorization.Users;
using TestDemo.Investment;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Investment.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.Investment
{
    public class InvestmentEclApprovalsAppService : TestDemoAppServiceBase, IInvestmentEclApprovalsAppService
    {
        private readonly IRepository<InvestmentEclApproval, Guid> _investmentEclApprovalRepository;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<InvestmentEcl, Guid> _lookup_investmentEclRepository;


        public InvestmentEclApprovalsAppService(IRepository<InvestmentEclApproval, Guid> investmentEclApprovalRepository, IRepository<User, long> lookup_userRepository, IRepository<InvestmentEcl, Guid> lookup_investmentEclRepository)
        {
            _investmentEclApprovalRepository = investmentEclApprovalRepository;
            _lookup_userRepository = lookup_userRepository;
            _lookup_investmentEclRepository = lookup_investmentEclRepository;

        }

        public async Task<PagedResultDto<GetInvestmentEclApprovalForViewDto>> GetAll(GetAllInvestmentEclApprovalsInput input)
        {
            var statusFilter = (GeneralStatusEnum)input.StatusFilter;

            var filteredInvestmentEclApprovals = _investmentEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.InvestmentEclFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.ReviewComment.Contains(input.Filter))
                        .WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name == input.UserNameFilter);

            var pagedAndFilteredInvestmentEclApprovals = filteredInvestmentEclApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var investmentEclApprovals = from o in pagedAndFilteredInvestmentEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         select new GetInvestmentEclApprovalForViewDto()
                                         {
                                             InvestmentEclApproval = new InvestmentEclApprovalDto
                                             {
                                                 ReviewedDate = o.ReviewedDate,
                                                 Status = o.Status,
                                                 Id = o.Id
                                             },
                                             UserName = s1 == null ? "" : s1.Name.ToString(),
                                             InvestmentEclReportingDate = s2 == null ? "" : s2.ReportingDate.ToString()
                                         };

            var totalCount = await filteredInvestmentEclApprovals.CountAsync();

            return new PagedResultDto<GetInvestmentEclApprovalForViewDto>(
                totalCount,
                await investmentEclApprovals.ToListAsync()
            );
        }

        public async Task<EclAuditInfoDto> GetEclAudit(EntityDto<Guid> input)
        {

            var filteredInvestmentEclApprovals = _investmentEclApprovalRepository.GetAll()
                        .Include(e => e.ReviewedByUserFk)
                        .Include(e => e.InvestmentEclFk)
                        .Where(e => e.InvestmentEclId == input.Id);

            var investmentEclApprovals = from o in filteredInvestmentEclApprovals
                                         join o1 in _lookup_userRepository.GetAll() on o.CreatorUserId equals o1.Id into j1
                                         from s1 in j1.DefaultIfEmpty()

                                         join o2 in _lookup_investmentEclRepository.GetAll() on o.InvestmentEclId equals o2.Id into j2
                                         from s2 in j2.DefaultIfEmpty()

                                         select new EclApprovalAuditInfoDto()
                                         {
                                             EclId = o.InvestmentEclId,
                                             ReviewedDate = o.CreationTime,
                                             Status = o.Status,
                                             ReviewComment = o.ReviewComment,
                                             ReviewedBy = s1 == null ? "" : s1.FullName.ToString()
                                         };

            var ecl = await _lookup_investmentEclRepository.FirstOrDefaultAsync(input.Id);
            string createdBy = _lookup_userRepository.FirstOrDefault((long)ecl.CreatorUserId).FullName;
            string updatedBy = "";
            if (ecl.LastModifierUserId != null)
            {
                updatedBy = _lookup_userRepository.FirstOrDefault((long)ecl.LastModifierUserId).FullName;
            }

            return new EclAuditInfoDto()
            {
                Approvals = await investmentEclApprovals.ToListAsync(),
                DateCreated = ecl.CreationTime,
                LastUpdated = ecl.LastModificationTime,
                CreatedBy = createdBy,
                UpdatedBy = updatedBy
            };
        }

        public async Task<GetInvestmentEclApprovalForEditOutput> GetInvestmentEclApprovalForEdit(EntityDto<Guid> input)
        {
            var investmentEclApproval = await _investmentEclApprovalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetInvestmentEclApprovalForEditOutput { InvestmentEclApproval = ObjectMapper.Map<CreateOrEditInvestmentEclApprovalDto>(investmentEclApproval) };

            if (output.InvestmentEclApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.InvestmentEclApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

            if (output.InvestmentEclApproval.InvestmentEclId != null)
            {
                var _lookupInvestmentEcl = await _lookup_investmentEclRepository.FirstOrDefaultAsync((Guid)output.InvestmentEclApproval.InvestmentEclId);
                output.InvestmentEclReportingDate = _lookupInvestmentEcl.ReportingDate.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditInvestmentEclApprovalDto input)
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

        protected virtual async Task Create(CreateOrEditInvestmentEclApprovalDto input)
        {
            var investmentEclApproval = ObjectMapper.Map<InvestmentEclApproval>(input);



            await _investmentEclApprovalRepository.InsertAsync(investmentEclApproval);
        }

        protected virtual async Task Update(CreateOrEditInvestmentEclApprovalDto input)
        {
            var investmentEclApproval = await _investmentEclApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, investmentEclApproval);
        }

        public async Task Delete(EntityDto<Guid> input)
        {
            await _investmentEclApprovalRepository.DeleteAsync(input.Id);
        }

        public async Task<PagedResultDto<InvestmentEclApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentEclApprovalUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new InvestmentEclApprovalUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<InvestmentEclApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        public async Task<PagedResultDto<InvestmentEclApprovalInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_investmentEclRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.ReportingDate.ToString().Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var investmentEclList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<InvestmentEclApprovalInvestmentEclLookupTableDto>();
            foreach (var investmentEcl in investmentEclList)
            {
                lookupTableDtoList.Add(new InvestmentEclApprovalInvestmentEclLookupTableDto
                {
                    Id = investmentEcl.Id.ToString(),
                    DisplayName = ""
                });
            }

            return new PagedResultDto<InvestmentEclApprovalInvestmentEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }
    }
}