
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestDemo.Common.Helpers;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared;

namespace TestDemo.Reports
{
    public class ReportComputation
    {
        public static string workbookFontType = "Arial";
        public static int workbookFontSize = 11;
        public static int workbookZoomSize = 80;
        public static int startCellIndex = 0;
        public ExcelPackage GenerateEclReport(EclType eclType, Guid eclId, ResultDetail rd)
        {
            //var rd=GetResultDetail(eclType, eclId);
            var rs=GetResultSummary(eclType, eclId, rd);
            var dataTable = new DataTable();
            //var fi = new FileInfo(@"C:\Users\Dev-Sys\Desktop\ETI_template.xlsx");
            //string path = Path.Combine(Path.GetDirectoryName(@"\Template\ETI_template.xlsx"));
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Template\", "ETI_template.xlsx");
            var fi = new FileInfo(path);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            //using (ExcelPackage excelPackage = new ExcelPackage(fi))
            //{
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "PwC";
                excelPackage.Workbook.Properties.Title = $"ECL Report for {eclType.ToString()}";
                excelPackage.Workbook.Properties.Subject = $"{eclType.ToString()} ECL";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //ChangeTitle(excelPackage, eclType.ToString());
                ResultSheet(dataTable, excelPackage, rd);
                SummarySheet(dataTable, excelPackage, rs);
                StartSheet(eclType, excelPackage);

                return excelPackage;
                //excelPackage.SaveAs(new FileInfo(@"C:\Users\Dev-Sys\Desktop\ETI_template2.xlsx"));//@"C:\Users\tarokodare001\Documents\WORK\ECOBANK\My Sample Excel\Retail Report Template_wholesale.xlsx"));
            //}
            //return true;
        }

        private void StartSheet(EclType eclType, ExcelPackage excelPackage)
        {
            ExcelWorksheet startSheet = excelPackage.Workbook.Worksheets["Start"];
            startSheet.View.ZoomScale = workbookZoomSize;
            startSheet.Cells["C4"].Value = eclType.ToString();
        }

        private void SummarySheet(DataTable dataTable, ExcelPackage excelPackage, ResultSummary rs)
        {
            if (dataTable is null)
            {
                throw new ArgumentNullException(nameof(dataTable));
            }

            ExcelWorksheet summarySheet = excelPackage.Workbook.Worksheets["Summary"];
            summarySheet.View.ZoomScale = workbookZoomSize;

            //styling for the whole worksheet
            summarySheet.View.ShowGridLines = false;
            summarySheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            summarySheet.Cells.Style.Font.Name = workbookFontType;
            summarySheet.Cells.Style.Font.Size = workbookFontSize;
            summarySheet.Cells.Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_TEXT_DARK));
            /////***********///
            ///

            //resize column A and B
            double columnWidth = 5;
            summarySheet.Column(1).Width = columnWidth;// column A
            summarySheet.Column(2).Width = columnWidth;//column B

            //results summary container
            summarySheet.Cells["B2:J80"].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            summarySheet.Cells["B2:J80"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["B2:J80"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_DARK_BLUE));
            ////////***///

            startCellIndex = 3;

            summarySheet.Cells["C" + startCellIndex.ToString()].Value = "Results Summary";
            summarySheet.Cells["C" + startCellIndex.ToString()].Style.Font.Size = 16;
            summarySheet.Cells["C" + startCellIndex.ToString()].Style.Font.Italic = true;
            summarySheet.Cells["C" + startCellIndex.ToString()].Style.Font.Bold = true;
            summarySheet.Cells["C" + startCellIndex.ToString()].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));

            #region ///styling for Overall Result (Allowing for final monetary overlay)
            ///
            ///
            startCellIndex += 2; //5

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Overall Result (Allowing for final monetary overlay");


            //table border styling
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Value = "Total Exposure";
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Total Impairment";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Pre Overrides & Overlays";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Post Overrides & Overlays";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            // summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 2).ToString()].Merge = true;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString()].Value = "Porfolio Overlay";
            summarySheet.Cells["F" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "(Amount in Naira)";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;

            //summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 2).ToString()].Merge = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Total Impairment";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "(Revised)";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;

            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 2).ToString()].Merge = true;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString()].Value = "Final Coverage";
            summarySheet.Cells["H" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Ratio";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;

            //styling the borders
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":E" + (startCellIndex + 2)].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":E" + (startCellIndex + 2)].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":H" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 3).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 3).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 3).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 3).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 3).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 3).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));



            //inserting the inputs
            summarySheet.Cells["C" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].Exposure_Pre;
            summarySheet.Cells["D" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].Impairment_Pre;
            summarySheet.Cells["E" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].CoverageRatio_Pre;
            summarySheet.Cells["F" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].Exposure_Post;
            summarySheet.Cells["G" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].Impairment_Post;
            summarySheet.Cells["H" + (startCellIndex + 3).ToString()].Value = rs.Overrall[0].CoverageRatio_Post;


            // summarySheet.Cells["C6:H8"].Style.WrapText = true;
            ///////
            ///**********////
            ///
            #endregion

            #region breakdown by scenario
            ///breakdown by scenario
            ///
            startCellIndex += 5; //10

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Breakdown by Scenario");

            //table border styling
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 1).ToString()].Merge = true;



            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Total Impairment";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["F" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Total Impairment";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":G" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":G" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":G" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":G" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":G" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":G" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":G" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":G" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":D" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":D" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));



            for (int i = 0; i < rs.Scenario.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.Scenario[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.Scenario[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.Scenario[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.Scenario[i].Exposure_Post;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.Scenario[i].Impairment_Post;
            }


            ///////
            ///**********////
            ///
            #endregion

            #region breakdown by stage
            ////breakdown by stage
            ///
            startCellIndex += 8; //18

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Breakdown by Stage");


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 1).ToString()].Merge = true;



            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 6).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 6).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            ///inserting the values
            for (int i = 0; i < rs.Stage.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].CoverageRatio_Pre;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].Exposure_Pre;
                summarySheet.Cells["H" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].Impairment_Post;
                summarySheet.Cells["I" + (startCellIndex + 3 + i).ToString()].Value = rs.Stage[i].CoverageRatio_Post;
            }


            ///////
            ///**********////
            ///
            #endregion

            #region Breakdown by product type
            ////breakdown by product type
            ///

            //lets make it dynamic
            startCellIndex += 8;   //26

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Breakdown by Product Type");

            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //i.e C27:I34
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // summarySheet.Cells["C19:C20"].Merge = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 1).ToString()].Merge = true; //D27:F27
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 1).ToString()].Merge = true; //G27:I27
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Value = "Product Type"; //C28
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Style.Font.Bold = true; //C28


            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 8).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 8).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            ///inserting the values
            for (int i = 0; i < rs.ProductType.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Exposure_Pre;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].CoverageRatio_Pre;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Exposure_Pre;
                summarySheet.Cells["H" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].Impairment_Post;
                summarySheet.Cells["I" + (startCellIndex + 3 + i).ToString()].Value = rs.ProductType[i].CoverageRatio_Post;
            }

            #endregion

            #region breakdown by segment and stage
            /////
            ///breakdown by segment and stage
            /// 
            startCellIndex += 10;  //36

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Breakdown by Segment and Stage");

            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //i.e C27:I34
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // summarySheet.Cells["C19:C20"].Merge = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Value = "Segment and Stage";
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            


            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 9).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 9).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            ///inserting the values
            for (int i = 0; i < rs.SegmentAndStage.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].CoverageRatio_Pre;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].Exposure_Pre;
                summarySheet.Cells["H" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].Impairment_Post;
                summarySheet.Cells["I" + (startCellIndex + 3 + i).ToString()].Value = rs.SegmentAndStage[i].CoverageRatio_Post;
            }


            /// 
            /////
            #endregion

            #region Top Exposure
            /////
            ///breakdown by segment and stage
            /// 
            startCellIndex += 11;  //36

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Top Exposure");

            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 14).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //i.e C27:I34
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 14).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // summarySheet.Cells["C19:C20"].Merge = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Value = "Top Exposure";
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;

            


            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 10).ToString() + ":I" + (startCellIndex + 10).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 10).ToString() + ":I" + (startCellIndex + 10).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 11).ToString() + ":I" + (startCellIndex + 11).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 11).ToString() + ":I" + (startCellIndex + 11).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 12).ToString() + ":I" + (startCellIndex + 12).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 12).ToString() + ":I" + (startCellIndex + 12).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 13).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 13).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            ///inserting the values
            for (int i = 0; i < rs.TopExposureSummary.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].CoverageRatio_Pre;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].Exposure_Pre;
                summarySheet.Cells["H" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].Impairment_Post;
                summarySheet.Cells["I" + (startCellIndex + 3 + i).ToString()].Value = rs.TopExposureSummary[i].CoverageRatio_Post;
            }


            /// 
            /////
            #endregion

            #region Top Impairment
            /////
            ///breakdown by segment and stage
            /// 
            startCellIndex += 16;  //36

            SummarySubheader("C" + startCellIndex.ToString(), summarySheet, "Top Impairment");

            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 14).ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //i.e C27:I34
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 14).ToString()].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // summarySheet.Cells["C19:C20"].Merge = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 1).ToString()].Merge = true;
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Value = "Top Impairment";
            summarySheet.Cells["C" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;




            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Value = "Model Output (Pre-Overrides & Overlays)";
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["E" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["F" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Value = "Model Output (Post-Overrides & Overlays)";
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Value = "Exposure";
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["G" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Value = "Impairment";
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["H" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Value = "Coverage Ratio";
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Bold = true;
            summarySheet.Cells["I" + (startCellIndex + 2).ToString()].Style.Font.Italic = true;

            //styling the borders;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thick, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));


            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            summarySheet.Cells["D" + (startCellIndex + 2).ToString() + ":I" + (startCellIndex + 2).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 3).ToString() + ":I" + (startCellIndex + 3).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 4).ToString() + ":I" + (startCellIndex + 4).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 5).ToString() + ":I" + (startCellIndex + 5).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 6).ToString() + ":I" + (startCellIndex + 6).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 7).ToString() + ":I" + (startCellIndex + 7).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 8).ToString() + ":I" + (startCellIndex + 8).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 9).ToString() + ":I" + (startCellIndex + 9).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 10).ToString() + ":I" + (startCellIndex + 10).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 10).ToString() + ":I" + (startCellIndex + 10).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 11).ToString() + ":I" + (startCellIndex + 11).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 11).ToString() + ":I" + (startCellIndex + 11).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 12).ToString() + ":I" + (startCellIndex + 12).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 12).ToString() + ":I" + (startCellIndex + 12).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["C" + (startCellIndex + 13).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            summarySheet.Cells["C" + (startCellIndex + 13).ToString() + ":I" + (startCellIndex + 13).ToString()].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));


            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["C" + (startCellIndex + 1).ToString() + ":C" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["D" + (startCellIndex + 1).ToString() + ":D" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["E" + (startCellIndex + 1).ToString() + ":E" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["F" + (startCellIndex + 1).ToString() + ":F" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["G" + (startCellIndex + 1).ToString() + ":G" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 13).ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            summarySheet.Cells["H" + (startCellIndex + 1).ToString() + ":H" + (startCellIndex + 13).ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            ///inserting the values
            for (int i = 0; i < rs.TopImpairmentSummary.Count; i++)
            {
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Style.Font.Bold = true;
                summarySheet.Cells["C" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].Field1;
                summarySheet.Cells["D" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].Exposure_Pre;
                summarySheet.Cells["E" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].Impairment_Pre;
                summarySheet.Cells["F" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].CoverageRatio_Pre;
                summarySheet.Cells["G" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].Exposure_Pre;
                summarySheet.Cells["H" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].Impairment_Post;
                summarySheet.Cells["I" + (startCellIndex + 3 + i).ToString()].Value = rs.TopImpairmentSummary[i].CoverageRatio_Post;
            }


            /// 
            /////
            #endregion

            summarySheet.Cells[summarySheet.Dimension.Address].AutoFitColumns();
        }


        private void SummarySubheader(string cellReference, ExcelWorksheet summarySheet, string subHeaderName)
        {
            summarySheet.Cells[cellReference].Value = subHeaderName;
            summarySheet.Cells[cellReference].Style.Font.Size = 12;
            summarySheet.Cells[cellReference].Style.Font.Italic = true;
            summarySheet.Cells[cellReference].Style.Font.Bold = true;
            summarySheet.Cells[cellReference].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
        }

        private void ResultSheet(DataTable dataTable, ExcelPackage excelPackage, ResultDetail rd)
        {
            ExcelWorksheet resultSheet = excelPackage.Workbook.Worksheets["Result"];
            resultSheet.View.ZoomScale = workbookZoomSize;

            ///styling for the whole worksheet
            resultSheet.View.ShowGridLines = false;
            resultSheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            resultSheet.Cells.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            resultSheet.Cells.Style.Font.Name = workbookFontType;
            resultSheet.Cells.Style.Font.Size = workbookFontSize;
            resultSheet.Cells.Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_TEXT_DARK));
            /////***********///
            ///


            //resize column A and B
            double columnWidth = 10;
            resultSheet.Column(1).Width = columnWidth;// column A
            resultSheet.Column(2).Width = columnWidth;//column B
            resultSheet.Column(23).Width = columnWidth;//column W
            ///

            //styling for the container that houses the total number of contracts etc  
            resultSheet.Cells["B2:Y2"].Style.Border.Top.Style = ExcelBorderStyle.Thick;//////2,2,2,23
            resultSheet.Cells["B5:Y5"].Style.Border.Bottom.Style = ExcelBorderStyle.Thick; ///5,2,5,23
            resultSheet.Cells["B2:B5"].Style.Border.Left.Style = ExcelBorderStyle.Thick; ////2,2,5,2
            resultSheet.Cells["Y2:Y5"].Style.Border.Right.Style = ExcelBorderStyle.Thick; ///2,23,5,23
            resultSheet.Cells["B2:Y5"].Style.Fill.PatternType = ExcelFillStyle.Solid; //2,2,5,23
            resultSheet.Cells["B2:Y5"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_DARK_BLUE));//2,2,5,23
            /////***********///
            ///

            ////styling the Total Number of Contracts//
            resultSheet.Cells["C3:D3"].Merge = true;
            resultSheet.Cells["C4:D4"].Merge = true;
            resultSheet.Cells["C3:D3"].Style.Border.BorderAround(ExcelBorderStyle.Dashed, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["C4:C4"].Style.Border.BorderAround(ExcelBorderStyle.Thin, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["C4:D4"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            //C3
            resultSheet.Cells["C3"].Value = "Total Number of Contracts";
            resultSheet.Cells["C3"].Style.Font.Bold = true;
            //resultSheet.Cells["C3:D4"].Style.Font.Size = 12;
            resultSheet.Cells["C3"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            //C4
            resultSheet.Cells["C4"].Value = rd.NumberOfContracts;
            resultSheet.Cells["C4"].Style.Font.Bold = true;
            resultSheet.Cells["C4"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_TEXT_DARK));
            /////***********///
            ///

            ////styling the Total Model Output (pre overrides)//
            resultSheet.Cells["J3:N3"].Merge = true;
            resultSheet.Cells["J3"].Value = "Total Model Output (Pre-Overrides)";
            //resultSheet.Cells["J3:N4"].Style.Font.Size = 12;
            resultSheet.Cells["J3"].Style.Font.Bold = true;
            resultSheet.Cells["J3"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            //the borders
            resultSheet.Cells["J3:N3"].Style.Border.BorderAround(ExcelBorderStyle.Dashed, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["J4:N4"].Style.Border.BorderAround(ExcelBorderStyle.Thin, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            //the numbers
            resultSheet.Cells["J4:N4"].Style.Font.Bold = true;
            resultSheet.Cells["J4:N4"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_TEXT_DARK));

            //the background
            resultSheet.Cells["J4:N4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            resultSheet.Cells["J4:N4"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            /////***********///
            ///

            ////styling the Total Model Output (post overrides)//
            resultSheet.Cells["S3:V3"].Merge = true;
            resultSheet.Cells["S3"].Value = "Total Model Output (Post-Overrides)";
            //resultSheet.Cells["S3:V4"].Style.Font.Size = 12;
            resultSheet.Cells["S3"].Style.Font.Bold = true;
            resultSheet.Cells["S3"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            //the borders
            resultSheet.Cells["S3:V3"].Style.Border.BorderAround(ExcelBorderStyle.Dashed, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["S4:V4"].Style.Border.BorderAround(ExcelBorderStyle.Thin, ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            //the numbers
            resultSheet.Cells["S4:V4"].Style.Font.Bold = true;
            resultSheet.Cells["S4:V4"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_TEXT_DARK));

            //the background
            resultSheet.Cells["S4:V4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            resultSheet.Cells["S4:V4"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            /////***********///
            ///

            //////styling the main body container//
            resultSheet.Cells["C7:X7"].Merge = true;
            resultSheet.Cells["C7:X9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            resultSheet.Cells["C7:X9"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_DARK_BLUE));
            resultSheet.Cells["C7:X9"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.WHITE));
            resultSheet.Cells["C7:X9"].Style.Border.BorderAround(ExcelBorderStyle.Thick);
            resultSheet.Cells["C7:X9"].Style.Font.Bold = true;
            resultSheet.Cells["C7:X9"].Style.Font.Italic = true;

            //////styling the contract level results//
            resultSheet.Cells["C7"].Value = "CONTRACT LEVEL RESULTS";
            // resultSheet.Cells["C7"].Style.Font.Bold = true;
            resultSheet.Cells["C7"].Style.Font.Size = 16;
            resultSheet.Cells["C7:X7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


            //styling for Contract Data - Snapshot Date////
            resultSheet.Cells["C8:X8"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            //resultSheet.Cells["C8:V8"].Style.Font.Italic = true;
            resultSheet.Cells["C8:J8"].Merge = true;
            resultSheet.Cells["C8"].Value = "Contract Data - Snapshot Date";
            resultSheet.Cells["C8:J8"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["C8:J8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            resultSheet.Cells["C9:V9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //styling for Model Output (Pre-Overrides and Overlays)////
            resultSheet.Cells["K8:N8"].Merge = true;
            resultSheet.Cells["K8"].Value = "Model Output (Pre-Overrides and Overlays)";
            resultSheet.Cells["K8:N8"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["K8:N8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            //styling for Overrides////
            resultSheet.Cells["O8:Q8"].Merge = true;
            resultSheet.Cells["O8"].Value = "Overrides";
            resultSheet.Cells["O8:Q8"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["O8:Q8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            //styling for Overlay
            resultSheet.Cells["R8"].Value = "Overlay";
            resultSheet.Cells["R8"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["R8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            //styling for Model Output (Post - Overrides and Overlays)////
            resultSheet.Cells["S8:X8"].Merge = true;
            resultSheet.Cells["S8"].Value = "Model Output (Post-Overrides and Overlays)";
            resultSheet.Cells["S8:X8"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["S8:X8"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            /////***********///
            ///

            //paste values into the summary sheet and adjust 
            resultSheet.Cells["C9"].Value = "CONTRACT_ID";
            resultSheet.Cells["D9"].Value = "ACCOUNT_NO";
            resultSheet.Cells["E9"].Value = "CUSTOMER_NO";
            resultSheet.Cells["F9"].Value = "SEGMENT";
            resultSheet.Cells["G9"].Value = "PRODUCT_TYPE";
            resultSheet.Cells["H9"].Value = "SECTOR";
            resultSheet.Cells["I9"].Value = "STAGE";
            resultSheet.Cells["J9"].Value = "Outstanding Balance";
            resultSheet.Cells["K9"].Value = "ECL - Best Estimate";
            resultSheet.Cells["L9"].Value = "ECL - Optimistic";
            resultSheet.Cells["M9"].Value = "ECL - Downturn";
            resultSheet.Cells["N9"].Value = "Impairment (Model Output)";
            resultSheet.Cells["O9"].Value = "Stage";
            resultSheet.Cells["P9"].Value = "TTR_YEARS";
            resultSheet.Cells["Q9"].Value = "FSV";
            resultSheet.Cells["R9"].Value = "Overlay %";
            resultSheet.Cells["S9"].Value = "ECL - Best Estimate";
            resultSheet.Cells["T9"].Value = "ECL - Optimistic";
            resultSheet.Cells["U9"].Value = "ECL - Downturn";
            resultSheet.Cells["V9"].Value = "Impairment (Manual Overrides)";
            resultSheet.Cells["W9"].Value = "Reason";
            resultSheet.Cells["X9"].Value = "Override Type";

            for (var i = 0; i < rd.ResultDetailDataMore.Count; i++)
            {
                var _rd = rd.ResultDetailDataMore[i];
                resultSheet.Cells["C" + (10 + i).ToString()].Value = _rd.ContractNo;
                resultSheet.Cells["D" + (10 + i).ToString()].Value = _rd.AccountNo;
                resultSheet.Cells["E" + (10 + i).ToString()].Value = _rd.CustomerNo;
                resultSheet.Cells["F" + (10 + i).ToString()].Value = _rd.Segment;
                resultSheet.Cells["G" + (10 + i).ToString()].Value = _rd.ProductType;
                resultSheet.Cells["H" + (10 + i).ToString()].Value = _rd.Sector;
                resultSheet.Cells["I" + (10 + i).ToString()].Value = _rd.Stage;
                resultSheet.Cells["J" + (10 + i).ToString()].Value = _rd.Outstanding_Balance;
                resultSheet.Cells["K" + (10 + i).ToString()].Value = _rd.ECL_Best_Estimate;
                resultSheet.Cells["L" + (10 + i).ToString()].Value = _rd.ECL_Optimistic;
                resultSheet.Cells["M" + (10 + i).ToString()].Value = _rd.ECL_Downturn;
                resultSheet.Cells["N" + (10 + i).ToString()].Value = _rd.Impairment_ModelOutput;
                resultSheet.Cells["O" + (10 + i).ToString()].Value = _rd.Overrides_Stage;
                resultSheet.Cells["P" + (10 + i).ToString()].Value = _rd.Overrides_TTR_Years;
                resultSheet.Cells["Q" + (10 + i).ToString()].Value = _rd.Overrides_FSV;
                resultSheet.Cells["R" + (10 + i).ToString()].Value = _rd.Overrides_Overlay;
                resultSheet.Cells["S" + (10 + i).ToString()].Value = _rd.Overrides_ECL_Best_Estimate;
                resultSheet.Cells["T" + (10 + i).ToString()].Value = _rd.Overrides_ECL_Optimistic;
                resultSheet.Cells["U" + (10 + i).ToString()].Value = _rd.Overrides_ECL_Downturn;
                resultSheet.Cells["V" + (10 + i).ToString()].Value = _rd.Overrides_Impairment_Manual;
                resultSheet.Cells["W" + (10 + i).ToString()].Value = _rd.Reason;
                resultSheet.Cells["X" + (10 + i).ToString()].Value = _rd.OverrideType;
            }

            int rowCount = resultSheet.Dimension.End.Row;
            int colCount = resultSheet.Dimension.End.Column;

            //hide unused rows and columns
            //for (int i = rowCount; i < maxNumberRows + 1; i++)
            //{
            //    resultSheet.Row(i).Hidden = true;
            //}

            //for (int i = colCount; i < maxNumberCol + 1; i++)
            //{
            //    resultSheet.Column(i).Hidden = true;
            //}

            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Top.Style = ExcelBorderStyle.Dashed;
            resultSheet.Cells["C3:D3"].Style.Border.Top.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Left.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Bottom.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            resultSheet.Cells["C9:X" + rowCount.ToString()].Style.Border.Right.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));

            //doing sums for total model output (pre overrides)
            resultSheet.Cells["J4"].Value = rd.OutStandingBalance;
            resultSheet.Cells["K4"].Value = rd.Pre_ECL_Best_Estimate;
            resultSheet.Cells["L4"].Value = rd.Pre_ECL_Optimistic;
            resultSheet.Cells["M4"].Value = rd.Pre_ECL_Downturn;
            resultSheet.Cells["N4"].Value = rd.Pre_Impairment_ModelOutput;

            //doing sums for total model output (post overrides)
            resultSheet.Cells["S4"].Value = rd.Post_ECL_Best_Estimate;
            resultSheet.Cells["T4"].Value = rd.Post_ECL_Optimistic;
            resultSheet.Cells["U4"].Value = rd.Post_ECL_Downturn;
            resultSheet.Cells["V4"].Value = rd.Post_Impairment_ModelOutput;

            //resultSheet.Cells["J9:N" + rowCount.ToString()].Style.Numberformat.Format = "#,##0.00";
            //resultSheet.Cells["S9:V" + rowCount.ToString()].Style.Numberformat.Format = "#,##0.00";

            resultSheet.Cells[resultSheet.Dimension.Address].AutoFitColumns();
        }

        //1767707583 polaris joshua 

        //private static void ChangeTitle(ExcelPackage excelPackage, string value)
        //{
        //    ExcelWorksheet titlePage = excelPackage.Workbook.Worksheets["Start"];

        //    titlePage.Cells["C4"].Value = value;
        //    titlePage.Cells["C4"].Style.Font.Size = 16;
        //    titlePage.Cells["C4"].Style.Font.Italic = true;
        //    titlePage.Cells["C4"].Style.Font.Bold = true;
        //    titlePage.Cells["C4"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(ETI_Colors.ETI_GREEN));
        //}


        private ResultSummary GetResultSummary(EclType eclType, Guid eclId, ResultDetail rd)
        {
            var rs= new ResultSummary();

            ///////////
            /////Oversall
            /////

            #region Overall
            rs.Overrall = new List<ReportBreakdown>();

            var totalExposure = string.Format("{0:N}", rd.OutStandingBalance);
            var preOverrideOverlay = string.Format("{0:N}", rd.Pre_Impairment_ModelOutput);
            var postOverrideOverlay = string.Format("{0:N}", rd.Post_Impairment_ModelOutput);
            var PortfoliOverlay = 0;
            var totalImpairment = string.Format("{0:N}", PortfoliOverlay + rd.Post_Impairment_ModelOutput);
            var finalCoverage = ((PortfoliOverlay + rd.Post_Impairment_ModelOutput / rd.OutStandingBalance) * 100).ToString();
            rs.Overrall.Add(new ReportBreakdown { Field1 = totalExposure, Exposure_Pre = preOverrideOverlay, Impairment_Pre = postOverrideOverlay, CoverageRatio_Pre = "", Exposure_Post = totalImpairment, Impairment_Post = finalCoverage, CoverageRatio_Post = "" });
            #endregion

            #region Scenerio
            rs.Scenario = new List<ReportBreakdown>();
            var ECL_BE = new ReportBreakdown();
            ECL_BE.Field1 = "ECL - Best Estimate";
            ECL_BE.Exposure_Pre = string.Format("{0:N}", rd.Pre_ECL_Best_Estimate);
            ECL_BE.Impairment_Pre = string.Format("{0:N}", rd.Pre_ECL_Best_Estimate / rd.OutStandingBalance);
            ECL_BE.Exposure_Post = string.Format("{0:N}", rd.Post_ECL_Best_Estimate);
            ECL_BE.Impairment_Post = string.Format("{0:N}", rd.Post_ECL_Best_Estimate / rd.OutStandingBalance);
            rs.Scenario.Add(ECL_BE);

            var ECL_O = new ReportBreakdown();
            ECL_O.Field1 = "ECL - Optimistic";
            ECL_O.Exposure_Pre = string.Format("{0:N}", rd.Pre_ECL_Optimistic);
            ECL_O.Impairment_Pre = string.Format("{0:N}", rd.Pre_ECL_Optimistic / rd.OutStandingBalance);
            ECL_O.Exposure_Post = string.Format("{0:N}", rd.Post_ECL_Optimistic);
            ECL_O.Impairment_Post = string.Format("{0:N}", rd.Post_ECL_Optimistic / rd.OutStandingBalance);
            rs.Scenario.Add(ECL_O);

            var ECL_D = new ReportBreakdown();
            ECL_D.Field1 = "ECL - Downturn";
            ECL_D.Exposure_Pre = string.Format("{0:N}", rd.Pre_ECL_Downturn);
            ECL_D.Impairment_Pre = string.Format("{0:N}", rd.Pre_ECL_Downturn / rd.OutStandingBalance);
            ECL_D.Exposure_Post = string.Format("{0:N}", rd.Post_ECL_Downturn);
            ECL_D.Impairment_Post = string.Format("{0:N}", rd.Post_ECL_Downturn / rd.OutStandingBalance);
            rs.Scenario.Add(ECL_D);


            var ECL_Impairment = new ReportBreakdown();
            ECL_Impairment.Field1 = "Impairment";
            ECL_Impairment.Exposure_Pre = string.Format("{0:N}", rd.Pre_Impairment_ModelOutput);
            ECL_Impairment.Impairment_Pre = string.Format("{0:N}", rd.Pre_Impairment_ModelOutput / rd.OutStandingBalance);
            ECL_Impairment.Exposure_Post = string.Format("{0:N}", rd.Post_Impairment_ModelOutput);
            ECL_Impairment.Impairment_Post = string.Format("{0:N}", rd.Post_Impairment_ModelOutput / rd.OutStandingBalance);
            rs.Scenario.Add(ECL_Impairment);
            #endregion

            //
            #region Stage
            rs.Stage = new List<ReportBreakdown>();
            var stage1 = new ReportBreakdown();
            stage1.Field1 = "Stage 1";
            var s1_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 1).Sum(p => p.Outstanding_Balance);
            stage1.Exposure_Pre = string.Format("{0:N}", s1_exposure_pre);
            var s1_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 1).Sum(p => p.Impairment_ModelOutput);
            stage1.Impairment_Pre = string.Format("{0:N}", s1_impairment_pre);
            try { stage1.CoverageRatio_Pre = ((s1_impairment_pre / s1_exposure_pre)).ToString(); } catch { stage1.CoverageRatio_Pre = "0.00"; };

            var s1_exposure_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 1).Sum(p => p.Outstanding_Balance);
            stage1.Exposure_Post = string.Format("{0:N}", s1_exposure_post);
            var s1_impairment_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 1).Sum(p => p.Overrides_Impairment_Manual);
            stage1.Impairment_Post = string.Format("{0:N}", s1_impairment_post);
            try { stage1.CoverageRatio_Post = ((s1_impairment_post / s1_exposure_post)*100).ToString(); } catch { stage1.CoverageRatio_Post = "0.00"; };
            rs.Stage.Add(stage1);

            var stage2 = new ReportBreakdown();
            stage2.Field1 = "Stage 2";
            var s2_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 2).Sum(p => p.Outstanding_Balance);
            stage2.Exposure_Pre = string.Format("{0:N}", s2_exposure_pre);
            var s2_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 2).Sum(p => p.Impairment_ModelOutput);
            stage2.Impairment_Pre = string.Format("{0:N}", s2_impairment_pre);
            try { stage2.CoverageRatio_Pre = ((s2_impairment_pre / s2_exposure_pre) * 100).ToString(); } catch { stage2.CoverageRatio_Pre = "0.00"; };

            var s2_exposure_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 2).Sum(p => p.Outstanding_Balance);
            stage2.Exposure_Post = string.Format("{0:N}", s2_exposure_post);
            var s2_impairment_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 2).Sum(p => p.Overrides_Impairment_Manual);
            stage2.Impairment_Post = string.Format("{0:N}", s2_impairment_post);
            try { stage2.CoverageRatio_Post = ((s2_impairment_post / s2_exposure_post) * 100).ToString(); } catch { stage2.CoverageRatio_Post = "0.00"; };
            rs.Stage.Add(stage2);



            var stage3 = new ReportBreakdown();
            stage3.Field1 = "Stage 3";
            var s3_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 3).Sum(p => p.Outstanding_Balance);
            stage3.Exposure_Pre = string.Format("{0:N}", s3_exposure_pre);
            var s3_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Stage == 3).Sum(p => p.Impairment_ModelOutput);
            stage3.Impairment_Pre = string.Format("{0:N}", s3_impairment_pre);
            try { stage3.CoverageRatio_Pre = ((s3_impairment_pre / s3_exposure_pre) * 100).ToString(); } catch { stage3.CoverageRatio_Pre = "0.00"; };

            var s3_exposure_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 3).Sum(p => p.Outstanding_Balance);
            stage3.Exposure_Post = string.Format("{0:N}", s3_exposure_post);
            var s3_impairment_post = rd.ResultDetailDataMore.Where(o => o.Overrides_Stage == 3).Sum(p => p.Overrides_Impairment_Manual);
            stage3.Impairment_Post = string.Format("{0:N}", s3_impairment_post);
            try { stage3.CoverageRatio_Post = ((s3_impairment_post / s3_exposure_post) * 100).ToString(); } catch { stage3.CoverageRatio_Post = "0.00"; };
            rs.Stage.Add(stage3);


            var stageTotal = new ReportBreakdown();
            stageTotal.Field1 = "Total";
            var t_exposure_pre = s1_exposure_pre + s2_exposure_pre + s3_exposure_pre;
            stageTotal.Exposure_Pre = string.Format("{0:N}", t_exposure_pre);
            var t_impairment_pre = s1_impairment_pre + s2_impairment_pre + s3_impairment_pre;
            stageTotal.Impairment_Pre = string.Format("{0:N}", t_impairment_pre);
            try { stageTotal.CoverageRatio_Pre = ((t_impairment_pre / t_exposure_pre) * 100).ToString(); } catch { stageTotal.CoverageRatio_Pre = "0.00"; };

            var t_exposure_post = s1_exposure_post + s2_exposure_post + s3_exposure_post;
            stageTotal.Exposure_Post = string.Format("{0:N}", t_exposure_post);
            var t_impairment_post = s1_impairment_post + s2_impairment_post + s3_impairment_post;
            stageTotal.Impairment_Post = string.Format("{0:N}", t_impairment_post);
            try { stageTotal.CoverageRatio_Post = ((t_impairment_post / t_exposure_post) * 100).ToString(); } catch { stageTotal.CoverageRatio_Post = "0.00"; };
            rs.Stage.Add(stageTotal);
            #endregion

            #region ProductType
            rs.ProductType = new List<ReportBreakdown>();
            var card = new ReportBreakdown();
            card.Field1 = "CARD";
            var p1_exposure_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "card").Sum(p => p.Outstanding_Balance);
            try { card.Exposure_Pre = string.Format("{0:N}", p1_exposure_pre); } catch { card.Exposure_Pre = "0.00"; };
            
            var p1_impairment_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "card").Sum(p => p.Impairment_ModelOutput);
            try { card.Impairment_Pre = string.Format("{0:N}", p1_impairment_pre); } catch { card.Impairment_Pre = "0.00"; };
            try { card.CoverageRatio_Pre = ((p1_impairment_pre / p1_exposure_pre) * 100).ToString(); } catch { card.CoverageRatio_Pre = "0.00"; };

            var p1_exposure_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "card").Sum(p => p.Outstanding_Balance);
            try { card.Exposure_Post = string.Format("{0:N}", p1_exposure_post); } catch { card.Exposure_Post = "0.00"; };
            var p1_impairment_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "card").Sum(p => p.Overrides_Impairment_Manual);
            try { card.Impairment_Post = string.Format("{0:N}", p1_impairment_post); } catch { card.Impairment_Post = "0.00"; };
            try { card.CoverageRatio_Post = ((p1_impairment_post / p1_exposure_post) * 100).ToString(); } catch { card.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(card);

            var lease = new ReportBreakdown();
            lease.Field1 = "LEASE";
            var p2_exposure_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "lease").Sum(p => p.Outstanding_Balance);
            try { lease.Exposure_Pre = string.Format("{0:N}", p2_exposure_pre); } catch { lease.Exposure_Pre = "0.00"; };

            var p2_impairment_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "lease").Sum(p => p.Impairment_ModelOutput);
            try { lease.Impairment_Pre = string.Format("{0:N}", p2_impairment_pre); } catch { lease.Impairment_Pre = "0.00"; };
            try { lease.CoverageRatio_Pre = ((p2_impairment_pre / p2_exposure_pre) * 100).ToString(); } catch { lease.CoverageRatio_Pre = "0.00"; };

            var p2_exposure_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "lease").Sum(p => p.Outstanding_Balance);
            try { lease.Exposure_Post = string.Format("{0:N}", p2_exposure_post); } catch { lease.Exposure_Post = "0.00"; };

            var p2_impairment_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "lease").Sum(p => p.Overrides_Impairment_Manual);
            try { lease.Impairment_Post = string.Format("{0:N}", p2_impairment_post); } catch { lease.Impairment_Post = "0.00"; };
            try { lease.CoverageRatio_Post = ((p2_impairment_post / p2_exposure_post) * 100).ToString(); } catch { lease.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(lease);


            var loan = new ReportBreakdown();
            loan.Field1 = "LOAN";
            var p3_exposure_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "loan").Sum(p => p.Outstanding_Balance);
            try { loan.Exposure_Pre = string.Format("{0:N}", p3_exposure_pre); } catch { loan.Exposure_Pre = "0.00"; };

            var p3_impairment_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "loan").Sum(p => p.Impairment_ModelOutput);
            try { loan.Impairment_Pre = string.Format("{0:N}", p3_impairment_pre); } catch { loan.Impairment_Pre = "0.00"; };
            try { loan.CoverageRatio_Pre = ((p3_impairment_pre / p3_exposure_pre) * 100).ToString(); } catch { loan.CoverageRatio_Pre = "0.00"; };

            var p3_exposure_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "loan").Sum(p => p.Outstanding_Balance);
            try { loan.Exposure_Post = string.Format("{0:N}", p3_exposure_post); } catch { loan.Exposure_Post = "0.00"; };

            var p3_impairment_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "loan").Sum(p => p.Overrides_Impairment_Manual);
            try { loan.Impairment_Post = string.Format("{0:N}", p3_impairment_post); } catch { loan.Impairment_Post = "0.00"; };
            try { loan.CoverageRatio_Post = ((p3_impairment_post / p3_exposure_post) * 100).ToString(); } catch { loan.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(loan);

            var mortgage = new ReportBreakdown();
            mortgage.Field1 = "MORTGAGE";
            var p4_exposure_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "mortgage").Sum(p => p.Outstanding_Balance);
            try { mortgage.Exposure_Pre = string.Format("{0:N}", p4_exposure_pre); } catch { mortgage.Exposure_Pre = "0.00"; };

            var p4_impairment_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "mortgage").Sum(p => p.Impairment_ModelOutput);
            try { mortgage.Impairment_Pre = string.Format("{0:N}", p4_impairment_pre); } catch { mortgage.Impairment_Pre = "0.00"; };
            try { mortgage.CoverageRatio_Pre = ((p4_impairment_pre / p4_exposure_pre) * 100).ToString(); } catch { mortgage.CoverageRatio_Pre = "0.00"; };

            var p4_exposure_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "mortgage").Sum(p => p.Outstanding_Balance);
            try { mortgage.Exposure_Post = string.Format("{0:N}", p4_exposure_post); } catch { mortgage.Exposure_Post = "0.00"; };
            var p4_impairment_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "mortgage").Sum(p => p.Overrides_Impairment_Manual);
            try { mortgage.Impairment_Post = string.Format("{0:N}", p4_impairment_post); } catch { mortgage.Impairment_Post = "0.00"; };
            try { mortgage.CoverageRatio_Post = ((p4_impairment_post / p4_exposure_post) * 100).ToString(); } catch { mortgage.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(mortgage);

            var od = new ReportBreakdown();
            od.Field1 = "OD";
            var p5_exposure_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "od").Sum(p => p.Outstanding_Balance);
            try { od.Exposure_Pre = string.Format("{0:N}", p5_exposure_pre); } catch { od.Exposure_Pre = "0.00"; };

            var p5_impairment_pre = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "od").Sum(p => p.Impairment_ModelOutput);
            try { od.Impairment_Pre = string.Format("{0:N}", p5_impairment_pre); } catch { od.Impairment_Pre = "0.00"; };
            try { od.CoverageRatio_Pre = ((p5_impairment_pre / p5_exposure_pre) * 100).ToString(); } catch { od.CoverageRatio_Pre = "0.00"; };

            var p5_exposure_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "od").Sum(p => p.Outstanding_Balance);
            try { od.Exposure_Post = string.Format("{0:N}", p5_exposure_post); } catch { od.Exposure_Post = "0.00"; };

            var p5_impairment_post = rd.ResultDetailDataMore.Where(o => o.ProductType.ToLower() == "od").Sum(p => p.Overrides_Impairment_Manual);
            try { od.Impairment_Post = string.Format("{0:N}", p5_impairment_post); } catch { od.Exposure_Post = "0.00"; };
            try { od.CoverageRatio_Post = ((p5_impairment_post / p5_exposure_post) * 100).ToString(); } catch { od.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(od);


            var producttypeTotal = new ReportBreakdown();
            producttypeTotal.Field1 = "Total";
            var p6_exposure_pre = p1_exposure_pre + p2_exposure_pre + p3_exposure_pre + p4_exposure_pre + p5_exposure_pre;
            try { producttypeTotal.Exposure_Pre = string.Format("{0:N}", p6_exposure_pre); } catch { producttypeTotal.Exposure_Pre = "0.00"; };

            var p6_impairment_pre = p1_impairment_pre + p2_impairment_pre + p3_impairment_pre + p4_impairment_pre + p5_impairment_pre;
            try { producttypeTotal.Impairment_Pre = string.Format("{0:N}", p6_impairment_pre); } catch { producttypeTotal.Impairment_Pre = "0.00"; };
            try { producttypeTotal.CoverageRatio_Pre = string.Format("{0:N}", (p6_impairment_pre / p6_exposure_pre) * 100); } catch { producttypeTotal.CoverageRatio_Pre = "0.00"; };

            var p6_exposure_post = p1_exposure_post + p2_exposure_post + p3_exposure_post + p4_exposure_post + p5_exposure_post;
            try { producttypeTotal.Exposure_Post = string.Format("{0:N}", p6_exposure_post); } catch { producttypeTotal.Exposure_Post = "0.00"; };

            var p6_impairment_post = p1_impairment_post + p2_impairment_post + p3_impairment_post + p4_impairment_post + p5_impairment_post;
            try { producttypeTotal.Impairment_Post = string.Format("{0:N}", p6_impairment_post); } catch { producttypeTotal.Impairment_Post = "0.00"; };
            try { producttypeTotal.CoverageRatio_Post = ((p6_impairment_post / p6_exposure_post) * 100).ToString(); } catch { producttypeTotal.CoverageRatio_Post = "0.00"; };
            rs.ProductType.Add(producttypeTotal);
            #endregion

            #region segment
            rs.Segment = new List<ReportBreakdown>();
            var commercial = new ReportBreakdown();
            commercial.Field1 = "COMMERCIAL";
            var sg1_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial").Sum(p => p.Outstanding_Balance);
            try { commercial.Exposure_Pre = string.Format("{0:N}", sg1_exposure_pre); } catch { commercial.Exposure_Pre = "0.00"; };

            var sg1_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial").Sum(p => p.Impairment_ModelOutput);
            try { commercial.Impairment_Pre = string.Format("{0:N}", sg1_impairment_pre); } catch { commercial.Impairment_Pre = "0.00"; };
            try { commercial.CoverageRatio_Pre = ((sg1_impairment_pre / sg1_exposure_pre) * 100).ToString(); } catch { commercial.CoverageRatio_Pre = "0.00"; };

            var sg1_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial").Sum(p => p.Outstanding_Balance);
            try { commercial.Exposure_Post = string.Format("{0:N}", sg1_exposure_post); } catch { commercial.Exposure_Post = "0.00"; };

            var sg1_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial").Sum(p => p.Overrides_Impairment_Manual);
            try { commercial.Impairment_Post = string.Format("{0:N}", sg1_impairment_post); } catch { commercial.Impairment_Post = "0.00"; };
            try { commercial.CoverageRatio_Post = ((sg1_impairment_post / sg1_exposure_post) * 100).ToString(); } catch { commercial.CoverageRatio_Post = "0.00"; };
            rs.Segment.Add(commercial);

            var corporate = new ReportBreakdown();
            corporate.Field1 = "CORPORATE";
            var sg2_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate").Sum(p => p.Outstanding_Balance);
            try { corporate.Exposure_Pre = string.Format("{0:N}", sg2_exposure_pre); } catch { corporate.Exposure_Pre = "0.00"; };

            var sg2_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate").Sum(p => p.Impairment_ModelOutput);
            try { corporate.Impairment_Pre = string.Format("{0:N}", sg2_impairment_pre); } catch { corporate.Impairment_Pre = "0.00"; };
            try { lease.CoverageRatio_Pre = ((sg2_impairment_pre / sg2_exposure_pre) * 100).ToString(); } catch { corporate.CoverageRatio_Pre = "0.00"; };

            var sg2_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate").Sum(p => p.Outstanding_Balance);
            try { corporate.Exposure_Post = string.Format("{0:N}", sg2_exposure_post); } catch { corporate.Exposure_Post = "0.00"; };

            var sg2_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate").Sum(p => p.Overrides_Impairment_Manual);
            try { corporate.Impairment_Post = string.Format("{0:N}", sg2_impairment_post); } catch { corporate.Impairment_Post = "0.00"; };
            try { corporate.CoverageRatio_Post = ((sg2_impairment_post / sg2_exposure_post) * 100).ToString(); } catch { corporate.CoverageRatio_Post = "0.00"; };
            rs.Segment.Add(corporate);


            var segmentTotal = new ReportBreakdown();
            segmentTotal.Field1 = "Total";
            var sg3_exposure_pre = sg1_exposure_pre + sg2_exposure_pre;
            try { segmentTotal.Exposure_Pre = string.Format("{0:N}", sg3_exposure_pre); } catch { segmentTotal.Exposure_Pre = "0.00"; };

            var sg3_impairment_pre = sg1_impairment_pre + sg2_impairment_pre;
            try { segmentTotal.Impairment_Pre = string.Format("{0:N}", sg3_impairment_pre); } catch { segmentTotal.Impairment_Pre = "0.00"; };
            try { segmentTotal.CoverageRatio_Pre = ((sg3_impairment_pre / sg3_exposure_pre) * 100).ToString(); } catch { segmentTotal.CoverageRatio_Pre = "0.00"; };

            var sg3_exposure_post = sg1_exposure_post + sg2_exposure_post;
            try { segmentTotal.Exposure_Post = string.Format("{0:N}", sg3_exposure_post); } catch { segmentTotal.Exposure_Post = "0.00"; };

            var sg3_impairment_post = sg1_impairment_post + sg2_impairment_post;
            try { segmentTotal.Impairment_Post = string.Format("{0:N}", sg3_impairment_post); } catch { segmentTotal.Impairment_Post = "0.00"; };
            try { segmentTotal.CoverageRatio_Post = ((sg3_impairment_post / sg3_exposure_post) * 100).ToString(); } catch { segmentTotal.CoverageRatio_Post = "0.00"; };
            rs.Segment.Add(segmentTotal);
            #endregion

            #region Segment and Stage
            rs.SegmentAndStage = new List<ReportBreakdown>();
            var COMMERCIAL_STAGE_1 = new ReportBreakdown();
            COMMERCIAL_STAGE_1.Field1 = "COMMERCIAL_STAGE_1";
            var ss1_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage==1).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_1.Exposure_Pre = string.Format("{0:N}", ss1_exposure_pre);
            var ss1_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 1).Sum(p => p.Impairment_ModelOutput);
            COMMERCIAL_STAGE_1.Impairment_Pre = string.Format("{0:N}", ss1_impairment_pre);
            try { COMMERCIAL_STAGE_1.CoverageRatio_Pre = ((ss1_impairment_pre / ss1_exposure_pre) * 100).ToString(); } catch { COMMERCIAL_STAGE_1.CoverageRatio_Pre = "0.00"; };

            var ss1_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 1).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_1.Exposure_Post = string.Format("{0:N}", ss1_exposure_post);
            var ss1_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 1).Sum(p => p.Overrides_Impairment_Manual);
            COMMERCIAL_STAGE_1.Impairment_Post = string.Format("{0:N}", ss1_impairment_post);
            try { COMMERCIAL_STAGE_1.CoverageRatio_Post = ((ss1_impairment_post / ss1_exposure_post) * 100).ToString(); } catch { COMMERCIAL_STAGE_1.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(COMMERCIAL_STAGE_1);

            var COMMERCIAL_STAGE_2 = new ReportBreakdown();
            COMMERCIAL_STAGE_2.Field1 = "COMMERCIAL_STAGE_2";
            var ss2_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 2).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_2.Exposure_Pre = string.Format("{0:N}", ss2_exposure_pre);
            var ss2_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 2).Sum(p => p.Impairment_ModelOutput);
            COMMERCIAL_STAGE_2.Impairment_Pre = string.Format("{0:N}", ss2_impairment_pre);
            try { COMMERCIAL_STAGE_2.CoverageRatio_Pre = ((ss2_impairment_pre / ss2_exposure_pre) * 100).ToString(); } catch { COMMERCIAL_STAGE_2.CoverageRatio_Pre = "0.00"; };

            var ss2_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 2).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_2.Exposure_Post = string.Format("{0:N}", ss2_exposure_post);
            var ss2_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 2).Sum(p => p.Overrides_Impairment_Manual);
            COMMERCIAL_STAGE_2.Impairment_Post = string.Format("{0:N}", ss2_impairment_post);
            try { COMMERCIAL_STAGE_2.CoverageRatio_Post = ((ss2_impairment_post / ss2_exposure_post) * 100).ToString(); } catch { COMMERCIAL_STAGE_2.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(COMMERCIAL_STAGE_2);


            var COMMERCIAL_STAGE_3 = new ReportBreakdown();
            COMMERCIAL_STAGE_3.Field1 = "COMMERCIAL_STAGE_3";
            var ss3_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 3).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_3.Exposure_Pre = string.Format("{0:N}", ss3_exposure_pre);
            var ss3_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 3).Sum(p => p.Impairment_ModelOutput);
            COMMERCIAL_STAGE_3.Impairment_Pre = string.Format("{0:N}", ss3_impairment_pre);
            try { COMMERCIAL_STAGE_3.CoverageRatio_Pre = ((ss3_impairment_pre / ss3_exposure_pre) * 100).ToString(); } catch { COMMERCIAL_STAGE_3.CoverageRatio_Pre = "0.00"; };

            var ss3_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 3).Sum(p => p.Outstanding_Balance);
            COMMERCIAL_STAGE_3.Exposure_Post = string.Format("{0:N}", ss3_exposure_post);
            var ss3_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "commercial" & o.Stage == 3).Sum(p => p.Overrides_Impairment_Manual);
            COMMERCIAL_STAGE_3.Impairment_Post = string.Format("{0:N}", ss3_impairment_post);
            try { COMMERCIAL_STAGE_3.CoverageRatio_Post = ((ss3_impairment_post / ss3_exposure_post) * 100).ToString(); } catch { COMMERCIAL_STAGE_3.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(COMMERCIAL_STAGE_3);


            var CORPORATE_STAGE_1 = new ReportBreakdown();
            CORPORATE_STAGE_1.Field1 = "CORPORATE_STAGE_1";
            var ss4_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 1).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_1.Exposure_Pre = string.Format("{0:N}", ss4_exposure_pre);
            var ss4_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 1).Sum(p => p.Impairment_ModelOutput);
            CORPORATE_STAGE_1.Impairment_Pre = string.Format("{0:N}", ss4_impairment_pre);
            try { CORPORATE_STAGE_1.CoverageRatio_Pre = ((ss4_impairment_pre / ss4_exposure_pre) * 100).ToString(); } catch { CORPORATE_STAGE_1.CoverageRatio_Pre = "0.00"; };

            var ss4_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 1).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_1.Exposure_Post = string.Format("{0:N}", ss4_exposure_post);
            var ss4_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 1).Sum(p => p.Overrides_Impairment_Manual);
            CORPORATE_STAGE_1.Impairment_Post = string.Format("{0:N}", ss4_impairment_post);
            try { CORPORATE_STAGE_1.CoverageRatio_Post = ((ss4_impairment_post / ss4_exposure_post) * 100).ToString(); } catch { CORPORATE_STAGE_1.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(CORPORATE_STAGE_1);

            var CORPORATE_STAGE_2 = new ReportBreakdown();
            CORPORATE_STAGE_2.Field1 = "CORPORATE_STAGE_2";
            var ss5_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 2).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_2.Exposure_Pre = string.Format("{0:N}", ss5_exposure_pre);
            var ss5_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 2).Sum(p => p.Impairment_ModelOutput);
            CORPORATE_STAGE_2.Impairment_Pre = string.Format("{0:N}", ss5_impairment_pre);
            try { CORPORATE_STAGE_2.CoverageRatio_Pre = ((ss5_impairment_pre / ss5_exposure_pre) * 100).ToString(); } catch { CORPORATE_STAGE_2.CoverageRatio_Pre = "0.00"; };

            var ss5_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 2).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_2.Exposure_Post = string.Format("{0:N}", ss5_exposure_post);
            var ss5_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 2).Sum(p => p.Overrides_Impairment_Manual);
            CORPORATE_STAGE_2.Impairment_Post = string.Format("{0:N}", ss5_impairment_post);
            try { CORPORATE_STAGE_2.CoverageRatio_Post = ((ss5_impairment_post / ss5_exposure_post) * 100).ToString(); } catch { CORPORATE_STAGE_2.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(CORPORATE_STAGE_2);


            var CORPORATE_STAGE_3 = new ReportBreakdown();
            CORPORATE_STAGE_3.Field1 = "CORPORATE_STAGE_3";
            var ss6_exposure_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 3).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_3.Exposure_Pre = string.Format("{0:N}", ss6_exposure_pre);
            var ss6_impairment_pre = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 3).Sum(p => p.Impairment_ModelOutput);
            CORPORATE_STAGE_3.Impairment_Pre = string.Format("{0:N}", ss6_impairment_pre);
            try { CORPORATE_STAGE_3.CoverageRatio_Pre = ((ss6_impairment_pre / ss6_exposure_pre) * 100).ToString(); } catch { CORPORATE_STAGE_3.CoverageRatio_Pre = "0.00"; };

            var ss6_exposure_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 3).Sum(p => p.Outstanding_Balance);
            CORPORATE_STAGE_3.Exposure_Post = string.Format("{0:N}", ss6_exposure_post);
            var ss6_impairment_post = rd.ResultDetailDataMore.Where(o => o.Segment.ToLower() == "corporate" & o.Stage == 3).Sum(p => p.Overrides_Impairment_Manual);
            CORPORATE_STAGE_3.Impairment_Post = string.Format("{0:N}", ss6_impairment_post);
            try { CORPORATE_STAGE_3.CoverageRatio_Post = ((ss6_impairment_post / ss6_exposure_post) * 100).ToString(); } catch { CORPORATE_STAGE_3.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(CORPORATE_STAGE_3);


            var sectorstageTotal = new ReportBreakdown();
            sectorstageTotal.Field1 = "Total";
            var ss7_exposure_pre = ss1_exposure_pre + ss2_exposure_pre + ss3_exposure_pre + ss4_exposure_pre + ss5_exposure_pre + ss6_exposure_pre;
            sectorstageTotal.Exposure_Pre = string.Format("{0:N}", ss7_exposure_pre);
            var ss7_impairment_pre = ss1_impairment_pre + ss2_impairment_pre + ss3_impairment_pre + ss4_impairment_pre + ss5_impairment_pre + ss6_impairment_pre;
            sectorstageTotal.Impairment_Pre = string.Format("{0:N}", ss7_impairment_pre);
            try { sectorstageTotal.CoverageRatio_Pre = ((ss7_impairment_pre / ss7_exposure_pre) * 100).ToString(); } catch { sectorstageTotal.CoverageRatio_Pre = "0.00"; };

            var ss7_exposure_post = ss1_exposure_post + ss2_exposure_post + ss3_exposure_post + ss4_exposure_post + ss5_exposure_post + ss6_exposure_post;
            sectorstageTotal.Exposure_Post = string.Format("{0:N}", ss7_exposure_post);
            var ss7_impairment_post = ss1_impairment_post + ss2_impairment_post + ss3_impairment_post + ss4_impairment_post + ss5_impairment_post + ss6_impairment_post;
            sectorstageTotal.Impairment_Post = string.Format("{0:N}", ss7_impairment_post);
            try { sectorstageTotal.CoverageRatio_Post = ((ss7_impairment_post / ss7_exposure_post) * 100).ToString(); } catch { sectorstageTotal.CoverageRatio_Post = "0.00"; };
            rs.SegmentAndStage.Add(sectorstageTotal);
            #endregion

            #region Top Exposure

            var topExposed = rd.ResultDetailDataMore.OrderByDescending(p => p.Outstanding_Balance).Take(10);

            rs.TopExposureSummary = new List<ReportBreakdown>();
            foreach(var itm in topExposed)
            {
                var rbd = new ReportBreakdown();
                rbd.Field1 = itm.ContractNo;
                rbd.Exposure_Pre = string.Format("{0:N}", itm.Outstanding_Balance);
                rbd.Impairment_Pre = string.Format("{0:N}", itm.Impairment_ModelOutput);
                rbd.CoverageRatio_Pre = ((itm.Impairment_ModelOutput/itm.Outstanding_Balance) * 100).ToString();
                rbd.Exposure_Post = string.Format("{0:N}", itm.Outstanding_Balance);
                rbd.Impairment_Post = string.Format("{0:N}", itm.Overrides_Impairment_Manual);
                rbd.CoverageRatio_Post = ((itm.Overrides_Impairment_Manual/itm.Outstanding_Balance) * 100).ToString();
                rs.TopExposureSummary.Add(rbd);
            }
            var rbdT = new ReportBreakdown();
            rbdT.Field1 = "Total";
            rbdT.Exposure_Pre = string.Format("{0:N}", topExposed.Sum(o=>o.Outstanding_Balance));
            rbdT.Impairment_Pre = string.Format("{0:N}", topExposed.Sum(o => o.Impairment_ModelOutput));
            rbdT.CoverageRatio_Pre = ((topExposed.Sum(o => o.Impairment_ModelOutput) / topExposed.Sum(o => o.Outstanding_Balance))*100).ToString();
            rbdT.Exposure_Post = string.Format("{0:N}", topExposed.Sum(o => o.Outstanding_Balance));
            rbdT.Impairment_Post = string.Format("{0:N}", topExposed.Sum(o => o.Overrides_Impairment_Manual));
            rbdT.CoverageRatio_Post = ((topExposed.Sum(o => o.Overrides_Impairment_Manual)  / topExposed.Sum(o => o.Outstanding_Balance)) * 100).ToString();
            rs.TopExposureSummary.Add(rbdT);



            #endregion

            #region Top Impairment

            var topImpairment = rd.ResultDetailDataMore.OrderByDescending(p => p.Impairment_ModelOutput).Take(10);

            rs.TopImpairmentSummary = new List<ReportBreakdown>();
            foreach (var itm in topImpairment)
            {
                var ibd = new ReportBreakdown();
                ibd.Field1 = itm.ContractNo;
                ibd.Exposure_Pre = string.Format("{0:N}", itm.Outstanding_Balance);
                ibd.Impairment_Pre = string.Format("{0:N}", itm.Impairment_ModelOutput);
                ibd.CoverageRatio_Pre = ((itm.Impairment_ModelOutput / itm.Outstanding_Balance) * 100).ToString();
                ibd.Exposure_Post = string.Format("{0:N}", itm.Outstanding_Balance);
                ibd.Impairment_Post = string.Format("{0:N}", itm.Overrides_Impairment_Manual);
                ibd.CoverageRatio_Post = ((itm.Overrides_Impairment_Manual / itm.Outstanding_Balance) * 100).ToString();
                rs.TopImpairmentSummary.Add(ibd);
            }
            var ibdT = new ReportBreakdown();
            ibdT.Field1 = "Total";
            ibdT.Exposure_Pre = string.Format("{0:N}", topImpairment.Sum(o => o.Outstanding_Balance));
            ibdT.Impairment_Pre = string.Format("{0:N}", topImpairment.Sum(o => o.Impairment_ModelOutput));
            ibdT.CoverageRatio_Pre = ((topImpairment.Sum(o => o.Impairment_ModelOutput) / topImpairment.Sum(o => o.Outstanding_Balance)) * 100).ToString();
            ibdT.Exposure_Post = string.Format("{0:N}", topImpairment.Sum(o => o.Outstanding_Balance));
            ibdT.Impairment_Post = string.Format("{0:N}", topImpairment.Sum(o => o.Overrides_Impairment_Manual));
            ibdT.CoverageRatio_Post = ((topImpairment.Sum(o => o.Overrides_Impairment_Manual) / topImpairment.Sum(o => o.Outstanding_Balance)) * 100).ToString();
            rs.TopImpairmentSummary.Add(ibdT);

            #endregion

            return rs;
        }
        private ResultDetail GetResultDetail(EclType eclType, Guid eclId)
        {
            var rd= new ResultDetail();
            var _eclId = eclId.ToString();
            var _eclType = eclType.ToString();
            var _eclTypeTable = eclType.ToString();

            var qry = $"select [Status] from {_eclType}Ecls where Id='{_eclId}'";
            var dt = DataAccess.i.GetData(qry);

            if (dt.Rows.Count > 0)
            {
                var eclStatus = int.Parse(dt.Rows[0][0].ToString());
                if(eclStatus==10)
                {
                    _eclTypeTable = $"IFRS9_DB_Archive.dbo.{_eclTypeTable}";
                }
            }
            
            qry = $"select " +
                $" NumberOfContracts=0, " +
                $"  SumOutStandingBalance=0," +
                $"   Pre_EclBestEstimate=0," +
                $"   Pre_Optimistic=0," +
                $"   Pre_Downturn=0," +
                
                $"   Post_EclBestEstimate=0," +
                $"   Post_Optimistic=0," +
                $"   Post_Downturn=0," +

                $"   try_convert(float, isnull((select UserInputValue from {_eclType}ReportUserInput where {_eclType}EclId = '{_eclId}' and UserInputKey = 2), 0)) UserInput_EclBE," +
                $"   try_convert(float, isnull((select UserInputValue from {_eclType}ReportUserInput where {_eclType}EclId = '{_eclId}' and UserInputKey = 3), 0)) UserInput_EclO," +
                $"   try_convert(float, isnull((select UserInputValue from {_eclType}ReportUserInput where {_eclType}EclId = '{_eclId}' and UserInputKey = 4), 0)) UserInput_EclD";

            dt=DataAccess.i.GetData(qry);

            var rde = new ReportDetailExtractor();
            var temp_header = DataAccess.i.ParseDataToObject(rde, dt.Rows[0]);

            var overrides_overlay = 0;

            qry = $"select f.Stage, f.FinalEclValue, f.Scenario, f.ContractId, fo.Stage StageOverride, fo.FinalEclValue FinalEclValueOverride, fo.Scenario ScenarioOverride, fo.ContractId ContractIOverride from {_eclTypeTable}ECLFrameworkFinal f left join {_eclTypeTable}ECLFrameworkFinalOverride fo on (f.contractId=fo.contractId and f.EclMonth=fo.EclMonth and f.Scenario=fo.Scenario) where f.{_eclType}EclId = '{_eclId}' and f.EclMonth=1";
            dt = DataAccess.i.GetData(qry);

            var lstTfer = new List<TempFinalEclResult>();

            foreach(DataRow dr in dt.Rows)
            {
                var tfer = new TempFinalEclResult();
                lstTfer.Add(DataAccess.i.ParseDataToObject(tfer, dr));
            }


            qry = $"select ContractId, [Value] from {_eclTypeTable}EadInputs where {_eclType}EclId='{_eclId}' and Months=1";
            dt = DataAccess.i.GetData(qry);

            var lstTWEI = new List<TempEadInput>();

            foreach (DataRow dr in dt.Rows)
            {
                var twei= new TempEadInput();
                lstTWEI.Add(DataAccess.i.ParseDataToObject(twei, dr));
            }

            rd.ResultDetailDataMore = new List<ResultDetailDataMore>();

            qry = $"select distinct ContractNo, AccountNo, CustomerNo, Segment, ProductType, Sector from {_eclTypeTable}EclDataLoanBooks where {_eclType}EclUploadId='{_eclId}'";
            dt = DataAccess.i.GetData(qry);


            foreach (DataRow dr in dt.Rows)
            {
                var rdd = new ResultDetailData();
                var itm = DataAccess.i.ParseDataToObject(rdd, dr);

                var _lstTfer = lstTfer.Where(o => o.ContractId == itm.ContractNo).ToList();

                var stage = 1;
                try { stage = _lstTfer.FirstOrDefault(o => o.Scenario == 1).Stage; } catch { }

                var BE_Value = 0.0;
                try { BE_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 1).FinalEclValue; } catch { }

                var O_Value = 0.0;
                try { O_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 2).FinalEclValue; } catch { }

                var D_Value = 0.0;
                try { D_Value = _lstTfer.FirstOrDefault(o => o.Scenario == 3).FinalEclValue; } catch { }

                var stage_Override = 1;
                try { stage_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 1).StageOverride; } catch { }

                var BE_Value_Override = 0.0;
                try { BE_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 1).FinalEclValueOverride; } catch { BE_Value_Override = BE_Value; }

                var O_Value_Override = 0.0;
                try { O_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 2).FinalEclValueOverride; } catch { O_Value_Override = O_Value; }

                var D_Value_Override = 0.0;
                try { D_Value_Override = _lstTfer.FirstOrDefault(o => o.ScenarioOverride == 3).FinalEclValueOverride; } catch { D_Value_Override = D_Value; }

                var outStandingBal = 0.0;
                try { outStandingBal = lstTWEI.FirstOrDefault(o => o.ContractId == itm.ContractNo).Value; } catch { }

                var rddm = new ResultDetailDataMore
                {
                    AccountNo = itm.AccountNo,
                    ContractNo = itm.ContractNo,
                    CustomerNo = itm.CustomerNo,
                    ProductType = itm.ProductType,
                    Sector = itm.Sector,
                    Stage = stage,
                    Overrides_Stage = stage_Override,
                    ECL_Best_Estimate = BE_Value,
                    ECL_Downturn = D_Value,
                    ECL_Optimistic = O_Value,
                    Overrides_ECL_Best_Estimate = BE_Value_Override * (1 + overrides_overlay),
                    Overrides_ECL_Downturn = D_Value_Override * (1 + overrides_overlay),
                    Overrides_ECL_Optimistic = O_Value_Override * (1 + overrides_overlay),
                    Segment = itm.Segment,
                    Overrides_FSV = 0,
                    Outstanding_Balance = outStandingBal,
                    Overrides_TTR_Years = 0,
                    Overrides_Overlay = 0,
                    Impairment_ModelOutput = 0,
                    Overrides_Impairment_Manual = 0
                };

                rddm.Impairment_ModelOutput = (rddm.ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rddm.ECL_Optimistic + temp_header.UserInput_EclO) + (rddm.ECL_Downturn * temp_header.UserInput_EclD);
                rddm.Overrides_Impairment_Manual = (rddm.Overrides_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rddm.Overrides_ECL_Optimistic + temp_header.UserInput_EclO) + (rddm.Overrides_ECL_Downturn * temp_header.UserInput_EclD);

                rd.ResultDetailDataMore.Add(rddm);
            }

            rd.NumberOfContracts = rd.ResultDetailDataMore.Count();
            rd.OutStandingBalance = rd.ResultDetailDataMore.Sum(o=>o.Outstanding_Balance);
            rd.Pre_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.ECL_Best_Estimate);
            rd.Pre_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.ECL_Optimistic);
            rd.Pre_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.ECL_Downturn);
            rd.Pre_Impairment_ModelOutput = (rd.Pre_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rd.Pre_ECL_Optimistic + temp_header.UserInput_EclO) + (rd.Pre_ECL_Downturn * temp_header.UserInput_EclD);

            rd.Post_ECL_Best_Estimate = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Best_Estimate);
            rd.Post_ECL_Optimistic = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Optimistic);
            rd.Post_ECL_Downturn = rd.ResultDetailDataMore.Sum(o => o.Overrides_ECL_Downturn);

            rd.Post_Impairment_ModelOutput = (rd.Pre_ECL_Best_Estimate * temp_header.UserInput_EclBE) + (rd.Pre_ECL_Optimistic + temp_header.UserInput_EclO) + (rd.Pre_ECL_Downturn * temp_header.UserInput_EclD);

            return rd;
        }
    }
}
