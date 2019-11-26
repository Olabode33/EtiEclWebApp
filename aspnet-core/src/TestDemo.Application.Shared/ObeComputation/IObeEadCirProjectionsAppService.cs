using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEadCirProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEadCirProjectionForViewDto>> GetAll(GetAllObeEadCirProjectionsInput input);

		Task<GetObeEadCirProjectionForEditOutput> GetObeEadCirProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEadCirProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEadCirProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}