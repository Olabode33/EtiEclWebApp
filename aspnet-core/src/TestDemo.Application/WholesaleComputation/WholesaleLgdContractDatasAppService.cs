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
	[AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas)]
    public class WholesaleLgdContractDatasAppService : TestDemoAppServiceBase, IWholesaleLgdContractDatasAppService
    {
		 private readonly IRepository<WholesaleLgdContractData, Guid> _wholesaleLgdContractDataRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleLgdContractDatasAppService(IRepository<WholesaleLgdContractData, Guid> wholesaleLgdContractDataRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleLgdContractDataRepository = wholesaleLgdContractDataRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleLgdContractDataForViewDto>> GetAll(GetAllWholesaleLgdContractDatasInput input)
         {
			
			var filteredWholesaleLgdContractDatas = _wholesaleLgdContractDataRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CONTRACT_NO.Contains(input.Filter));

			var pagedAndFilteredWholesaleLgdContractDatas = filteredWholesaleLgdContractDatas
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleLgdContractDatas = from o in pagedAndFilteredWholesaleLgdContractDatas
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleLgdContractDataForViewDto() {
							WholesaleLgdContractData = new WholesaleLgdContractDataDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleLgdContractDatas.CountAsync();

            return new PagedResultDto<GetWholesaleLgdContractDataForViewDto>(
                totalCount,
                await wholesaleLgdContractDatas.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas_Edit)]
		 public async Task<GetWholesaleLgdContractDataForEditOutput> GetWholesaleLgdContractDataForEdit(EntityDto<Guid> input)
         {
            var wholesaleLgdContractData = await _wholesaleLgdContractDataRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleLgdContractDataForEditOutput {WholesaleLgdContractData = ObjectMapper.Map<CreateOrEditWholesaleLgdContractDataDto>(wholesaleLgdContractData)};

		    if (output.WholesaleLgdContractData.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleLgdContractData.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleLgdContractDataDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleLgdContractDataDto input)
         {
            var wholesaleLgdContractData = ObjectMapper.Map<WholesaleLgdContractData>(input);

			

            await _wholesaleLgdContractDataRepository.InsertAsync(wholesaleLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleLgdContractDataDto input)
         {
            var wholesaleLgdContractData = await _wholesaleLgdContractDataRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleLgdContractData);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleLgdContractDataRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleLgdContractDatas)]
         public async Task<PagedResultDto<WholesaleLgdContractDataWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleLgdContractDataWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleLgdContractDataWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleLgdContractDataWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}