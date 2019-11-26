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
	[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests)]
    public class ObePdRedefaultLifetimeBestsAppService : TestDemoAppServiceBase, IObePdRedefaultLifetimeBestsAppService
    {
		 private readonly IRepository<ObePdRedefaultLifetimeBest, Guid> _obePdRedefaultLifetimeBestRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdRedefaultLifetimeBestsAppService(IRepository<ObePdRedefaultLifetimeBest, Guid> obePdRedefaultLifetimeBestRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdRedefaultLifetimeBestRepository = obePdRedefaultLifetimeBestRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdRedefaultLifetimeBestForViewDto>> GetAll(GetAllObePdRedefaultLifetimeBestsInput input)
         {
			
			var filteredObePdRedefaultLifetimeBests = _obePdRedefaultLifetimeBestRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObePdRedefaultLifetimeBests = filteredObePdRedefaultLifetimeBests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdRedefaultLifetimeBests = from o in pagedAndFilteredObePdRedefaultLifetimeBests
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdRedefaultLifetimeBestForViewDto() {
							ObePdRedefaultLifetimeBest = new ObePdRedefaultLifetimeBestDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdRedefaultLifetimeBests.CountAsync();

            return new PagedResultDto<GetObePdRedefaultLifetimeBestForViewDto>(
                totalCount,
                await obePdRedefaultLifetimeBests.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Edit)]
		 public async Task<GetObePdRedefaultLifetimeBestForEditOutput> GetObePdRedefaultLifetimeBestForEdit(EntityDto<Guid> input)
         {
            var obePdRedefaultLifetimeBest = await _obePdRedefaultLifetimeBestRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdRedefaultLifetimeBestForEditOutput {ObePdRedefaultLifetimeBest = ObjectMapper.Map<CreateOrEditObePdRedefaultLifetimeBestDto>(obePdRedefaultLifetimeBest)};

		    if (output.ObePdRedefaultLifetimeBest.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdRedefaultLifetimeBest.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeBestDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Create)]
		 protected virtual async Task Create(CreateOrEditObePdRedefaultLifetimeBestDto input)
         {
            var obePdRedefaultLifetimeBest = ObjectMapper.Map<ObePdRedefaultLifetimeBest>(input);

			

            await _obePdRedefaultLifetimeBestRepository.InsertAsync(obePdRedefaultLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdRedefaultLifetimeBestDto input)
         {
            var obePdRedefaultLifetimeBest = await _obePdRedefaultLifetimeBestRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdRedefaultLifetimeBest);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdRedefaultLifetimeBestRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeBests)]
         public async Task<PagedResultDto<ObePdRedefaultLifetimeBestObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdRedefaultLifetimeBestObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdRedefaultLifetimeBestObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdRedefaultLifetimeBestObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}