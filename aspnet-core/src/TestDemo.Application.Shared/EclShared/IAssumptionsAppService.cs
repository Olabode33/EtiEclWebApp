using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAssumptionForViewDto>> GetAll(GetAllAssumptionsInput input);

		Task<GetAssumptionForEditOutput> GetAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}