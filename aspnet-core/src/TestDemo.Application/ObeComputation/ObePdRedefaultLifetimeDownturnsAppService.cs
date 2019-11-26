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
	[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns)]
    public class ObePdRedefaultLifetimeDownturnsAppService : TestDemoAppServiceBase, IObePdRedefaultLifetimeDownturnsAppService
    {
		 private readonly IRepository<ObePdRedefaultLifetimeDownturn, Guid> _obePdRedefaultLifetimeDownturnRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdRedefaultLifetimeDownturnsAppService(IRepository<ObePdRedefaultLifetimeDownturn, Guid> obePdRedefaultLifetimeDownturnRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdRedefaultLifetimeDownturnRepository = obePdRedefaultLifetimeDownturnRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllObePdRedefaultLifetimeDownturnsInput input)
         {
			
			var filteredObePdRedefaultLifetimeDownturns = _obePdRedefaultLifetimeDownturnRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObePdRedefaultLifetimeDownturns = filteredObePdRedefaultLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdRedefaultLifetimeDownturns = from o in pagedAndFilteredObePdRedefaultLifetimeDownturns
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdRedefaultLifetimeDownturnForViewDto() {
							ObePdRedefaultLifetimeDownturn = new ObePdRedefaultLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdRedefaultLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetObePdRedefaultLifetimeDownturnForViewDto>(
                totalCount,
                await obePdRedefaultLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Edit)]
		 public async Task<GetObePdRedefaultLifetimeDownturnForEditOutput> GetObePdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var obePdRedefaultLifetimeDownturn = await _obePdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdRedefaultLifetimeDownturnForEditOutput {ObePdRedefaultLifetimeDownturn = ObjectMapper.Map<CreateOrEditObePdRedefaultLifetimeDownturnDto>(obePdRedefaultLifetimeDownturn)};

		    if (output.ObePdRedefaultLifetimeDownturn.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdRedefaultLifetimeDownturn.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditObePdRedefaultLifetimeDownturnDto input)
         {
            var obePdRedefaultLifetimeDownturn = ObjectMapper.Map<ObePdRedefaultLifetimeDownturn>(input);

			

            await _obePdRedefaultLifetimeDownturnRepository.InsertAsync(obePdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdRedefaultLifetimeDownturnDto input)
         {
            var obePdRedefaultLifetimeDownturn = await _obePdRedefaultLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdRedefaultLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdRedefaultLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns)]
         public async Task<PagedResultDto<ObePdRedefaultLifetimeDownturnObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdRedefaultLifetimeDownturnObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdRedefaultLifetimeDownturnObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdRedefaultLifetimeDownturnObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}