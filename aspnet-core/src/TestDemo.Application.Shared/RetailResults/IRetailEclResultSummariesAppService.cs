using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailResults
{
    public interface IRetailEclResultSummariesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclResultSummaryForViewDto>> GetAll(GetAllRetailEclResultSummariesInput input);

		Task<GetRetailEclResultSummaryForEditOutput> GetRetailEclResultSummaryForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclResultSummaryDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclResultSummaryRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}