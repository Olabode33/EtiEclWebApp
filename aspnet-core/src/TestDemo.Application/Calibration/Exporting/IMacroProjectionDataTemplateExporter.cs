using System.Collections.Generic;
using TestDemo.Dto;

namespace TestDemo.Calibration.Exporting
{
    public interface IMacroProjectionDataTemplateExporter
    {
        FileDto ExportProjectionTemplateToFile(List<string> inputDtos);
    }
}