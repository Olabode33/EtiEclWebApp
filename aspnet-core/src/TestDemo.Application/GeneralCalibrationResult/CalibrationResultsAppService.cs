
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.GeneralCalibrationResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.GeneralCalibrationResult
{
	[AbpAuthorize(AppPermissions.Pages_CalibrationResults)]
    public class CalibrationResultsAppService : TestDemoAppServiceBase, ICalibrationResultsAppService
    {
		 private readonly IRepository<GeneralCalibrationResult> _calibrationResultRepository;
		 

		  public CalibrationResultsAppService(IRepository<GeneralCalibrationResult> calibrationResultRepository ) 
		  {
			_calibrationResultRepository = calibrationResultRepository;
			
		  }

		 public async Task<PagedResultDto<GetCalibrationResultForViewDto>> GetAll(GetAllCalibrationResultsInput input)
         {
			
			var filteredCalibrationResults = _calibrationResultRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Key.Contains(input.Filter) || e.InputName.Contains(input.Filter) || e.Value.Contains(input.Filter));

			var pagedAndFilteredCalibrationResults = filteredCalibrationResults
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var calibrationResults = from o in pagedAndFilteredCalibrationResults
                         select new GetCalibrationResultForViewDto() {
							CalibrationResult = new CalibrationResultDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredCalibrationResults.CountAsync();

            return new PagedResultDto<GetCalibrationResultForViewDto>(
                totalCount,
                await calibrationResults.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_CalibrationResults_Edit)]
		 public async Task<GetCalibrationResultForEditOutput> GetCalibrationResultForEdit(EntityDto input)
         {
            var calibrationResult = await _calibrationResultRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCalibrationResultForEditOutput {CalibrationResult = ObjectMapper.Map<CreateOrEditCalibrationResultDto>(calibrationResult)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCalibrationResultDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResults_Create)]
		 protected virtual async Task Create(CreateOrEditCalibrationResultDto input)
         {
            var calibrationResult = ObjectMapper.Map<GeneralCalibrationResult>(input);

			

            await _calibrationResultRepository.InsertAsync(calibrationResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResults_Edit)]
		 protected virtual async Task Update(CreateOrEditCalibrationResultDto input)
         {
            var calibrationResult = await _calibrationResultRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, calibrationResult);
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResults_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _calibrationResultRepository.DeleteAsync(input.Id);
         } 
    }
}