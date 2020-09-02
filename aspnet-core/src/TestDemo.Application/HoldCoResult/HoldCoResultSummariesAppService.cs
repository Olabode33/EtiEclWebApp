

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.HoldCoResult
{
	[AbpAuthorize(AppPermissions.Pages_HoldCoResultSummaries)]
    public class HoldCoResultSummariesAppService : TestDemoAppServiceBase, IHoldCoResultSummariesAppService
    {
		 private readonly IRepository<HoldCoResultSummary, Guid> _holdCoResultSummaryRepository;
		 

		  public HoldCoResultSummariesAppService(IRepository<HoldCoResultSummary, Guid> holdCoResultSummaryRepository ) 
		  {
			_holdCoResultSummaryRepository = holdCoResultSummaryRepository;
			
		  }

        public async Task<List<CreateOrEditHoldCoResultSummaryDto>> GetResults(Guid id)
        {
            return ObjectMapper.Map<List<CreateOrEditHoldCoResultSummaryDto>>(await _holdCoResultSummaryRepository.GetAll().Where(a => a.RegistrationId == id).ToListAsync());

        }

        public async Task<PagedResultDto<GetHoldCoResultSummaryForViewDto>> GetAll(GetAllHoldCoResultSummariesInput input)
         {
			
			var filteredHoldCoResultSummaries = _holdCoResultSummaryRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.DownturnTotal.Contains(input.Filter) || e.Diff.Contains(input.Filter));

			var pagedAndFilteredHoldCoResultSummaries = filteredHoldCoResultSummaries
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var holdCoResultSummaries = from o in pagedAndFilteredHoldCoResultSummaries
                         select new GetHoldCoResultSummaryForViewDto() {
							HoldCoResultSummary = new HoldCoResultSummaryDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredHoldCoResultSummaries.CountAsync();

            return new PagedResultDto<GetHoldCoResultSummaryForViewDto>(
                totalCount,
                await holdCoResultSummaries.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_HoldCoResultSummaries_Edit)]
		 public async Task<GetHoldCoResultSummaryForEditOutput> GetHoldCoResultSummaryForEdit(EntityDto<Guid> input)
         {
            var holdCoResultSummary = await _holdCoResultSummaryRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetHoldCoResultSummaryForEditOutput {HoldCoResultSummary = ObjectMapper.Map<CreateOrEditHoldCoResultSummaryDto>(holdCoResultSummary)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditHoldCoResultSummaryDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoResultSummaries_Create)]
		 protected virtual async Task Create(CreateOrEditHoldCoResultSummaryDto input)
         {
            var holdCoResultSummary = ObjectMapper.Map<HoldCoResultSummary>(input);

			

            await _holdCoResultSummaryRepository.InsertAsync(holdCoResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoResultSummaries_Edit)]
		 protected virtual async Task Update(CreateOrEditHoldCoResultSummaryDto input)
         {
            var holdCoResultSummary = await _holdCoResultSummaryRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, holdCoResultSummary);
         }

		 [AbpAuthorize(AppPermissions.Pages_HoldCoResultSummaries_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _holdCoResultSummaryRepository.DeleteAsync(input.Id);
         } 
    }
}