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
	[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes)]
    public class ObeEclPdAssumptionNplIndexesAppService : TestDemoAppServiceBase, IObeEclPdAssumptionNplIndexesAppService
    {
		 private readonly IRepository<ObeEclPdAssumptionNplIndex, Guid> _obeEclPdAssumptionNplIndexRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumptionNplIndexesAppService(IRepository<ObeEclPdAssumptionNplIndex, Guid> obeEclPdAssumptionNplIndexRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumptionNplIndexRepository = obeEclPdAssumptionNplIndexRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllObeEclPdAssumptionNplIndexesInput input)
         {
			
			var filteredObeEclPdAssumptionNplIndexes = _obeEclPdAssumptionNplIndexRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter));

			var pagedAndFilteredObeEclPdAssumptionNplIndexes = filteredObeEclPdAssumptionNplIndexes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumptionNplIndexes = from o in pagedAndFilteredObeEclPdAssumptionNplIndexes
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumptionNplIndexForViewDto() {
							ObeEclPdAssumptionNplIndex = new ObeEclPdAssumptionNplIndexDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumptionNplIndexes.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumptionNplIndexForViewDto>(
                totalCount,
                await obeEclPdAssumptionNplIndexes.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Edit)]
		 public async Task<GetObeEclPdAssumptionNplIndexForEditOutput> GetObeEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumptionNplIndex = await _obeEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumptionNplIndexForEditOutput {ObeEclPdAssumptionNplIndex = ObjectMapper.Map<CreateOrEditObeEclPdAssumptionNplIndexDto>(obeEclPdAssumptionNplIndex)};

		    if (output.ObeEclPdAssumptionNplIndex.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumptionNplIndex.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumptionNplIndexDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclPdAssumptionNplIndexDto input)
         {
            var obeEclPdAssumptionNplIndex = ObjectMapper.Map<ObeEclPdAssumptionNplIndex>(input);

			

            await _obeEclPdAssumptionNplIndexRepository.InsertAsync(obeEclPdAssumptionNplIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclPdAssumptionNplIndexDto input)
         {
            var obeEclPdAssumptionNplIndex = await _obeEclPdAssumptionNplIndexRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumptionNplIndex);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumptionNplIndexRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes)]
         public async Task<PagedResultDto<ObeEclPdAssumptionNplIndexObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumptionNplIndexObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumptionNplIndexObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumptionNplIndexObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}