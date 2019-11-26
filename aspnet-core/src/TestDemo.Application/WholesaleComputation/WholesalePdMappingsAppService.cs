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
	[AbpAuthorize(AppPermissions.Pages_WholesalePdMappings)]
    public class WholesalePdMappingsAppService : TestDemoAppServiceBase, IWholesalePdMappingsAppService
    {
		 private readonly IRepository<WholesalePdMapping, Guid> _wholesalePdMappingRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdMappingsAppService(IRepository<WholesalePdMapping, Guid> wholesalePdMappingRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdMappingRepository = wholesalePdMappingRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdMappingForViewDto>> GetAll(GetAllWholesalePdMappingsInput input)
         {
			
			var filteredWholesalePdMappings = _wholesalePdMappingRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractIdFilter),  e => e.ContractId == input.ContractIdFilter);

			var pagedAndFilteredWholesalePdMappings = filteredWholesalePdMappings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdMappings = from o in pagedAndFilteredWholesalePdMappings
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdMappingForViewDto() {
							WholesalePdMapping = new WholesalePdMappingDto
							{
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdMappings.CountAsync();

            return new PagedResultDto<GetWholesalePdMappingForViewDto>(
                totalCount,
                await wholesalePdMappings.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdMappings_Edit)]
		 public async Task<GetWholesalePdMappingForEditOutput> GetWholesalePdMappingForEdit(EntityDto<Guid> input)
         {
            var wholesalePdMapping = await _wholesalePdMappingRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdMappingForEditOutput {WholesalePdMapping = ObjectMapper.Map<CreateOrEditWholesalePdMappingDto>(wholesalePdMapping)};

		    if (output.WholesalePdMapping.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdMapping.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdMappingDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdMappings_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdMappingDto input)
         {
            var wholesalePdMapping = ObjectMapper.Map<WholesalePdMapping>(input);

			

            await _wholesalePdMappingRepository.InsertAsync(wholesalePdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdMappings_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdMappingDto input)
         {
            var wholesalePdMapping = await _wholesalePdMappingRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdMappings_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdMappingRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdMappings)]
         public async Task<PagedResultDto<WholesalePdMappingWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdMappingWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdMappingWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdMappingWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}