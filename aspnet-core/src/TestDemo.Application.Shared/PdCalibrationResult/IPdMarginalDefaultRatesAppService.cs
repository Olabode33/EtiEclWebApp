using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdMarginalDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdMarginalDefaultRateForViewDto>> GetAll(GetAllPdMarginalDefaultRatesInput input);

		Task<GetPdMarginalDefaultRateForEditOutput> GetPdMarginalDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdMarginalDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}