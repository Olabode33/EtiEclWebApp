using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IAssetBookExcelDataReader : ITransientDependency
    {
        List<ImportAssetBookDto> GetImportAssetBookFromExcel(byte[] fileBytes);
    }
}