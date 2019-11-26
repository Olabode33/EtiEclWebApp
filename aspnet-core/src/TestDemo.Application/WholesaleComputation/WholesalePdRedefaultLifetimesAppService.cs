using TestDemo.Wholesale;

using TestDemo.EclShared;

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
	[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes)]
    public class WholesalePdRedefaultLifetimesAppService : TestDemoAppServiceBase, IWholesalePdRedefaultLifetimesAppService
    {
		 private readonly IRepository<WholesalePdScenarioRedefaultLifetime, Guid> _wholesalePdRedefaultLifetimeRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdRedefaultLifetimesAppService(IRepository<WholesalePdScenarioRedefaultLifetime, Guid> wholesalePdRedefaultLifetimeRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdRedefaultLifetimeRepository = wholesalePdRedefaultLifetimeRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdRedefaultLifetimeForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimesInput input)
         {
			
			var filteredWholesalePdRedefaultLifetimes = _wholesalePdRedefaultLifetimeRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredWholesalePdRedefaultLifetimes = filteredWholesalePdRedefaultLifetimes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdRedefaultLifetimes = from o in pagedAndFilteredWholesalePdRedefaultLifetimes
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdRedefaultLifetimeForViewDto() {
							WholesalePdRedefaultLifetime = new WholesalePdRedefaultLifetimeDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdRedefaultLifetimes.CountAsync();

            return new PagedResultDto<GetWholesalePdRedefaultLifetimeForViewDto>(
                totalCount,
                await wholesalePdRedefaultLifetimes.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Edit)]
		 public async Task<GetWholesalePdRedefaultLifetimeForEditOutput> GetWholesalePdRedefaultLifetimeForEdit(EntityDto<Guid> input)
         {
            var wholesalePdRedefaultLifetime = await _wholesalePdRedefaultLifetimeRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdRedefaultLifetimeForEditOutput {WholesalePdRedefaultLifetime = ObjectMapper.Map<CreateOrEditWholesalePdRedefaultLifetimeDto>(wholesalePdRedefaultLifetime)};

		    if (output.WholesalePdRedefaultLifetime.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdRedefaultLifetime.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdRedefaultLifetimeDto input)
         {
            var wholesalePdRedefaultLifetime = ObjectMapper.Map<WholesalePdScenarioRedefaultLifetime>(input);

			

            await _wholesalePdRedefaultLifetimeRepository.InsertAsync(wholesalePdRedefaultLifetime);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdRedefaultLifetimeDto input)
         {
            var wholesalePdRedefaultLifetime = await _wholesalePdRedefaultLifetimeRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdRedefaultLifetime);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdRedefaultLifetimeRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimes)]
         public async Task<PagedResultDto<WholesalePdRedefaultLifetimeWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdRedefaultLifetimeWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdRedefaultLifetimeWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdRedefaultLifetimeWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}