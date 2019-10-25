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
	[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months)]
    public class RetailEclPdAssumption12MonthsAppService : TestDemoAppServiceBase, IRetailEclPdAssumption12MonthsAppService
    {
		 private readonly IRepository<RetailEclPdAssumption12Month, Guid> _retailEclPdAssumption12MonthRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclPdAssumption12MonthsAppService(IRepository<RetailEclPdAssumption12Month, Guid> retailEclPdAssumption12MonthRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclPdAssumption12MonthRepository = retailEclPdAssumption12MonthRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclPdAssumption12MonthForViewDto>> GetAll(GetAllRetailEclPdAssumption12MonthsInput input)
         {
			
			var filteredRetailEclPdAssumption12Months = _retailEclPdAssumption12MonthRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SnPMappingEtiCreditPolicy.Contains(input.Filter) || e.SnPMappingBestFit.Contains(input.Filter))
						.WhereIf(input.MinCreditFilter != null, e => e.Credit >= input.MinCreditFilter)
						.WhereIf(input.MaxCreditFilter != null, e => e.Credit <= input.MaxCreditFilter)
						.WhereIf(input.MinPDFilter != null, e => e.PD >= input.MinPDFilter)
						.WhereIf(input.MaxPDFilter != null, e => e.PD <= input.MaxPDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingEtiCreditPolicyFilter),  e => e.SnPMappingEtiCreditPolicy.ToLower() == input.SnPMappingEtiCreditPolicyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingBestFitFilter),  e => e.SnPMappingBestFit.ToLower() == input.SnPMappingBestFitFilter.ToLower().Trim())
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter )
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId.ToLower() == input.RetailEclTenantIdFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclPdAssumption12Months = filteredRetailEclPdAssumption12Months
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclPdAssumption12Months = from o in pagedAndFilteredRetailEclPdAssumption12Months
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclPdAssumption12MonthForViewDto() {
							RetailEclPdAssumption12Month = new RetailEclPdAssumption12MonthDto
							{
                                Credit = o.Credit,
                                PD = o.PD,
                                SnPMappingEtiCreditPolicy = o.SnPMappingEtiCreditPolicy,
                                SnPMappingBestFit = o.SnPMappingBestFit,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclPdAssumption12Months.CountAsync();

            return new PagedResultDto<GetRetailEclPdAssumption12MonthForViewDto>(
                totalCount,
                await retailEclPdAssumption12Months.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months_Edit)]
		 public async Task<GetRetailEclPdAssumption12MonthForEditOutput> GetRetailEclPdAssumption12MonthForEdit(EntityDto<Guid> input)
         {
            var retailEclPdAssumption12Month = await _retailEclPdAssumption12MonthRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclPdAssumption12MonthForEditOutput {RetailEclPdAssumption12Month = ObjectMapper.Map<CreateOrEditRetailEclPdAssumption12MonthDto>(retailEclPdAssumption12Month)};

		    if (output.RetailEclPdAssumption12Month.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclPdAssumption12Month.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclPdAssumption12MonthDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclPdAssumption12MonthDto input)
         {
            var retailEclPdAssumption12Month = ObjectMapper.Map<RetailEclPdAssumption12Month>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclPdAssumption12Month.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclPdAssumption12MonthRepository.InsertAsync(retailEclPdAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclPdAssumption12MonthDto input)
         {
            var retailEclPdAssumption12Month = await _retailEclPdAssumption12MonthRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclPdAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclPdAssumption12MonthRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclPdAssumption12Months)]
         public async Task<PagedResultDto<RetailEclPdAssumption12MonthRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclPdAssumption12MonthRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclPdAssumption12MonthRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclPdAssumption12MonthRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}