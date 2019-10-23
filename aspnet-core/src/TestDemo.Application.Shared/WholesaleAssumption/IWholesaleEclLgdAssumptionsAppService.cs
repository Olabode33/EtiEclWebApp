using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclLgdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclLgdAssumptionForViewDto>> GetAll(GetAllWholesaleEclLgdAssumptionsInput input);

		Task<GetWholesaleEclLgdAssumptionForEditOutput> GetWholesaleEclLgdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclLgdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclLgdAssumptionWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}