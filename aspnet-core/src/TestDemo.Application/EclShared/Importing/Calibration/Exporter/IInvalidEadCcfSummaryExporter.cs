using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidEadCcfSummaryExporter
    {
        FileDto ExportToFile(List<ImportCalibrationCcfSummaryDto> inputDtos);
    }
}