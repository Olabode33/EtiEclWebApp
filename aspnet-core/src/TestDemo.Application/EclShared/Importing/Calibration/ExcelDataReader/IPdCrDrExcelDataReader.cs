using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IPdCrDrExcelDataReader
    {
        List<ImportCalibrationPdCrDrDto> GetImportPdCrDrFromExcel(byte[] fileBytes);
    }
}