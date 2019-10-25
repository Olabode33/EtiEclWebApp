using TestDemo.OBE;


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
	[AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates)]
    public class ObeEclPdSnPCummulativeDefaultRatesAppService : TestDemoAppServiceBase, IObeEclPdSnPCummulativeDefaultRatesAppService
    {
		 private readonly IRepository<ObeEclPdSnPCummulativeDefaultRate, Guid> _obeEclPdSnPCummulativeDefaultRateRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdSnPCummulativeDefaultRatesAppService(IRepository<ObeEclPdSnPCummulativeDefaultRate, Guid> obeEclPdSnPCummulativeDefaultRateRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdSnPCummulativeDefaultRateRepository = obeEclPdSnPCummulativeDefaultRateRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllObeEclPdSnPCummulativeDefaultRatesInput input)
         {
			
			var filteredObeEclPdSnPCummulativeDefaultRates = _obeEclPdSnPCummulativeDefaultRateRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.Rating.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.KeyFilter),  e => e.Key.ToLower() == input.KeyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.RatingFilter),  e => e.Rating.ToLower() == input.RatingFilter.ToLower().Trim())
						.WhereIf(input.MinYearsFilter != null, e => e.Years >= input.MinYearsFilter)
						.WhereIf(input.MaxYearsFilter != null, e => e.Years <= input.MaxYearsFilter)
						.WhereIf(input.MinValueFilter != null, e => e.Value >= input.MinValueFilter)
						.WhereIf(input.MaxValueFilter != null, e => e.Value <= input.MaxValueFilter)
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredObeEclPdSnPCummulativeDefaultRates = filteredObeEclPdSnPCummulativeDefaultRates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdSnPCummulativeDefaultRates = from o in pagedAndFilteredObeEclPdSnPCummulativeDefaultRates
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdSnPCummulativeDefaultRateForViewDto() {
							ObeEclPdSnPCummulativeDefaultRate = new ObeEclPdSnPCummulativeDefaultRateDto
							{
                                Key = o.Key,
                                Rating = o.Rating,
                                Years = o.Years,
                                Value = o.Value,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdSnPCummulativeDefaultRates.CountAsync();

            return new PagedResultDto<GetObeEclPdSnPCummulativeDefaultRateForViewDto>(
                totalCount,
                await obeEclPdSnPCummulativeDefaultRates.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Edit)]
		 public async Task<GetObeEclPdSnPCummulativeDefaultRateForEditOutput> GetObeEclPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input)
         {
            var obeEclPdSnPCummulativeDefaultRate = await _obeEclPdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdSnPCummulativeDefaultRateForEditOutput {ObeEclPdSnPCummulativeDefaultRate = ObjectMapper.Map<CreateOrEditObeEclPdSnPCummulativeDefaultRateDto>(obeEclPdSnPCummulativeDefaultRate)};

		    if (output.ObeEclPdSnPCummulativeDefaultRate.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdSnPCummulativeDefaultRate.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdSnPCummulativeDefaultRateDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclPdSnPCummulativeDefaultRateDto input)
         {
            var obeEclPdSnPCummulativeDefaultRate = ObjectMapper.Map<ObeEclPdSnPCummulativeDefaultRate>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclPdSnPCummulativeDefaultRate.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclPdSnPCummulativeDefaultRateRepository.InsertAsync(obeEclPdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclPdSnPCummulativeDefaultRateDto input)
         {
            var obeEclPdSnPCummulativeDefaultRate = await _obeEclPdSnPCummulativeDefaultRateRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdSnPCummulativeDefaultRate);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdSnPCummulativeDefaultRateRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates)]
         public async Task<PagedResultDto<ObeEclPdSnPCummulativeDefaultRateObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdSnPCummulativeDefaultRateObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdSnPCummulativeDefaultRateObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdSnPCummulativeDefaultRateObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}