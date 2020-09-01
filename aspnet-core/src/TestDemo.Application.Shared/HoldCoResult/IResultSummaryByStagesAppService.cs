using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;


namespace TestDemo.HoldCoResult
{
    public interface IResultSummaryByStagesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetResultSummaryByStageForViewDto>> GetAll(GetAllResultSummaryByStagesInput input);

        Task<GetResultSummaryByStageForViewDto> GetResultSummaryByStageForView(Guid id);

		Task<GetResultSummaryByStageForEditOutput> GetResultSummaryByStageForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditResultSummaryByStageDto input);

		Task Delete(EntityDto<Guid> input);

		
    }
}