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
	[AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions)]
    public class ObeEclAssumptionsAppService : TestDemoAppServiceBase, IObeEclAssumptionsAppService
    {
		 private readonly IRepository<ObeEclAssumption, Guid> _obeEclAssumptionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclAssumptionsAppService(IRepository<ObeEclAssumption, Guid> obeEclAssumptionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclAssumptionRepository = obeEclAssumptionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclAssumptionForViewDto>> GetAll(GetAllObeEclAssumptionsInput input)
         {
			var datatypeFilter = (DataTypeEnum) input.DatatypeFilter;
			var assumptionGroupFilter = (AssumptionGroupEnum) input.AssumptionGroupFilter;
			
			var filteredObeEclAssumptions = _obeEclAssumptionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.DatatypeFilter > -1, e => e.DataType == datatypeFilter)
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter )
						.WhereIf(input.AssumptionGroupFilter > -1, e => e.AssumptionGroup == assumptionGroupFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredObeEclAssumptions = filteredObeEclAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclAssumptions = from o in pagedAndFilteredObeEclAssumptions
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclAssumptionForViewDto() {
							ObeEclAssumption = new ObeEclAssumptionDto
							{
                                Key = o.Key,
                                InputName = o.InputName,
                                Value = o.Value,
                                Datatype = o.DataType,
                                IsComputed = o.IsComputed,
                                AssumptionGroup = o.AssumptionGroup,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclAssumptions.CountAsync();

            return new PagedResultDto<GetObeEclAssumptionForViewDto>(
                totalCount,
                await obeEclAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions_Edit)]
		 public async Task<GetObeEclAssumptionForEditOutput> GetObeEclAssumptionForEdit(EntityDto<Guid> input)
         {
            var obeEclAssumption = await _obeEclAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclAssumptionForEditOutput {ObeEclAssumption = ObjectMapper.Map<CreateOrEditObeEclAssumptionDto>(obeEclAssumption)};

		    if (output.ObeEclAssumption.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclAssumption.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclAssumptionDto input)
         {
            var obeEclAssumption = ObjectMapper.Map<ObeEclAssumption>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclAssumption.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclAssumptionRepository.InsertAsync(obeEclAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclAssumptionDto input)
         {
            var obeEclAssumption = await _obeEclAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclAssumptions)]
         public async Task<PagedResultDto<ObeEclAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclAssumptionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclAssumptionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclAssumptionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}