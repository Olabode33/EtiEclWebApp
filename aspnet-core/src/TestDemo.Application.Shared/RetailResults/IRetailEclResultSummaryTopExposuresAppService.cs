using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailResults
{
    public interface IRetailEclResultSummaryTopExposuresAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclResultSummaryTopExposureForViewDto>> GetAll(GetAllRetailEclResultSummaryTopExposuresInput input);

		Task<GetRetailEclResultSummaryTopExposureForEditOutput> GetRetailEclResultSummaryTopExposureForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclResultSummaryTopExposureDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclResultSummaryTopExposureRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<RetailEclResultSummaryTopExposureRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}