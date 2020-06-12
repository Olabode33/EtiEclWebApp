using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;

namespace TestDemo.Calibration.Exporting
{
    public interface IInputPdCrDrExporter
    {
        FileDto ExportToFile(List<InputPdCrDrDto> inputDtos);
    }
}