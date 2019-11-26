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
	[AbpAuthorize(AppPermissions.Pages_RetailPdMappings)]
    public class RetailPdMappingsAppService : TestDemoAppServiceBase, IRetailPdMappingsAppService
    {
		 private readonly IRepository<RetailPdMapping, Guid> _retailPdMappingRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailPdMappingsAppService(IRepository<RetailPdMapping, Guid> retailPdMappingRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailPdMappingRepository = retailPdMappingRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailPdMappingForViewDto>> GetAll(GetAllRetailPdMappingsInput input)
         {
			
			var filteredRetailPdMappings = _retailPdMappingRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredRetailPdMappings = filteredRetailPdMappings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailPdMappings = from o in pagedAndFilteredRetailPdMappings
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailPdMappingForViewDto() {
							RetailPdMapping = new RetailPdMappingDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailPdMappings.CountAsync();

            return new PagedResultDto<GetRetailPdMappingForViewDto>(
                totalCount,
                await retailPdMappings.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailPdMappings_Edit)]
		 public async Task<GetRetailPdMappingForEditOutput> GetRetailPdMappingForEdit(EntityDto<Guid> input)
         {
            var retailPdMapping = await _retailPdMappingRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailPdMappingForEditOutput {RetailPdMapping = ObjectMapper.Map<CreateOrEditRetailPdMappingDto>(retailPdMapping)};

		    if (output.RetailPdMapping.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailPdMapping.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailPdMappingDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdMappings_Create)]
		 protected virtual async Task Create(CreateOrEditRetailPdMappingDto input)
         {
            var retailPdMapping = ObjectMapper.Map<RetailPdMapping>(input);

			

            await _retailPdMappingRepository.InsertAsync(retailPdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdMappings_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailPdMappingDto input)
         {
            var retailPdMapping = await _retailPdMappingRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailPdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailPdMappings_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailPdMappingRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailPdMappings)]
         public async Task<PagedResultDto<RetailPdMappingRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailPdMappingRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailPdMappingRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailPdMappingRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}