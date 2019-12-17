using TestDemo.Wholesale;

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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections)]
    public class WholesaleEclPdAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IWholesaleEclPdAssumptionMacroeconomicProjectionsAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> _wholesaleEclPdAssumptionMacroeconomicProjectionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdAssumptionMacroeconomicProjectionsAppService(IRepository<WholesaleEclPdAssumptionMacroeconomicProjection, Guid> wholesaleEclPdAssumptionMacroeconomicProjectionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdAssumptionMacroeconomicProjectionRepository = wholesaleEclPdAssumptionMacroeconomicProjectionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionMacroeconomicProjectionsInput input)
         {
			
			var filteredWholesaleEclPdAssumptionMacroeconomicProjections = _wholesaleEclPdAssumptionMacroeconomicProjectionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

			var pagedAndFilteredWholesaleEclPdAssumptionMacroeconomicProjections = filteredWholesaleEclPdAssumptionMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdAssumptionMacroeconomicProjections = from o in pagedAndFilteredWholesaleEclPdAssumptionMacroeconomicProjections
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdAssumptionMacroeconomicProjectionForViewDto() {
							WholesaleEclPdAssumptionMacroeconomicProjection = new WholesaleEclPdAssumptionMacroeconomicProjectionDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdAssumptionMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicProjectionForViewDto>(
                totalCount,
                await wholesaleEclPdAssumptionMacroeconomicProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Edit)]
		 public async Task<GetWholesaleEclPdAssumptionMacroeconomicProjectionForEditOutput> GetWholesaleEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdAssumptionMacroeconomicProjection = await _wholesaleEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdAssumptionMacroeconomicProjectionForEditOutput {WholesaleEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto>(wholesaleEclPdAssumptionMacroeconomicProjection)};

		    if (output.WholesaleEclPdAssumptionMacroeconomicProjection.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdAssumptionMacroeconomicProjection.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto input)
         {
            var wholesaleEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<WholesaleEclPdAssumptionMacroeconomicProjection>(input);

			

            await _wholesaleEclPdAssumptionMacroeconomicProjectionRepository.InsertAsync(wholesaleEclPdAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto input)
         {
            var wholesaleEclPdAssumptionMacroeconomicProjection = await _wholesaleEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclPdAssumptionMacroeconomicProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections)]
         public async Task<PagedResultDto<WholesaleEclPdAssumptionMacroeconomicProjectionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclPdAssumptionMacroeconomicProjectionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclPdAssumptionMacroeconomicProjectionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclPdAssumptionMacroeconomicProjectionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}