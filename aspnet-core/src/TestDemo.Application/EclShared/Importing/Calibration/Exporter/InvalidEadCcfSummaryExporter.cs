using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class InvalidEadCcfSummaryExporter : EpPlusExcelExporterBase, IInvalidEadCcfSummaryExporter
    {
        public InvalidEadCcfSummaryExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportCalibrationCcfSummaryDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidEadCcfSummaryImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidCcfSummaryImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Customer_No",
                        "Account_No",
                        "Product_Type",
                        "Settlement_Account",
                        "Snapshot_Date",
                        "Contract_Start_Date",
                        "Contract_End_Date",
                        "Limit",
                        "Outstanding_Balance",
                        "Classification",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Product_Type,
                        _ => _.Settlement_Account,
                        _ => _.Snapshot_Date,
                        _ => _.Contract_Start_Date,
                        _ => _.Contract_End_Date,
                        _ => _.Limit,
                        _ => _.Outstanding_Balance,
                        _ => _.Classification,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 11; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
