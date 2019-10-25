using TestDemo.Retail;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.RetailResults
{
	[AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs)]
    public class RetailEclResultSummaryKeyInputsAppService : TestDemoAppServiceBase, IRetailEclResultSummaryKeyInputsAppService
    {
		 private readonly IRepository<RetailEclResultSummaryKeyInput, Guid> _retailEclResultSummaryKeyInputRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclResultSummaryKeyInputsAppService(IRepository<RetailEclResultSummaryKeyInput, Guid> retailEclResultSummaryKeyInputRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclResultSummaryKeyInputRepository = retailEclResultSummaryKeyInputRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclResultSummaryKeyInputForViewDto>> GetAll(GetAllRetailEclResultSummaryKeyInputsInput input)
         {
			
			var filteredRetailEclResultSummaryKeyInputs = _retailEclResultSummaryKeyInputRepository.GetAll()
						.Include( e => e.RetailEclFk)
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
						.WhereIf(input.MaxMonths24CummulativeBestPDsFilter != null, e => e.Months24CummulativeBestPDs <= input.MaxMonths24CummulativeBestPDsFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.RetailEclTenantIdFilter), e => e.RetailEclFk != null && e.RetailEclFk.TenantId.ToLower() == input.RetailEclTenantIdFilter.ToLower().Trim());

			var pagedAndFilteredRetailEclResultSummaryKeyInputs = filteredRetailEclResultSummaryKeyInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclResultSummaryKeyInputs = from o in pagedAndFilteredRetailEclResultSummaryKeyInputs
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclResultSummaryKeyInputForViewDto() {
							RetailEclResultSummaryKeyInput = new RetailEclResultSummaryKeyInputDto
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
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclResultSummaryKeyInputs.CountAsync();

            return new PagedResultDto<GetRetailEclResultSummaryKeyInputForViewDto>(
                totalCount,
                await retailEclResultSummaryKeyInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Edit)]
		 public async Task<GetRetailEclResultSummaryKeyInputForEditOutput> GetRetailEclResultSummaryKeyInputForEdit(EntityDto<Guid> input)
         {
            var retailEclResultSummaryKeyInput = await _retailEclResultSummaryKeyInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclResultSummaryKeyInputForEditOutput {RetailEclResultSummaryKeyInput = ObjectMapper.Map<CreateOrEditRetailEclResultSummaryKeyInputDto>(retailEclResultSummaryKeyInput)};

		    if (output.RetailEclResultSummaryKeyInput.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultSummaryKeyInput.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclResultSummaryKeyInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclResultSummaryKeyInputDto input)
         {
            var retailEclResultSummaryKeyInput = ObjectMapper.Map<RetailEclResultSummaryKeyInput>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclResultSummaryKeyInput.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclResultSummaryKeyInputRepository.InsertAsync(retailEclResultSummaryKeyInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclResultSummaryKeyInputDto input)
         {
            var retailEclResultSummaryKeyInput = await _retailEclResultSummaryKeyInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclResultSummaryKeyInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclResultSummaryKeyInputRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaryKeyInputs)]
         public async Task<PagedResultDto<RetailEclResultSummaryKeyInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultSummaryKeyInputRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclResultSummaryKeyInputRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultSummaryKeyInputRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}