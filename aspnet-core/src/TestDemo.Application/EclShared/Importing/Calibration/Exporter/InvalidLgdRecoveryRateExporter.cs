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
    public class InvalidLgdRecoveryRateExporter : EpPlusExcelExporterBase, IInvalidLgdRecoveryRateExporter
    {
        public InvalidLgdRecoveryRateExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportCalibrationLgdRecoveryRateDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidLgdRecoveryRateImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidLgdRecoveryRateImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Customer_No",
                        "Account_No",
                        "Contract_No",
                        "Account_Name",
                        "Segment",
                        "Days_Past_Due",
                        "Classification",
                        "Default_Date",
                        "Outstanding_Balance_Lcy",
                        "Contractual_Interest_Rate",
                        "Amount_Recovered",
                        "Date_Of_Recovery",
                        "Type_Of_Recovery",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Contract_No,
                        _ => _.Account_Name,
                        _ => _.Segment,
                        _ => _.Days_Past_Due,
                        _ => _.Classification,
                        _ => _.Default_Date,
                        _ => _.Outstanding_Balance_Lcy,
                        _ => _.Contractual_Interest_Rate,
                        _ => _.Amount_Recovered,
                        _ => _.Date_Of_Recovery,
                        _ => _.Type_Of_Recovery,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 14; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
