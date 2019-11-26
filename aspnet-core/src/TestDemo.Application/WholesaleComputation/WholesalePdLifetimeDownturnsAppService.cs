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
	[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns)]
    public class WholesalePdLifetimeDownturnsAppService : TestDemoAppServiceBase, IWholesalePdLifetimeDownturnsAppService
    {
		 private readonly IRepository<WholesalePdLifetimeDownturn, Guid> _wholesalePdLifetimeDownturnRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdLifetimeDownturnsAppService(IRepository<WholesalePdLifetimeDownturn, Guid> wholesalePdLifetimeDownturnRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdLifetimeDownturnRepository = wholesalePdLifetimeDownturnRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdLifetimeDownturnForViewDto>> GetAll(GetAllWholesalePdLifetimeDownturnsInput input)
         {
			
			var filteredWholesalePdLifetimeDownturns = _wholesalePdLifetimeDownturnRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclTenantIdFilter), e => e.WholesaleEclFk != null && e.WholesaleEclFk.TenantId == input.WholesaleEclTenantIdFilter);

			var pagedAndFilteredWholesalePdLifetimeDownturns = filteredWholesalePdLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdLifetimeDownturns = from o in pagedAndFilteredWholesalePdLifetimeDownturns
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdLifetimeDownturnForViewDto() {
							WholesalePdLifetimeDownturn = new WholesalePdLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetWholesalePdLifetimeDownturnForViewDto>(
                totalCount,
                await wholesalePdLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns_Edit)]
		 public async Task<GetWholesalePdLifetimeDownturnForEditOutput> GetWholesalePdLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var wholesalePdLifetimeDownturn = await _wholesalePdLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdLifetimeDownturnForEditOutput {WholesalePdLifetimeDownturn = ObjectMapper.Map<CreateOrEditWholesalePdLifetimeDownturnDto>(wholesalePdLifetimeDownturn)};

		    if (output.WholesalePdLifetimeDownturn.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdLifetimeDownturn.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdLifetimeDownturnDto input)
         {
            var wholesalePdLifetimeDownturn = ObjectMapper.Map<WholesalePdLifetimeDownturn>(input);

			

            await _wholesalePdLifetimeDownturnRepository.InsertAsync(wholesalePdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdLifetimeDownturnDto input)
         {
            var wholesalePdLifetimeDownturn = await _wholesalePdLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdLifetimeDownturns)]
         public async Task<PagedResultDto<WholesalePdLifetimeDownturnWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdLifetimeDownturnWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdLifetimeDownturnWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdLifetimeDownturnWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}