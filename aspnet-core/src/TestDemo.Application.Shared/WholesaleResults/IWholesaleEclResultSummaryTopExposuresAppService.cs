using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleResults
{
    public interface IWholesaleEclResultSummaryTopExposuresAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclResultSummaryTopExposureForViewDto>> GetAll(GetAllWholesaleEclResultSummaryTopExposuresInput input);

		Task<GetWholesaleEclResultSummaryTopExposureForEditOutput> GetWholesaleEclResultSummaryTopExposureForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryTopExposureDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}