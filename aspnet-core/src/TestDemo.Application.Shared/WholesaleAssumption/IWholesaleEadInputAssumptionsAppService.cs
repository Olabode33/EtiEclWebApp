using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEadInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEadInputAssumptionForViewDto>> GetAll(GetAllWholesaleEadInputAssumptionsInput input);

		Task<GetWholesaleEadInputAssumptionForEditOutput> GetWholesaleEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEadInputAssumptionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}