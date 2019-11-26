using TestDemo.Authorization.Users;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Retail.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TestDemo.Retail
{
	[AbpAuthorize(AppPermissions.Pages_RetailEcls)]
    public class RetailEclsAppService : TestDemoAppServiceBase, IRetailEclsAppService
    {
		 private readonly IRepository<RetailEcl, Guid> _retailEclRepository;
		 private readonly IRepository<User,long> _lookup_userRepository;
		 

		  public RetailEclsAppService(IRepository<RetailEcl, Guid> retailEclRepository , IRepository<User, long> lookup_userRepository) 
		  {
			_retailEclRepository = retailEclRepository;
			_lookup_userRepository = lookup_userRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclForViewDto>> GetAll(GetAllRetailEclsInput input)
         {
			var statusFilter = (EclStatusEnum) input.StatusFilter;
			
			var filteredRetailEcls = _retailEclRepository.GetAll()
						.Include( e => e.ClosedByUserFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false )
						.WhereIf(input.MinReportingDateFilter != null, e => e.ReportingDate >= input.MinReportingDateFilter)
						.WhereIf(input.MaxReportingDateFilter != null, e => e.ReportingDate <= input.MaxReportingDateFilter)
						.WhereIf(input.MinClosedDateFilter != null, e => e.ClosedDate >= input.MinClosedDateFilter)
						.WhereIf(input.MaxClosedDateFilter != null, e => e.ClosedDate <= input.MaxClosedDateFilter)
						.WhereIf(input.IsApprovedFilter > -1,  e => Convert.ToInt32(e.IsApproved) == input.IsApprovedFilter )
						.WhereIf(input.StatusFilter > -1, e => e.Status == statusFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.ClosedByUserFk != null && e.ClosedByUserFk.Name.ToLower() == input.UserNameFilter.ToLower().Trim());

			var pagedAndFilteredRetailEcls = filteredRetailEcls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEcls = from o in pagedAndFilteredRetailEcls
                         join o1 in _lookup_userRepository.GetAll() on o.ClosedByUserId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclForViewDto() {
							RetailEcl = new RetailEclDto
							{
                                ReportingDate = o.ReportingDate,
                                ClosedDate = o.ClosedDate,
                                IsApproved = o.IsApproved,
                                Status = o.Status,
                                Id = o.Id
							},
                         	UserName = s1 == null ? "" : s1.Name.ToString()
						};

            var totalCount = await filteredRetailEcls.CountAsync();

            return new PagedResultDto<GetRetailEclForViewDto>(
                totalCount,
                await retailEcls.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
		 public async Task<GetRetailEclForEditOutput> GetRetailEclForEdit(EntityDto<Guid> input)
         {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclForEditOutput {RetailEcl = ObjectMapper.Map<CreateOrEditRetailEclDto>(retailEcl)};

		    if (output.RetailEcl.ClosedByUserId != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.RetailEcl.ClosedByUserId);
                output.UserName = _lookupUser.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEcls_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclDto input)
         {
            var retailEcl = ObjectMapper.Map<RetailEcl>(input);

            var user = await UserManager.GetUserByIdAsync((long)AbpSession.UserId);
            var userSubsidiaries = await UserManager.GetOrganizationUnitsAsync(user);

            if (userSubsidiaries.Count > 0)
            {
                retailEcl.OrganizationUnitId = userSubsidiaries[0].Id;
            }

            if (AbpSession.TenantId != null)
			{
				retailEcl.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclRepository.InsertAsync(retailEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEcls_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclDto input)
         {
            var retailEcl = await _retailEclRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEcl);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEcls_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEcls)]
         public async Task<PagedResultDto<RetailEclUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_userRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.Name.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclUserLookupTableDto>();
			foreach(var user in userList){
				lookupTableDtoList.Add(new RetailEclUserLookupTableDto
				{
					Id = user.Id,
					DisplayName = user.Name?.ToString()
				});
			}

            return new PagedResultDto<RetailEclUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}