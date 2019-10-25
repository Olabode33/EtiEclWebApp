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
	[AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions)]
    public class ObeEclLgdAssumptionsAppService : TestDemoAppServiceBase, IObeEclLgdAssumptionsAppService
    {
		 private readonly IRepository<ObeEclLgdAssumption, Guid> _obeEclLgdAssumptionRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclLgdAssumptionsAppService(IRepository<ObeEclLgdAssumption, Guid> obeEclLgdAssumptionRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclLgdAssumptionRepository = obeEclLgdAssumptionRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclLgdAssumptionForViewDto>> GetAll(GetAllObeEclLgdAssumptionsInput input)
         {
			var dataTypeFilter = (DataTypeEnum) input.DataTypeFilter;
			var lgdGroupFilter = (LdgInputAssumptionEnum) input.LgdGroupFilter;
			
			var filteredObeEclLgdAssumptions = _obeEclLgdAssumptionRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.DataTypeFilter > -1, e => e.DataType == dataTypeFilter)
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter )
						.WhereIf(input.LgdGroupFilter > -1, e => e.LgdGroup == lgdGroupFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.ObeEclTenantIdFilter), e => e.ObeEclFk != null && e.ObeEclFk.TenantId.ToLower() == input.ObeEclTenantIdFilter.ToLower().Trim());

			var pagedAndFilteredObeEclLgdAssumptions = filteredObeEclLgdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclLgdAssumptions = from o in pagedAndFilteredObeEclLgdAssumptions
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclLgdAssumptionForViewDto() {
							ObeEclLgdAssumption = new ObeEclLgdAssumptionDto
							{
                                Key = o.Key,
                                InputName = o.InputName,
                                Value = o.Value,
                                DataType = o.DataType,
                                IsComputed = o.IsComputed,
                                LgdGroup = o.LgdGroup,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclLgdAssumptions.CountAsync();

            return new PagedResultDto<GetObeEclLgdAssumptionForViewDto>(
                totalCount,
                await obeEclLgdAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions_Edit)]
		 public async Task<GetObeEclLgdAssumptionForEditOutput> GetObeEclLgdAssumptionForEdit(EntityDto<Guid> input)
         {
            var obeEclLgdAssumption = await _obeEclLgdAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclLgdAssumptionForEditOutput {ObeEclLgdAssumption = ObjectMapper.Map<CreateOrEditObeEclLgdAssumptionDto>(obeEclLgdAssumption)};

		    if (output.ObeEclLgdAssumption.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclLgdAssumption.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclLgdAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclLgdAssumptionDto input)
         {
            var obeEclLgdAssumption = ObjectMapper.Map<ObeEclLgdAssumption>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclLgdAssumption.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclLgdAssumptionRepository.InsertAsync(obeEclLgdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclLgdAssumptionDto input)
         {
            var obeEclLgdAssumption = await _obeEclLgdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclLgdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclLgdAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclLgdAssumptions)]
         public async Task<PagedResultDto<ObeEclLgdAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclLgdAssumptionObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclLgdAssumptionObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclLgdAssumptionObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}