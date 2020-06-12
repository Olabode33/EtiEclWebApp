using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidLgdRecoveryRateExporter
    {
        FileDto ExportToFile(List<ImportCalibrationLgdRecoveryRateDto> inputDtos);
    }
}