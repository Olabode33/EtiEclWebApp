using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdUpperboundsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdUpperboundForViewDto>> GetAll(GetAllPdUpperboundsInput input);

		Task<GetPdUpperboundForEditOutput> GetPdUpperboundForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdUpperboundDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}