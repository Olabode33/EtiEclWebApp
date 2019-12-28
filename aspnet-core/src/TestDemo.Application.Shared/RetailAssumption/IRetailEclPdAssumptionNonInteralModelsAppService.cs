﻿using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionNonInteralModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionNonInteralModelForViewDto>> GetAll(GetAllRetailEclPdAssumptionNonInteralModelsInput input);

		Task<GetRetailEclPdAssumptionNonInteralModelForEditOutput> GetRetailEclPdAssumptionNonInteralModelForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionNonInteralModelDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionNonInteralModelRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}