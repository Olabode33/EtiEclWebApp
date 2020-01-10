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
    public interface IInvestmentEclLgdInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclLgdInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclLgdInputAssumptionsInput input);

        Task<List<LgdAssumptionDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetInvestmentEclLgdInputAssumptionForEditOutput> GetInvestmentEclLgdInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclLgdInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclLgdInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}