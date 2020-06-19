using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Localization.Sources;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Assumptions.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public class MacroProjectionDataExcelDataReader : EpPlusExcelImporterBase<ImportMacroProjectionDataDto>, IMacroProjectionDataExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;
        private List<NameValueDto<int>> _affiliateMacroVariable;

        public MacroProjectionDataExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _affiliateMacroVariable = new List<NameValueDto<int>>();
        }

        public List<ImportMacroProjectionDataDto> GetImportMacroProjectionDataFromExcel(byte[] fileBytes, List<NameValueDto<int>> affiliateMacroVariables)
        {
            _affiliateMacroVariable = affiliateMacroVariables;
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        public List<ImportMacroProjectionDataDto> ProcessExcelFile(byte[] fileBytes, Func<ExcelWorksheet, int, List<ImportMacroProjectionDataDto>> processExcelRow)
        {
            var entities = new List<ImportMacroProjectionDataDto>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        var entitiesInWorksheet = ProcessWorksheet(worksheet, processExcelRow);

                        entities.AddRange(entitiesInWorksheet);
                    }
                }
            }

            return entities;
        }

        private List<ImportMacroProjectionDataDto> ProcessWorksheet(ExcelWorksheet worksheet, Func<ExcelWorksheet, int, List<ImportMacroProjectionDataDto>> processExcelRow)
        {
            var entities = new List<ImportMacroProjectionDataDto>();

            for (var i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                try
                {
                    var entity = processExcelRow(worksheet, i);

                    if (entity != null)
                    {
                        entities.AddRange(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }

        private List<ImportMacroProjectionDataDto> ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var list = new List<ImportMacroProjectionDataDto>();

            if (_affiliateMacroVariable.Count > 0)
            {
                int column = 1;
                //Read Macroeconomic values
                for (int i = 0; i < _affiliateMacroVariable.Count; i++)
                {
                    var data = new ImportMacroProjectionDataDto();
                    try
                    {
                        data.Date = GetDateTimeValueFromRowOrNull(worksheet, row, 1, nameof(data.Date), exceptionMessage);
                        data.InputName = _affiliateMacroVariable[i].Name;
                        data.BestValue = GetDoubleValueFromRowOrNull(worksheet, row, column + 1, _affiliateMacroVariable[i].Name + " (Best)", exceptionMessage);
                        data.OptimisticValue = GetDoubleValueFromRowOrNull(worksheet, row, column + 2, _affiliateMacroVariable[i].Name + " (Optimistic)", exceptionMessage);
                        data.DownturnValue = GetDoubleValueFromRowOrNull(worksheet, row, column + 3, _affiliateMacroVariable[i].Name + " (Downturn)", exceptionMessage);
                        data.MacroeconomicVariableId = _affiliateMacroVariable[i].Value;
                        column += 3;
                    }
                    catch (Exception exception)
                    {
                        data.Exception = exception.Message;
                    }
                    list.Add(data);
                }
            }
            else
            {
                return null;
            }

            return list;
        }

        private string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private int? GetIntegerValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            int returnValue;

            if (cellValue == null)
            {
                return null;
            }
            else if (int.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private double? GetDoubleValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            double returnValue;

            if (cellValue == null)
            {
                return null;
            }
            else if (double.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private DateTime? GetDateTimeValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            DateTime returnValue;

            if (cellValue == null)
            {
                return null;
            }
            else if (DateTime.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return null;
        }

        private string GetLocalizedExceptionMessagePart(string parameter)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + "; ";
        }


        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            return worksheet.Cells[row, 1].Value == null || string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value.ToString());
        }
    }
}
