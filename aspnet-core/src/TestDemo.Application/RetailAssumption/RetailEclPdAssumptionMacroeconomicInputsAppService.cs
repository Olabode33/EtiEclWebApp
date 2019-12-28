﻿using TestDemo.Retail;

using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailAssumption
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs)]
    public class RetailEclPdAssumptionMacroeconomicInputsAppService : TestDemoAppServiceBase, IRetailEclPdAssumptionMacroeconomicInputsAppService
    {
		 private readonly IRepository<RetailEclPdAssumptionMacroeconomicInput, Guid> _retailEclPdAssumptionMacroeconomicInputRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclPdAssumptionMacroeconomicInputsAppService(IRepository<RetailEclPdAssumptionMacroeconomicInput, Guid> retailEclPdAssumptionMacroeconomicInputRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclPdAssumptionMacroeconomicInputRepository = retailEclPdAssumptionMacroeconomicInputRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclPdAssumptionMacroeconomicInputForViewDto>> GetAll(GetAllRetailEclPdAssumptionMacroeconomicInputsInput input)
         {
			
			var filteredRetailEclPdAssumptionMacroeconomicInputs = _retailEclPdAssumptionMacroeconomicInputRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredRetailEclPdAssumptionMacroeconomicInputs = filteredRetailEclPdAssumptionMacroeconomicInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclPdAssumptionMacroeconomicInputs = from o in pagedAndFilteredRetailEclPdAssumptionMacroeconomicInputs
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclPdAssumptionMacroeconomicInputForViewDto() {
							RetailEclPdAssumptionMacroeconomicInput = new RetailEclPdAssumptionMacroeconomicInputDto
							{
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclPdAssumptionMacroeconomicInputs.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumptionMacroeconomicInputForViewDto>(
                totalCount,
                await retailEclPdAssumptionMacroeconomicInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Edit)]
		 public async Task<GetRetailEclPdAssumptionMacroeconomicInputForEditOutput> GetRetailEclPdAssumptionMacroeconomicInputForEdit(EntityDto<Guid> input)
         {
            var retailEclPdAssumptionMacroeconomicInput = await _retailEclPdAssumptionMacroeconomicInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclPdAssumptionMacroeconomicInputForEditOutput {RetailEclPdAssumptionMacroeconomicInput = ObjectMapper.Map<CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto>(retailEclPdAssumptionMacroeconomicInput)};

		    if (output.RetailEclPdAssumptionMacroeconomicInput.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumptionMacroeconomicInput.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto input)
         {
            var retailEclPdAssumptionMacroeconomicInput = ObjectMapper.Map<RetailEclPdAssumptionMacroeconomicInput>(input);

			

            await _retailEclPdAssumptionMacroeconomicInputRepository.InsertAsync(retailEclPdAssumptionMacroeconomicInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto input)
         {
            var retailEclPdAssumptionMacroeconomicInput = await _retailEclPdAssumptionMacroeconomicInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclPdAssumptionMacroeconomicInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclPdAssumptionMacroeconomicInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs)]
         public async Task<PagedResultDto<RetailEclPdAssumptionMacroeconomicInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclPdAssumptionMacroeconomicInputRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclPdAssumptionMacroeconomicInputRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclPdAssumptionMacroeconomicInputRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}