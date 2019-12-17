using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesalePdAssumptionNonInternalModelsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesalePdAssumptionNonInternalModelForViewDto>> GetAll(GetAllWholesalePdAssumptionNonInternalModelsInput input);

		Task<GetWholesalePdAssumptionNonInternalModelForEditOutput> GetWholesalePdAssumptionNonInternalModelForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesalePdAssumptionNonInternalModelDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesalePdAssumptionNonInternalModelWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}