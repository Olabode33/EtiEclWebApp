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
	[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns)]
    public class RetailPdRedefaultLifetimeDownturnsAppService : TestDemoAppServiceBase, IRetailPdRedefaultLifetimeDownturnsAppService
    {
		 private readonly IRepository<RetailPdRedefaultLifetimeDownturn, Guid> _retailPdRedefaultLifetimeDownturnRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdRedefaultLifetimeDownturnsAppService(IRepository<RetailPdRedefaultLifetimeDownturn, Guid> retailPdRedefaultLifetimeDownturnRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdRedefaultLifetimeDownturnRepository = retailPdRedefaultLifetimeDownturnRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeDownturnsInput input)
         {
			
			var filteredRetailPdRedefaultLifetimeDownturns = _retailPdRedefaultLifetimeDownturnRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredRetailPdRedefaultLifetimeDownturns = filteredRetailPdRedefaultLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdRedefaultLifetimeDownturns = from o in pagedAndFilteredRetailPdRedefaultLifetimeDownturns
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdRedefaultLifetimeDownturnForViewDto() {
							RetailPdRedefaultLifetimeDownturn = new RetailPdRedefaultLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdRedefaultLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetRetailPdRedefaultLifetimeDownturnForViewDto>(
                totalCount,
                await retailPdRedefaultLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Edit)]
		 public async Task<GetRetailPdRedefaultLifetimeDownturnForEditOutput> GetRetailPdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var retailPdRedefaultLifetimeDownturn = await _retailPdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdRedefaultLifetimeDownturnForEditOutput {RetailPdRedefaultLifetimeDownturn = ObjectMapper.Map<CreateOrEditRetailPdRedefaultLifetimeDownturnDto>(retailPdRedefaultLifetimeDownturn)};

		    if (output.RetailPdRedefaultLifetimeDownturn.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdRedefaultLifetimeDownturn.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdRedefaultLifetimeDownturnDto input)
         {
            var retailPdRedefaultLifetimeDownturn = ObjectMapper.Map<RetailPdRedefaultLifetimeDownturn>(input);

			

            await _retailPdRedefaultLifetimeDownturnRepository.InsertAsync(retailPdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdRedefaultLifetimeDownturnDto input)
         {
            var retailPdRedefaultLifetimeDownturn = await _retailPdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdRedefaultLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns)]
         public async Task<PagedResultDto<RetailPdRedefaultLifetimeDownturnRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdRedefaultLifetimeDownturnRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdRedefaultLifetimeDownturnRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdRedefaultLifetimeDownturnRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}