using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEclComputedEadResultsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclComputedEadResultForViewDto>> GetAll(GetAllRetailEclComputedEadResultsInput input);

		Task<GetRetailEclComputedEadResultForEditOutput> GetRetailEclComputedEadResultForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclComputedEadResultDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclComputedEadResultRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}