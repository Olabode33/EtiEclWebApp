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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions)]
    public class WholesaleEadInputAssumptionsAppService : TestDemoAppServiceBase, IWholesaleEadInputAssumptionsAppService
    {
		 private readonly IRepository<WholesaleEclEadInputAssumption, Guid> _wholesaleEadInputAssumptionRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEadInputAssumptionsAppService(IRepository<WholesaleEclEadInputAssumption, Guid> wholesaleEadInputAssumptionRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEadInputAssumptionRepository = wholesaleEadInputAssumptionRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEadInputAssumptionForViewDto>> GetAll(GetAllWholesaleEadInputAssumptionsInput input)
         {
			
			var filteredWholesaleEadInputAssumptions = _wholesaleEadInputAssumptionRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEadInputAssumptions = filteredWholesaleEadInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEadInputAssumptions = from o in pagedAndFilteredWholesaleEadInputAssumptions
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEadInputAssumptionForViewDto() {
							WholesaleEadInputAssumption = new WholesaleEadInputAssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                EadGroup = o.EadGroup,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEadInputAssumptions.CountAsync();

            return new PagedResultDto<GetWholesaleEadInputAssumptionForViewDto>(
                totalCount,
                await wholesaleEadInputAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions_Edit)]
		 public async Task<GetWholesaleEadInputAssumptionForEditOutput> GetWholesaleEadInputAssumptionForEdit(EntityDto<Guid> input)
         {
            var wholesaleEadInputAssumption = await _wholesaleEadInputAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEadInputAssumptionForEditOutput {WholesaleEadInputAssumption = ObjectMapper.Map<CreateOrEditWholesaleEadInputAssumptionDto>(wholesaleEadInputAssumption)};

		    if (output.WholesaleEadInputAssumption.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEadInputAssumption.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEadInputAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEadInputAssumptionDto input)
         {
            var wholesaleEadInputAssumption = ObjectMapper.Map<WholesaleEclEadInputAssumption>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEadInputAssumption.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEadInputAssumptionRepository.InsertAsync(wholesaleEadInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEadInputAssumptionDto input)
         {
            var wholesaleEadInputAssumption = await _wholesaleEadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEadInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEadInputAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEadInputAssumptions)]
         public async Task<PagedResultDto<WholesaleEadInputAssumptionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEadInputAssumptionWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEadInputAssumptionWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEadInputAssumptionWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}