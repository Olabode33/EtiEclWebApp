using TestDemo.Wholesale;
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
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleAssumption
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals)]
    public class WholesaleEclAssumptionApprovalsAppService : TestDemoAppServiceBase, IWholesaleEclAssumptionApprovalsAppService
    {
		 private readonly IRepository<WholesaleEclAssumptionApproval, Guid> _wholesaleEclAssumptionApprovalRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public WholesaleEclAssumptionApprovalsAppService(IRepository<WholesaleEclAssumptionApproval, Guid> wholesaleEclAssumptionApprovalRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository, IRepository<User, long> lookup_userRepository) 
		  {
			_wholesaleEclAssumptionApprovalRepository = wholesaleEclAssumptionApprovalRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclAssumptionApprovalForViewDto>> GetAll(GetAllWholesaleEclAssumptionApprovalsInput input)
         {
			
			var filteredWholesaleEclAssumptionApprovals = _wholesaleEclAssumptionApprovalRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.Include( e => e.ReviewedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.OldValue.Contains(input.Filter) || e.NewValue.Contains(input.Filter) || e.ReviewComment.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ReviewedByUserFk != null && e.ReviewedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclAssumptionApprovals = filteredWholesaleEclAssumptionApprovals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclAssumptionApprovals = from o in pagedAndFilteredWholesaleEclAssumptionApprovals
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_userRepository.GetAll() on o.ReviewedByUserId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetWholesaleEclAssumptionApprovalForViewDto() {
							WholesaleEclAssumptionApproval = new WholesaleEclAssumptionApprovalDto
							{
                                AssumptionType = o.AssumptionType,
                                OldValue = o.OldValue,
                                DateReviewed = o.DateReviewed,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	UserName = s2 == null ? "" : s2.Name.ToString()
						};

            var totalCount = await filteredWholesaleEclAssumptionApprovals.CountAsync();

            return new PagedResultDto<GetWholesaleEclAssumptionApprovalForViewDto>(
                totalCount,
                await wholesaleEclAssumptionApprovals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Edit)]
		 public async Task<GetWholesaleEclAssumptionApprovalForEditOutput> GetWholesaleEclAssumptionApprovalForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclAssumptionApproval = await _wholesaleEclAssumptionApprovalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclAssumptionApprovalForEditOutput {WholesaleEclAssumptionApproval = ObjectMapper.Map<CreateOrEditWholesaleEclAssumptionApprovalDto>(wholesaleEclAssumptionApproval)};

		    if (output.WholesaleEclAssumptionApproval.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclAssumptionApproval.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }

		    if (output.WholesaleEclAssumptionApproval.ReviewedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.WholesaleEclAssumptionApproval.ReviewedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclAssumptionApprovalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclAssumptionApprovalDto input)
         {
            var wholesaleEclAssumptionApproval = ObjectMapper.Map<WholesaleEclAssumptionApproval>(input);

			

            await _wholesaleEclAssumptionApprovalRepository.InsertAsync(wholesaleEclAssumptionApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclAssumptionApprovalDto input)
         {
            var wholesaleEclAssumptionApproval = await _wholesaleEclAssumptionApprovalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclAssumptionApproval);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclAssumptionApprovalRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals)]
         public async Task<PagedResultDto<WholesaleEclAssumptionApprovalWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclAssumptionApprovalWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclAssumptionApprovalWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclAssumptionApprovalWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptionApprovals)]
         public async Task<PagedResultDto<WholesaleEclAssumptionApprovalUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclAssumptionApprovalUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new WholesaleEclAssumptionApprovalUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclAssumptionApprovalUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}