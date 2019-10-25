using TestDemo.OBE;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeAssumption
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals)]
    public class ObeEclAssumptionApprovalsAppService : TestDemoAppServiceBase, IObeEclAssumptionApprovalsAppService
    {
		 private readonly IRepository<ObeEclAssumptionApproval, Guid> _obeEclAssumptionApprovalRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public ObeEclAssumptionApprovalsAppService(IRepository<ObeEclAssumptionApproval, Guid> obeEclAssumptionApprovalRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_obeEclAssumptionApprovalRepository = obeEclAssumptionApprovalRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclAssumptionApprovalForViewDto>> GetAll(GetAllObeEclAssumptionApprovalsInput input)
         {
			var assumptionTypeFilter = (AssumptionTypeEnum) input.AssumptionTypeFilter;
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredObeEclAssumptionApprovals = _obeEclAssumptionApprovalRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.Include( e => e.ReviewedByUserFk)
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

			var pagedAndFilteredObeEclAssumptionApprovals = filteredObeEclAssumptionApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclAssumptionApprovals = from o in pagedAndFilteredObeEclAssumptionApprovals
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetObeEclAssumptionApprovalForViewDto() {
							ObeEclAssumptionApproval = new ObeEclAssumptionApprovalDto
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
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredObeEclAssumptionApprovals.CountAsync();

            return new PagedResultDto<GetObeEclAssumptionApprovalForViewDto>(
                totalCount,
                await obeEclAssumptionApprovals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals_Edit)]
		 public async Task<GetObeEclAssumptionApprovalForEditOutput> GetObeEclAssumptionApprovalForEdit(EntityDto<Guid> input)
         {
            var obeEclAssumptionApproval = await _obeEclAssumptionApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclAssumptionApprovalForEditOutput {ObeEclAssumptionApproval = ObjectMapper.Map<CreateOrEditObeEclAssumptionApprovalDto>(obeEclAssumptionApproval)};

		    if (output.ObeEclAssumptionApproval.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclAssumptionApproval.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }

		    if (output.ObeEclAssumptionApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.ObeEclAssumptionApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclAssumptionApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclAssumptionApprovalDto input)
         {
            var obeEclAssumptionApproval = ObjectMapper.Map<ObeEclAssumptionApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclAssumptionApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclAssumptionApprovalRepository.InsertAsync(obeEclAssumptionApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclAssumptionApprovalDto input)
         {
            var obeEclAssumptionApproval = await _obeEclAssumptionApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclAssumptionApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclAssumptionApprovalRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals)]
         public async Task<PagedResultDto<ObeEclAssumptionApprovalObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclAssumptionApprovalObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclAssumptionApprovalObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclAssumptionApprovalObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_ObeEclAssumptionApprovals)]
         public async Task<PagedResultDto<ObeEclAssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclAssumptionApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new ObeEclAssumptionApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<ObeEclAssumptionApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}