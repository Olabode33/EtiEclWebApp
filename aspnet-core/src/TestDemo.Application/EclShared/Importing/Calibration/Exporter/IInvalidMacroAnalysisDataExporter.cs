using Abp.Application.Services.Dto;
using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidMacroAnalysisDataExporter
    {
        FileDto ExportToFile(List<ImportMacroAnalysisDataDto> inputDtos, List<NameValueDto<int>> macroVariable);
    }
}