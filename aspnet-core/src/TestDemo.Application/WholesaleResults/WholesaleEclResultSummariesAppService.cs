using TestDemo.Wholesale;

using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.WholesaleResults.Dtos;

namespace TestDemo.WholesaleResults
{
    public class WholesaleEclResultSummariesAppService : TestDemoAppServiceBase, IWholesaleEclResultSummariesAppService
    {
		 private readonly IRepository<WholesaleEclResultSummary, Guid> _wholesaleEclResultSummaryRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 

		  public WholesaleEclResultSummariesAppService(IRepository<WholesaleEclResultSummary, Guid> wholesaleEclResultSummaryRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository) 
		  {
			_wholesaleEclResultSummaryRepository = wholesaleEclResultSummaryRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		
		  }

		 public async Task<PagedResultDto<GetWholesaleEclResultSummaryForViewDto>> GetAll(GetAllWholesaleEclResultSummariesInput input)
         {
			var summaryTypeFilter = (ResultSummaryTypeEnum) input.SummaryTypeFilter;
			
			var filteredWholesaleEclResultSummaries = _wholesaleEclResultSummaryRepository.GetAll()
						.Include( e => e.WholesaleEclFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Title.Contains(input.Filter))
						.WhereIf(input.SummaryTypeFilter > -1, e => e.SummaryType == summaryTypeFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter),  e => e.Title.ToLower() == input.TitleFilter.ToLower().Trim());

			var pagedAndFilteredWholesaleEclResultSummaries = filteredWholesaleEclResultSummaries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var wholesaleEclResultSummaries = from o in pagedAndFilteredWholesaleEclResultSummaries
                         join o1 in _lookup_wholesaleEclRepository.GetAll() on o.WholesaleEclId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetWholesaleEclResultSummaryForViewDto() {
							WholesaleEclResultSummary = new WholesaleEclResultSummaryDto
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
                         	WholesaleEclTenantId = s1 == null ? "" : s1.TenantId.ToString()
						};

            var totalCount = await filteredWholesaleEclResultSummaries.CountAsync();

            return new PagedResultDto<GetWholesaleEclResultSummaryForViewDto>(
                totalCount,
                await wholesaleEclResultSummaries.ToListAsync()
            );
         }
		 
		 public async Task<GetWholesaleEclResultSummaryForEditOutput> GetWholesaleEclResultSummaryForEdit(EntityDto<Guid> input)
         {
            var wholesaleEclResultSummary = await _wholesaleEclResultSummaryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetWholesaleEclResultSummaryForEditOutput {WholesaleEclResultSummary = ObjectMapper.Map<CreateOrEditWholesaleEclResultSummaryDto>(wholesaleEclResultSummary)};

		    if (output.WholesaleEclResultSummary.WholesaleEclId != null)
            {
                var _lookupWholesaleEcl = await _lookup_wholesaleEclRepository.FirstOrDefaultAsync((Guid)output.WholesaleEclResultSummary.WholesaleEclId);
                output.WholesaleEclTenantId = _lookupWholesaleEcl.TenantId.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 protected virtual async Task Create(CreateOrEditWholesaleEclResultSummaryDto input)
         {
            var wholesaleEclResultSummary = ObjectMapper.Map<WholesaleEclResultSummary>(input);

			
			if (AbpSession.TenantId != null)
			{
				wholesaleEclResultSummary.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _wholesaleEclResultSummaryRepository.InsertAsync(wholesaleEclResultSummary);
         }

		 protected virtual async Task Update(CreateOrEditWholesaleEclResultSummaryDto input)
         {
            var wholesaleEclResultSummary = await _wholesaleEclResultSummaryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, wholesaleEclResultSummary);
         }

         public async Task Delete(EntityDto<Guid> input)
         {
            await _wholesaleEclResultSummaryRepository.DeleteAsync(input.Id);
         } 

         public async Task<PagedResultDto<WholesaleEclResultSummaryWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_wholesaleEclRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.TenantId.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var wholesaleEclList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<WholesaleEclResultSummaryWholesaleEclLookupTableDto>();
			foreach(var wholesaleEcl in wholesaleEclList){
				lookupTableDtoList.Add(new WholesaleEclResultSummaryWholesaleEclLookupTableDto
				{
					Id = wholesaleEcl.Id.ToString(),
					DisplayName = wholesaleEcl.TenantId?.ToString()
				});
			}

            return new PagedResultDto<WholesaleEclResultSummaryWholesaleEclLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}