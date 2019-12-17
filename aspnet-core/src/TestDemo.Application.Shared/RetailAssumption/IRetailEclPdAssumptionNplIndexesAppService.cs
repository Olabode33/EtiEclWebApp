using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.RetailAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.RetailAssumption
{
    public interface IRetailEclPdAssumptionNplIndexesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetRetailEclPdAssumptionNplIndexForViewDto>> GetAll(GetAllRetailEclPdAssumptionNplIndexesInput input);

		Task<GetRetailEclPdAssumptionNplIndexForEditOutput> GetRetailEclPdAssumptionNplIndexForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditRetailEclPdAssumptionNplIndexDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<RetailEclPdAssumptionNplIndexRetailEclLookupTableDto>> GetAllRetailEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}