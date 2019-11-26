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
	[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns)]
    public class ObePdLifetimeDownturnsAppService : TestDemoAppServiceBase, IObePdLifetimeDownturnsAppService
    {
		 private readonly IRepository<ObePdLifetimeDownturn, Guid> _obePdLifetimeDownturnRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObePdLifetimeDownturnsAppService(IRepository<ObePdLifetimeDownturn, Guid> obePdLifetimeDownturnRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obePdLifetimeDownturnRepository = obePdLifetimeDownturnRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObePdLifetimeDownturnForViewDto>> GetAll(GetAllObePdLifetimeDownturnsInput input)
         {
			
			var filteredObePdLifetimeDownturns = _obePdLifetimeDownturnRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredObePdLifetimeDownturns = filteredObePdLifetimeDownturns
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obePdLifetimeDownturns = from o in pagedAndFilteredObePdLifetimeDownturns
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObePdLifetimeDownturnForViewDto() {
							ObePdLifetimeDownturn = new ObePdLifetimeDownturnDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObePdLifetimeDownturns.CountAsync();

            return new PagedResultDto<GetObePdLifetimeDownturnForViewDto>(
                totalCount,
                await obePdLifetimeDownturns.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns_Edit)]
		 public async Task<GetObePdLifetimeDownturnForEditOutput> GetObePdLifetimeDownturnForEdit(EntityDto<Guid> input)
         {
            var obePdLifetimeDownturn = await _obePdLifetimeDownturnRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObePdLifetimeDownturnForEditOutput {ObePdLifetimeDownturn = ObjectMapper.Map<CreateOrEditObePdLifetimeDownturnDto>(obePdLifetimeDownturn)};

		    if (output.ObePdLifetimeDownturn.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObePdLifetimeDownturn.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObePdLifetimeDownturnDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns_Create)]
		 protected virtual async Task Create(CreateOrEditObePdLifetimeDownturnDto input)
         {
            var obePdLifetimeDownturn = ObjectMapper.Map<ObePdLifetimeDownturn>(input);

			

            await _obePdLifetimeDownturnRepository.InsertAsync(obePdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns_Edit)]
		 protected virtual async Task Update(CreateOrEditObePdLifetimeDownturnDto input)
         {
            var obePdLifetimeDownturn = await _obePdLifetimeDownturnRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obePdLifetimeDownturn);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obePdLifetimeDownturnRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObePdLifetimeDownturns)]
         public async Task<PagedResultDto<ObePdLifetimeDownturnObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObePdLifetimeDownturnObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObePdLifetimeDownturnObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObePdLifetimeDownturnObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}