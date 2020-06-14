using Abp.Application.Services.Dto;
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
    public class InvalidMacroAnalysisDataExporter : EpPlusExcelExporterBase, IInvalidMacroAnalysisDataExporter
    {
        public InvalidMacroAnalysisDataExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportMacroAnalysisDataDto> inputDtos, List<NameValueDto<int>> macroVariable)
        {
            return CreateExcelPackage(
                "InvalidPdCrDrImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidPdCrDrImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Period",
                        "MacroeconomicVariable",
                        "Value",
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Period,
                        _ => _.MacroeconomicId == -1 ? "NPL Percentage Ratio" : macroVariable.Find(x => x.Value == _.MacroeconomicId).Name,
                        _ => _.Value,
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
