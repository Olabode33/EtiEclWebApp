using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IEadCcfSummaryExcelDataReader
    {
        List<ImportCalibrationCcfSummaryDto> GetImportCcfSummaryFromExcel(byte[] fileBytes);
    }
}