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
	[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics)]
    public class ObePdLifetimeOptimisticsAppService : TestDemoAppServiceBase, IObePdLifetimeOptimisticsAppService
    {
		 private readonly IRepository<ObePdLifetimeOptimistic, Guid> _obePdLifetimeOptimisticRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdLifetimeOptimisticsAppService(IRepository<ObePdLifetimeOptimistic, Guid> obePdLifetimeOptimisticRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdLifetimeOptimisticRepository = obePdLifetimeOptimisticRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdLifetimeOptimisticForViewDto>> GetAll(GetAllObePdLifetimeOptimisticsInput input)
         {
			
			var filteredObePdLifetimeOptimistics = _obePdLifetimeOptimisticRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObePdLifetimeOptimistics = filteredObePdLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdLifetimeOptimistics = from o in pagedAndFilteredObePdLifetimeOptimistics
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdLifetimeOptimisticForViewDto() {
							ObePdLifetimeOptimistic = new ObePdLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetObePdLifetimeOptimisticForViewDto>(
                totalCount,
                await obePdLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics_Edit)]
		 public async Task<GetObePdLifetimeOptimisticForEditOutput> GetObePdLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var obePdLifetimeOptimistic = await _obePdLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdLifetimeOptimisticForEditOutput {ObePdLifetimeOptimistic = ObjectMapper.Map<CreateOrEditObePdLifetimeOptimisticDto>(obePdLifetimeOptimistic)};

		    if (output.ObePdLifetimeOptimistic.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdLifetimeOptimistic.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditObePdLifetimeOptimisticDto input)
         {
            var obePdLifetimeOptimistic = ObjectMapper.Map<ObePdLifetimeOptimistic>(input);

			

            await _obePdLifetimeOptimisticRepository.InsertAsync(obePdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdLifetimeOptimisticDto input)
         {
            var obePdLifetimeOptimistic = await _obePdLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeOptimistics)]
         public async Task<PagedResultDto<ObePdLifetimeOptimisticObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdLifetimeOptimisticObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdLifetimeOptimisticObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdLifetimeOptimisticObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}