using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdLifetimeOptimisticForViewDto>> GetAll(GetAllRetailPdLifetimeOptimisticsInput input);

		Task<GetRetailPdLifetimeOptimisticForEditOutput> GetRetailPdLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdLifetimeOptimisticRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}