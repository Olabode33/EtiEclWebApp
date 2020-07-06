using OfficeOpenXml;
using TestDemo.Dto;

namespace TestDemo.Reports
{
    public interface IExcelReportGenerator
    {
        FileDto DownloadExcelReport(GenerateReportJobArgs args);
        ExcelPackage GenerateExcelReport(GenerateReportJobArgs args);
    }
}