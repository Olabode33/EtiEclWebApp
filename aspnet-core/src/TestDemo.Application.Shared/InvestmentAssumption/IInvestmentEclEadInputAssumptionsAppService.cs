using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption
{
    public interface IInvestmentEclEadInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclEadInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclEadInputAssumptionsInput input);

		Task<GetInvestmentEclEadInputAssumptionForEditOutput> GetInvestmentEclEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclEadInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}