using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.Calibration.Exporting
{
    public class InputLgdHaircutExporter : EpPlusExcelExporterBase, IInputLgdHaircutExporter
    {
        public InputLgdHaircutExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<InputLgdHaircutDto> inputDtos)
        {
            return CreateExcelPackage(
                "LgdHaircutImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LgdHaircutImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Customer_No",
                        "Account_No",
                        "Contract_No",
                        "Snapshot_Date",
                        "Period",
                        "Outstanding_Balance_Lcy",
                        "Debenture_OMV",
                        "Debenture_FSV",
                        "Cash_OMV",
                        "Cash_FSV",
                        "Inventory_OMV",
                        "Inventory_FSV",
                        "Plant_And_Equipment_OMV",
                        "Plant_And_Equipment_FSV",
                        "Residential_Property_OMV",
                        "Residential_Property_FSV",
                        "Commercial_Property_OMV",
                        "Commercial_Property_FSV",
                        "Receivables_OMV",
                        "Receivables_FSV",
                        "Shares_OMV",
                        "Shares_FSV",
                        "Vehicle_OMV",
                        "Vehicle_FSV",
                        "Guarantee_Value"
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Contract_No,
                        _ => _.Snapshot_Date,
                        _ => _.Period,
                        _ => _.Outstanding_Balance_Lcy,
                        _ => _.Debenture_OMV,
                        _ => _.Debenture_FSV,
                        _ => _.Cash_OMV,
                        _ => _.Cash_FSV,
                        _ => _.Inventory_OMV,
                        _ => _.Inventory_FSV,
                        _ => _.Plant_And_Equipment_OMV,
                        _ => _.Plant_And_Equipment_FSV,
                        _ => _.Residential_Property_OMV,
                        _ => _.Residential_Property_FSV,
                        _ => _.Commercial_Property_OMV,
                        _ => _.Commercial_Property_FSV,
                        _ => _.Receivables_OMV,
                        _ => _.Receivables_FSV,
                        _ => _.Shares_OMV,
                        _ => _.Shares_FSV,
                        _ => _.Vehicle_OMV,
                        _ => _.Vehicle_FSV,
                        _ => _.Guarantee_Value
                        );

                    for (var i = 1; i <= 25; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
