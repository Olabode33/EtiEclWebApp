using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IEadBehaviouralTermExcelDataReader : ITransientDependency
    {
        List<ImportCalibrationBehaviouralTermAsStringDto> GetImportBehaviouralTermFromExcel(byte[] fileBytes);
    }
}