using TestDemo.Retail;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailComputation
{
	[AbpAuthorize(AppPermissions.Pages_RetailEadInputs)]
    public class RetailEadInputsAppService : TestDemoAppServiceBase, IRetailEadInputsAppService
    {
		 private readonly IRepository<RetailEadInput, Guid> _retailEadInputRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEadInputsAppService(IRepository<RetailEadInput, Guid> retailEadInputRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEadInputRepository = retailEadInputRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEadInputForViewDto>> GetAll(GetAllRetailEadInputsInput input)
         {
			
			var filteredRetailEadInputs = _retailEadInputRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter) || e.EIR_GROUP.Contains(input.Filter) || e.CIR_GROUP.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId == input.RetailEclTenantIdFilter);

			var pagedAndFilteredRetailEadInputs = filteredRetailEadInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEadInputs = from o in pagedAndFilteredRetailEadInputs
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEadInputForViewDto() {
							RetailEadInput = new RetailEadInputDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEadInputs.CountAsync();

            return new PagedResultDto<GetRetailEadInputForViewDto>(
                totalCount,
                await retailEadInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEadInputs_Edit)]
		 public async Task<GetRetailEadInputForEditOutput> GetRetailEadInputForEdit(EntityDto<Guid> input)
         {
            var retailEadInput = await _retailEadInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEadInputForEditOutput {RetailEadInput = ObjectMapper.Map<CreateOrEditRetailEadInputDto>(retailEadInput)};

		    if (output.RetailEadInput.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEadInput.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEadInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadInputs_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEadInputDto input)
         {
            var retailEadInput = ObjectMapper.Map<RetailEadInput>(input);

			

            await _retailEadInputRepository.InsertAsync(retailEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEadInputDto input)
         {
            var retailEadInput = await _retailEadInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEadInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEadInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEadInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEadInputs)]
         public async Task<PagedResultDto<RetailEadInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEadInputRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEadInputRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEadInputRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}