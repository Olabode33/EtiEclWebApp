using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPd12MonthPdsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPd12MonthPdForViewDto>> GetAll(GetAllPd12MonthPdsInput input);

		Task<GetPd12MonthPdForEditOutput> GetPd12MonthPdForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPd12MonthPdDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}