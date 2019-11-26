using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputatoin.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputatoin
{
    public interface IWholesalePdLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdLifetimeOptimisticForViewDto>> GetAll(GetAllWholesalePdLifetimeOptimisticsInput input);

		Task<GetWholesalePdLifetimeOptimisticForEditOutput> GetWholesalePdLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdLifetimeOptimisticWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}