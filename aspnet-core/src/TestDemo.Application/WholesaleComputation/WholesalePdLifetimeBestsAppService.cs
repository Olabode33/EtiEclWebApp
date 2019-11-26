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
	[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests)]
    public class WholesalePdLifetimeBestsAppService : TestDemoAppServiceBase, IWholesalePdLifetimeBestsAppService
    {
		 private readonly IRepository<WholesalePdScenarioLifetime, Guid> _wholesalePdLifetimeBestRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdLifetimeBestsAppService(IRepository<WholesalePdScenarioLifetime, Guid> wholesalePdLifetimeBestRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdLifetimeBestRepository = wholesalePdLifetimeBestRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdLifetimeBestForViewDto>> GetAll(GetAllWholesalePdLifetimeBestsInput input)
         {
			
			var filteredWholesalePdLifetimeBests = _wholesalePdLifetimeBestRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredWholesalePdLifetimeBests = filteredWholesalePdLifetimeBests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdLifetimeBests = from o in pagedAndFilteredWholesalePdLifetimeBests
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdLifetimeBestForViewDto() {
							WholesalePdLifetimeBest = new WholesalePdLifetimeBestDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdLifetimeBests.CountAsync();

            return new PagedResultDto<GetWholesalePdLifetimeBestForViewDto>(
                totalCount,
                await wholesalePdLifetimeBests.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests_Edit)]
		 public async Task<GetWholesalePdLifetimeBestForEditOutput> GetWholesalePdLifetimeBestForEdit(EntityDto<Guid> input)
         {
            var wholesalePdLifetimeBest = await _wholesalePdLifetimeBestRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdLifetimeBestForEditOutput {WholesalePdLifetimeBest = ObjectMapper.Map<CreateOrEditWholesalePdLifetimeBestDto>(wholesalePdLifetimeBest)};

		    if (output.WholesalePdLifetimeBest.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdLifetimeBest.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdLifetimeBestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdLifetimeBestDto input)
         {
            var wholesalePdLifetimeBest = ObjectMapper.Map<WholesalePdScenarioLifetime>(input);

			

            await _wholesalePdLifetimeBestRepository.InsertAsync(wholesalePdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdLifetimeBestDto input)
         {
            var wholesalePdLifetimeBest = await _wholesalePdLifetimeBestRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdLifetimeBestRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeBests)]
         public async Task<PagedResultDto<WholesalePdLifetimeBestWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdLifetimeBestWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdLifetimeBestWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdLifetimeBestWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}