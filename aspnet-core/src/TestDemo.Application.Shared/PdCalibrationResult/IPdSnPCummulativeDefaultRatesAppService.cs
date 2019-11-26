using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdSnPCummulativeDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllPdSnPCummulativeDefaultRatesInput input);

		Task<GetPdSnPCummulativeDefaultRateForEditOutput> GetPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdSnPCummulativeDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}