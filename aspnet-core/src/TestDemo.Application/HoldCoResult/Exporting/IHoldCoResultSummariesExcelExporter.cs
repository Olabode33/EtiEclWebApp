using System.Collections.Generic;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.HoldCoResult.Exporting
{
    public interface IHoldCoResultSummariesExcelExporter
    {
        FileDto ExportToFile(List<GetHoldCoResultSummaryForViewDto> holdCoResultSummaries);
    }
}