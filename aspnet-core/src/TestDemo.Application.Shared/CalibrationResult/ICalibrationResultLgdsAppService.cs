using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.LgdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.LgdCalibrationResult
{
    public interface ICalibrationResultLgdsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetCalibrationResultLgdForViewDto>> GetAll(GetAllCalibrationResultLgdsInput input);

		Task<GetCalibrationResultLgdForEditOutput> GetCalibrationResultLgdForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditCalibrationResultLgdDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}