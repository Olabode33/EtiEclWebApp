using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IEadInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetEadInputAssumptionForViewDto>> GetAll(GetAllEadInputAssumptionsInput input);

		Task<GetEadInputAssumptionForEditOutput> GetEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

        Task UpdateStatus(UpdateAssumptionStatusDto input);
    }
}