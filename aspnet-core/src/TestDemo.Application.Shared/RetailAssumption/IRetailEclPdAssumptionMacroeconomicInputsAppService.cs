﻿using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionMacroeconomicInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionMacroeconomicInputForViewDto>> GetAll(GetAllRetailEclPdAssumptionMacroeconomicInputsInput input);

        Task<List<EclShared.Dtos.PdInputAssumptionMacroeconomicInputDto>> GetListForEclView(EntityDto<Guid> input);


        Task<GetRetailEclPdAssumptionMacroeconomicInputForEditOutput> GetRetailEclPdAssumptionMacroeconomicInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionMacroeconomicInputRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}