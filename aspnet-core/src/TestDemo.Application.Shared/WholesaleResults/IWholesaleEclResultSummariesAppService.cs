using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleResults
{
    public interface IWholesaleEclResultSummariesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclResultSummaryForViewDto>> GetAll(GetAllWholesaleEclResultSummariesInput input);

		Task<GetWholesaleEclResultSummaryForEditOutput> GetWholesaleEclResultSummaryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclResultSummaryWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}