using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;


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
	[AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures)]
    public class WholesaleEclResultSummaryTopExposuresAppService : TestDemoAppServiceBase, IWholesaleEclResultSummaryTopExposuresAppService
    {
		 private readonly IRepository<WholesaleEclResultSummaryTopExposure, Guid> _wholesaleEclResultSummaryTopExposureRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclResultSummaryTopExposuresAppService(IRepository<WholesaleEclResultSummaryTopExposure, Guid> wholesaleEclResultSummaryTopExposureRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository, IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclResultSummaryTopExposureRepository = wholesaleEclResultSummaryTopExposureRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclResultSummaryTopExposureForViewDto>> GetAll(GetAllWholesaleEclResultSummaryTopExposuresInput input)
         {
			
			var filteredWholesaleEclResultSummaryTopExposures = _wholesaleEclResultSummaryTopExposureRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.Include( e => e.WholesaleEclDataLoanBookFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ContractId.Contains(input.Filter))
						.WhereIf(input.MinPreOverrideExposureFilter != null, e => e.PreOverrideExposure >= input.MinPreOverrideExposureFilter)
						.WhereIf(input.MaxPreOverrideExposureFilter != null, e => e.PreOverrideExposure <= input.MaxPreOverrideExposureFilter)
						.WhereIf(input.MinPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment >= input.MinPreOverrideImpairmentFilter)
						.WhereIf(input.MaxPreOverrideImpairmentFilter != null, e => e.PreOverrideImpairment <= input.MaxPreOverrideImpairmentFilter)
						.WhereIf(input.MinPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio >= input.MinPreOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPreOverrideCoverageRatioFilter != null, e => e.PreOverrideCoverageRatio <= input.MaxPreOverrideCoverageRatioFilter)
						.WhereIf(input.MinPostOverrideExposureFilter != null, e => e.PostOverrideExposure >= input.MinPostOverrideExposureFilter)
						.WhereIf(input.MaxPostOverrideExposureFilter != null, e => e.PostOverrideExposure <= input.MaxPostOverrideExposureFilter)
						.WhereIf(input.MinPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment >= input.MinPostOverrideImpairmentFilter)
						.WhereIf(input.MaxPostOverrideImpairmentFilter != null, e => e.PostOverrideImpairment <= input.MaxPostOverrideImpairmentFilter)
						.WhereIf(input.MinPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio >= input.MinPostOverrideCoverageRatioFilter)
						.WhereIf(input.MaxPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio <= input.MaxPostOverrideCoverageRatioFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ContractIdFilter),  e => e.ContractId.ToLower() == input.ContractIdFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.WholesaleEclDataLoanBookCustomerNameFilter), e => e.WholesaleEclDataLoanBookFk != null && e.WholesaleEclDataLoanBookFk.CustomerName.ToLower() == input.WholesaleEclDataLoanBookCustomerNameFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclResultSummaryTopExposures = filteredWholesaleEclResultSummaryTopExposures
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclResultSummaryTopExposures = from o in pagedAndFilteredWholesaleEclResultSummaryTopExposures
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_wholesaleEclDataLoanBookRepository.GetAll() on o.WholesaleEclDataLoanBookId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         select new GetWholesaleEclResultSummaryTopExposureForViewDto() {
							WholesaleEclResultSummaryTopExposure = new WholesaleEclResultSummaryTopExposureDto
							{
                                PreOverrideExposure = o.PreOverrideExposure,
                                PreOverrideImpairment = o.PreOverrideImpairment,
                                PreOverrideCoverageRatio = o.PreOverrideCoverageRatio,
                                PostOverrideExposure = o.PostOverrideExposure,
                                PostOverrideImpairment = o.PostOverrideImpairment,
                                PostOverrideCoverageRatio = o.PostOverrideCoverageRatio,
                                ContractId = o.ContractId,
                                Id = o.Id
							},
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString(),
                         	WholesaleEclDataLoanBookCustomerName = s2 == null ? "" : s2.CustomerName.ToString()
						};

            var totalCount = await filteredWholesaleEclResultSummaryTopExposures.CountAsync();

            return new PagedResultDto<GetWholesaleEclResultSummaryTopExposureForViewDto>(
                totalCount,
                await wholesaleEclResultSummaryTopExposures.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Edit)]
		 public async Task<GetWholesaleEclResultSummaryTopExposureForEditOutput> GetWholesaleEclResultSummaryTopExposureForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclResultSummaryTopExposure = await _wholesaleEclResultSummaryTopExposureRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclResultSummaryTopExposureForEditOutput {WholesaleEclResultSummaryTopExposure = ObjectMapper.Map<CreateOrEditWholesaleEclResultSummaryTopExposureDto>(wholesaleEclResultSummaryTopExposure)};

		    if (output.WholesaleEclResultSummaryTopExposure.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultSummaryTopExposure.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }

		    if (output.WholesaleEclResultSummaryTopExposure.WholesaleEclDataLoanBookId != null)
            {
                var _lookupWholesaleEclDataLoanBook = await _lookup_wholesaleEclDataLoanBookRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultSummaryTopExposure.WholesaleEclDataLoanBookId);
                output.WholesaleEclDataLoanBookCustomerName = _lookupWholesaleEclDataLoanBook.CustomerName.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryTopExposureDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Create)]
		 protected virtual async Task Create(CreateOrEditWholesaleEclResultSummaryTopExposureDto input)
         {
            var wholesaleEclResultSummaryTopExposure = ObjectMapper.Map<WholesaleEclResultSummaryTopExposure>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclResultSummaryTopExposure.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclResultSummaryTopExposureRepository.InsertAsync(wholesaleEclResultSummaryTopExposure);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Edit)]
		 protected virtual async Task Update(CreateOrEditWholesaleEclResultSummaryTopExposureDto input)
         {
            var wholesaleEclResultSummaryTopExposure = await _wholesaleEclResultSummaryTopExposureRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclResultSummaryTopExposure);
         }

		 [AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclResultSummaryTopExposureRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures)]
         public async Task<PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures)]
         public async Task<PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclDataLoanBookRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.CustomerName.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclDataLoanBookList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableDto>();
			foreach(var wholesaleEclDataLoanBook in wholesaleEclDataLoanBookList){
				lookupTableDtoList.Add(new WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableDto
				{
					Id = wholesaleEclDataLoanBook.Id.ToString(),
					DisplayName = wholesaleEclDataLoanBook.CustomerName?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}