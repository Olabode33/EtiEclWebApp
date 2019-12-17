using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdAssumptionNplIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllObeEclPdAssumptionNplIndexesInput input);

		Task<GetObeEclPdAssumptionNplIndexForEditOutput> GetObeEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdAssumptionNplIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdAssumptionNplIndexObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}