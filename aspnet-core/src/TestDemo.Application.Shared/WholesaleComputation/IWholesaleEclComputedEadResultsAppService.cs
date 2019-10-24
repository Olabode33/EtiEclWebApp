using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleComputation
{
    public interface IWholesaleEclComputedEadResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclComputedEadResultForViewDto>> GetAll(GetAllWholesaleEclComputedEadResultsInput input);

		Task<GetWholesaleEclComputedEadResultForEditOutput> GetWholesaleEclComputedEadResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclComputedEadResultDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclComputedEadResultWholesaleEclDataLoanBookLookupTableDto>> GetAllWholesaleEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}