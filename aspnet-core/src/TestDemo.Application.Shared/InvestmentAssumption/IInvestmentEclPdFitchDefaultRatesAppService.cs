using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.InvestmentAssumption.Dtos;
using TestDemo.Dto;
using TestDemo.EclShared.Dtos;
using System.Collections.Generic;
using GetAllForLookupTableInput = TestDemo.InvestmentAssumption.Dtos.GetAllForLookupTableInput;

namespace TestDemo.InvestmentAssumption
{
    public interface IInvestmentEclPdFitchDefaultRatesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclPdFitchDefaultRateForViewDto>> GetAll(GetAllInvestmentEclPdFitchDefaultRatesInput input);

        Task<List<InvSecFitchCummulativeDefaultRateDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetInvestmentEclPdFitchDefaultRateForEditOutput> GetInvestmentEclPdFitchDefaultRateForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclPdFitchDefaultRateDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclPdFitchDefaultRateInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}