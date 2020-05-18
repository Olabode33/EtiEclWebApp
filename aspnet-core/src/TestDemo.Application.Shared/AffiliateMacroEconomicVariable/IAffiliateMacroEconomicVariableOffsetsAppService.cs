using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using TestDemo.AffiliateMacroEconomicVariable.Dtos;
using TestDemo.Dto;


namespace TestDemo.AffiliateMacroEconomicVariable
{
    public interface IAffiliateMacroEconomicVariableOffsetsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAffiliateMacroEconomicVariableOffsetForViewDto>> GetAll(GetAllAffiliateMacroEconomicVariableOffsetsInput input);

        Task<GetAffiliateMacroEconomicVariableOffsetForViewDto> GetAffiliateMacroEconomicVariableOffsetForView(int id);

		Task<GetAffiliateMacroEconomicVariableOffsetForEditOutput> GetAffiliateMacroEconomicVariableOffsetForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditAffiliateMacroEconomicVariableOffsetDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetAffiliateMacroEconomicVariableOffsetsToExcel(GetAllAffiliateMacroEconomicVariableOffsetsForExcelInput input);

		
		Task<PagedResultDto<AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto>> GetAllOrganizationUnitForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableDto>> GetAllMacroeconomicVariableForLookupTable(GetAllForLookupTableInput input);
		
    }
}