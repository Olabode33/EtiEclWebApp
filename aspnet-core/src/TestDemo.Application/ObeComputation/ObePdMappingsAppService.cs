using TestDemo.OBE;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeComputation
{
	[AbpAuthorize(AppPermissions.Pages_ObePdMappings)]
    public class ObePdMappingsAppService : TestDemoAppServiceBase, IObePdMappingsAppService
    {
		 private readonly IRepository<ObePdMapping, Guid> _obePdMappingRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdMappingsAppService(IRepository<ObePdMapping, Guid> obePdMappingRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdMappingRepository = obePdMappingRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdMappingForViewDto>> GetAll(GetAllObePdMappingsInput input)
         {
			
			var filteredObePdMappings = _obePdMappingRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObePdMappings = filteredObePdMappings
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdMappings = from o in pagedAndFilteredObePdMappings
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdMappingForViewDto() {
							ObePdMapping = new ObePdMappingDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdMappings.CountAsync();

            return new PagedResultDto<GetObePdMappingForViewDto>(
                totalCount,
                await obePdMappings.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdMappings_Edit)]
		 public async Task<GetObePdMappingForEditOutput> GetObePdMappingForEdit(EntityDto<Guid> input)
         {
            var obePdMapping = await _obePdMappingRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdMappingForEditOutput {ObePdMapping = ObjectMapper.Map<CreateOrEditObePdMappingDto>(obePdMapping)};

		    if (output.ObePdMapping.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdMapping.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdMappingDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdMappings_Create)]
		 protected virtual async Task Create(CreateOrEditObePdMappingDto input)
         {
            var obePdMapping = ObjectMapper.Map<ObePdMapping>(input);

			

            await _obePdMappingRepository.InsertAsync(obePdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdMappings_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdMappingDto input)
         {
            var obePdMapping = await _obePdMappingRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdMapping);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdMappings_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdMappingRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdMappings)]
         public async Task<PagedResultDto<ObePdMappingObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdMappingObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdMappingObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdMappingObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}