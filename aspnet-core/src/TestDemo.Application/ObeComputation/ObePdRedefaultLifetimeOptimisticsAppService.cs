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
	[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics)]
    public class ObePdRedefaultLifetimeOptimisticsAppService : TestDemoAppServiceBase, IObePdRedefaultLifetimeOptimisticsAppService
    {
		 private readonly IRepository<ObePdRedefaultLifetimeOptimistic, Guid> _obePdRedefaultLifetimeOptimisticRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdRedefaultLifetimeOptimisticsAppService(IRepository<ObePdRedefaultLifetimeOptimistic, Guid> obePdRedefaultLifetimeOptimisticRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdRedefaultLifetimeOptimisticRepository = obePdRedefaultLifetimeOptimisticRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllObePdRedefaultLifetimeOptimisticsInput input)
         {
			
			var filteredObePdRedefaultLifetimeOptimistics = _obePdRedefaultLifetimeOptimisticRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredObePdRedefaultLifetimeOptimistics = filteredObePdRedefaultLifetimeOptimistics
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdRedefaultLifetimeOptimistics = from o in pagedAndFilteredObePdRedefaultLifetimeOptimistics
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdRedefaultLifetimeOptimisticForViewDto() {
							ObePdRedefaultLifetimeOptimistic = new ObePdRedefaultLifetimeOptimisticDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdRedefaultLifetimeOptimistics.CountAsync();

            return new PagedResultDto<GetObePdRedefaultLifetimeOptimisticForViewDto>(
                totalCount,
                await obePdRedefaultLifetimeOptimistics.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Edit)]
		 public async Task<GetObePdRedefaultLifetimeOptimisticForEditOutput> GetObePdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input)
         {
            var obePdRedefaultLifetimeOptimistic = await _obePdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdRedefaultLifetimeOptimisticForEditOutput {ObePdRedefaultLifetimeOptimistic = ObjectMapper.Map<CreateOrEditObePdRedefaultLifetimeOptimisticDto>(obePdRedefaultLifetimeOptimistic)};

		    if (output.ObePdRedefaultLifetimeOptimistic.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdRedefaultLifetimeOptimistic.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeOptimisticDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Create)]
		 protected virtual async Task Create(CreateOrEditObePdRedefaultLifetimeOptimisticDto input)
         {
            var obePdRedefaultLifetimeOptimistic = ObjectMapper.Map<ObePdRedefaultLifetimeOptimistic>(input);

			

            await _obePdRedefaultLifetimeOptimisticRepository.InsertAsync(obePdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdRedefaultLifetimeOptimisticDto input)
         {
            var obePdRedefaultLifetimeOptimistic = await _obePdRedefaultLifetimeOptimisticRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdRedefaultLifetimeOptimistic);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdRedefaultLifetimeOptimisticRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics)]
         public async Task<PagedResultDto<ObePdRedefaultLifetimeOptimisticObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdRedefaultLifetimeOptimisticObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdRedefaultLifetimeOptimisticObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdRedefaultLifetimeOptimisticObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}