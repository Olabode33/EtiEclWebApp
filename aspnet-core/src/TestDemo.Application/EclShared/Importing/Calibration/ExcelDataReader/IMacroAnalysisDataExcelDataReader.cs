using Abp.Application.Services.Dto;
using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IMacroAnalysisDataExcelDataReader: ITransientDependency
    {
        List<ImportMacroAnalysisDataDto> GetImportMacroAnalysisDataFromExcel(byte[] fileBytes, List<NameValueDto<int>> affiliateMacroVariables);
    }
}