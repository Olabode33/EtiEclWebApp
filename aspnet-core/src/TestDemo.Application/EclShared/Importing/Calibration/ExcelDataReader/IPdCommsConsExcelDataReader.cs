using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IPdCommsConsExcelDataReader : ITransientDependency
    {
        List<ImportCalibrationPdCommsConsAsStringDto> GetImportPdCommsConsFromExcel(byte[] fileBytes);
    }
}