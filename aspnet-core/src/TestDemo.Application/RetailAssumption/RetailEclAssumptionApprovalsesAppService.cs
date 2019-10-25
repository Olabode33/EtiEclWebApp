using TestDemo.Authorization.Users;
using TestDemo.Retail;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailAssumption
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses)]
    public class RetailEclAssumptionApprovalsesAppService : TestDemoAppServiceBase, IRetailEclAssumptionApprovalsesAppService
    {
		 private readonly IRepository<RetailEclAssumptionApproval, Guid> _retailEclAssumptionApprovalsRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclAssumptionApprovalsesAppService(IRepository<RetailEclAssumptionApproval, Guid> retailEclAssumptionApprovalsRepository , IRepository<User, long> lookup_userRepository, IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclAssumptionApprovalsRepository = retailEclAssumptionApprovalsRepository;
			_lookup_userRepository = lookup_userRepository;
		_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclAssumptionApprovalsForViewDto>> GetAll(GetAllRetailEclAssumptionApprovalsesInput input)
         {
			var assumptionTypeFilter = (AssumptionTypeEnum) input.AssumptionTypeFilter;
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredRetailEclAssumptionApprovalses = _retailEclAssumptionApprovalsRepository.GetAll()
						.Include( e => e.ReviewedByUserFk)
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.OldValue.Contains(input.Filter) || e.NewValue.Contains(input.Filter) || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.AssumptionTypeFilter > -1, e => e.AssumptionType == assumptionTypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.OldValueFilter),  e => e.OldValue.ToLower() == input.OldValueFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NewValueFilter),  e => e.NewValue.ToLower() == input.NewValueFilter.ToLower().Trim())
						.WhereIf(input.MinDateReviewedFilter != null, e => e.DateReviewed >= input.MinDateReviewedFilter)
						.WhereIf(input.MaxDateReviewedFilter != null, e => e.DateReviewed <= input.MaxDateReviewedFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclAssumptionApprovalses = filteredRetailEclAssumptionApprovalses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclAssumptionApprovalses = from o in pagedAndFilteredRetailEclAssumptionApprovalses
                         join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetRetailEclAssumptionApprovalsForViewDto() {
							RetailEclAssumptionApprovals = new RetailEclAssumptionApprovalsDto
							{
                                AssumptionType = o.AssumptionType,
                                OldValue = o.OldValue,
                                NewValue = o.NewValue,
                                DateReviewed = o.DateReviewed,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	RetailEclTenantId = s2 == null ? "" : s2.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclAssumptionApprovalses.CountAsync();

            return new PagedResultDto<GetRetailEclAssumptionApprovalsForViewDto>(
                totalCount,
                await retailEclAssumptionApprovalses.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses_Edit)]
		 public async Task<GetRetailEclAssumptionApprovalsForEditOutput> GetRetailEclAssumptionApprovalsForEdit(EntityDto<Guid> input)
         {
            var retailEclAssumptionApprovals = await _retailEclAssumptionApprovalsRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclAssumptionApprovalsForEditOutput {RetailEclAssumptionApprovals = ObjectMapper.Map<CreateOrEditRetailEclAssumptionApprovalsDto>(retailEclAssumptionApprovals)};

		    if (output.RetailEclAssumptionApprovals.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEclAssumptionApprovals.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.RetailEclAssumptionApprovals.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclAssumptionApprovals.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclAssumptionApprovalsDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclAssumptionApprovalsDto input)
         {
            var retailEclAssumptionApprovals = ObjectMapper.Map<RetailEclAssumptionApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclAssumptionApprovals.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclAssumptionApprovalsRepository.InsertAsync(retailEclAssumptionApprovals);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclAssumptionApprovalsDto input)
         {
            var retailEclAssumptionApprovals = await _retailEclAssumptionApprovalsRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclAssumptionApprovals);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclAssumptionApprovalsRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses)]
         public async Task<PagedResultDto<RetailEclAssumptionApprovalsUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclAssumptionApprovalsUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new RetailEclAssumptionApprovalsUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<RetailEclAssumptionApprovalsUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_RetailEclAssumptionApprovalses)]
         public async Task<PagedResultDto<RetailEclAssumptionApprovalsRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclAssumptionApprovalsRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclAssumptionApprovalsRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclAssumptionApprovalsRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}