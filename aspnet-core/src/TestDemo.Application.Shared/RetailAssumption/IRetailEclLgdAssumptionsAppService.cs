using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;
using System.Collections.Generic;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclLgdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclLgdAssumptionForViewDto>> GetAll(GetAllRetailEclLgdAssumptionsInput input);

		Task<GetRetailEclLgdAssumptionForEditOutput> GetRetailEclLgdAssumptionForEdit(EntityDto<Guid> input);

        Task<List<CreateOrEditRetailEclLgdAssumptionDto>> GetRetailEclLgdAssumptionsList(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditRetailEclLgdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclLgdAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}