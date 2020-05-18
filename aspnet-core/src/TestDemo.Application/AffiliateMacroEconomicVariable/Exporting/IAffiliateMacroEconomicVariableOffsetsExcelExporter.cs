using System.Collections.Generic;
using TestDemo.AffiliateMacroEconomicVariable.Dtos;
using TestDemo.Dto;

namespace TestDemo.AffiliateMacroEconomicVariable.Exporting
{
    public interface IAffiliateMacroEconomicVariableOffsetsExcelExporter
    {
        FileDto ExportToFile(List<GetAffiliateMacroEconomicVariableOffsetForViewDto> affiliateMacroEconomicVariableOffsets);
    }
}