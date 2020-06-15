using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Assumptions.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface INplDataExcelDataReader: ITransientDependency
    {
        List<ImportNplDataDto> GetImportDataFromExcel(byte[] fileBytes);
    }
}