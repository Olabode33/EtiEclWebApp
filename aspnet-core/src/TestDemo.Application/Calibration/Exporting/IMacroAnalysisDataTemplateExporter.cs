using System;
using System.Collections.Generic;
using TestDemo.Dto;

namespace TestDemo.Calibration.Exporting
{
    public interface IMacroAnalysisDataTemplateExporter
    {
        FileDto ExportTemplateToFile(List<string> inputDtos);
        FileDto ExportInputToFile(List<string> columns, List<List<double?>> values, List<DateTime?> periods);
    }
}