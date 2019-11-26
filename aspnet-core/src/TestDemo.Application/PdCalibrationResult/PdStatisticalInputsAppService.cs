

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
	[AbpAuthorize(AppPermissions.Pages_PdStatisticalInputs)]
    public class PdStatisticalInputsAppService : TestDemoAppServiceBase, IPdStatisticalInputsAppService
    {
		 private readonly IRepository<CalibrationResultPdStatisticalInput, Guid> _pdStatisticalInputRepository;
		 

		  public PdStatisticalInputsAppService(IRepository<CalibrationResultPdStatisticalInput, Guid> pdStatisticalInputRepository ) 
		  {
			_pdStatisticalInputRepository = pdStatisticalInputRepository;
			
		  }

		 public async Task<PagedResultDto<GetPdStatisticalInputForViewDto>> GetAll(GetAllPdStatisticalInputsInput input)
         {
			
			var filteredPdStatisticalInputs = _pdStatisticalInputRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.StatisticalInputs.Contains(input.Filter));

			var pagedAndFilteredPdStatisticalInputs = filteredPdStatisticalInputs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var pdStatisticalInputs = from o in pagedAndFilteredPdStatisticalInputs
                         select new GetPdStatisticalInputForViewDto() {
							PdStatisticalInput = new PdStatisticalInputDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredPdStatisticalInputs.CountAsync();

            return new PagedResultDto<GetPdStatisticalInputForViewDto>(
                totalCount,
                await pdStatisticalInputs.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_PdStatisticalInputs_Edit)]
		 public async Task<GetPdStatisticalInputForEditOutput> GetPdStatisticalInputForEdit(EntityDto<Guid> input)
         {
            var pdStatisticalInput = await _pdStatisticalInputRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPdStatisticalInputForEditOutput {PdStatisticalInput = ObjectMapper.Map<CreateOrEditPdStatisticalInputDto>(pdStatisticalInput)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPdStatisticalInputDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_PdStatisticalInputs_Create)]
		 protected virtual async Task Create(CreateOrEditPdStatisticalInputDto input)
         {
            var pdStatisticalInput = ObjectMapper.Map<CalibrationResultPdStatisticalInput>(input);

			

            await _pdStatisticalInputRepository.InsertAsync(pdStatisticalInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdStatisticalInputs_Edit)]
		 protected virtual async Task Update(CreateOrEditPdStatisticalInputDto input)
         {
            var pdStatisticalInput = await _pdStatisticalInputRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, pdStatisticalInput);
         }

		 [AbpAuthorize(AppPermissions.Pages_PdStatisticalInputs_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _pdStatisticalInputRepository.DeleteAsync(input.Id);
         } 
    }
}