using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeResults
{
    public interface IObesaleEclResultSummariesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObesaleEclResultSummaryForViewDto>> GetAll(GetAllObesaleEclResultSummariesInput input);

		Task<GetObesaleEclResultSummaryForEditOutput> GetObesaleEclResultSummaryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObesaleEclResultSummaryDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObesaleEclResultSummaryObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}