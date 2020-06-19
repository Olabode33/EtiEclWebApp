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
                    var bestSheet = excelPackage.Workbook.Worksheets.Add(L("Best"));
                    bestSheet.OutLineApplyStyle = true;
                    var o_Sheet = excelPackage.Workbook.Worksheets.Add(L("Optimistic"));
                    o_Sheet.OutLineApplyStyle = true;
                    var d_sheet = excelPackage.Workbook.Worksheets.Add(L("Downturn"));
                    d_sheet.OutLineApplyStyle = true;

                    string[] header = new string[inputDtos.Count + 2];
                    header[0] = "Date";
                    for (int i = 0; i < inputDtos.Count; i++)
                    {
                        header[i + 1] = inputDtos[i];
                    }

                    AddHeader(bestSheet, header);
                    AddHeader(o_Sheet, header);
                    AddHeader(d_sheet, header);
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
