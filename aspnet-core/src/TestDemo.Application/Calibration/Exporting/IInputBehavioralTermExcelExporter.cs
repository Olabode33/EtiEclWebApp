using System.Collections.Generic;
using TestDemo.Calibration.Dtos;
using TestDemo.Dto;

namespace TestDemo.Calibration.Exporting
{
    public interface IInputBehavioralTermExcelExporter
    {
        FileDto ExportToFile(List<InputBehaviouralTermsDto> inputDtos);
    }
}