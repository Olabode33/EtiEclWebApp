using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.GeneralCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.GeneralCalibrationResult
{
    public interface ICalibrationResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCalibrationResultForViewDto>> GetAll(GetAllCalibrationResultsInput input);

		Task<GetCalibrationResultForEditOutput> GetCalibrationResultForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditCalibrationResultDto input);

		Task Delete(EntityDto input);

		
    }
}