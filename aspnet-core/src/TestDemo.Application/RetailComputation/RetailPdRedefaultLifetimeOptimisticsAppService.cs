using TestDemo.Retail;


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
	[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics)]
    public class RetailPdRedefaultLifetimeOptimisticsAppService : TestDemoAppServiceBase, IRetailPdRedefaultLifetimeOptimisticsAppService
    {
		 private readonly IRepository<RetailPdRedefaultLifetimeOptimistic, Guid> _retailPdRedefaultLifetimeOptimisticRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdRedefaultLifetimeOptimisticsAppService(IRepository<RetailPdRedefaultLifetimeOptimistic, Guid> retailPdRedefaultLifetimeOptimisticRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdRedefaultLifetimeOptimisticRepository = retailPdRedefaultLifetimeOptimisticRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeOptimisticsInput input)
         {
			
			var filteredRetailPdRedefaultLifetimeOptimistics = _retailPdRedefaultLifetimeOptimisticRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailPdRedefaultLifetimeOptimistics = filteredRetailPdRedefaultLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdRedefaultLifetimeOptimistics = from o in pagedAndFilteredRetailPdRedefaultLifetimeOptimistics
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdRedefaultLifetimeOptimisticForViewDto() {
							RetailPdRedefaultLifetimeOptimistic = new RetailPdRedefaultLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdRedefaultLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetRetailPdRedefaultLifetimeOptimisticForViewDto>(
                totalCount,
                await retailPdRedefaultLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Edit)]
		 public async Task<GetRetailPdRedefaultLifetimeOptimisticForEditOutput> GetRetailPdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var retailPdRedefaultLifetimeOptimistic = await _retailPdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdRedefaultLifetimeOptimisticForEditOutput {RetailPdRedefaultLifetimeOptimistic = ObjectMapper.Map<CreateOrEditRetailPdRedefaultLifetimeOptimisticDto>(retailPdRedefaultLifetimeOptimistic)};

		    if (output.RetailPdRedefaultLifetimeOptimistic.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdRedefaultLifetimeOptimistic.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdRedefaultLifetimeOptimisticDto input)
         {
            var retailPdRedefaultLifetimeOptimistic = ObjectMapper.Map<RetailPdRedefaultLifetimeOptimistic>(input);

			

            await _retailPdRedefaultLifetimeOptimisticRepository.InsertAsync(retailPdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdRedefaultLifetimeOptimisticDto input)
         {
            var retailPdRedefaultLifetimeOptimistic = await _retailPdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdRedefaultLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics)]
         public async Task<PagedResultDto<RetailPdRedefaultLifetimeOptimisticRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdRedefaultLifetimeOptimisticRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdRedefaultLifetimeOptimisticRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdRedefaultLifetimeOptimisticRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}