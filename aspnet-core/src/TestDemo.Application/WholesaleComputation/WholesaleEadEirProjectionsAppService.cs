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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections)]
    public class WholesaleEadEirProjectionsAppService : TestDemoAppServiceBase, IWholesaleEadEirProjectionsAppService
    {
		 private readonly IRepository<WholesaleEadEirProjection, Guid> _wholesaleEadEirProjectionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEadEirProjectionsAppService(IRepository<WholesaleEadEirProjection, Guid> wholesaleEadEirProjectionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEadEirProjectionRepository = wholesaleEadEirProjectionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEadEirProjectionForViewDto>> GetAll(GetAllWholesaleEadEirProjectionsInput input)
         {
			
			var filteredWholesaleEadEirProjections = _wholesaleEadEirProjectionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.EIR_GROUP.Contains(input.Filter));

			var pagedAndFilteredWholesaleEadEirProjections = filteredWholesaleEadEirProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEadEirProjections = from o in pagedAndFilteredWholesaleEadEirProjections
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEadEirProjectionForViewDto() {
							WholesaleEadEirProjection = new WholesaleEadEirProjectionDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEadEirProjections.CountAsync();

            return new PagedResultDto<GetWholesaleEadEirProjectionForViewDto>(
                totalCount,
                await wholesaleEadEirProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections_Edit)]
		 public async Task<GetWholesaleEadEirProjectionForEditOutput> GetWholesaleEadEirProjectionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEadEirProjection = await _wholesaleEadEirProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEadEirProjectionForEditOutput {WholesaleEadEirProjection = ObjectMapper.Map<CreateOrEditWholesaleEadEirProjectionDto>(wholesaleEadEirProjection)};

		    if (output.WholesaleEadEirProjection.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEadEirProjection.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEadEirProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEadEirProjectionDto input)
         {
            var wholesaleEadEirProjection = ObjectMapper.Map<WholesaleEadEirProjection>(input);

			

            await _wholesaleEadEirProjectionRepository.InsertAsync(wholesaleEadEirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEadEirProjectionDto input)
         {
            var wholesaleEadEirProjection = await _wholesaleEadEirProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEadEirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEadEirProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEadEirProjections)]
         public async Task<PagedResultDto<WholesaleEadEirProjectionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEadEirProjectionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEadEirProjectionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEadEirProjectionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}