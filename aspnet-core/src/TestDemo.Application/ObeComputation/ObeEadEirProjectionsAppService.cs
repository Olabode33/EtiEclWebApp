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
	[AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections)]
    public class ObeEadEirProjectionsAppService : TestDemoAppServiceBase, IObeEadEirProjectionsAppService
    {
		 private readonly IRepository<ObeEadEirProjection, Guid> _obeEadEirProjectionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEadEirProjectionsAppService(IRepository<ObeEadEirProjection, Guid> obeEadEirProjectionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEadEirProjectionRepository = obeEadEirProjectionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEadEirProjectionForViewDto>> GetAll(GetAllObeEadEirProjectionsInput input)
         {
			
			var filteredObeEadEirProjections = _obeEadEirProjectionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.EIR_GROUP.Contains(input.Filter));

			var pagedAndFilteredObeEadEirProjections = filteredObeEadEirProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEadEirProjections = from o in pagedAndFilteredObeEadEirProjections
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEadEirProjectionForViewDto() {
							ObeEadEirProjection = new ObeEadEirProjectionDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEadEirProjections.CountAsync();

            return new PagedResultDto<GetObeEadEirProjectionForViewDto>(
                totalCount,
                await obeEadEirProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections_Edit)]
		 public async Task<GetObeEadEirProjectionForEditOutput> GetObeEadEirProjectionForEdit(EntityDto<Guid> input)
         {
            var obeEadEirProjection = await _obeEadEirProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEadEirProjectionForEditOutput {ObeEadEirProjection = ObjectMapper.Map<CreateOrEditObeEadEirProjectionDto>(obeEadEirProjection)};

		    if (output.ObeEadEirProjection.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEadEirProjection.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEadEirProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections_Create)]
		 protected virtual async Task Create(CreateOrEditObeEadEirProjectionDto input)
         {
            var obeEadEirProjection = ObjectMapper.Map<ObeEadEirProjection>(input);

			

            await _obeEadEirProjectionRepository.InsertAsync(obeEadEirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEadEirProjectionDto input)
         {
            var obeEadEirProjection = await _obeEadEirProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEadEirProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEadEirProjectionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEadEirProjections)]
         public async Task<PagedResultDto<ObeEadEirProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEadEirProjectionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEadEirProjectionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEadEirProjectionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}