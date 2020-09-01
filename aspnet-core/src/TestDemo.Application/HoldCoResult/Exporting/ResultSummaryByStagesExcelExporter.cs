﻿using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.HoldCoResult.Exporting
{
    public class ResultSummaryByStagesExcelExporter : EpPlusExcelExporterBase, IResultSummaryByStagesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ResultSummaryByStagesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetResultSummaryByStageForViewDto> resultSummaryByStages)
        {
            return CreateExcelPackage(
                "ResultSummaryByStages.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ResultSummaryByStages"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, resultSummaryByStages
                        );

					
					
                });
        }
    }
}
