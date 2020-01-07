using TestDemo.Retail;

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
using TestDemo.EclShared.Dtos;
using GetAllForLookupTableInput = TestDemo.RetailAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.RetailAssumption
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions)]
    public class RetailEclLgdAssumptionsAppService : TestDemoAppServiceBase, IRetailEclLgdAssumptionsAppService
    {
		 private readonly IRepository<RetailEclLgdAssumption, Guid> _retailEclLgdAssumptionRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclLgdAssumptionsAppService(IRepository<RetailEclLgdAssumption, Guid> retailEclLgdAssumptionRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclLgdAssumptionRepository = retailEclLgdAssumptionRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclLgdAssumptionForViewDto>> GetAll(GetAllRetailEclLgdAssumptionsInput input)
         {
			var dataTypeFilter = (DataTypeEnum) input.DataTypeFilter;
			var lgdGroupFilter = (LdgInputAssumptionGroupEnum) input.LgdGroupFilter;
			
			var filteredRetailEclLgdAssumptions = _retailEclLgdAssumptionRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.DataTypeFilter > -1, e => e.DataType == dataTypeFilter)
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter )
						.WhereIf(input.LgdGroupFilter > -1, e => e.LgdGroup == lgdGroupFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredRetailEclLgdAssumptions = filteredRetailEclLgdAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclLgdAssumptions = from o in pagedAndFilteredRetailEclLgdAssumptions
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclLgdAssumptionForViewDto() {
							RetailEclLgdAssumption = new RetailEclLgdAssumptionDto
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
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclLgdAssumptions.CountAsync();

            return new PagedResultDto<GetRetailEclLgdAssumptionForViewDto>(
                totalCount,
                await retailEclLgdAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions_Edit)]
		 public async Task<GetRetailEclLgdAssumptionForEditOutput> GetRetailEclLgdAssumptionForEdit(EntityDto<Guid> input)
         {
            var retailEclLgdAssumption = await _retailEclLgdAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclLgdAssumptionForEditOutput {RetailEclLgdAssumption = ObjectMapper.Map<CreateOrEditRetailEclLgdAssumptionDto>(retailEclLgdAssumption)};

		    if (output.RetailEclLgdAssumption.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclLgdAssumption.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

        public async Task<List<LgdAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclLgdAssumptionRepository.GetAll().Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new LgdAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.LgdGroup,
                                                                  Key = x.Key,
                                                                  InputName = x.InputName,
                                                                  Value = x.Value,
                                                                  DataType = x.DataType,
                                                                  IsComputed = x.IsComputed,
                                                                  RequiresGroupApproval = x.RequiresGroupApproval,
                                                                  CanAffiliateEdit = x.CanAffiliateEdit,
                                                                  OrganizationUnitId = x.OrganizationUnitId,
                                                                  //Status = x.s,
                                                                  Id = x.Id
                                                              });

            return await assumptions.ToListAsync();

        }

        public async Task CreateOrEdit(CreateOrEditRetailEclLgdAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclLgdAssumptionDto input)
         {
            var retailEclLgdAssumption = ObjectMapper.Map<RetailEclLgdAssumption>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclLgdAssumption.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclLgdAssumptionRepository.InsertAsync(retailEclLgdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclLgdAssumptionDto input)
         {
            var retailEclLgdAssumption = await _retailEclLgdAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclLgdAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclLgdAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclLgdAssumptions)]
         public async Task<PagedResultDto<RetailEclLgdAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclLgdAssumptionRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclLgdAssumptionRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclLgdAssumptionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}