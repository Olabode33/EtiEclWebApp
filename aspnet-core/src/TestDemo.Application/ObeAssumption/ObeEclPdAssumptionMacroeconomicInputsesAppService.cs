using TestDemo.OBE;

using TestDemo.EclShared;
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
    public class ObeEclPdAssumptionMacroeconomicInputsesAppService : TestDemoAppServiceBase, IObeEclPdAssumptionMacroeconomicInputsesAppService
    {
		 private readonly IRepository<ObeEclPdAssumptionMacroeconomicInputs, Guid> _obeEclPdAssumptionMacroeconomicInputsRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumptionMacroeconomicInputsesAppService(IRepository<ObeEclPdAssumptionMacroeconomicInputs, Guid> obeEclPdAssumptionMacroeconomicInputsRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumptionMacroeconomicInputsRepository = obeEclPdAssumptionMacroeconomicInputsRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumptionMacroeconomicInputsForViewDto>> GetAll(GetAllObeEclPdAssumptionMacroeconomicInputsesInput input)
         {
			
			var filteredObeEclPdAssumptionMacroeconomicInputses = _obeEclPdAssumptionMacroeconomicInputsRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter));

			var pagedAndFilteredObeEclPdAssumptionMacroeconomicInputses = filteredObeEclPdAssumptionMacroeconomicInputses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumptionMacroeconomicInputses = from o in pagedAndFilteredObeEclPdAssumptionMacroeconomicInputses
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumptionMacroeconomicInputsForViewDto() {
							ObeEclPdAssumptionMacroeconomicInputs = new ObeEclPdAssumptionMacroeconomicInputsDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumptionMacroeconomicInputses.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumptionMacroeconomicInputsForViewDto>(
                totalCount,
                await obeEclPdAssumptionMacroeconomicInputses.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclPdAssumptionMacroeconomicInputsForEditOutput> GetObeEclPdAssumptionMacroeconomicInputsForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumptionMacroeconomicInputs = await _obeEclPdAssumptionMacroeconomicInputsRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumptionMacroeconomicInputsForEditOutput {ObeEclPdAssumptionMacroeconomicInputs = ObjectMapper.Map<CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto>(obeEclPdAssumptionMacroeconomicInputs)};

		    if (output.ObeEclPdAssumptionMacroeconomicInputs.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumptionMacroeconomicInputs.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto input)
         {
            var obeEclPdAssumptionMacroeconomicInputs = ObjectMapper.Map<ObeEclPdAssumptionMacroeconomicInputs>(input);

			

            await _obeEclPdAssumptionMacroeconomicInputsRepository.InsertAsync(obeEclPdAssumptionMacroeconomicInputs);
         }

		 protected virtual async Task Update(CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto input)
         {
            var obeEclPdAssumptionMacroeconomicInputs = await _obeEclPdAssumptionMacroeconomicInputsRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumptionMacroeconomicInputs);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumptionMacroeconomicInputsRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclPdAssumptionMacroeconomicInputsObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumptionMacroeconomicInputsObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumptionMacroeconomicInputsObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumptionMacroeconomicInputsObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}