using System.Collections.Generic;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;

namespace TestDemo.WholesaleAssumption.Exporting
{
    public interface IWholesaleEclPdAssumptionMacroeconomicInputsExcelExporter
    {
        FileDto ExportToFile(List<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto> wholesaleEclPdAssumptionMacroeconomicInputs);
    }
}