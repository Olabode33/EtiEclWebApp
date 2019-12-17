using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption
{
    public interface IWholesaleEclPdAssumptionMacroeconomicInputsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto>> GetAll(GetAllWholesaleEclPdAssumptionMacroeconomicInputsInput input);

		Task<GetWholesaleEclPdAssumptionMacroeconomicInputForEditOutput> GetWholesaleEclPdAssumptionMacroeconomicInputForEdit(EntityDto<Guid> input);

		Task CreateOrEdit(CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto input);

		Task Delete(EntityDto<Guid> input);

		
		Task<PagedResultDto<WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableDto>> GetAllWholesaleEclForLookupTable(GetAllForLookupTableInput input);
		
    }
}