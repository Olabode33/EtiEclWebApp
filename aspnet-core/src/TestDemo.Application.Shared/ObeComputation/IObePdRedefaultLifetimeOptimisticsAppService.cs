using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdRedefaultLifetimeOptimisticsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdRedefaultLifetimeOptimisticForViewDto>> GetAll(GetAllObePdRedefaultLifetimeOptimisticsInput input);

		Task<GetObePdRedefaultLifetimeOptimisticForEditOutput> GetObePdRedefaultLifetimeOptimisticForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeOptimisticDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdRedefaultLifetimeOptimisticObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}