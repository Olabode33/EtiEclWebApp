using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdRedefaultLifetimeBestsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdRedefaultLifetimeBestForViewDto>> GetAll(GetAllObePdRedefaultLifetimeBestsInput input);

		Task<GetObePdRedefaultLifetimeBestForEditOutput> GetObePdRedefaultLifetimeBestForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdRedefaultLifetimeBestDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdRedefaultLifetimeBestObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}