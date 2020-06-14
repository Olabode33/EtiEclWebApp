using Abp.Application.Services.Dto;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IMacroAnalysisDataExcelDataReader
    {
        List<ImportMacroAnalysisDataDto> GetImportMacroAnalysisDataFromExcel(byte[] fileBytes, List<NameValueDto<int>> affiliateMacroVariables);
    }
}