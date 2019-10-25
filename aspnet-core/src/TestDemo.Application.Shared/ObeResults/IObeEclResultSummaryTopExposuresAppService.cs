using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeResults
{
    public interface IObeEclResultSummaryTopExposuresAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclResultSummaryTopExposureForViewDto>> GetAll(GetAllObeEclResultSummaryTopExposuresInput input);

		Task<GetObeEclResultSummaryTopExposureForEditOutput> GetObeEclResultSummaryTopExposureForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclResultSummaryTopExposureDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclResultSummaryTopExposureObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<ObeEclResultSummaryTopExposureObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}