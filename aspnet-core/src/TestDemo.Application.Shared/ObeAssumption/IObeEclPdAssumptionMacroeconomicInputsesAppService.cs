using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumptionMacroeconomicInputsesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumptionMacroeconomicInputsForViewDto>> GetAll(GetAllObeEclPdAssumptionMacroeconomicInputsesInput input);

		Task<GetObeEclPdAssumptionMacroeconomicInputsForEditOutput> GetObeEclPdAssumptionMacroeconomicInputsForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumptionMacroeconomicInputsObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}