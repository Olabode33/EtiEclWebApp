using TestDemo.WholesaleInputs;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleInputs
{
    public class WholesaleEclUploadApprovalsAppService : TestDemoAppServiceBase, IWholesaleEclUploadApprovalsAppService
    {
		 private readonly IRepository<WholesaleEclUploadApproval, Guid> _wholesaleEclUploadApprovalRepository;
		 private readonly IRepository<WholesaleEclUpload,Guid> _lookup_wholesaleEclUploadRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public WholesaleEclUploadApprovalsAppService(IRepository<WholesaleEclUploadApproval, Guid> wholesaleEclUploadApprovalRepository , IRepository<WholesaleEclUpload, Guid> lookup_wholesaleEclUploadRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_wholesaleEclUploadApprovalRepository = wholesaleEclUploadApprovalRepository;
			_lookup_wholesaleEclUploadRepository = lookup_wholesaleEclUploadRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclUploadApprovalForViewDto>> GetAll(GetAllWholesaleEclUploadApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredWholesaleEclUploadApprovals = _wholesaleEclUploadApprovalRepository.GetAll()
						.Include( e => e.WholesaleEclUploadFk)
						.Include( e => e.ReviewedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclUploadUploadCommentFilter), e => e.WholesaleEclUploadFk != null && e.WholesaleEclUploadFk.UploadComment.ToLower() == input.WholesaleEclUploadUploadCommentFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclUploadApprovals = filteredWholesaleEclUploadApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclUploadApprovals = from o in pagedAndFilteredWholesaleEclUploadApprovals
                         join o1 in _lookup_wholesaleEclUploadRepository.GetAll() on o.WholesaleEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetWholesaleEclUploadApprovalForViewDto() {
							WholesaleEclUploadApproval = new WholesaleEclUploadApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	WholesaleEclUploadUploadComment = s1 == null ? "" : s1.UploadComment.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredWholesaleEclUploadApprovals.CountAsync();

            return new PagedResultDto<GetWholesaleEclUploadApprovalForViewDto>(
                totalCount,
                await wholesaleEclUploadApprovals.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclUploadApprovalForEditOutput> GetWholesaleEclUploadApprovalForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclUploadApproval = await _wholesaleEclUploadApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclUploadApprovalForEditOutput {WholesaleEclUploadApproval = ObjectMapper.Map<CreateOrEditWholesaleEclUploadApprovalDto>(wholesaleEclUploadApproval)};

		    if (output.WholesaleEclUploadApproval.WholesaleEclUploadId != null)
            {
                var _lookupWholesaleEclUpload = await _lookup_wholesaleEclUploadRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclUploadApproval.WholesaleEclUploadId);
                output.WholesaleEclUploadUploadComment = _lookupWholesaleEclUpload.UploadComment.ToString();
            }

		    if (output.WholesaleEclUploadApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEclUploadApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclUploadApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclUploadApprovalDto input)
         {
            var wholesaleEclUploadApproval = ObjectMapper.Map<WholesaleEclUploadApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclUploadApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclUploadApprovalRepository.InsertAsync(wholesaleEclUploadApproval);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclUploadApprovalDto input)
         {
            var wholesaleEclUploadApproval = await _wholesaleEclUploadApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclUploadApproval);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclUploadApprovalRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclUploadApprovalWholesaleEclUploadLookupTableDto>> GetAllWholesaleEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.UploadComment.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclUploadApprovalWholesaleEclUploadLookupTableDto>();
			foreach(var wholesaleEclUpload in wholesaleEclUploadList){
				lookupTableDtoList.Add(new WholesaleEclUploadApprovalWholesaleEclUploadLookupTableDto
				{
					Id = wholesaleEclUpload.Id.ToString(),
					DisplayName = wholesaleEclUpload.UploadComment?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclUploadApprovalWholesaleEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<WholesaleEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclUploadApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new WholesaleEclUploadApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclUploadApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}