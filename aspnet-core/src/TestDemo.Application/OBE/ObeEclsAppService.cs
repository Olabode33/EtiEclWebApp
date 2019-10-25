using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.OBE.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.OBE
{
	[AbpAuthorize(AppPermissions.Pages_ObeEcls)]
    public class ObeEclsAppService : TestDemoAppServiceBase, IObeEclsAppService
    {
		 private readonly IRepository<ObeEcl, Guid> _obeEclRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public ObeEclsAppService(IRepository<ObeEcl, Guid> obeEclRepository , IRepository<User, long> lookup_userRepository) 
		  {
			_obeEclRepository = obeEclRepository;
			_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclForViewDto>> GetAll(GetAllObeEclsInput input)
         {
			var statusFilter = (EclStatusEnum) input.StatusFilter;
			
			var filteredObeEcls = _obeEclRepository.GetAll()
						.Include( e => e.ClosedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
						.WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
						.WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
						.WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
						.WhereIf(input.IsApprovedFilter > -1,  e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter )
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredObeEcls = filteredObeEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEcls = from o in pagedAndFilteredObeEcls
                         join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclForViewDto() {
							ObeEcl = new ObeEclDto
							{
                                ReportingDate = o.ReportingDate,
                                ClosedDate = o.ClosedDate,
                                IsApproved = o.IsApproved,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredObeEcls.CountAsync();

            return new PagedResultDto<GetObeEclForViewDto>(
                totalCount,
                await obeEcls.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEcls_Edit)]
		 public async Task<GetObeEclForEditOutput> GetObeEclForEdit(EntityDto<Guid> input)
         {
            var obeEcl = await _obeEclRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclForEditOutput {ObeEcl = ObjectMapper.Map<CreateOrEditObeEclDto>(obeEcl)};

		    if (output.ObeEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.ObeEcl.ClosedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEcls_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclDto input)
         {
            var obeEcl = ObjectMapper.Map<ObeEcl>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEcl.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclRepository.InsertAsync(obeEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEcls_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclDto input)
         {
            var obeEcl = await _obeEclRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEcls_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEcls)]
         public async Task<PagedResultDto<ObeEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new ObeEclUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<ObeEclUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}