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
    public class InvalidEadBehaviouralTermExporter : EpPlusExcelExporterBase, IInvalidEadBehaviouralTermExporter
    {
        public InvalidEadBehaviouralTermExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportCalibrationBehaviouralTermDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidEadBehaviouralTermImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidEadBehaviouralTermImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Customer_No"),
                        L("Account_No"),
                        L("Contract_No"),
                        L("Customer_Name"),
                        L("Snapshot_Date"),
                        L("Classification"),
                        L("Original_Balance_Lcy"),
                        L("Outstanding_Balance_Lcy"),
                        L("Outstanding_Balance_Acy"),
                        L("Contract_Start_Date"),
                        L("Contract_End_Date"),
                        L("Restructure_Indicator"),
                        L("Restructure_Type"),
                        L("Restructure_Start_Date"),
                        L("Restructure_End_Date"),
                        L("Assumption_NonExpired"),
                        L("Freq_NonExpired"),
                        L("Assumption_Expired"),
                        L("Freq_Expired"),
                        L("Comment"),
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Contract_No,
                        _ => _.Customer_Name,
                        _ => _.Snapshot_Date,
                        _ => _.Classification,
                        _ => _.Original_Balance_Lcy,
                        _ => _.Outstanding_Balance_Lcy,
                        _ => _.Outstanding_Balance_Acy,
                        _ => _.Contract_Start_Date,
                        _ => _.Contract_End_Date,
                        _ => _.Restructure_Indicator,
                        _ => _.Restructure_Type,
                        _ => _.Restructure_Start_Date,
                        _ => _.Restructure_End_Date,
                        _ => _.Assumption_NonExpired,
                        _ => _.Freq_NonExpired,
                        _ => _.Assumption_Expired,
                        _ => _.Freq_Expired,
                        _ => _.Comment,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 21; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
