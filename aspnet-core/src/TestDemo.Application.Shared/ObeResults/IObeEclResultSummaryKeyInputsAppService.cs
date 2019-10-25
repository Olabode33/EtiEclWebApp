using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeResults
{
    public interface IObeEclResultSummaryKeyInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclResultSummaryKeyInputForViewDto>> GetAll(GetAllObeEclResultSummaryKeyInputsInput input);

		Task<GetObeEclResultSummaryKeyInputForEditOutput> GetObeEclResultSummaryKeyInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclResultSummaryKeyInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclResultSummaryKeyInputObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}