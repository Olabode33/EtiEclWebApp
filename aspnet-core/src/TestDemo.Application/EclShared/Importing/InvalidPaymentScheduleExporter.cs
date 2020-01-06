using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class InvalidPaymentScheduleExporter : EpPlusExcelExporterBase, IInvalidPaymentScheduleExporter
    {
        public InvalidPaymentScheduleExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportPaymentScheduleDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidPaymentScheduleImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidPaymentScheduleImports"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("ContractRefNo"),
                        L("StartDate"),
                        L("Component"),
                        L("NoOfSchedules"),
                        L("Frequency"),
                        L("Amount"),
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.ContractRefNo,
                        _ => _.StartDate,
                        _ => _.Component,
                        _ => _.NoOfSchedules,
                        _ => _.Frequency,
                        _ => _.Amount,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 8; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
