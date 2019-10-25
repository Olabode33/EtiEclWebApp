using TestDemo.OBE;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.ObeResults
{
	[AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs)]
    public class ObeEclResultSummaryKeyInputsAppService : TestDemoAppServiceBase, IObeEclResultSummaryKeyInputsAppService
    {
		 private readonly IRepository<ObeEclResultSummaryKeyInput, Guid> _obeEclResultSummaryKeyInputRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObeEclResultSummaryKeyInputsAppService(IRepository<ObeEclResultSummaryKeyInput, Guid> obeEclResultSummaryKeyInputRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obeEclResultSummaryKeyInputRepository = obeEclResultSummaryKeyInputRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObeEclResultSummaryKeyInputForViewDto>> GetAll(GetAllObeEclResultSummaryKeyInputsInput input)
         {
			
			var filteredObeEclResultSummaryKeyInputs = _obeEclResultSummaryKeyInputRepository.GetAll()
						.Include( e => e.ObeEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PDGrouping.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.PDGroupingFilter),  e => e.PDGrouping.ToLower() == input.PDGroupingFilter.ToLower().Trim())
						.WhereIf(input.MinExposureFilter != null, e => e.Exposure >= input.MinExposureFilter)
						.WhereIf(input.MaxExposureFilter != null, e => e.Exposure <= input.MaxExposureFilter)
						.WhereIf(input.MinCollateralFilter != null, e => e.Collateral >= input.MinCollateralFilter)
						.WhereIf(input.MaxCollateralFilter != null, e => e.Collateral <= input.MaxCollateralFilter)
						.WhereIf(input.MinUnsecuredPercentageFilter != null, e => e.UnsecuredPercentage >= input.MinUnsecuredPercentageFilter)
						.WhereIf(input.MaxUnsecuredPercentageFilter != null, e => e.UnsecuredPercentage <= input.MaxUnsecuredPercentageFilter)
						.WhereIf(input.MinPercentageOfBookFilter != null, e => e.PercentageOfBook >= input.MinPercentageOfBookFilter)
						.WhereIf(input.MaxPercentageOfBookFilter != null, e => e.PercentageOfBook <= input.MaxPercentageOfBookFilter)
						.WhereIf(input.MinMonths6CummulativeBestPDsFilter != null, e => e.Months6CummulativeBestPDs >= input.MinMonths6CummulativeBestPDsFilter)
						.WhereIf(input.MaxMonths6CummulativeBestPDsFilter != null, e => e.Months6CummulativeBestPDs <= input.MaxMonths6CummulativeBestPDsFilter)
						.WhereIf(input.MinMonths12CummulativeBestPDsFilter != null, e => e.Months12CummulativeBestPDs >= input.MinMonths12CummulativeBestPDsFilter)
						.WhereIf(input.MaxMonths12CummulativeBestPDsFilter != null, e => e.Months12CummulativeBestPDs <= input.MaxMonths12CummulativeBestPDsFilter)
						.WhereIf(input.MinMonths24CummulativeBestPDsFilter != null, e => e.Months24CummulativeBestPDs >= input.MinMonths24CummulativeBestPDsFilter)
						.WhereIf(input.MaxMonths24CummulativeBestPDsFilter != null, e => e.Months24CummulativeBestPDs <= input.MaxMonths24CummulativeBestPDsFilter);

			var pagedAndFilteredObeEclResultSummaryKeyInputs = filteredObeEclResultSummaryKeyInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obeEclResultSummaryKeyInputs = from o in pagedAndFilteredObeEclResultSummaryKeyInputs
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObeEclResultSummaryKeyInputForViewDto() {
							ObeEclResultSummaryKeyInput = new ObeEclResultSummaryKeyInputDto
							{
                                PDGrouping = o.PDGrouping,
                                Exposure = o.Exposure,
                                Collateral = o.Collateral,
                                UnsecuredPercentage = o.UnsecuredPercentage,
                                PercentageOfBook = o.PercentageOfBook,
                                Months6CummulativeBestPDs = o.Months6CummulativeBestPDs,
                                Months12CummulativeBestPDs = o.Months12CummulativeBestPDs,
                                Months24CummulativeBestPDs = o.Months24CummulativeBestPDs,
                                Id = o.Id
							},
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObeEclResultSummaryKeyInputs.CountAsync();

            return new PagedResultDto<GetObeEclResultSummaryKeyInputForViewDto>(
                totalCount,
                await obeEclResultSummaryKeyInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Edit)]
		 public async Task<GetObeEclResultSummaryKeyInputForEditOutput> GetObeEclResultSummaryKeyInputForEdit(EntityDto<Guid> input)
         {
            var obeEclResultSummaryKeyInput = await _obeEclResultSummaryKeyInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObeEclResultSummaryKeyInputForEditOutput {ObeEclResultSummaryKeyInput = ObjectMapper.Map<CreateOrEditObeEclResultSummaryKeyInputDto>(obeEclResultSummaryKeyInput)};

		    if (output.ObeEclResultSummaryKeyInput.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObeEclResultSummaryKeyInput.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObeEclResultSummaryKeyInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Create)]
		 protected virtual async Task Create(CreateOrEditObeEclResultSummaryKeyInputDto input)
         {
            var obeEclResultSummaryKeyInput = ObjectMapper.Map<ObeEclResultSummaryKeyInput>(input);

			
			if (AbpSession.TenantId != null)
			{
				obeEclResultSummaryKeyInput.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obeEclResultSummaryKeyInputRepository.InsertAsync(obeEclResultSummaryKeyInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditObeEclResultSummaryKeyInputDto input)
         {
            var obeEclResultSummaryKeyInput = await _obeEclResultSummaryKeyInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obeEclResultSummaryKeyInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obeEclResultSummaryKeyInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObeEclResultSummaryKeyInputs)]
         public async Task<PagedResultDto<ObeEclResultSummaryKeyInputObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObeEclResultSummaryKeyInputObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObeEclResultSummaryKeyInputObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObeEclResultSummaryKeyInputObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}