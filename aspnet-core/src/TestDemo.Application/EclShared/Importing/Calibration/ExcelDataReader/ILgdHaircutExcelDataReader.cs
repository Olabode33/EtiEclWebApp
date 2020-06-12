using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface ILgdHaircutExcelDataReader
    {
        List<ImportCalibrationLgdHaircutDto> GetImportLgdHaircutFromExcel(byte[] fileBytes);
    }
}