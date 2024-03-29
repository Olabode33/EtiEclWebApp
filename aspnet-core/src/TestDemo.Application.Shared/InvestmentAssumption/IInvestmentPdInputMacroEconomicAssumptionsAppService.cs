﻿using System;
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
    public interface IInvestmentPdInputMacroEconomicAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetInvestmentPdInputMacroEconomicAssumptionForViewDto>> GetAll(GetAllInvestmentPdInputMacroEconomicAssumptionsInput input);

        Task<List<InvSecMacroEconomicAssumptionDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetInvestmentPdInputMacroEconomicAssumptionForEditOutput> GetInvestmentPdInputMacroEconomicAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<InvestmentPdInputMacroEconomicAssumptionInvestmentEclLookupTableDto>> GetAllInvestmentEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}