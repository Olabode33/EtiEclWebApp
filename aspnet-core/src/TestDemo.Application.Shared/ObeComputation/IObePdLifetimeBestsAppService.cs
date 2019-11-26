using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObePdLifetimeBestsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObePdLifetimeBestForViewDto>> GetAll(GetAllObePdLifetimeBestsInput input);

		Task<GetObePdLifetimeBestForEditOutput> GetObePdLifetimeBestForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObePdLifetimeBestDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObePdLifetimeBestObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}