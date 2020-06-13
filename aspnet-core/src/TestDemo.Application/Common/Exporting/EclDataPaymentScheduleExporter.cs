using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.Dto.Inputs;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.Common.Exporting
{
    public class EclDataPaymentScheduleExporter : EpPlusExcelExporterBase, IEclDataPaymentScheduleExporter
    {
        public EclDataPaymentScheduleExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<EclDataPaymentScheduleDto> inputDtos)
        {
            return CreateExcelPackage(
                "PaymentScheduleImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PaymentScheduleImports"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ContractRefNo"),
                        L("StartDate"),
                        L("Component"),
                        L("NoOfSchedules"),
                        L("Frequency"),
                        L("Amount")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.ContractRefNo,
                        _ => _.StartDate,
                        _ => _.Component,
                        _ => _.NoOfSchedules,
                        _ => _.Frequency,
                        _ => _.Amount
                        );

                    for (var i = 1; i <= 7; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
