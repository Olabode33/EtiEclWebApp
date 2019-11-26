using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdRedefaultLifetimeDownturnsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdRedefaultLifetimeDownturnForViewDto>> GetAll(GetAllObePdRedefaultLifetimeDownturnsInput input);

		Task<GetObePdRedefaultLifetimeDownturnForEditOutput> GetObePdRedefaultLifetimeDownturnForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeDownturnDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdRedefaultLifetimeDownturnObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}