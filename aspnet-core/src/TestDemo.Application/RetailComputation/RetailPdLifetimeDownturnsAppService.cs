using TestDemo.Retail;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailComputation
{
	[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns)]
    public class RetailPdLifetimeDownturnsAppService : TestDemoAppServiceBase, IRetailPdLifetimeDownturnsAppService
    {
		 private readonly IRepository<RetailPdLifetimeDownturn, Guid> _retailPdLifetimeDownturnRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdLifetimeDownturnsAppService(IRepository<RetailPdLifetimeDownturn, Guid> retailPdLifetimeDownturnRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdLifetimeDownturnRepository = retailPdLifetimeDownturnRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdLifetimeDownturnForViewDto>> GetAll(GetAllRetailPdLifetimeDownturnsInput input)
         {
			
			var filteredRetailPdLifetimeDownturns = _retailPdLifetimeDownturnRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailPdLifetimeDownturns = filteredRetailPdLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdLifetimeDownturns = from o in pagedAndFilteredRetailPdLifetimeDownturns
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdLifetimeDownturnForViewDto() {
							RetailPdLifetimeDownturn = new RetailPdLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetRetailPdLifetimeDownturnForViewDto>(
                totalCount,
                await retailPdLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns_Edit)]
		 public async Task<GetRetailPdLifetimeDownturnForEditOutput> GetRetailPdLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var retailPdLifetimeDownturn = await _retailPdLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdLifetimeDownturnForEditOutput {RetailPdLifetimeDownturn = ObjectMapper.Map<CreateOrEditRetailPdLifetimeDownturnDto>(retailPdLifetimeDownturn)};

		    if (output.RetailPdLifetimeDownturn.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdLifetimeDownturn.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdLifetimeDownturnDto input)
         {
            var retailPdLifetimeDownturn = ObjectMapper.Map<RetailPdLifetimeDownturn>(input);

			

            await _retailPdLifetimeDownturnRepository.InsertAsync(retailPdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdLifetimeDownturnDto input)
         {
            var retailPdLifetimeDownturn = await _retailPdLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeDownturns)]
         public async Task<PagedResultDto<RetailPdLifetimeDownturnRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdLifetimeDownturnRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdLifetimeDownturnRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdLifetimeDownturnRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}