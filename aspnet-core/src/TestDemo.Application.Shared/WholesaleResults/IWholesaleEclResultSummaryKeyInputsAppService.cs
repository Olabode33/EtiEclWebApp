using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleResults
{
    public interface IWholesaleEclResultSummaryKeyInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclResultSummaryKeyInputForViewDto>> GetAll(GetAllWholesaleEclResultSummaryKeyInputsInput input);

		Task<GetWholesaleEclResultSummaryKeyInputForEditOutput> GetWholesaleEclResultSummaryKeyInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclResultSummaryKeyInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}