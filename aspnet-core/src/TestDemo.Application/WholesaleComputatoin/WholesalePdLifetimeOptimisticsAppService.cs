using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputatoin.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleComputatoin
{
	[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics)]
    public class WholesalePdLifetimeOptimisticsAppService : TestDemoAppServiceBase, IWholesalePdLifetimeOptimisticsAppService
    {
		 private readonly IRepository<WholesalePdLifetimeOptimistic, Guid> _wholesalePdLifetimeOptimisticRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdLifetimeOptimisticsAppService(IRepository<WholesalePdLifetimeOptimistic, Guid> wholesalePdLifetimeOptimisticRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdLifetimeOptimisticRepository = wholesalePdLifetimeOptimisticRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdLifetimeOptimisticForViewDto>> GetAll(GetAllWholesalePdLifetimeOptimisticsInput input)
         {
			
			var filteredWholesalePdLifetimeOptimistics = _wholesalePdLifetimeOptimisticRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclTenantIdFilter), e => e.WholesaleEclFk != null && e.WholesaleEclFk.TenantId == input.WholesaleEclTenantIdFilter);

			var pagedAndFilteredWholesalePdLifetimeOptimistics = filteredWholesalePdLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdLifetimeOptimistics = from o in pagedAndFilteredWholesalePdLifetimeOptimistics
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdLifetimeOptimisticForViewDto() {
							WholesalePdLifetimeOptimistic = new WholesalePdLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetWholesalePdLifetimeOptimisticForViewDto>(
                totalCount,
                await wholesalePdLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Edit)]
		 public async Task<GetWholesalePdLifetimeOptimisticForEditOutput> GetWholesalePdLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var wholesalePdLifetimeOptimistic = await _wholesalePdLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdLifetimeOptimisticForEditOutput {WholesalePdLifetimeOptimistic = ObjectMapper.Map<CreateOrEditWholesalePdLifetimeOptimisticDto>(wholesalePdLifetimeOptimistic)};

		    if (output.WholesalePdLifetimeOptimistic.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdLifetimeOptimistic.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdLifetimeOptimisticDto input)
         {
            var wholesalePdLifetimeOptimistic = ObjectMapper.Map<WholesalePdLifetimeOptimistic>(input);

			

            await _wholesalePdLifetimeOptimisticRepository.InsertAsync(wholesalePdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdLifetimeOptimisticDto input)
         {
            var wholesalePdLifetimeOptimistic = await _wholesalePdLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeOptimistics)]
         public async Task<PagedResultDto<WholesalePdLifetimeOptimisticWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdLifetimeOptimisticWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdLifetimeOptimisticWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdLifetimeOptimisticWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}