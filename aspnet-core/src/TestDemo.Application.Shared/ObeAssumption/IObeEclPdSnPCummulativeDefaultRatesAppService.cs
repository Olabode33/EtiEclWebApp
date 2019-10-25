using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeAssumption
{
    public interface IObeEclPdSnPCummulativeDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclPdSnPCummulativeDefaultRateForViewDto>> GetAll(GetAllObeEclPdSnPCummulativeDefaultRatesInput input);

		Task<GetObeEclPdSnPCummulativeDefaultRateForEditOutput> GetObeEclPdSnPCummulativeDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclPdSnPCummulativeDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclPdSnPCummulativeDefaultRateObeEclLookupTableDto>> GetAllObeEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}