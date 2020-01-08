using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.InvestmentAssumption
{
    public interface IInvestmentEclPdFitchDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclPdFitchDefaultRateForViewDto>> GetAll(GetAllInvestmentEclPdFitchDefaultRatesInput input);

		Task<GetInvestmentEclPdFitchDefaultRateForEditOutput> GetInvestmentEclPdFitchDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclPdFitchDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}