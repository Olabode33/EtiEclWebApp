using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.HoldCoResult.Exporting
{
    public class HoldCoResultSummariesExcelExporter : EpPlusExcelExporterBase, IHoldCoResultSummariesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public HoldCoResultSummariesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetHoldCoResultSummaryForViewDto> holdCoResultSummaries)
        {
            return CreateExcelPackage(
                "HoldCoResultSummaries.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("HoldCoResultSummaries"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, holdCoResultSummaries
                        );

					
					
                });
        }
    }
}
