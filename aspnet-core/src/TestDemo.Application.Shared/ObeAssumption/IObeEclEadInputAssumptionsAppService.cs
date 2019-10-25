using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclEadInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclEadInputAssumptionForViewDto>> GetAll(GetAllObeEclEadInputAssumptionsInput input);

		Task<GetObeEclEadInputAssumptionForEditOutput> GetObeEclEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclEadInputAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}