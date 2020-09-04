using System.Collections.Generic;
using TestDemo.ReceivablesResults.Dtos;
using TestDemo.Dto;

namespace TestDemo.ReceivablesResults.Exporting
{
    public interface IReceivablesResultsExcelExporter
    {
        FileDto ExportToFile(List<GetReceivablesResultForViewDto> receivablesResults);
    }
}