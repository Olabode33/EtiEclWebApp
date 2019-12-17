using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumptionForViewDto>> GetAll(GetAllObeEclPdAssumptionsInput input);

		Task<GetObeEclPdAssumptionForEditOutput> GetObeEclPdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}