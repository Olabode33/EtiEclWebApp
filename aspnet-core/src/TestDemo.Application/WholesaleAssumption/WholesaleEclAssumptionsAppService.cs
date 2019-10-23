using TestDemo.Wholesale;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleAssumption
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions)]
    public class WholesaleEclAssumptionsAppService : TestDemoAppServiceBase, IWholesaleEclAssumptionsAppService
    {
		 private readonly IRepository<WholesaleEclAssumption, Guid> _wholesaleEclAssumptionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclAssumptionsAppService(IRepository<WholesaleEclAssumption, Guid> wholesaleEclAssumptionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclAssumptionRepository = wholesaleEclAssumptionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclAssumptionForViewDto>> GetAll(GetAllWholesaleEclAssumptionsInput input)
         {
			
			var filteredWholesaleEclAssumptions = _wholesaleEclAssumptionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter );

			var pagedAndFilteredWholesaleEclAssumptions = filteredWholesaleEclAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclAssumptions = from o in pagedAndFilteredWholesaleEclAssumptions
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclAssumptionForViewDto() {
							WholesaleEclAssumption = new WholesaleEclAssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                IsComputed = o.IsComputed,
                                AssumptionGroup = o.AssumptionGroup,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclAssumptions.CountAsync();

            return new PagedResultDto<GetWholesaleEclAssumptionForViewDto>(
                totalCount,
                await wholesaleEclAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions_Edit)]
		 public async Task<GetWholesaleEclAssumptionForEditOutput> GetWholesaleEclAssumptionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclAssumption = await _wholesaleEclAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclAssumptionForEditOutput {WholesaleEclAssumption = ObjectMapper.Map<CreateOrEditWholesaleEclAssumptionDto>(wholesaleEclAssumption)};

		    if (output.WholesaleEclAssumption.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclAssumption.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclAssumptionDto input)
         {
            var wholesaleEclAssumption = ObjectMapper.Map<WholesaleEclAssumption>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclAssumption.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclAssumptionRepository.InsertAsync(wholesaleEclAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclAssumptionDto input)
         {
            var wholesaleEclAssumption = await _wholesaleEclAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclAssumptions)]
         public async Task<PagedResultDto<WholesaleEclAssumptionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclAssumptionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclAssumptionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclAssumptionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}