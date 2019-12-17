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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs)]
    public class WholesaleEclPdAssumptionMacroeconomicInputsAppService : TestDemoAppServiceBase, IWholesaleEclPdAssumptionMacroeconomicInputsAppService
    {
		 private readonly IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> _wholesaleEclPdAssumptionMacroeconomicInputRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclPdAssumptionMacroeconomicInputsAppService(IRepository<WholesaleEclPdAssumptionMacroeconomicInput, Guid> wholesaleEclPdAssumptionMacroeconomicInputRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclPdAssumptionMacroeconomicInputRepository = wholesaleEclPdAssumptionMacroeconomicInputRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionMacroeconomicInputsInput input)
         {
			
			var filteredWholesaleEclPdAssumptionMacroeconomicInputs = _wholesaleEclPdAssumptionMacroeconomicInputRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

			var pagedAndFilteredWholesaleEclPdAssumptionMacroeconomicInputs = filteredWholesaleEclPdAssumptionMacroeconomicInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclPdAssumptionMacroeconomicInputs = from o in pagedAndFilteredWholesaleEclPdAssumptionMacroeconomicInputs
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto() {
							WholesaleEclPdAssumptionMacroeconomicInput = new WholesaleEclPdAssumptionMacroeconomicInputDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclPdAssumptionMacroeconomicInputs.CountAsync();

            return new PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto>(
                totalCount,
                await wholesaleEclPdAssumptionMacroeconomicInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Edit)]
		 public async Task<GetWholesaleEclPdAssumptionMacroeconomicInputForEditOutput> GetWholesaleEclPdAssumptionMacroeconomicInputForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclPdAssumptionMacroeconomicInput = await _wholesaleEclPdAssumptionMacroeconomicInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclPdAssumptionMacroeconomicInputForEditOutput {WholesaleEclPdAssumptionMacroeconomicInput = ObjectMapper.Map<CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto>(wholesaleEclPdAssumptionMacroeconomicInput)};

		    if (output.WholesaleEclPdAssumptionMacroeconomicInput.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclPdAssumptionMacroeconomicInput.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto input)
         {
            var wholesaleEclPdAssumptionMacroeconomicInput = ObjectMapper.Map<WholesaleEclPdAssumptionMacroeconomicInput>(input);

			

            await _wholesaleEclPdAssumptionMacroeconomicInputRepository.InsertAsync(wholesaleEclPdAssumptionMacroeconomicInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto input)
         {
            var wholesaleEclPdAssumptionMacroeconomicInput = await _wholesaleEclPdAssumptionMacroeconomicInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclPdAssumptionMacroeconomicInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclPdAssumptionMacroeconomicInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs)]
         public async Task<PagedResultDto<WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}