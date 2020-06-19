using Abp.Application.Services.Dto;
using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Assumptions.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IMacroProjectionDataExcelDataReader: ITransientDependency
    {
        List<ImportMacroProjectionDataDto> GetImportMacroProjectionDataFromExcel(byte[] fileBytes, List<NameValueDto<int>> affiliateMacroVariables);
    }
}