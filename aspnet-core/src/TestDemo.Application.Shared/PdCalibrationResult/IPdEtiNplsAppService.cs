using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.PdCalibrationResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.PdCalibrationResult
{
    public interface IPdEtiNplsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdEtiNplForViewDto>> GetAll(GetAllPdEtiNplsInput input);

		Task<GetPdEtiNplForEditOutput> GetPdEtiNplForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdEtiNplDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}