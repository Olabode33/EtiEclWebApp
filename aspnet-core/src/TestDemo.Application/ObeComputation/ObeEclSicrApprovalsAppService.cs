using TestDemo.Authorization.Users;
using TestDemo.ObeComputation;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeComputation
{
    public class ObeEclSicrApprovalsAppService : TestDemoAppServiceBase, IObeEclSicrApprovalsAppService
    {
		 private readonly IRepository<ObeEclSicrApproval, Guid> _obeEclSicrApprovalRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 private readonly IRepository<ObeEclSicr,Guid> _lookup_obeEclSicrRepository;
		 

		  public ObeEclSicrApprovalsAppService(IRepository<ObeEclSicrApproval, Guid> obeEclSicrApprovalRepository , IRepository<User, long> lookup_userRepository, IRepository<ObeEclSicr, Guid> lookup_obeEclSicrRepository) 
		  {
			_obeEclSicrApprovalRepository = obeEclSicrApprovalRepository;
			_lookup_userRepository = lookup_userRepository;
		_lookup_obeEclSicrRepository = lookup_obeEclSicrRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclSicrApprovalForViewDto>> GetAll(GetAllObeEclSicrApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredObeEclSicrApprovals = _obeEclSicrApprovalRepository.GetAll()
						.Include( e => e.ReviewedByUserFk)
						.Include( e => e.ObeEclSicrFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEclSicrApprovals = filteredObeEclSicrApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclSicrApprovals = from o in pagedAndFilteredObeEclSicrApprovals
                         join o1 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_obeEclSicrRepository.GetAll() on o.ObeEclSicrId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetObeEclSicrApprovalForViewDto() {
							ObeEclSicrApproval = new ObeEclSicrApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString(),
                         	ObeEclSicrTenantId = s2 == null ? "" : s2.TenantId.ToString()
						};

            var totalCount = await filteredObeEclSicrApprovals.CountAsync();

            return new PagedResultDto<GetObeEclSicrApprovalForViewDto>(
                totalCount,
                await obeEclSicrApprovals.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclSicrApprovalForEditOutput> GetObeEclSicrApprovalForEdit(EntityDto<Guid> input)
         {
            var obeEclSicrApproval = await _obeEclSicrApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclSicrApprovalForEditOutput {ObeEclSicrApproval = ObjectMapper.Map<CreateOrEditObeEclSicrApprovalDto>(obeEclSicrApproval)};

		    if (output.ObeEclSicrApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.ObeEclSicrApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }

		    if (output.ObeEclSicrApproval.ObeEclSicrId != null)
            {
                var _lookupObeEclSicr = await _lookup_obeEclSicrRepository.FirstOrDefaultAsync((Guid)output.ObeEclSicrApproval.ObeEclSicrId);
                output.ObeEclSicrTenantId = _lookupObeEclSicr.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclSicrApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclSicrApprovalDto input)
         {
            var obeEclSicrApproval = ObjectMapper.Map<ObeEclSicrApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclSicrApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclSicrApprovalRepository.InsertAsync(obeEclSicrApproval);
         }

		 protected virtual async Task Update(CreateOrEditObeEclSicrApprovalDto input)
         {
            var obeEclSicrApproval = await _obeEclSicrApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclSicrApproval);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclSicrApprovalRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclSicrApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclSicrApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new ObeEclSicrApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<ObeEclSicrApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<ObeEclSicrApprovalObeEclSicrLookupTableDto>> GetAllObeEclSicrForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclSicrRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclSicrList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclSicrApprovalObeEclSicrLookupTableDto>();
			foreach(var obeEclSicr in obeEclSicrList){
				lookupTableDtoList.Add(new ObeEclSicrApprovalObeEclSicrLookupTableDto
				{
					Id = obeEclSicr.Id.ToString(),
					DisplayName = obeEclSicr.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclSicrApprovalObeEclSicrLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}