using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumptionNonInternalModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumptionNonInternalModelForViewDto>> GetAll(GetAllObeEclPdAssumptionNonInternalModelsInput input);

		Task<GetObeEclPdAssumptionNonInternalModelForEditOutput> GetObeEclPdAssumptionNonInternalModelForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumptionNonInternalModelDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumptionNonInternalModelObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}