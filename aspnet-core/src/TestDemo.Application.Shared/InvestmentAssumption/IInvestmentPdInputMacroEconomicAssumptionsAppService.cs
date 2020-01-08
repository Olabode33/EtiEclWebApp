using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption
{
    public interface IInvestmentPdInputMacroEconomicAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentPdInputMacroEconomicAssumptionForViewDto>> GetAll(GetAllInvestmentPdInputMacroEconomicAssumptionsInput input);

		Task<GetInvestmentPdInputMacroEconomicAssumptionForEditOutput> GetInvestmentPdInputMacroEconomicAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}