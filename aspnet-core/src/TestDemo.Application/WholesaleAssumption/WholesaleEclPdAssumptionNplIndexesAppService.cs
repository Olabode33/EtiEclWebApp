using TestDemo.Wholesale;

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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes)]
    public class WholesaleEclPdAssumptionNplIndexesAppService : TestDemoAppServiceBase, IWholesaleEclPdAssumptionNplIndexesAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumptionNplIndex, Guid> _wholesaleEclPdAssumptionNplIndexRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdAssumptionNplIndexesAppService(IRepository<WholesaleEclPdAssumptionNplIndex, Guid> wholesaleEclPdAssumptionNplIndexRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdAssumptionNplIndexRepository = wholesaleEclPdAssumptionNplIndexRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionNplIndexesInput input)
         {
			
			var filteredWholesaleEclPdAssumptionNplIndexes = _wholesaleEclPdAssumptionNplIndexRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter));

			var pagedAndFilteredWholesaleEclPdAssumptionNplIndexes = filteredWholesaleEclPdAssumptionNplIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdAssumptionNplIndexes = from o in pagedAndFilteredWholesaleEclPdAssumptionNplIndexes
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdAssumptionNplIndexForViewDto() {
							WholesaleEclPdAssumptionNplIndex = new WholesaleEclPdAssumptionNplIndexDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdAssumptionNplIndexes.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdAssumptionNplIndexForViewDto>(
                totalCount,
                await wholesaleEclPdAssumptionNplIndexes.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Edit)]
		 public async Task<GetWholesaleEclPdAssumptionNplIndexForEditOutput> GetWholesaleEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdAssumptionNplIndex = await _wholesaleEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdAssumptionNplIndexForEditOutput {WholesaleEclPdAssumptionNplIndex = ObjectMapper.Map<CreateOrEditWholesaleEclPdAssumptionNplIndexDto>(wholesaleEclPdAssumptionNplIndex)};

		    if (output.WholesaleEclPdAssumptionNplIndex.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdAssumptionNplIndex.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionNplIndexDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclPdAssumptionNplIndexDto input)
         {
            var wholesaleEclPdAssumptionNplIndex = ObjectMapper.Map<WholesaleEclPdAssumptionNplIndex>(input);

			

            await _wholesaleEclPdAssumptionNplIndexRepository.InsertAsync(wholesaleEclPdAssumptionNplIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclPdAssumptionNplIndexDto input)
         {
            var wholesaleEclPdAssumptionNplIndex = await _wholesaleEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdAssumptionNplIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclPdAssumptionNplIndexRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes)]
         public async Task<PagedResultDto<WholesaleEclPdAssumptionNplIndexWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclPdAssumptionNplIndexWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclPdAssumptionNplIndexWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclPdAssumptionNplIndexWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}