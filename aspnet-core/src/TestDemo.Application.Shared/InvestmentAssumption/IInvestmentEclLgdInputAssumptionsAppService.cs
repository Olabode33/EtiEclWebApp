using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption
{
    public interface IInvestmentEclLgdInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclLgdInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclLgdInputAssumptionsInput input);

		Task<GetInvestmentEclLgdInputAssumptionForEditOutput> GetInvestmentEclLgdInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclLgdInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}