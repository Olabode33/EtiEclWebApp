using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface ILoanbookExcelDataReader : ITransientDependency
    {
        List<ImportLoanbookDtoNew> GetImportLoanbookFromExcel(byte[] fileBytes);
    }
}