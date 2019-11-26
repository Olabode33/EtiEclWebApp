using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdScenarioMacroeconomicProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdScenarioMacroeconomicProjectionForViewDto>> GetAll(GetAllPdScenarioMacroeconomicProjectionsInput input);

		Task<GetPdScenarioMacroeconomicProjectionForEditOutput> GetPdScenarioMacroeconomicProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdScenarioMacroeconomicProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}