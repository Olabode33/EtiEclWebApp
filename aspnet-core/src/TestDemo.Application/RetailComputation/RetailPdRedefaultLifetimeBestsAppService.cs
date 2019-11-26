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
	[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests)]
    public class RetailPdRedefaultLifetimeBestsAppService : TestDemoAppServiceBase, IRetailPdRedefaultLifetimeBestsAppService
    {
		 private readonly IRepository<RetailPdRedefaultLifetimeBest, Guid> _retailPdRedefaultLifetimeBestRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdRedefaultLifetimeBestsAppService(IRepository<RetailPdRedefaultLifetimeBest, Guid> retailPdRedefaultLifetimeBestRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdRedefaultLifetimeBestRepository = retailPdRedefaultLifetimeBestRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdRedefaultLifetimeBestForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeBestsInput input)
         {
			
			var filteredRetailPdRedefaultLifetimeBests = _retailPdRedefaultLifetimeBestRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailPdRedefaultLifetimeBests = filteredRetailPdRedefaultLifetimeBests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdRedefaultLifetimeBests = from o in pagedAndFilteredRetailPdRedefaultLifetimeBests
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdRedefaultLifetimeBestForViewDto() {
							RetailPdRedefaultLifetimeBest = new RetailPdRedefaultLifetimeBestDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdRedefaultLifetimeBests.CountAsync();

            return new PagedResultDto<GetRetailPdRedefaultLifetimeBestForViewDto>(
                totalCount,
                await retailPdRedefaultLifetimeBests.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Edit)]
		 public async Task<GetRetailPdRedefaultLifetimeBestForEditOutput> GetRetailPdRedefaultLifetimeBestForEdit(EntityDto<Guid> input)
         {
            var retailPdRedefaultLifetimeBest = await _retailPdRedefaultLifetimeBestRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdRedefaultLifetimeBestForEditOutput {RetailPdRedefaultLifetimeBest = ObjectMapper.Map<CreateOrEditRetailPdRedefaultLifetimeBestDto>(retailPdRedefaultLifetimeBest)};

		    if (output.RetailPdRedefaultLifetimeBest.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdRedefaultLifetimeBest.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeBestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdRedefaultLifetimeBestDto input)
         {
            var retailPdRedefaultLifetimeBest = ObjectMapper.Map<RetailPdRedefaultLifetimeBest>(input);

			

            await _retailPdRedefaultLifetimeBestRepository.InsertAsync(retailPdRedefaultLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdRedefaultLifetimeBestDto input)
         {
            var retailPdRedefaultLifetimeBest = await _retailPdRedefaultLifetimeBestRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdRedefaultLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdRedefaultLifetimeBestRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdRedefaultLifetimeBests)]
         public async Task<PagedResultDto<RetailPdRedefaultLifetimeBestRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdRedefaultLifetimeBestRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdRedefaultLifetimeBestRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdRedefaultLifetimeBestRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}