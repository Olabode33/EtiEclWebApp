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
    public class ObeEclEadInputAssumptionsAppService : TestDemoAppServiceBase, IObeEclEadInputAssumptionsAppService
    {
		 private readonly IRepository<ObeEclEadInputAssumption, Guid> _obeEclEadInputAssumptionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclEadInputAssumptionsAppService(IRepository<ObeEclEadInputAssumption, Guid> obeEclEadInputAssumptionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclEadInputAssumptionRepository = obeEclEadInputAssumptionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclEadInputAssumptionForViewDto>> GetAll(GetAllObeEclEadInputAssumptionsInput input)
         {
			var datatypeFilter = (DataTypeEnum) input.DatatypeFilter;
			var eadGroupFilter = (EadInputAssumptionGroupEnum) input.EadGroupFilter;
			
			var filteredObeEclEadInputAssumptions = _obeEclEadInputAssumptionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.DatatypeFilter > -1, e => e.DataType == datatypeFilter)
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter )
						.WhereIf(input.EadGroupFilter > -1, e => e.EadGroup == eadGroupFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredObeEclEadInputAssumptions = filteredObeEclEadInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclEadInputAssumptions = from o in pagedAndFilteredObeEclEadInputAssumptions
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclEadInputAssumptionForViewDto() {
							ObeEclEadInputAssumption = new ObeEclEadInputAssumptionDto
							{
                                Key = o.Key,
                                InputName = o.InputName,
                                Value = o.Value,
                                Datatype = o.DataType,
                                IsComputed = o.IsComputed,
                                EadGroup = o.EadGroup,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclEadInputAssumptions.CountAsync();

            return new PagedResultDto<GetObeEclEadInputAssumptionForViewDto>(
                totalCount,
                await obeEclEadInputAssumptions.ToListAsync()
            );
         }
		 
		 public async Task<GetObeEclEadInputAssumptionForEditOutput> GetObeEclEadInputAssumptionForEdit(EntityDto<Guid> input)
         {
            var obeEclEadInputAssumption = await _obeEclEadInputAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclEadInputAssumptionForEditOutput {ObeEclEadInputAssumption = ObjectMapper.Map<CreateOrEditObeEclEadInputAssumptionDto>(obeEclEadInputAssumption)};

		    if (output.ObeEclEadInputAssumption.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclEadInputAssumption.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclEadInputAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditObeEclEadInputAssumptionDto input)
         {
            var obeEclEadInputAssumption = ObjectMapper.Map<ObeEclEadInputAssumption>(input);

            await _obeEclEadInputAssumptionRepository.InsertAsync(obeEclEadInputAssumption);
         }

		 protected virtual async Task Update(CreateOrEditObeEclEadInputAssumptionDto input)
         {
            var obeEclEadInputAssumption = await _obeEclEadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclEadInputAssumption);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclEadInputAssumptionRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<ObeEclEadInputAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclEadInputAssumptionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclEadInputAssumptionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclEadInputAssumptionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}