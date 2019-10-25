using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclLgdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclLgdAssumptionForViewDto>> GetAll(GetAllObeEclLgdAssumptionsInput input);

		Task<GetObeEclLgdAssumptionForEditOutput> GetObeEclLgdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclLgdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclLgdAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}