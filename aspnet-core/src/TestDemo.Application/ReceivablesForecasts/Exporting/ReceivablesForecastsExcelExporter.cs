using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.ReceivablesForecasts.Exporting
{
    public class ReceivablesForecastsExcelExporter : EpPlusExcelExporterBase, IReceivablesForecastsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ReceivablesForecastsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetReceivablesForecastForViewDto> receivablesForecasts)
        {
            return CreateExcelPackage(
                "ReceivablesForecasts.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ReceivablesForecasts"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, receivablesForecasts
                        );

					
					
                });
        }
    }
}
