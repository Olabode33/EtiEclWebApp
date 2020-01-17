using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentComputation.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentComputation
{
    public interface IInvestmentEclOverridesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclOverrideForViewDto>> GetAll(GetAllInvestmentEclOverridesInput input);

		Task<GetInvestmentEclOverrideForEditOutput> GetInvestmentEclOverrideForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclOverrideDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclOverrideInvestmentEclSicrLookupTableDto>> GetAllInvestmentEclSicrForLookupTable(GetAllForLookupTableInput input);
		
    }
}