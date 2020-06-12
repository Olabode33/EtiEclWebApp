using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IEadCcfSummaryExcelDataReader : ITransientDependency
    {
        List<ImportCalibrationCcfSummaryDto> GetImportCcfSummaryFromExcel(byte[] fileBytes);
    }
}