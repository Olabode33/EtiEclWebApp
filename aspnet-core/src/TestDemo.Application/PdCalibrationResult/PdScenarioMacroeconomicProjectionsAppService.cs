
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.PdCalibrationResult
{
	[AbpAuthorize(AppPermissions.Pages_PdScenarioMacroeconomicProjections)]
    public class PdScenarioMacroeconomicProjectionsAppService : TestDemoAppServiceBase, IPdScenarioMacroeconomicProjectionsAppService
    {
		 private readonly IRepository<CalibrationResultPdScenarioMacroeconomicProjection, Guid> _pdScenarioMacroeconomicProjectionRepository;
		 

		  public PdScenarioMacroeconomicProjectionsAppService(IRepository<CalibrationResultPdScenarioMacroeconomicProjection, Guid> pdScenarioMacroeconomicProjectionRepository ) 
		  {
			_pdScenarioMacroeconomicProjectionRepository = pdScenarioMacroeconomicProjectionRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdScenarioMacroeconomicProjectionForViewDto>> GetAll(GetAllPdScenarioMacroeconomicProjectionsInput input)
         {
			
			var filteredPdScenarioMacroeconomicProjections = _pdScenarioMacroeconomicProjectionRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false );

			var pagedAndFilteredPdScenarioMacroeconomicProjections = filteredPdScenarioMacroeconomicProjections
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdScenarioMacroeconomicProjections = from o in pagedAndFilteredPdScenarioMacroeconomicProjections
                         select new GetPdScenarioMacroeconomicProjectionForViewDto() {
							PdScenarioMacroeconomicProjection = new PdScenarioMacroeconomicProjectionDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdScenarioMacroeconomicProjections.CountAsync();

            return new PagedResultDto<GetPdScenarioMacroeconomicProjectionForViewDto>(
                totalCount,
                await pdScenarioMacroeconomicProjections.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Edit)]
		 public async Task<GetPdScenarioMacroeconomicProjectionForEditOutput> GetPdScenarioMacroeconomicProjectionForEdit(EntityDto<Guid> input)
         {
            var pdScenarioMacroeconomicProjection = await _pdScenarioMacroeconomicProjectionRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdScenarioMacroeconomicProjectionForEditOutput {PdScenarioMacroeconomicProjection = ObjectMapper.Map<CreateOrEditPdScenarioMacroeconomicProjectionDto>(pdScenarioMacroeconomicProjection)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdScenarioMacroeconomicProjectionDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Create)]
		 protected virtual async Task Create(CreateOrEditPdScenarioMacroeconomicProjectionDto input)
         {
            var pdScenarioMacroeconomicProjection = ObjectMapper.Map<CalibrationResultPdScenarioMacroeconomicProjection>(input);

			

            await _pdScenarioMacroeconomicProjectionRepository.InsertAsync(pdScenarioMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Edit)]
		 protected virtual async Task Update(CreateOrEditPdScenarioMacroeconomicProjectionDto input)
         {
            var pdScenarioMacroeconomicProjection = await _pdScenarioMacroeconomicProjectionRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdScenarioMacroeconomicProjection);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdScenarioMacroeconomicProjectionRepository.DeleteAsync(input.Id);
         } 
    }
}