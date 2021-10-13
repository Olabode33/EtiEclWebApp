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
    public class InvalidPdCommsConsExporter : EpPlusExcelExporterBase, IInvalidPdCommsConsExporter
    {
        public InvalidPdCommsConsExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportCalibrationPdCommsConsDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidPdCommsConsImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidPdCommsConsImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Customer_No",
                        "Account_No",
                        "Contract_No",
                        "Product_Type",
                        "Current_Rating",
                        "Days_Past_Due",
                        "Classification",
                        "Outstanding_Balance_Lcy",
                        "Contract_Start_Date",
                        "Contract_End_Date",
                        "Snapshot_Date",
                        "Segment",
                        "WI",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Contract_No,
                        _ => _.Product_Type,
                        _ => _.Current_Rating,
                        _ => _.Days_Past_Due,
                        _ => _.Classification,
                        _ => _.Outstanding_Balance_Lcy,
                        _ => _.Contract_Start_Date,
                        _ => _.Contract_End_Date,
                        _ => _.Snapshot_Date,
                        _ => _.Segment,
                        _ => _.WI,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 13; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
