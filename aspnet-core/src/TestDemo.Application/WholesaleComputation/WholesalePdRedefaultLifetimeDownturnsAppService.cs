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
	[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns)]
    public class WholesalePdRedefaultLifetimeDownturnsAppService : TestDemoAppServiceBase, IWholesalePdRedefaultLifetimeDownturnsAppService
    {
		 private readonly IRepository<WholesalePdRedefaultLifetimeDownturn, Guid> _wholesalePdRedefaultLifetimeDownturnRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesalePdRedefaultLifetimeDownturnsAppService(IRepository<WholesalePdRedefaultLifetimeDownturn, Guid> wholesalePdRedefaultLifetimeDownturnRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesalePdRedefaultLifetimeDownturnRepository = wholesalePdRedefaultLifetimeDownturnRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesalePdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimeDownturnsInput input)
         {
			
			var filteredWholesalePdRedefaultLifetimeDownturns = _wholesalePdRedefaultLifetimeDownturnRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclTenantIdFilter), e => e.WholesaleEclFk != null && e.WholesaleEclFk.TenantId == input.WholesaleEclTenantIdFilter);

			var pagedAndFilteredWholesalePdRedefaultLifetimeDownturns = filteredWholesalePdRedefaultLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesalePdRedefaultLifetimeDownturns = from o in pagedAndFilteredWholesalePdRedefaultLifetimeDownturns
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesalePdRedefaultLifetimeDownturnForViewDto() {
							WholesalePdRedefaultLifetimeDownturn = new WholesalePdRedefaultLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesalePdRedefaultLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetWholesalePdRedefaultLifetimeDownturnForViewDto>(
                totalCount,
                await wholesalePdRedefaultLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Edit)]
		 public async Task<GetWholesalePdRedefaultLifetimeDownturnForEditOutput> GetWholesalePdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var wholesalePdRedefaultLifetimeDownturn = await _wholesalePdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesalePdRedefaultLifetimeDownturnForEditOutput {WholesalePdRedefaultLifetimeDownturn = ObjectMapper.Map<CreateOrEditWholesalePdRedefaultLifetimeDownturnDto>(wholesalePdRedefaultLifetimeDownturn)};

		    if (output.WholesalePdRedefaultLifetimeDownturn.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesalePdRedefaultLifetimeDownturn.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditWholesalePdRedefaultLifetimeDownturnDto input)
         {
            var wholesalePdRedefaultLifetimeDownturn = ObjectMapper.Map<WholesalePdRedefaultLifetimeDownturn>(input);

			

            await _wholesalePdRedefaultLifetimeDownturnRepository.InsertAsync(wholesalePdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesalePdRedefaultLifetimeDownturnDto input)
         {
            var wholesalePdRedefaultLifetimeDownturn = await _wholesalePdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesalePdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesalePdRedefaultLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns)]
         public async Task<PagedResultDto<WholesalePdRedefaultLifetimeDownturnWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesalePdRedefaultLifetimeDownturnWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesalePdRedefaultLifetimeDownturnWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesalePdRedefaultLifetimeDownturnWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}