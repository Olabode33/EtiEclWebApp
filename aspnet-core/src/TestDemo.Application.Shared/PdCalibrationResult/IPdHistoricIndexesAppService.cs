using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdHistoricIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdHistoricIndexForViewDto>> GetAll(GetAllPdHistoricIndexesInput input);

		Task<GetPdHistoricIndexForEditOutput> GetPdHistoricIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdHistoricIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}