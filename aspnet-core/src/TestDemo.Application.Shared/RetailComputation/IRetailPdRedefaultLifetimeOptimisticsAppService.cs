using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailPdRedefaultLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailPdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllRetailPdRedefaultLifetimeOptimisticsInput input);

		Task<GetRetailPdRedefaultLifetimeOptimisticForEditOutput> GetRetailPdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailPdRedefaultLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailPdRedefaultLifetimeOptimisticRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}