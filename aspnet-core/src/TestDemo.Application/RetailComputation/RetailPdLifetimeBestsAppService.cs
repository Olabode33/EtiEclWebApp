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
	[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests)]
    public class RetailPdLifetimeBestsAppService : TestDemoAppServiceBase, IRetailPdLifetimeBestsAppService
    {
		 private readonly IRepository<RetailPdLifetimeBest, Guid> _retailPdLifetimeBestRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdLifetimeBestsAppService(IRepository<RetailPdLifetimeBest, Guid> retailPdLifetimeBestRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdLifetimeBestRepository = retailPdLifetimeBestRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdLifetimeBestForViewDto>> GetAll(GetAllRetailPdLifetimeBestsInput input)
         {
			
			var filteredRetailPdLifetimeBests = _retailPdLifetimeBestRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredRetailPdLifetimeBests = filteredRetailPdLifetimeBests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdLifetimeBests = from o in pagedAndFilteredRetailPdLifetimeBests
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdLifetimeBestForViewDto() {
							RetailPdLifetimeBest = new RetailPdLifetimeBestDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdLifetimeBests.CountAsync();

            return new PagedResultDto<GetRetailPdLifetimeBestForViewDto>(
                totalCount,
                await retailPdLifetimeBests.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests_Edit)]
		 public async Task<GetRetailPdLifetimeBestForEditOutput> GetRetailPdLifetimeBestForEdit(EntityDto<Guid> input)
         {
            var retailPdLifetimeBest = await _retailPdLifetimeBestRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdLifetimeBestForEditOutput {RetailPdLifetimeBest = ObjectMapper.Map<CreateOrEditRetailPdLifetimeBestDto>(retailPdLifetimeBest)};

		    if (output.RetailPdLifetimeBest.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdLifetimeBest.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdLifetimeBestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdLifetimeBestDto input)
         {
            var retailPdLifetimeBest = ObjectMapper.Map<RetailPdLifetimeBest>(input);

			

            await _retailPdLifetimeBestRepository.InsertAsync(retailPdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdLifetimeBestDto input)
         {
            var retailPdLifetimeBest = await _retailPdLifetimeBestRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdLifetimeBestRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdLifetimeBests)]
         public async Task<PagedResultDto<RetailPdLifetimeBestRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdLifetimeBestRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdLifetimeBestRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdLifetimeBestRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}