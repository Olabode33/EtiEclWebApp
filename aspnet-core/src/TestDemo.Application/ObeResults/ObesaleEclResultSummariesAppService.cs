using TestDemo.OBE;

using TestDemo.EclShared;

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
	[AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries)]
    public class ObesaleEclResultSummariesAppService : TestDemoAppServiceBase, IObesaleEclResultSummariesAppService
    {
		 private readonly IRepository<ObesaleEclResultSummary, Guid> _obesaleEclResultSummaryRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 

		  public ObesaleEclResultSummariesAppService(IRepository<ObesaleEclResultSummary, Guid> obesaleEclResultSummaryRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository) 
		  {
			_obesaleEclResultSummaryRepository = obesaleEclResultSummaryRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetObesaleEclResultSummaryForViewDto>> GetAll(GetAllObesaleEclResultSummariesInput input)
         {
			var summaryTypeFilter = (ResultSummaryTypeEnum) input.SummaryTypeFilter;
			
			var filteredObesaleEclResultSummaries = _obesaleEclResultSummaryRepository.GetAll()
						.Include( e => e.ObeEclFk)
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

			var pagedAndFilteredObesaleEclResultSummaries = filteredObesaleEclResultSummaries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var obesaleEclResultSummaries = from o in pagedAndFilteredObesaleEclResultSummaries
                         join o1 in _lookup_obeEclRepository.GetAll() on o.ObeEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetObesaleEclResultSummaryForViewDto() {
							ObesaleEclResultSummary = new ObesaleEclResultSummaryDto
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
                         	ObeEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredObesaleEclResultSummaries.CountAsync();

            return new PagedResultDto<GetObesaleEclResultSummaryForViewDto>(
                totalCount,
                await obesaleEclResultSummaries.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries_Edit)]
		 public async Task<GetObesaleEclResultSummaryForEditOutput> GetObesaleEclResultSummaryForEdit(EntityDto<Guid> input)
         {
            var obesaleEclResultSummary = await _obesaleEclResultSummaryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetObesaleEclResultSummaryForEditOutput {ObesaleEclResultSummary = ObjectMapper.Map<CreateOrEditObesaleEclResultSummaryDto>(obesaleEclResultSummary)};

		    if (output.ObesaleEclResultSummary.ObeEclId != null)
            {
                var _lookupObeEcl = await _lookup_obeEclRepository.FirstOrDefaultAsync((Guid)output.ObesaleEclResultSummary.ObeEclId);
                output.ObeEclTenantId = _lookupObeEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditObesaleEclResultSummaryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries_Create)]
		 protected virtual async Task Create(CreateOrEditObesaleEclResultSummaryDto input)
         {
            var obesaleEclResultSummary = ObjectMapper.Map<ObesaleEclResultSummary>(input);

			
			if (AbpSession.TenantId != null)
			{
				obesaleEclResultSummary.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _obesaleEclResultSummaryRepository.InsertAsync(obesaleEclResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries_Edit)]
		 protected virtual async Task Update(CreateOrEditObesaleEclResultSummaryDto input)
         {
            var obesaleEclResultSummary = await _obesaleEclResultSummaryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, obesaleEclResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _obesaleEclResultSummaryRepository.DeleteAsync(input.Id);
         } 

		[AbpAuthorize(AppPermissions.Pages_ObesaleEclResultSummaries)]
         public async Task<PagedResultDto<ObesaleEclResultSummaryObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_obeEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var obeEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ObesaleEclResultSummaryObeEclLookupTableDto>();
			foreach(var obeEcl in obeEclList){
				lookupTableDtoList.Add(new ObesaleEclResultSummaryObeEclLookupTableDto
				{
					Id = obeEcl.Id.ToString(),
					DisplayName = obeEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<ObesaleEclResultSummaryObeEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}