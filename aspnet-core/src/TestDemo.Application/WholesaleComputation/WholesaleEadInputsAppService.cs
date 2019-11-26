using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleComputation
{
	[AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs)]
    public class WholesaleEadInputsAppService : TestDemoAppServiceBase, IWholesaleEadInputsAppService
    {
		 private readonly IRepository<WholesaleEadInput, Guid> _wholesaleEadInputRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEadInputsAppService(IRepository<WholesaleEadInput, Guid> wholesaleEadInputRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEadInputRepository = wholesaleEadInputRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEadInputForViewDto>> GetAll(GetAllWholesaleEadInputsInput input)
         {
			
			var filteredWholesaleEadInputs = _wholesaleEadInputRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.EIR_GROUP.Contains(input.Filter) || e.CIR_GROUP.Contains(input.Filter));

			var pagedAndFilteredWholesaleEadInputs = filteredWholesaleEadInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEadInputs = from o in pagedAndFilteredWholesaleEadInputs
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEadInputForViewDto() {
							WholesaleEadInput = new WholesaleEadInputDto
							{
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEadInputs.CountAsync();

            return new PagedResultDto<GetWholesaleEadInputForViewDto>(
                totalCount,
                await wholesaleEadInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs_Edit)]
		 public async Task<GetWholesaleEadInputForEditOutput> GetWholesaleEadInputForEdit(EntityDto<Guid> input)
         {
            var wholesaleEadInput = await _wholesaleEadInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEadInputForEditOutput {WholesaleEadInput = ObjectMapper.Map<CreateOrEditWholesaleEadInputDto>(wholesaleEadInput)};

		    if (output.WholesaleEadInput.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEadInput.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEadInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEadInputDto input)
         {
            var wholesaleEadInput = ObjectMapper.Map<WholesaleEadInput>(input);

			

            await _wholesaleEadInputRepository.InsertAsync(wholesaleEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEadInputDto input)
         {
            var wholesaleEadInput = await _wholesaleEadInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEadInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEadInputs)]
         public async Task<PagedResultDto<WholesaleEadInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEadInputWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEadInputWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEadInputWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}