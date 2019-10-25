using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclLgdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclLgdAssumptionForViewDto>> GetAll(GetAllRetailEclLgdAssumptionsInput input);

		Task<GetRetailEclLgdAssumptionForEditOutput> GetRetailEclLgdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclLgdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclLgdAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}