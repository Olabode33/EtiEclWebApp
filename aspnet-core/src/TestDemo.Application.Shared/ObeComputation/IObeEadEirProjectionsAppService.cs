using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEadEirProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEadEirProjectionForViewDto>> GetAll(GetAllObeEadEirProjectionsInput input);

		Task<GetObeEadEirProjectionForEditOutput> GetObeEadEirProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEadEirProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEadEirProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}