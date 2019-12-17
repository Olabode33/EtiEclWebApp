using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumptionMacroeconomicProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllPdInputAssumptionMacroeconomicProjectionsInput input);

		Task<GetPdInputAssumptionMacroeconomicProjectionForEditOutput> GetPdInputAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumptionMacroeconomicProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}