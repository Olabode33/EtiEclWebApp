using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEadInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEadInputForViewDto>> GetAll(GetAllObeEadInputsInput input);

		Task<GetObeEadInputForEditOutput> GetObeEadInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEadInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEadInputObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}