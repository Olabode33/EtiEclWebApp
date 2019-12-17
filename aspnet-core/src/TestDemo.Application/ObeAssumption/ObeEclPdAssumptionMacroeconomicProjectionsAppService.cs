using TestDemo.OBE;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeAssumption
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections)]
    public class ObeEclPdAssumptionMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IObeEclPdAssumptionMacroeconomicProjectionsAppService
    {
		 private readonly IRepository<ObeEclPdAssumptionMacroeconomicProjection, Guid> _obeEclPdAssumptionMacroeconomicProjectionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumptionMacroeconomicProjectionsAppService(IRepository<ObeEclPdAssumptionMacroeconomicProjection, Guid> obeEclPdAssumptionMacroeconomicProjectionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumptionMacroeconomicProjectionRepository = obeEclPdAssumptionMacroeconomicProjectionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllObeEclPdAssumptionMacroeconomicProjectionsInput input)
         {
			
			var filteredObeEclPdAssumptionMacroeconomicProjections = _obeEclPdAssumptionMacroeconomicProjectionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

			var pagedAndFilteredObeEclPdAssumptionMacroeconomicProjections = filteredObeEclPdAssumptionMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumptionMacroeconomicProjections = from o in pagedAndFilteredObeEclPdAssumptionMacroeconomicProjections
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumptionMacroeconomicProjectionForViewDto() {
							ObeEclPdAssumptionMacroeconomicProjection = new ObeEclPdAssumptionMacroeconomicProjectionDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumptionMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumptionMacroeconomicProjectionForViewDto>(
                totalCount,
                await obeEclPdAssumptionMacroeconomicProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Edit)]
		 public async Task<GetObeEclPdAssumptionMacroeconomicProjectionForEditOutput> GetObeEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumptionMacroeconomicProjection = await _obeEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumptionMacroeconomicProjectionForEditOutput {ObeEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto>(obeEclPdAssumptionMacroeconomicProjection)};

		    if (output.ObeEclPdAssumptionMacroeconomicProjection.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumptionMacroeconomicProjection.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto input)
         {
            var obeEclPdAssumptionMacroeconomicProjection = ObjectMapper.Map<ObeEclPdAssumptionMacroeconomicProjection>(input);

			

            await _obeEclPdAssumptionMacroeconomicProjectionRepository.InsertAsync(obeEclPdAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto input)
         {
            var obeEclPdAssumptionMacroeconomicProjection = await _obeEclPdAssumptionMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumptionMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumptionMacroeconomicProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections)]
         public async Task<PagedResultDto<ObeEclPdAssumptionMacroeconomicProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumptionMacroeconomicProjectionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumptionMacroeconomicProjectionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumptionMacroeconomicProjectionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}