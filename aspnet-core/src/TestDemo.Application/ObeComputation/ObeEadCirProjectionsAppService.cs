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
	[AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections)]
    public class ObeEadCirProjectionsAppService : TestDemoAppServiceBase, IObeEadCirProjectionsAppService
    {
		 private readonly IRepository<ObeEadCirProjection, Guid> _obeEadCirProjectionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEadCirProjectionsAppService(IRepository<ObeEadCirProjection, Guid> obeEadCirProjectionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEadCirProjectionRepository = obeEadCirProjectionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEadCirProjectionForViewDto>> GetAll(GetAllObeEadCirProjectionsInput input)
         {
			
			var filteredObeEadCirProjections = _obeEadCirProjectionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.CIR_GROUP.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId == input.ObeEclTenantIdFilter);

			var pagedAndFilteredObeEadCirProjections = filteredObeEadCirProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEadCirProjections = from o in pagedAndFilteredObeEadCirProjections
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEadCirProjectionForViewDto() {
							ObeEadCirProjection = new ObeEadCirProjectionDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEadCirProjections.CountAsync();

            return new PagedResultDto<GetObeEadCirProjectionForViewDto>(
                totalCount,
                await obeEadCirProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections_Edit)]
		 public async Task<GetObeEadCirProjectionForEditOutput> GetObeEadCirProjectionForEdit(EntityDto<Guid> input)
         {
            var obeEadCirProjection = await _obeEadCirProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEadCirProjectionForEditOutput {ObeEadCirProjection = ObjectMapper.Map<CreateOrEditObeEadCirProjectionDto>(obeEadCirProjection)};

		    if (output.ObeEadCirProjection.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEadCirProjection.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEadCirProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections_Create)]
		 protected virtual async Task Create(CreateOrEditObeEadCirProjectionDto input)
         {
            var obeEadCirProjection = ObjectMapper.Map<ObeEadCirProjection>(input);

			

            await _obeEadCirProjectionRepository.InsertAsync(obeEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEadCirProjectionDto input)
         {
            var obeEadCirProjection = await _obeEadCirProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEadCirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEadCirProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEadCirProjections)]
         public async Task<PagedResultDto<ObeEadCirProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEadCirProjectionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEadCirProjectionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEadCirProjectionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}