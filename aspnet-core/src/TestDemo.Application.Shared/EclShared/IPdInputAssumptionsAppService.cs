using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumptionForViewDto>> GetAll(GetAllPdInputAssumptionsInput input);

		Task<GetPdInputAssumptionForEditOutput> GetPdInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}