using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidLgdHaircutExporter
    {
        FileDto ExportToFile(List<ImportCalibrationLgdHaircutDto> inputDtos);
    }
}