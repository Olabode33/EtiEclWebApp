using System.Collections.Generic;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.Dto;

namespace TestDemo.ReceivablesForecasts.Exporting
{
    public interface IReceivablesForecastsExcelExporter
    {
        FileDto ExportToFile(List<GetReceivablesForecastForViewDto> receivablesForecasts);
    }
}