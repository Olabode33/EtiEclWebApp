using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdLifetimeDownturnForViewDto>> GetAll(GetAllObePdLifetimeDownturnsInput input);

		Task<GetObePdLifetimeDownturnForEditOutput> GetObePdLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdLifetimeDownturnObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}