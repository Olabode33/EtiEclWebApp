using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdCummulativeSurvivalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdCummulativeSurvivalForViewDto>> GetAll(GetAllPdCummulativeSurvivalsInput input);

		Task<GetPdCummulativeSurvivalForEditOutput> GetPdCummulativeSurvivalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdCummulativeSurvivalDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}