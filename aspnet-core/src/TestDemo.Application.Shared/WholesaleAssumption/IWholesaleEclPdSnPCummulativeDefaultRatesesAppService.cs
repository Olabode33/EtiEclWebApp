using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclPdSnPCummulativeDefaultRatesesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdSnPCummulativeDefaultRatesForViewDto>> GetAll(GetAllWholesaleEclPdSnPCummulativeDefaultRatesesInput input);

		Task<GetWholesaleEclPdSnPCummulativeDefaultRatesForEditOutput> GetWholesaleEclPdSnPCummulativeDefaultRatesForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}