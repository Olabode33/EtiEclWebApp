using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.ObeComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.ObeComputation
{
    public interface IObeEclComputedEadResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetObeEclComputedEadResultForViewDto>> GetAll(GetAllObeEclComputedEadResultsInput input);

		Task<GetObeEclComputedEadResultForEditOutput> GetObeEclComputedEadResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditObeEclComputedEadResultDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<ObeEclComputedEadResultObeEclDataLoanBookLookupTableDto>> GetAllObeEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}