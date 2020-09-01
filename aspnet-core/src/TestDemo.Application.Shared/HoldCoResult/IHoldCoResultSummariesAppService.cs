using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;


namespace TestDemo.HoldCoResult
{
    public interface IHoldCoResultSummariesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetHoldCoResultSummaryForViewDto>> GetAll(GetAllHoldCoResultSummariesInput input);

		Task<GetHoldCoResultSummaryForEditOutput> GetHoldCoResultSummaryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditHoldCoResultSummaryDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}