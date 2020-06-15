using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Assumptions.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class InvalidNplExporter : EpPlusExcelExporterBase, IInvalidNplExporter
    {
        public InvalidNplExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportNplDataDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidNplImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidNplImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Date",
                        "Actual",
                        "Standardised",
                        "NPL Series",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Date,
                        _ => _.Actual,
                        _ => _.Standardised,
                        _ => _.EtiNplSeries,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 5; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
