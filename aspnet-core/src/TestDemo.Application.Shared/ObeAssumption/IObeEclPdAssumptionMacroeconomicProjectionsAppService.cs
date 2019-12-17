using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumptionMacroeconomicProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllObeEclPdAssumptionMacroeconomicProjectionsInput input);

		Task<GetObeEclPdAssumptionMacroeconomicProjectionForEditOutput> GetObeEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumptionMacroeconomicProjectionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}