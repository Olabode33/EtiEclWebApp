﻿using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IPdCrDrExcelDataReader : ITransientDependency
    {
        List<ImportCalibrationPdCrDrAsStringDto> GetImportPdCrDrFromExcel(byte[] fileBytes);
    }
}