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
    public class ObeEclPdAssumptionsAppService : TestDemoAppServiceBase, IObeEclPdAssumptionsAppService
    {
		 private readonly IRepository<ObeEclPdAssumption, Guid> _obeEclPdAssumptionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumptionsAppService(IRepository<ObeEclPdAssumption, Guid> obeEclPdAssumptionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumptionRepository = obeEclPdAssumptionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumptionForViewDto>> GetAll(GetAllObeEclPdAssumptionsInput input)
         {
			
			var filteredObeEclPdAssumptions = _obeEclPdAssumptionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredObeEclPdAssumptions = filteredObeEclPdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumptions = from o in pagedAndFilteredObeEclPdAssumptions
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumptionForViewDto() {
							ObeEclPdAssumption = new ObeEclPdAssumptionDto
							{
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumptions.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumptionForViewDto>(
                totalCount,
                await obeEclPdAssumptions.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclPdAssumptionForEditOutput> GetObeEclPdAssumptionForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumption = await _obeEclPdAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumptionForEditOutput {ObeEclPdAssumption = ObjectMapper.Map<CreateOrEditObeEclPdAssumptionDto>(obeEclPdAssumption)};

		    if (output.ObeEclPdAssumption.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumption.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclPdAssumptionDto input)
         {
            var obeEclPdAssumption = ObjectMapper.Map<ObeEclPdAssumption>(input);

			

            await _obeEclPdAssumptionRepository.InsertAsync(obeEclPdAssumption);
         }

		 protected virtual async Task Update(CreateOrEditObeEclPdAssumptionDto input)
         {
            var obeEclPdAssumption = await _obeEclPdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumption);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumptionRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclPdAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumptionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumptionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumptionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}