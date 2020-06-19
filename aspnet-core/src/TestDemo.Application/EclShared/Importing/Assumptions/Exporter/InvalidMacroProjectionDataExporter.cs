using Abp.Application.Services.Dto;
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
    public class InvalidMacroProjectionDataExporter : EpPlusExcelExporterBase, IInvalidMacroProjectionDataExporter
    {
        public InvalidMacroProjectionDataExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportMacroProjectionDataDto> inputDtos, List<NameValueDto<int>> macroVariable)
        {
            return CreateExcelPackage(
                "InvalidMacroProjectionImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidMacroProjection"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Period",
                        "MacroeconomicVariable",
                        "Best",
                        "Optimistic",
                        "Downturn",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Date,
                        _ => macroVariable.Find(x => x.Value == _.MacroeconomicVariableId).Name,
                        _ => _.BestValue,
                        _ => _.OptimisticValue,
                        _ => _.DownturnValue,
                        _ => _.Exception
                        );

                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
