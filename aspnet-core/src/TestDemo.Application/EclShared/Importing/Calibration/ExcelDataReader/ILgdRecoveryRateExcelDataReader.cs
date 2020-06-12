using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface ILgdRecoveryRateExcelDataReader
    {
        List<ImportCalibrationLgdRecoveryRateDto> GetImportLgdRecoveryRateFromExcel(byte[] fileBytes);
    }
}