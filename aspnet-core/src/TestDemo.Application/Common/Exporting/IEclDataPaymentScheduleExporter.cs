using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.Dto.Inputs;

namespace TestDemo.Common.Exporting
{
    public interface IEclDataPaymentScheduleExporter
    {
        FileDto ExportToFile(List<EclDataPaymentScheduleDto> inputDtos);
    }
}