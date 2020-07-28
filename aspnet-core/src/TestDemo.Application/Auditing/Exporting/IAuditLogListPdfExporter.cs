using System.Collections.Generic;
using TestDemo.Auditing.Dto;
using TestDemo.Dto;

namespace TestDemo.Auditing.Exporting
{
    public interface IAuditLogListPdfExporter
    {
        FileDto ExportToPdf(List<PrintAuditLogDto> input);
    }
}