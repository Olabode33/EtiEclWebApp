using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface ILgdHaircutExcelDataReader : ITransientDependency
    {
        List<ImportCalibrationLgdHaircutDto> GetImportLgdHaircutFromExcel(byte[] fileBytes);
    }
}