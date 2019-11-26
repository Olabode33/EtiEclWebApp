using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdLifetimeOptimisticForViewDto>> GetAll(GetAllObePdLifetimeOptimisticsInput input);

		Task<GetObePdLifetimeOptimisticForEditOutput> GetObePdLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdLifetimeOptimisticObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}