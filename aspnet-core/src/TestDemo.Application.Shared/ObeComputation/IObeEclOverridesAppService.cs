using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclOverrideForViewDto>> GetAll(GetAllObeEclOverridesInput input);

		Task<GetObeEclOverrideForEditOutput> GetObeEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclOverrideObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}