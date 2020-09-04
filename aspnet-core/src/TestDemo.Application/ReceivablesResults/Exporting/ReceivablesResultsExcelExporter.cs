using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.ReceivablesResults.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.ReceivablesResults.Exporting
{
    public class ReceivablesResultsExcelExporter : EpPlusExcelExporterBase, IReceivablesResultsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ReceivablesResultsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetReceivablesResultForViewDto> receivablesResults)
        {
            return CreateExcelPackage(
                "ReceivablesResults.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ReceivablesResults"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, receivablesResults
                        );

					
					
                });
        }
    }
}
