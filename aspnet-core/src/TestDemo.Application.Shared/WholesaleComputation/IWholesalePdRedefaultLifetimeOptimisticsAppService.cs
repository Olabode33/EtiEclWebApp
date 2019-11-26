using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesalePdRedefaultLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllWholesalePdRedefaultLifetimeOptimisticsInput input);

		Task<GetWholesalePdRedefaultLifetimeOptimisticForEditOutput> GetWholesalePdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdRedefaultLifetimeOptimisticWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}