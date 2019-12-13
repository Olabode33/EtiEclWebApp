using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.EclShared.Dtos;
using TestDemo.Dto;

namespace TestDemo.EclShared
{
    public interface IPdInputAssumptionStatisticalsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPdInputAssumptionStatisticalForViewDto>> GetAll(GetAllPdInputAssumptionStatisticalsInput input);

		Task<GetPdInputAssumptionStatisticalForEditOutput> GetPdInputAssumptionStatisticalForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditPdInputAssumptionStatisticalDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}