
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EclShared
{
	[AbpAuthorize(AppPermissions.Pages_Assumptions)]
    public class AssumptionsAppService : TestDemoAppServiceBase, IAssumptionsAppService
    {
		 private readonly IRepository<Assumption, Guid> _assumptionRepository;
		 

		  public AssumptionsAppService(IRepository<Assumption, Guid> assumptionRepository ) 
		  {
			_assumptionRepository = assumptionRepository;
			
		  }

		 public async Task<PagedResultDto<GetAssumptionForViewDto>> GetAll(GetAllAssumptionsInput input)
         {
			
			var filteredAssumptions = _assumptionRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.InputNameFilter),  e => e.InputName.ToLower() == input.InputNameFilter.ToLower().Trim());

			var pagedAndFilteredAssumptions = filteredAssumptions
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var assumptions = from o in pagedAndFilteredAssumptions
                         select new GetAssumptionForViewDto() {
							Assumption = new AssumptionDto
							{
                                InputName = o.InputName,
                                Value = o.Value,
                                Id = o.Id
							}
						};

            var totalCount = await filteredAssumptions.CountAsync();

            return new PagedResultDto<GetAssumptionForViewDto>(
                totalCount,
                await assumptions.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Assumptions_Edit)]
		 public async Task<GetAssumptionForEditOutput> GetAssumptionForEdit(EntityDto<Guid> input)
         {
            var assumption = await _assumptionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAssumptionForEditOutput {Assumption = ObjectMapper.Map<CreateOrEditAssumptionDto>(assumption)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAssumptionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Assumptions_Create)]
		 protected virtual async Task Create(CreateOrEditAssumptionDto input)
         {
            var assumption = ObjectMapper.Map<Assumption>(input);

			

            await _assumptionRepository.InsertAsync(assumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_Assumptions_Edit)]
		 protected virtual async Task Update(CreateOrEditAssumptionDto input)
         {
            var assumption = await _assumptionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, assumption);
         }

		 [AbpAuthorize(AppPermissions.Pages_Assumptions_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _assumptionRepository.DeleteAsync(input.Id);
         } 
    }
}