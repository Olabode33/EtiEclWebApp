using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclAssumptionForViewDto>> GetAll(GetAllObeEclAssumptionsInput input);

		Task<GetObeEclAssumptionForEditOutput> GetObeEclAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclAssumptionObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}