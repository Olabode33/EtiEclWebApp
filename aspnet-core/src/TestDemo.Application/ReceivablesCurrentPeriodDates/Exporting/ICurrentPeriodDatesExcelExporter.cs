using System.Collections.Generic;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.Dto;

namespace TestDemo.ReceivablesCurrentPeriodDates.Exporting
{
    public interface ICurrentPeriodDatesExcelExporter
    {
        FileDto ExportToFile(List<GetCurrentPeriodDateForViewDto> currentPeriodDates);
    }
}