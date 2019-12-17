using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionForViewDto>> GetAll(GetAllRetailEclPdAssumptionsInput input);

		Task<GetRetailEclPdAssumptionForEditOutput> GetRetailEclPdAssumptionForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}