using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdStatisticalInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdStatisticalInputForViewDto>> GetAll(GetAllPdStatisticalInputsInput input);

		Task<GetPdStatisticalInputForEditOutput> GetPdStatisticalInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdStatisticalInputDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}