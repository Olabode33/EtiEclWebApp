
using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.LgdCalibrationResult.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.LgdCalibrationResult
{
	[AbpAuthorize(AppPermissions.Pages_CalibrationResultLgds)]
    public class CalibrationResultLgdsAppService : TestDemoAppServiceBase, ICalibrationResultLgdsAppService
    {
		 private readonly IRepository<CalibrationResultLgd, Guid> _calibrationResultLgdRepository;
		 

		  public CalibrationResultLgdsAppService(IRepository<CalibrationResultLgd, Guid> calibrationResultLgdRepository ) 
		  {
			_calibrationResultLgdRepository = calibrationResultLgdRepository;
			
		  }

		 public async Task<PagedResultDto<GetCalibrationResultLgdForViewDto>> GetAll(GetAllCalibrationResultLgdsInput input)
         {
			
			var filteredCalibrationResultLgds = _calibrationResultLgdRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.InputName.Contains(input.Filter) || e.InputValue.Contains(input.Filter));

			var pagedAndFilteredCalibrationResultLgds = filteredCalibrationResultLgds
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var calibrationResultLgds = from o in pagedAndFilteredCalibrationResultLgds
                         select new GetCalibrationResultLgdForViewDto() {
							CalibrationResultLgd = new CalibrationResultLgdDto
							{
                                Id = o.Id
							}
						};

            var totalCount = await filteredCalibrationResultLgds.CountAsync();

            return new PagedResultDto<GetCalibrationResultLgdForViewDto>(
                totalCount,
                await calibrationResultLgds.ToListAsync()
            );
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_CalibrationResultLgds_Edit)]
		 public async Task<GetCalibrationResultLgdForEditOutput> GetCalibrationResultLgdForEdit(EntityDto<Guid> input)
         {
            var calibrationResultLgd = await _calibrationResultLgdRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetCalibrationResultLgdForEditOutput {CalibrationResultLgd = ObjectMapper.Map<CreateOrEditCalibrationResultLgdDto>(calibrationResultLgd)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditCalibrationResultLgdDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResultLgds_Create)]
		 protected virtual async Task Create(CreateOrEditCalibrationResultLgdDto input)
         {
            var calibrationResultLgd = ObjectMapper.Map<CalibrationResultLgd>(input);

			

            await _calibrationResultLgdRepository.InsertAsync(calibrationResultLgd);
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResultLgds_Edit)]
		 protected virtual async Task Update(CreateOrEditCalibrationResultLgdDto input)
         {
            var calibrationResultLgd = await _calibrationResultLgdRepository.FirstOrDefaultAsync((Guid)input.Id);
             ObjectMapper.Map(input, calibrationResultLgd);
         }

		 [AbpAuthorize(AppPermissions.Pages_CalibrationResultLgds_Delete)]
         public async Task Delete(EntityDto<Guid> input)
         {
            await _calibrationResultLgdRepository.DeleteAsync(input.Id);
         } 
    }
}