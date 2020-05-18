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
	[AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions)]
    public class RetailEclEadInputAssumptionsAppService : TestDemoAppServiceBase, IRetailEclEadInputAssumptionsAppService
    {
		 private readonly IRepository<RetailEclEadInputAssumption, Guid> _retailEclEadInputAssumptionRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclEadInputAssumptionsAppService(IRepository<RetailEclEadInputAssumption, Guid> retailEclEadInputAssumptionRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclEadInputAssumptionRepository = retailEclEadInputAssumptionRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclEadInputAssumptionForViewDto>> GetAll(GetAllRetailEclEadInputAssumptionsInput input)
         {
			var datatypeFilter = (DataTypeEnum) input.DatatypeFilter;
			var eadGroupFilter = (EadInputAssumptionGroupEnum) input.EadGroupFilter;
			
			var filteredRetailEclEadInputAssumptions = _retailEclEadInputAssumptionRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter),  e => e.Value.ToLower() == input.ValueFilter.ToLower().Trim())
						.WhereIf(input.DatatypeFilter > -1, e => e.DataType == datatypeFilter)
						.WhereIf(input.IsComputedFilter > -1,  e => Convert.ToInt32(e.IsComputed) == input.IsComputedFilter )
						.WhereIf(input.EadGroupFilter > -1, e => e.EadGroup == eadGroupFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredRetailEclEadInputAssumptions = filteredRetailEclEadInputAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclEadInputAssumptions = from o in pagedAndFilteredRetailEclEadInputAssumptions
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclEadInputAssumptionForViewDto() {
							RetailEclEadInputAssumption = new RetailEclEadInputAssumptionDto
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
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclEadInputAssumptions.CountAsync();

            return new PagedResultDto<GetRetailEclEadInputAssumptionForViewDto>(
                totalCount,
                await retailEclEadInputAssumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions_Edit)]
		 public async Task<GetRetailEclEadInputAssumptionForEditOutput> GetRetailEclEadInputAssumptionForEdit(EntityDto<Guid> input)
         {
            var retailEclEadInputAssumption = await _retailEclEadInputAssumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclEadInputAssumptionForEditOutput {RetailEclEadInputAssumption = ObjectMapper.Map<CreateOrEditRetailEclEadInputAssumptionDto>(retailEclEadInputAssumption)};

		    if (output.RetailEclEadInputAssumption.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclEadInputAssumption.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

        public async Task<List<EadInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input)
        {
            var assumptions = _retailEclEadInputAssumptionRepository.GetAll().Where(x => x.RetailEclId == input.Id)
                                                              .Select(x => new EadInputAssumptionDto()
                                                              {
                                                                  AssumptionGroup = x.EadGroup,
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

        public async Task CreateOrEdit(CreateOrEditRetailEclEadInputAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclEadInputAssumptionDto input)
         {
            var retailEclEadInputAssumption = ObjectMapper.Map<RetailEclEadInputAssumption>(input);

            await _retailEclEadInputAssumptionRepository.InsertAsync(retailEclEadInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclEadInputAssumptionDto input)
         {
            var retailEclEadInputAssumption = await _retailEclEadInputAssumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclEadInputAssumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclEadInputAssumptionRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclEadInputAssumptions)]
         public async Task<PagedResultDto<RetailEclEadInputAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclEadInputAssumptionRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclEadInputAssumptionRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclEadInputAssumptionRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}