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
	[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests)]
    public class ObePdLifetimeBestsAppService : TestDemoAppServiceBase, IObePdLifetimeBestsAppService
    {
		 private readonly IRepository<ObePdLifetimeBest, Guid> _obePdLifetimeBestRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdLifetimeBestsAppService(IRepository<ObePdLifetimeBest, Guid> obePdLifetimeBestRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdLifetimeBestRepository = obePdLifetimeBestRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdLifetimeBestForViewDto>> GetAll(GetAllObePdLifetimeBestsInput input)
         {
			
			var filteredObePdLifetimeBests = _obePdLifetimeBestRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObePdLifetimeBests = filteredObePdLifetimeBests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdLifetimeBests = from o in pagedAndFilteredObePdLifetimeBests
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdLifetimeBestForViewDto() {
							ObePdLifetimeBest = new ObePdLifetimeBestDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdLifetimeBests.CountAsync();

            return new PagedResultDto<GetObePdLifetimeBestForViewDto>(
                totalCount,
                await obePdLifetimeBests.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests_Edit)]
		 public async Task<GetObePdLifetimeBestForEditOutput> GetObePdLifetimeBestForEdit(EntityDto<Guid> input)
         {
            var obePdLifetimeBest = await _obePdLifetimeBestRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdLifetimeBestForEditOutput {ObePdLifetimeBest = ObjectMapper.Map<CreateOrEditObePdLifetimeBestDto>(obePdLifetimeBest)};

		    if (output.ObePdLifetimeBest.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdLifetimeBest.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdLifetimeBestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests_Create)]
		 protected virtual async Task Create(CreateOrEditObePdLifetimeBestDto input)
         {
            var obePdLifetimeBest = ObjectMapper.Map<ObePdLifetimeBest>(input);

			

            await _obePdLifetimeBestRepository.InsertAsync(obePdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdLifetimeBestDto input)
         {
            var obePdLifetimeBest = await _obePdLifetimeBestRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdLifetimeBestRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeBests)]
         public async Task<PagedResultDto<ObePdLifetimeBestObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdLifetimeBestObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdLifetimeBestObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdLifetimeBestObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}