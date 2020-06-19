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
    public class MacroProjectionDataTemplateExporter : EpPlusExcelExporterBase, IMacroProjectionDataTemplateExporter
    {
        public MacroProjectionDataTemplateExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportProjectionTemplateToFile(List<string> inputDtos)
        {
            return CreateExcelPackage(
                "MacroProjection_Template.xlsx",
                excelPackage =>
                {
                    var bestSheet = excelPackage.Workbook.Worksheets.Add(L("Data"));

                    string[] header = new string[1 + (inputDtos.Count * 3)];
                    header[0] = "Date";

                    int column = 1;

                    for (int i = 0; i < inputDtos.Count; i++)
                    {
                        header[column] = inputDtos[i] + "(Best)";
                        header[column + 1] = inputDtos[i] + "(Optimistic)";
                        header[column + 2] = inputDtos[i] + "(Downturn)";

                        column += 3;
                    }

                    AddHeader(bestSheet, header);

                    for (var i = 1; i <= 1 + (inputDtos.Count * 3); i++)
                    {
                        bestSheet.Column(i).AutoFit();
                    }
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
