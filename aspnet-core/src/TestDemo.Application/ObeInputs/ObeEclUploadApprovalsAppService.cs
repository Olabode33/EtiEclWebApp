using TestDemo.ObeInputs;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeInputs
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals)]
    public class ObeEclUploadApprovalsAppService : TestDemoAppServiceBase, IObeEclUploadApprovalsAppService
    {
		 private readonly IRepository<ObeEclUploadApproval, Guid> _obeEclUploadApprovalRepository;
		 private readonly IRepository<ObeEclUpload,Guid> _lookup_obeEclUploadRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public ObeEclUploadApprovalsAppService(IRepository<ObeEclUploadApproval, Guid> obeEclUploadApprovalRepository , IRepository<ObeEclUpload, Guid> lookup_obeEclUploadRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_obeEclUploadApprovalRepository = obeEclUploadApprovalRepository;
			_lookup_obeEclUploadRepository = lookup_obeEclUploadRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclUploadApprovalForViewDto>> GetAll(GetAllObeEclUploadApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredObeEclUploadApprovals = _obeEclUploadApprovalRepository.GetAll()
						.Include( e => e.ObeEclUploadFk)
						.Include( e => e.ReviewedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclUploadTenantIdFilter), e => e.ObeEclUploadFk != null && e.ObeEclUploadFk.TenantId.ToLower() == input.ObeEclUploadTenantIdFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEclUploadApprovals = filteredObeEclUploadApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclUploadApprovals = from o in pagedAndFilteredObeEclUploadApprovals
                         join o1 in _lookup_obeEclUploadRepository.GetAll() on o.ObeEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetObeEclUploadApprovalForViewDto() {
							ObeEclUploadApproval = new ObeEclUploadApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	ObeEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredObeEclUploadApprovals.CountAsync();

            return new PagedResultDto<GetObeEclUploadApprovalForViewDto>(
                totalCount,
                await obeEclUploadApprovals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals_Edit)]
		 public async Task<GetObeEclUploadApprovalForEditOutput> GetObeEclUploadApprovalForEdit(EntityDto<Guid> input)
         {
            var obeEclUploadApproval = await _obeEclUploadApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclUploadApprovalForEditOutput {ObeEclUploadApproval = ObjectMapper.Map<CreateOrEditObeEclUploadApprovalDto>(obeEclUploadApproval)};

		    if (output.ObeEclUploadApproval.ObeEclUploadId != null)
            {
                var _lookupObeEclUpload = await _lookup_obeEclUploadRepository.FirstOrDefaultAsync((Guid)output.ObeEclUploadApproval.ObeEclUploadId);
                output.ObeEclUploadTenantId = _lookupObeEclUpload.TenantId.ToString();
            }

		    if (output.ObeEclUploadApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.ObeEclUploadApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclUploadApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclUploadApprovalDto input)
         {
            var obeEclUploadApproval = ObjectMapper.Map<ObeEclUploadApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclUploadApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclUploadApprovalRepository.InsertAsync(obeEclUploadApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclUploadApprovalDto input)
         {
            var obeEclUploadApproval = await _obeEclUploadApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclUploadApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclUploadApprovalRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals)]
         public async Task<PagedResultDto<ObeEclUploadApprovalObeEclUploadLookupTableDto>> GetAllObeEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclUploadApprovalObeEclUploadLookupTableDto>();
			foreach(var obeEclUpload in obeEclUploadList){
				lookupTableDtoList.Add(new ObeEclUploadApprovalObeEclUploadLookupTableDto
				{
					Id = obeEclUpload.Id.ToString(),
					DisplayName = obeEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclUploadApprovalObeEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_ObeEclUploadApprovals)]
         public async Task<PagedResultDto<ObeEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclUploadApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new ObeEclUploadApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<ObeEclUploadApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}