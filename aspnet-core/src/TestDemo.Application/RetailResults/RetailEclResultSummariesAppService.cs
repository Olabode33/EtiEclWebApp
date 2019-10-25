using TestDemo.Retail;

using TestDemo.EclShared;

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
	[AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries)]
    public class RetailEclResultSummariesAppService : TestDemoAppServiceBase, IRetailEclResultSummariesAppService
    {
		 private readonly IRepository<RetailEclResultSummary, Guid> _retailEclResultSummaryRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 

		  public RetailEclResultSummariesAppService(IRepository<RetailEclResultSummary, Guid> retailEclResultSummaryRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository) 
		  {
			_retailEclResultSummaryRepository = retailEclResultSummaryRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetRetailEclResultSummaryForViewDto>> GetAll(GetAllRetailEclResultSummariesInput input)
         {
			var summaryTypeFilter = (ResultSummaryTypeEnum) input.SummaryTypeFilter;
			
			var filteredRetailEclResultSummaries = _retailEclResultSummaryRepository.GetAll()
						.Include( e => e.RetailEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Title.Contains(input.Filter))
						.WhereIf(input.SummaryTypeFilter > -1, e => e.SummaryType == summaryTypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter),  e => e.Title.ToLower() == input.TitleFilter.ToLower().Trim())
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
						.WhereIf(input.MaxPostOverrideCoverageRatioFilter != null, e => e.PostOverrideCoverageRatio <= input.MaxPostOverrideCoverageRatioFilter);

			var pagedAndFilteredRetailEclResultSummaries = filteredRetailEclResultSummaries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var retailEclResultSummaries = from o in pagedAndFilteredRetailEclResultSummaries
                         join o1 in _lookup_retailEclRepository.GetAll() on o.RetailEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetRetailEclResultSummaryForViewDto() {
							RetailEclResultSummary = new RetailEclResultSummaryDto
							{
                                SummaryType = o.SummaryType,
                                Title = o.Title,
                                PreOverrideExposure = o.PreOverrideExposure,
                                PreOverrideImpairment = o.PreOverrideImpairment,
                                PreOverrideCoverageRatio = o.PreOverrideCoverageRatio,
                                PostOverrideExposure = o.PostOverrideExposure,
                                PostOverrideImpairment = o.PostOverrideImpairment,
                                PostOverrideCoverageRatio = o.PostOverrideCoverageRatio,
                                Id = o.Id
							},
                         	RetailEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredRetailEclResultSummaries.CountAsync();

            return new PagedResultDto<GetRetailEclResultSummaryForViewDto>(
                totalCount,
                await retailEclResultSummaries.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries_Edit)]
		 public async Task<GetRetailEclResultSummaryForEditOutput> GetRetailEclResultSummaryForEdit(EntityDto<Guid> input)
         {
            var retailEclResultSummary = await _retailEclResultSummaryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetRetailEclResultSummaryForEditOutput {RetailEclResultSummary = ObjectMapper.Map<CreateOrEditRetailEclResultSummaryDto>(retailEclResultSummary)};

		    if (output.RetailEclResultSummary.RetailEclId != null)
            {
                var _lookupRetailEcl = await _lookup_retailEclRepository.FirstOrDefaultAsync((Guid)output.RetailEclResultSummary.RetailEclId);
                output.RetailEclTenantId = _lookupRetailEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditRetailEclResultSummaryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries_Create)]
		 protected virtual async Task Create(CreateOrEditRetailEclResultSummaryDto input)
         {
            var retailEclResultSummary = ObjectMapper.Map<RetailEclResultSummary>(input);

			
			if (AbpSession.TenantId != null)
			{
				retailEclResultSummary.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _retailEclResultSummaryRepository.InsertAsync(retailEclResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries_Edit)]
		 protected virtual async Task Update(CreateOrEditRetailEclResultSummaryDto input)
         {
            var retailEclResultSummary = await _retailEclResultSummaryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, retailEclResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _retailEclResultSummaryRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_RetailEclResultSummaries)]
         public async Task<PagedResultDto<RetailEclResultSummaryRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_retailEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var retailEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<RetailEclResultSummaryRetailEclLookupTableDto>();
			foreach(var retailEcl in retailEclList){
				lookupTableDtoList.Add(new RetailEclResultSummaryRetailEclLookupTableDto
				{
					Id = retailEcl.Id.ToString(),
					DisplayName = retailEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<RetailEclResultSummaryRetailEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}