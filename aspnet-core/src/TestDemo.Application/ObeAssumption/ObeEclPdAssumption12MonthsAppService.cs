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
	[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months)]
    public class ObeEclPdAssumption12MonthsAppService : TestDemoAppServiceBase, IObeEclPdAssumption12MonthsAppService
    {
		 private readonly IRepository<ObeEclPdAssumption12Month, Guid> _obeEclPdAssumption12MonthRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclPdAssumption12MonthsAppService(IRepository<ObeEclPdAssumption12Month, Guid> obeEclPdAssumption12MonthRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclPdAssumption12MonthRepository = obeEclPdAssumption12MonthRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclPdAssumption12MonthForViewDto>> GetAll(GetAllObeEclPdAssumption12MonthsInput input)
         {
			
			var filteredObeEclPdAssumption12Months = _obeEclPdAssumption12MonthRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.SnPMappingEtiCreditPolicy.Contains(input.Filter) || e.SnPMappingBestFit.Contains(input.Filter))
						.WhereIf(input.MinCreditFilter != null, e => e.Credit >= input.MinCreditFilter)
						.WhereIf(input.MaxCreditFilter != null, e => e.Credit <= input.MaxCreditFilter)
						.WhereIf(input.MinPDFilter != null, e => e.PD >= input.MinPDFilter)
						.WhereIf(input.MaxPDFilter != null, e => e.PD <= input.MaxPDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingEtiCreditPolicyFilter),  e => e.SnPMappingEtiCreditPolicy.ToLower() == input.SnPMappingEtiCreditPolicyFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.SnPMappingBestFitFilter),  e => e.SnPMappingBestFit.ToLower() == input.SnPMappingBestFitFilter.ToLower().Trim())
						.WhereIf(input.RequiresGroupApprovalFilter > -1,  e => Convert.ToInt32(e.RequiresGroupApproval) == input.RequiresGroupApprovalFilter );

			var pagedAndFilteredObeEclPdAssumption12Months = filteredObeEclPdAssumption12Months
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclPdAssumption12Months = from o in pagedAndFilteredObeEclPdAssumption12Months
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclPdAssumption12MonthForViewDto() {
							ObeEclPdAssumption12Month = new ObeEclPdAssumption12MonthDto
							{
                                Credit = o.Credit,
                                PD = o.PD,
                                SnPMappingEtiCreditPolicy = o.SnPMappingEtiCreditPolicy,
                                SnPMappingBestFit = o.SnPMappingBestFit,
                                RequiresGroupApproval = o.RequiresGroupApproval,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclPdAssumption12Months.CountAsync();

            return new PagedResultDto<GetObeEclPdAssumption12MonthForViewDto>(
                totalCount,
                await obeEclPdAssumption12Months.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months_Edit)]
		 public async Task<GetObeEclPdAssumption12MonthForEditOutput> GetObeEclPdAssumption12MonthForEdit(EntityDto<Guid> input)
         {
            var obeEclPdAssumption12Month = await _obeEclPdAssumption12MonthRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclPdAssumption12MonthForEditOutput {ObeEclPdAssumption12Month = ObjectMapper.Map<CreateOrEditObeEclPdAssumption12MonthDto>(obeEclPdAssumption12Month)};

		    if (output.ObeEclPdAssumption12Month.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclPdAssumption12Month.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclPdAssumption12MonthDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclPdAssumption12MonthDto input)
         {
            var obeEclPdAssumption12Month = ObjectMapper.Map<ObeEclPdAssumption12Month>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclPdAssumption12Month.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclPdAssumption12MonthRepository.InsertAsync(obeEclPdAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclPdAssumption12MonthDto input)
         {
            var obeEclPdAssumption12Month = await _obeEclPdAssumption12MonthRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclPdAssumption12Month);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclPdAssumption12MonthRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclPdAssumption12Months)]
         public async Task<PagedResultDto<ObeEclPdAssumption12MonthObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclPdAssumption12MonthObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclPdAssumption12MonthObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclPdAssumption12MonthObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}