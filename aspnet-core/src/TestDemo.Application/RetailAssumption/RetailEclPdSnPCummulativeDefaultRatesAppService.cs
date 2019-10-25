using TestDemo.Retail;


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
	[AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates)]
    public class RetailEclPdSnPCummulativeDefaultRatesAppService : TestDemoAppServiceBase, IRetailEclPdSnPCummulativeDefaultRatesAppService
    {
		 private readonly IRepository<RetailEclPdSnPCummulativeDefaultRate, Guid> _retailEclPdSnPCummulativeDefaultRateRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclPdSnPCummulativeDefaultRatesAppService(IRepository<RetailEclPdSnPCummulativeDefaultRate, Guid> retailEclPdSnPCummulativeDefaultRateRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclPdSnPCummulativeDefaultRateRepository = retailEclPdSnPCummulativeDefaultRateRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllRetailEclPdSnPCummulativeDefaultRatesInput input)
         {
			
			var filteredRetailEclPdSnPCummulativeDefaultRates = _retailEclPdSnPCummulativeDefaultRateRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RatingFilter),  e => e.Rating.ToLower() == input.RatingFilter.ToLower().Trim())
						.WhereIf(input.MinYearsFilter != null, e => e.Years >= input.MinYearsFilter)
						.WhereIf(input.MaxYearsFilter != null, e => e.Years <= input.MaxYearsFilter)
						.WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
						.WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredRetailEclPdSnPCummulativeDefaultRates = filteredRetailEclPdSnPCummulativeDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclPdSnPCummulativeDefaultRates = from o in pagedAndFilteredRetailEclPdSnPCummulativeDefaultRates
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclPdSnPCummulativeDefaultRateForViewDto() {
							RetailEclPdSnPCummulativeDefaultRate = new RetailEclPdSnPCummulativeDefaultRateDto
							{
                                Key = o.Key,
                                Rating = o.Rating,
                                Years = o.Years,
                                Value = o.Value,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclPdSnPCummulativeDefaultRates.CountAsync();

            return new PagedResultDto<GetRetailEclPdSnPCummulativeDefaultRateForViewDto>(
                totalCount,
                await retailEclPdSnPCummulativeDefaultRates.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Edit)]
		 public async Task<GetRetailEclPdSnPCummulativeDefaultRateForEditOutput> GetRetailEclPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input)
         {
            var retailEclPdSnPCummulativeDefaultRate = await _retailEclPdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclPdSnPCummulativeDefaultRateForEditOutput {RetailEclPdSnPCummulativeDefaultRate = ObjectMapper.Map<CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto>(retailEclPdSnPCummulativeDefaultRate)};

		    if (output.RetailEclPdSnPCummulativeDefaultRate.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdSnPCummulativeDefaultRate.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto input)
         {
            var retailEclPdSnPCummulativeDefaultRate = ObjectMapper.Map<RetailEclPdSnPCummulativeDefaultRate>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclPdSnPCummulativeDefaultRate.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclPdSnPCummulativeDefaultRateRepository.InsertAsync(retailEclPdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto input)
         {
            var retailEclPdSnPCummulativeDefaultRate = await _retailEclPdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclPdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclPdSnPCummulativeDefaultRateRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates)]
         public async Task<PagedResultDto<RetailEclPdSnPCummulativeDefaultRateRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclPdSnPCummulativeDefaultRateRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclPdSnPCummulativeDefaultRateRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclPdSnPCummulativeDefaultRateRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}