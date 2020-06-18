using TestDemo.Wholesale;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.WholesaleResults
{
    public class WholesaleEclResultSummaryKeyInputsAppService : TestDemoAppServiceBase, IWholesaleEclResultSummaryKeyInputsAppService
    {
		 private readonly IRepository<WholesaleEclResultSummaryKeyInput, Guid> _wholesaleEclResultSummaryKeyInputRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclResultSummaryKeyInputsAppService(IRepository<WholesaleEclResultSummaryKeyInput, Guid> wholesaleEclResultSummaryKeyInputRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclResultSummaryKeyInputRepository = wholesaleEclResultSummaryKeyInputRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclResultSummaryKeyInputForViewDto>> GetAll(GetAllWholesaleEclResultSummaryKeyInputsInput input)
         {
			
			var filteredWholesaleEclResultSummaryKeyInputs = _wholesaleEclResultSummaryKeyInputRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
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

			var pagedAndFilteredWholesaleEclResultSummaryKeyInputs = filteredWholesaleEclResultSummaryKeyInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclResultSummaryKeyInputs = from o in pagedAndFilteredWholesaleEclResultSummaryKeyInputs
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclResultSummaryKeyInputForViewDto() {
							WholesaleEclResultSummaryKeyInput = new WholesaleEclResultSummaryKeyInputDto
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
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclResultSummaryKeyInputs.CountAsync();

            return new PagedResultDto<GetWholesaleEclResultSummaryKeyInputForViewDto>(
                totalCount,
                await wholesaleEclResultSummaryKeyInputs.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclResultSummaryKeyInputForEditOutput> GetWholesaleEclResultSummaryKeyInputForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclResultSummaryKeyInput = await _wholesaleEclResultSummaryKeyInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclResultSummaryKeyInputForEditOutput {WholesaleEclResultSummaryKeyInput = ObjectMapper.Map<CreateOrEditWholesaleEclResultSummaryKeyInputDto>(wholesaleEclResultSummaryKeyInput)};

		    if (output.WholesaleEclResultSummaryKeyInput.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultSummaryKeyInput.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryKeyInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclResultSummaryKeyInputDto input)
         {
            var wholesaleEclResultSummaryKeyInput = ObjectMapper.Map<WholesaleEclResultSummaryKeyInput>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclResultSummaryKeyInput.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclResultSummaryKeyInputRepository.InsertAsync(wholesaleEclResultSummaryKeyInput);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclResultSummaryKeyInputDto input)
         {
            var wholesaleEclResultSummaryKeyInput = await _wholesaleEclResultSummaryKeyInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclResultSummaryKeyInput);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclResultSummaryKeyInputRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}