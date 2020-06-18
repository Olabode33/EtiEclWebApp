using TestDemo.Authorization.Users;
using TestDemo.WholesaleComputation;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleComputation
{
    public class WholesaleEclSicrApprovalsAppService : TestDemoAppServiceBase, IWholesaleEclSicrApprovalsAppService
    {
		 private readonly IRepository<WholesaleEclSicrApproval, Guid> _wholesaleEclSicrApprovalRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<WholesaleEclSicr,Guid> _lookup_wholesaleEclSicrRepository;
		 

		  public WholesaleEclSicrApprovalsAppService(IRepository<WholesaleEclSicrApproval, Guid> wholesaleEclSicrApprovalRepository , IRepository<User, long> lookup_userRepository, IRepository<WholesaleEclSicr, Guid> lookup_wholesaleEclSicrRepository) 
		  {
			_wholesaleEclSicrApprovalRepository = wholesaleEclSicrApprovalRepository;
			_lookup_userRepository = lookup_userRepository;
		_lookup_wholesaleEclSicrRepository = lookup_wholesaleEclSicrRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclSicrApprovalForViewDto>> GetAll(GetAllWholesaleEclSicrApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredWholesaleEclSicrApprovals = _wholesaleEclSicrApprovalRepository.GetAll()
						.Include( e => e.ReviewedByUserFk)
						.Include( e => e.WholesaleEclSicrFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclSicrOverrideCommentFilter), e => e.WholesaleEclSicrFk != null && e.WholesaleEclSicrFk.OverrideComment.ToLower() == input.WholesaleEclSicrOverrideCommentFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclSicrApprovals = filteredWholesaleEclSicrApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclSicrApprovals = from o in pagedAndFilteredWholesaleEclSicrApprovals
                         join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_wholesaleEclSicrRepository.GetAll() on o.WholesaleEclSicrId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetWholesaleEclSicrApprovalForViewDto() {
							WholesaleEclSicrApproval = new WholesaleEclSicrApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	WholesaleEclSicrOverrideComment = s2 == null ? "" : s2.OverrideComment.ToString()
						};

            var totalCount = await filteredWholesaleEclSicrApprovals.CountAsync();

            return new PagedResultDto<GetWholesaleEclSicrApprovalForViewDto>(
                totalCount,
                await wholesaleEclSicrApprovals.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclSicrApprovalForEditOutput> GetWholesaleEclSicrApprovalForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclSicrApproval = await _wholesaleEclSicrApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclSicrApprovalForEditOutput {WholesaleEclSicrApproval = ObjectMapper.Map<CreateOrEditWholesaleEclSicrApprovalDto>(wholesaleEclSicrApproval)};

		    if (output.WholesaleEclSicrApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEclSicrApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.WholesaleEclSicrApproval.WholesaleEclSicrId != null)
            {
                var _lookupWholesaleEclSicr = await _lookup_wholesaleEclSicrRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclSicrApproval.WholesaleEclSicrId);
                output.WholesaleEclSicrOverrideComment = _lookupWholesaleEclSicr.OverrideComment.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclSicrApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclSicrApprovalDto input)
         {
            var wholesaleEclSicrApproval = ObjectMapper.Map<WholesaleEclSicrApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclSicrApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclSicrApprovalRepository.InsertAsync(wholesaleEclSicrApproval);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclSicrApprovalDto input)
         {
            var wholesaleEclSicrApproval = await _wholesaleEclSicrApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclSicrApproval);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclSicrApprovalRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclSicrApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new WholesaleEclSicrApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclSicrApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<WholesaleEclSicrApprovalWholesaleEclSicrLookupTableDto>> GetAllWholesaleEclSicrForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclSicrRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.OverrideComment.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclSicrList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclSicrApprovalWholesaleEclSicrLookupTableDto>();
			foreach(var wholesaleEclSicr in wholesaleEclSicrList){
				lookupTableDtoList.Add(new WholesaleEclSicrApprovalWholesaleEclSicrLookupTableDto
				{
					Id = wholesaleEclSicr.Id.ToString(),
					DisplayName = wholesaleEclSicr.OverrideComment?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclSicrApprovalWholesaleEclSicrLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}