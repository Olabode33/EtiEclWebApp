

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
	[AbpAuthorize(AppPermissions.Pages_PdCummulativeSurvivals)]
    public class PdCummulativeSurvivalsAppService : TestDemoAppServiceBase, IPdCummulativeSurvivalsAppService
    {
		 private readonly IRepository<CalibrationResultPdCummulativeSurvival, Guid> _pdCummulativeSurvivalRepository;
		 

		  public PdCummulativeSurvivalsAppService(IRepository<CalibrationResultPdCummulativeSurvival, Guid> pdCummulativeSurvivalRepository ) 
		  {
			_pdCummulativeSurvivalRepository = pdCummulativeSurvivalRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdCummulativeSurvivalForViewDto>> GetAll(GetAllPdCummulativeSurvivalsInput input)
         {
			
			var filteredPdCummulativeSurvivals = _pdCummulativeSurvivalRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.PdGroup.Contains(input.Filter));

			var pagedAndFilteredPdCummulativeSurvivals = filteredPdCummulativeSurvivals
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdCummulativeSurvivals = from o in pagedAndFilteredPdCummulativeSurvivals
                         select new GetPdCummulativeSurvivalForViewDto() {
							PdCummulativeSurvival = new PdCummulativeSurvivalDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdCummulativeSurvivals.CountAsync();

            return new PagedResultDto<GetPdCummulativeSurvivalForViewDto>(
                totalCount,
                await pdCummulativeSurvivals.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdCummulativeSurvivals_Edit)]
		 public async Task<GetPdCummulativeSurvivalForEditOutput> GetPdCummulativeSurvivalForEdit(EntityDto<Guid> input)
         {
            var pdCummulativeSurvival = await _pdCummulativeSurvivalRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdCummulativeSurvivalForEditOutput {PdCummulativeSurvival = ObjectMapper.Map<CreateOrEditPdCummulativeSurvivalDto>(pdCummulativeSurvival)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdCummulativeSurvivalDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdCummulativeSurvivals_Create)]
		 protected virtual async Task Create(CreateOrEditPdCummulativeSurvivalDto input)
         {
            var pdCummulativeSurvival = ObjectMapper.Map<CalibrationResultPdCummulativeSurvival>(input);

			

            await _pdCummulativeSurvivalRepository.InsertAsync(pdCummulativeSurvival);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdCummulativeSurvivals_Edit)]
		 protected virtual async Task Update(CreateOrEditPdCummulativeSurvivalDto input)
         {
            var pdCummulativeSurvival = await _pdCummulativeSurvivalRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdCummulativeSurvival);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdCummulativeSurvivals_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdCummulativeSurvivalRepository.DeleteAsync(input.Id);
         } 
    }
}