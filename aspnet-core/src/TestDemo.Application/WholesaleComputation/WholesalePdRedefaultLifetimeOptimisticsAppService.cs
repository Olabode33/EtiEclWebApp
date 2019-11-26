using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleComputation
{
	[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics)]
    public class WholesalePdRedefaultLifetimeOptimisticsAppService : TestDemoAppServiceBase, IWholesalePdRedefaultLifetimeOptimisticsAppService
    {
		 private readonly IRepository<WholesalePdRedefaultLifetimeOptimistic, Guid> _wholesalePdRedefaultLifetimeOptimisticRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdRedefaultLifetimeOptimisticsAppService(IRepository<WholesalePdRedefaultLifetimeOptimistic, Guid> wholesalePdRedefaultLifetimeOptimisticRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdRedefaultLifetimeOptimisticRepository = wholesalePdRedefaultLifetimeOptimisticRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimeOptimisticsInput input)
         {
			
			var filteredWholesalePdRedefaultLifetimeOptimistics = _wholesalePdRedefaultLifetimeOptimisticRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredWholesalePdRedefaultLifetimeOptimistics = filteredWholesalePdRedefaultLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdRedefaultLifetimeOptimistics = from o in pagedAndFilteredWholesalePdRedefaultLifetimeOptimistics
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdRedefaultLifetimeOptimisticForViewDto() {
							WholesalePdRedefaultLifetimeOptimistic = new WholesalePdRedefaultLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdRedefaultLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetWholesalePdRedefaultLifetimeOptimisticForViewDto>(
                totalCount,
                await wholesalePdRedefaultLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Edit)]
		 public async Task<GetWholesalePdRedefaultLifetimeOptimisticForEditOutput> GetWholesalePdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var wholesalePdRedefaultLifetimeOptimistic = await _wholesalePdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdRedefaultLifetimeOptimisticForEditOutput {WholesalePdRedefaultLifetimeOptimistic = ObjectMapper.Map<CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto>(wholesalePdRedefaultLifetimeOptimistic)};

		    if (output.WholesalePdRedefaultLifetimeOptimistic.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdRedefaultLifetimeOptimistic.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto input)
         {
            var wholesalePdRedefaultLifetimeOptimistic = ObjectMapper.Map<WholesalePdRedefaultLifetimeOptimistic>(input);

			

            await _wholesalePdRedefaultLifetimeOptimisticRepository.InsertAsync(wholesalePdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto input)
         {
            var wholesalePdRedefaultLifetimeOptimistic = await _wholesalePdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdRedefaultLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics)]
         public async Task<PagedResultDto<WholesalePdRedefaultLifetimeOptimisticWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdRedefaultLifetimeOptimisticWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdRedefaultLifetimeOptimisticWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdRedefaultLifetimeOptimisticWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}