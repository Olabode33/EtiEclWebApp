using TestDemo.RetailInputs;
using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailInputs.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailInputs
{
    public class RetailEclUploadApprovalsAppService : TestDemoAppServiceBase, IRetailEclUploadApprovalsAppService
    {
		 private readonly IRepository<RetailEclUploadApproval, Guid> _retailEclUploadApprovalRepository;
		 private readonly IRepository<RetailEclUpload,Guid> _lookup_retailEclUploadRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public RetailEclUploadApprovalsAppService(IRepository<RetailEclUploadApproval, Guid> retailEclUploadApprovalRepository , IRepository<RetailEclUpload, Guid> lookup_retailEclUploadRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_retailEclUploadApprovalRepository = retailEclUploadApprovalRepository;
			_lookup_retailEclUploadRepository = lookup_retailEclUploadRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclUploadApprovalForViewDto>> GetAll(GetAllRetailEclUploadApprovalsInput input)
         {
			var statusFilter = (GeneralStatusEnum) input.StatusFilter;
			
			var filteredRetailEclUploadApprovals = _retailEclUploadApprovalRepository.GetAll()
						.Include( e => e.RetailEclUploadFk)
						.Include( e => e.ReviewedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ReviewComment.Contains(input.Filter))
						.WhereIf(input.MinReviewedDateFilter != null, e => e.ReviewedDate >= input.MinReviewedDateFilter)
						.WhereIf(input.MaxReviewedDateFilter != null, e => e.ReviewedDate <= input.MaxReviewedDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ReviewCommentFilter),  e => e.ReviewComment.ToLower() == input.ReviewCommentFilter.ToLower().Trim())
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclUploadApprovals = filteredRetailEclUploadApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclUploadApprovals = from o in pagedAndFilteredRetailEclUploadApprovals
                         join o1 in _lookup_retailEclUploadRepository.GetAll() on o.RetailEclUploadId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetRetailEclUploadApprovalForViewDto() {
							RetailEclUploadApproval = new RetailEclUploadApprovalDto
							{
                                ReviewedDate = o.ReviewedDate,
                                ReviewComment = o.ReviewComment,
                                Status = o.Status,
                                Id = o.Id
							},
                         	RetailEclUploadTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredRetailEclUploadApprovals.CountAsync();

            return new PagedResultDto<GetRetailEclUploadApprovalForViewDto>(
                totalCount,
                await retailEclUploadApprovals.ToListAsync()
            );
         }
		 
		 public async Task<GetRetailEclUploadApprovalForEditOutput> GetRetailEclUploadApprovalForEdit(EntityDto<Guid> input)
         {
            var retailEclUploadApproval = await _retailEclUploadApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclUploadApprovalForEditOutput {RetailEclUploadApproval = ObjectMapper.Map<CreateOrEditRetailEclUploadApprovalDto>(retailEclUploadApproval)};

		    if (output.RetailEclUploadApproval.RetailEclUploadId != null)
            {
                var _lookupRetailEclUpload = await _lookup_retailEclUploadRepository.FirstOrDefaultAsync((Guid)output.RetailEclUploadApproval.RetailEclUploadId);
                output.RetailEclUploadTenantId = _lookupRetailEclUpload.TenantId.ToString();
            }

		    if (output.RetailEclUploadApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEclUploadApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclUploadApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditRetailEclUploadApprovalDto input)
         {
            var retailEclUploadApproval = ObjectMapper.Map<RetailEclUploadApproval>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclUploadApproval.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclUploadApprovalRepository.InsertAsync(retailEclUploadApproval);
         }

		 protected virtual async Task Update(CreateOrEditRetailEclUploadApprovalDto input)
         {
            var retailEclUploadApproval = await _retailEclUploadApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclUploadApproval);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclUploadApprovalRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<RetailEclUploadApprovalRetailEclUploadLookupTableDto>> GetAllRetailEclUploadForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclUploadRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclUploadList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclUploadApprovalRetailEclUploadLookupTableDto>();
			foreach(var retailEclUpload in retailEclUploadList){
				lookupTableDtoList.Add(new RetailEclUploadApprovalRetailEclUploadLookupTableDto
				{
					Id = retailEclUpload.Id.ToString(),
					DisplayName = retailEclUpload.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclUploadApprovalRetailEclUploadLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

         public async Task<PagedResultDto<RetailEclUploadApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclUploadApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new RetailEclUploadApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<RetailEclUploadApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}