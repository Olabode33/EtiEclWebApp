using TestDemo.Wholesale;

using TestDemo.EclShared;
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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions)]
    public class WholesaleEclPdAssumptionsAppService : TestDemoAppServiceBase, IWholesaleEclPdAssumptionsAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumption, Guid> _wholesaleEclPdAssumptionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdAssumptionsAppService(IRepository<WholesaleEclPdAssumption, Guid> wholesaleEclPdAssumptionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdAssumptionRepository = wholesaleEclPdAssumptionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclPdAssumptionForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionsInput input)
         {
			
			var filteredWholesaleEclPdAssumptions = _wholesaleEclPdAssumptionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredWholesaleEclPdAssumptions = filteredWholesaleEclPdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdAssumptions = from o in pagedAndFilteredWholesaleEclPdAssumptions
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdAssumptionForViewDto() {
							WholesaleEclPdAssumption = new WholesaleEclPdAssumptionDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdAssumptions.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdAssumptionForViewDto>(
                totalCount,
                await wholesaleEclPdAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions_Edit)]
		 public async Task<GetWholesaleEclPdAssumptionForEditOutput> GetWholesaleEclPdAssumptionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdAssumption = await _wholesaleEclPdAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdAssumptionForEditOutput {WholesaleEclPdAssumption = ObjectMapper.Map<CreateOrEditWholesaleEclPdAssumptionDto>(wholesaleEclPdAssumption)};

		    if (output.WholesaleEclPdAssumption.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdAssumption.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclPdAssumptionDto input)
         {
            var wholesaleEclPdAssumption = ObjectMapper.Map<WholesaleEclPdAssumption>(input);

			

            await _wholesaleEclPdAssumptionRepository.InsertAsync(wholesaleEclPdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclPdAssumptionDto input)
         {
            var wholesaleEclPdAssumption = await _wholesaleEclPdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclPdAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptions)]
         public async Task<PagedResultDto<WholesaleEclPdAssumptionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclPdAssumptionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclPdAssumptionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclPdAssumptionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}