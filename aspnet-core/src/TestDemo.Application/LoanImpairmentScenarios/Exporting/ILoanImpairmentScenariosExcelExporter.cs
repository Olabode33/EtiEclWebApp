using System.Collections.Generic;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.Dto;

namespace TestDemo.LoanImpairmentScenarios.Exporting
{
    public interface ILoanImpairmentScenariosExcelExporter
    {
        FileDto ExportToFile(List<GetLoanImpairmentScenarioForViewDto> loanImpairmentScenarios);
    }
}