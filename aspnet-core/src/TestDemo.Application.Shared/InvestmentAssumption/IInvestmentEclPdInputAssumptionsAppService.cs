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
    public interface IInvestmentEclPdInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentEclPdInputAssumptionForViewDto>> GetAll(GetAllInvestmentEclPdInputAssumptionsInput input);

        Task<List<PdInputAssumptionDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetInvestmentEclPdInputAssumptionForEditOutput> GetInvestmentEclPdInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentEclPdInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentEclPdInputAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}