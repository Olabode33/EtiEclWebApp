using TestDemo.RetailComputation;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailComputation
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals)]
    public class RetailEclSicrApprovalsAppService : TestDemoAppServiceBase, IRetailEclSicrApprovalsAppService
    {
		 private readonly IRepository<RetailEclSicrApproval, Guid> _retailEclSicrApprovalRepository;
		 private readonly IRepository<RetailEclSicr,Guid> _lookup_retailEclSicrRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public RetailEclSicrApprovalsAppService(IRepository<RetailEclSicrApproval, Guid> retailEclSicrApprovalRepository , IRepository<RetailEclSicr, Guid> lookup_retailEclSicrRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_retailEclSicrApprovalRepository = retailEclSicrApprovalRepository;
			_lookup_retailEclSicrRepository = lookup_retailEclSicrRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclSicrApprovalForViewDto>> GetAll(GetAllRetailEclSicrApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredRetailEclSicrApprovals = _retailEclSicrApprovalRepository.GetAll()
						.Include( e => e.RetailEclSicrFk)
						.Include( e => e.ReviewedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclSicrApprovals = filteredRetailEclSicrApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclSicrApprovals = from o in pagedAndFilteredRetailEclSicrApprovals
                         join o1 in _lookup_retailEclSicrRepository.GetAll() on o.RetailEclSicrId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetRetailEclSicrApprovalForViewDto() {
							RetailEclSicrApproval = new RetailEclSicrApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	RetailEclSicrTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredRetailEclSicrApprovals.CountAsync();

            return new PagedResultDto<GetRetailEclSicrApprovalForViewDto>(
                totalCount,
                await retailEclSicrApprovals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals_Edit)]
		 public async Task<GetRetailEclSicrApprovalForEditOutput> GetRetailEclSicrApprovalForEdit(EntityDto<Guid> input)
         {
            var retailEclSicrApproval = await _retailEclSicrApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclSicrApprovalForEditOutput {RetailEclSicrApproval = ObjectMapper.Map<CreateOrEditRetailEclSicrApprovalDto>(retailEclSicrApproval)};

		    if (output.RetailEclSicrApproval.RetailEclSicrId != null)
            {
                var _lookupRetailEclSicr = await _lookup_retailEclSicrRepository.FirstOrDefaultAsync((Guid)output.RetailEclSicrApproval.RetailEclSicrId);
                output.RetailEclSicrTenantId = _lookupRetailEclSicr.TenantId.ToString();
            }

		    if (output.RetailEclSicrApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEclSicrApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclSicrApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclSicrApprovalDto input)
         {
            var retailEclSicrApproval = ObjectMapper.Map<RetailEclSicrApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclSicrApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclSicrApprovalRepository.InsertAsync(retailEclSicrApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclSicrApprovalDto input)
         {
            var retailEclSicrApproval = await _retailEclSicrApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclSicrApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclSicrApprovalRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals)]
         public async Task<PagedResultDto<RetailEclSicrApprovalRetailEclSicrLookupTableDto>> GetAllRetailEclSicrForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclSicrRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclSicrList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclSicrApprovalRetailEclSicrLookupTableDto>();
			foreach(var retailEclSicr in retailEclSicrList){
				lookupTableDtoList.Add(new RetailEclSicrApprovalRetailEclSicrLookupTableDto
				{
					Id = retailEclSicr.Id.ToString(),
					DisplayName = retailEclSicr.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclSicrApprovalRetailEclSicrLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_RetailEclSicrApprovals)]
         public async Task<PagedResultDto<RetailEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclSicrApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new RetailEclSicrApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<RetailEclSicrApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}