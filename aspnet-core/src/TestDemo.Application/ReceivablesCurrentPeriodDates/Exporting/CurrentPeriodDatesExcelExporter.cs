using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.ReceivablesCurrentPeriodDates.Exporting
{
    public class CurrentPeriodDatesExcelExporter : EpPlusExcelExporterBase, ICurrentPeriodDatesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CurrentPeriodDatesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCurrentPeriodDateForViewDto> currentPeriodDates)
        {
            return CreateExcelPackage(
                "CurrentPeriodDates.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("CurrentPeriodDates"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, currentPeriodDates
                        );

					
					
                });
        }
    }
}
