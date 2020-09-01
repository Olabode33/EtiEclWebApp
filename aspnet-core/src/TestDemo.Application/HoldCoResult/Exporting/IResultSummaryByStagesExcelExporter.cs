using System.Collections.Generic;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;

namespace TestDemo.HoldCoResult.Exporting
{
    public interface IResultSummaryByStagesExcelExporter
    {
        FileDto ExportToFile(List<GetResultSummaryByStageForViewDto> resultSummaryByStages);
    }
}