using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclEadInputAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclEadInputAssumptionForViewDto>> GetAll(GetAllRetailEclEadInputAssumptionsInput input);

		Task<GetRetailEclEadInputAssumptionForEditOutput> GetRetailEclEadInputAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclEadInputAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclEadInputAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}