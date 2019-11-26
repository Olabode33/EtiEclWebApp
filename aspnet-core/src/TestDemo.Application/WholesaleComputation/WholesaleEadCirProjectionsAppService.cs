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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections)]
    public class WholesaleEadCirProjectionsAppService : TestDemoAppServiceBase, IWholesaleEadCirProjectionsAppService
    {
		 private readonly IRepository<WholesaleEadCirProjection, Guid> _wholesaleEadCirProjectionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEadCirProjectionsAppService(IRepository<WholesaleEadCirProjection, Guid> wholesaleEadCirProjectionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEadCirProjectionRepository = wholesaleEadCirProjectionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEadCirProjectionForViewDto>> GetAll(GetAllWholesaleEadCirProjectionsInput input)
         {
			
			var filteredWholesaleEadCirProjections = _wholesaleEadCirProjectionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CIR_GROUP.Contains(input.Filter));

			var pagedAndFilteredWholesaleEadCirProjections = filteredWholesaleEadCirProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEadCirProjections = from o in pagedAndFilteredWholesaleEadCirProjections
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEadCirProjectionForViewDto() {
							WholesaleEadCirProjection = new WholesaleEadCirProjectionDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEadCirProjections.CountAsync();

            return new PagedResultDto<GetWholesaleEadCirProjectionForViewDto>(
                totalCount,
                await wholesaleEadCirProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections_Edit)]
		 public async Task<GetWholesaleEadCirProjectionForEditOutput> GetWholesaleEadCirProjectionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEadCirProjection = await _wholesaleEadCirProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEadCirProjectionForEditOutput {WholesaleEadCirProjection = ObjectMapper.Map<CreateOrEditWholesaleEadCirProjectionDto>(wholesaleEadCirProjection)};

		    if (output.WholesaleEadCirProjection.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEadCirProjection.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEadCirProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEadCirProjectionDto input)
         {
            var wholesaleEadCirProjection = ObjectMapper.Map<WholesaleEadCirProjection>(input);

			

            await _wholesaleEadCirProjectionRepository.InsertAsync(wholesaleEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEadCirProjectionDto input)
         {
            var wholesaleEadCirProjection = await _wholesaleEadCirProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEadCirProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEadCirProjections)]
         public async Task<PagedResultDto<WholesaleEadCirProjectionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEadCirProjectionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEadCirProjectionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEadCirProjectionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}