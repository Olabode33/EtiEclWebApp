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
	[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics)]
    public class RetailPdLifetimeOptimisticsAppService : TestDemoAppServiceBase, IRetailPdLifetimeOptimisticsAppService
    {
		 private readonly IRepository<RetailPdLifetimeOptimistic, Guid> _retailPdLifetimeOptimisticRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdLifetimeOptimisticsAppService(IRepository<RetailPdLifetimeOptimistic, Guid> retailPdLifetimeOptimisticRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdLifetimeOptimisticRepository = retailPdLifetimeOptimisticRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdLifetimeOptimisticForViewDto>> GetAll(GetAllRetailPdLifetimeOptimisticsInput input)
         {
			
			var filteredRetailPdLifetimeOptimistics = _retailPdLifetimeOptimisticRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailPdLifetimeOptimistics = filteredRetailPdLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdLifetimeOptimistics = from o in pagedAndFilteredRetailPdLifetimeOptimistics
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdLifetimeOptimisticForViewDto() {
							RetailPdLifetimeOptimistic = new RetailPdLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetRetailPdLifetimeOptimisticForViewDto>(
                totalCount,
                await retailPdLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics_Edit)]
		 public async Task<GetRetailPdLifetimeOptimisticForEditOutput> GetRetailPdLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var retailPdLifetimeOptimistic = await _retailPdLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdLifetimeOptimisticForEditOutput {RetailPdLifetimeOptimistic = ObjectMapper.Map<CreateOrEditRetailPdLifetimeOptimisticDto>(retailPdLifetimeOptimistic)};

		    if (output.RetailPdLifetimeOptimistic.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdLifetimeOptimistic.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdLifetimeOptimisticDto input)
         {
            var retailPdLifetimeOptimistic = ObjectMapper.Map<RetailPdLifetimeOptimistic>(input);

			

            await _retailPdLifetimeOptimisticRepository.InsertAsync(retailPdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdLifetimeOptimisticDto input)
         {
            var retailPdLifetimeOptimistic = await _retailPdLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeOptimistics)]
         public async Task<PagedResultDto<RetailPdLifetimeOptimisticRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdLifetimeOptimisticRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdLifetimeOptimisticRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdLifetimeOptimisticRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}