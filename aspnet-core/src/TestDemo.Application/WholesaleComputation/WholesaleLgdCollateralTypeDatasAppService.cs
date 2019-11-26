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
	[AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas)]
    public class WholesaleLgdCollateralTypeDatasAppService : TestDemoAppServiceBase, IWholesaleLgdCollateralTypeDatasAppService
    {
		 private readonly IRepository<WholesaleLgdCollateralTypeData, Guid> _wholesaleLgdCollateralTypeDataRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleLgdCollateralTypeDatasAppService(IRepository<WholesaleLgdCollateralTypeData, Guid> wholesaleLgdCollateralTypeDataRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleLgdCollateralTypeDataRepository = wholesaleLgdCollateralTypeDataRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleLgdCollateralTypeDataForViewDto>> GetAll(GetAllWholesaleLgdCollateralTypeDatasInput input)
         {
			
			var filteredWholesaleLgdCollateralTypeDatas = _wholesaleLgdCollateralTypeDataRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CONTRACT_NO.Contains(input.Filter))
						.WhereIf(input.MinINVENTORY_OMVFilter != null, e => e.INVENTORY_OMV >= input.MinINVENTORY_OMVFilter)
						.WhereIf(input.MaxINVENTORY_OMVFilter != null, e => e.INVENTORY_OMV <= input.MaxINVENTORY_OMVFilter);

			var pagedAndFilteredWholesaleLgdCollateralTypeDatas = filteredWholesaleLgdCollateralTypeDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleLgdCollateralTypeDatas = from o in pagedAndFilteredWholesaleLgdCollateralTypeDatas
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleLgdCollateralTypeDataForViewDto() {
							WholesaleLgdCollateralTypeData = new WholesaleLgdCollateralTypeDataDto
							{
                                INVENTORY_OMV = o.INVENTORY_OMV,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleLgdCollateralTypeDatas.CountAsync();

            return new PagedResultDto<GetWholesaleLgdCollateralTypeDataForViewDto>(
                totalCount,
                await wholesaleLgdCollateralTypeDatas.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Edit)]
		 public async Task<GetWholesaleLgdCollateralTypeDataForEditOutput> GetWholesaleLgdCollateralTypeDataForEdit(EntityDto<Guid> input)
         {
            var wholesaleLgdCollateralTypeData = await _wholesaleLgdCollateralTypeDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleLgdCollateralTypeDataForEditOutput {WholesaleLgdCollateralTypeData = ObjectMapper.Map<CreateOrEditWholesaleLgdCollateralTypeDataDto>(wholesaleLgdCollateralTypeData)};

		    if (output.WholesaleLgdCollateralTypeData.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleLgdCollateralTypeData.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleLgdCollateralTypeDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleLgdCollateralTypeDataDto input)
         {
            var wholesaleLgdCollateralTypeData = ObjectMapper.Map<WholesaleLgdCollateralTypeData>(input);

			

            await _wholesaleLgdCollateralTypeDataRepository.InsertAsync(wholesaleLgdCollateralTypeData);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleLgdCollateralTypeDataDto input)
         {
            var wholesaleLgdCollateralTypeData = await _wholesaleLgdCollateralTypeDataRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleLgdCollateralTypeData);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleLgdCollateralTypeDataRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas)]
         public async Task<PagedResultDto<WholesaleLgdCollateralTypeDataWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleLgdCollateralTypeDataWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleLgdCollateralTypeDataWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleLgdCollateralTypeDataWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}