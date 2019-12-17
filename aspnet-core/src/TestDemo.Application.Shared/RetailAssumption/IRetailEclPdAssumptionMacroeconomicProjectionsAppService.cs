using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionMacroeconomicProjectionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionMacroeconomicProjectionForViewDto>> GetAll(GetAllRetailEclPdAssumptionMacroeconomicProjectionsInput input);

		Task<GetRetailEclPdAssumptionMacroeconomicProjectionForEditOutput> GetRetailEclPdAssumptionMacroeconomicProjectionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionMacroeconomicProjectionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}