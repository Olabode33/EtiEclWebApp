using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailComputation
{
    public interface IRetailEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclOverrideForViewDto>> GetAll(GetAllRetailEclOverridesInput input);

		Task<GetRetailEclOverrideForEditOutput> GetRetailEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclOverrideRetailEclDataLoanBookLookupTableDto>> GetAllRetailEclDataLoanBookForLookupTable(GetAllForLookupTableInput input);
		
    }
}