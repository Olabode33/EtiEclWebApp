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
    public class MacroAnalysisDataTemplateExporter : EpPlusExcelExporterBase, IMacroAnalysisDataTemplateExporter
    {
        public MacroAnalysisDataTemplateExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportTemplateToFile(List<string> inputDtos)
        {
            return CreateExcelPackage(
                "MacroAnalysisData_Template.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Data"));
                    sheet.OutLineApplyStyle = true;

                    string[] header = new string[inputDtos.Count + 2];
                    header[0] = "Period";
                    header[1] = "NPL_Percentage_Ratio";
                    for (int i = 0; i < inputDtos.Count; i++)
                    {
                        header[i + 2] = inputDtos[i];
                    }

                    AddHeader(
                        sheet,
                        header
                        );
                });
        }

        public FileDto ExportInputToFile(List<string> columns, List<List<double?>> values, List<DateTime?> periods)
        {
            return CreateExcelPackage(
                "MacroAnalysisData_Input.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Data"));
                    sheet.OutLineApplyStyle = true;

                    string[] header = new string[columns.Count + 2];
                    header[0] = "Period";
                    for (int i = 0; i < columns.Count; i++)
                    {
                        header[i + 1] = columns[i];
                    }

                    AddHeader(
                        sheet,
                        header
                        );

                    var startRowIndex = 2;

                    for (var i = 0; i < periods.Count; i++)
                    {
                        sheet.Cells[i + startRowIndex, 1].Style.Numberformat.Format = "yyyy-mm-dd";
                        sheet.Cells[i + startRowIndex, 1].Value = periods[i];
                        for (int j = 0; j < values.Count; j++)
                        {
                            sheet.Cells[i + startRowIndex, j + 2].Value = values[j][i];
                        }
                    }
                });
        }
    }
}
