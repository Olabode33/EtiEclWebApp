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
    public class NonInternalModelDataExcelDataReader : EpPlusExcelImporterBase<ImportNonInternalModelDataDto>, INonInternalModelDataExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public NonInternalModelDataExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportNonInternalModelDataDto> GetImportDataFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        public List<ImportNonInternalModelDataDto> ProcessExcelFile(byte[] fileBytes, Func<ExcelWorksheet, int, List<ImportNonInternalModelDataDto>> processExcelRow)
        {
            var entities = new List<ImportNonInternalModelDataDto>();

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

        private List<ImportNonInternalModelDataDto> ProcessWorksheet(ExcelWorksheet worksheet, Func<ExcelWorksheet, int, List<ImportNonInternalModelDataDto>> processExcelRow)
        {
            var entities = new List<ImportNonInternalModelDataDto>();

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

        private List<ImportNonInternalModelDataDto> ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var list = new List<ImportNonInternalModelDataDto>();

            for (int i = 2; i <= 5; i++)
            {
                var data = new ImportNonInternalModelDataDto();
                var pdGroup = "";

                switch (i)
                {
                    case 2:
                        pdGroup = "CONS_STAGE_1";
                        break;
                    case 3:
                        pdGroup = "CONS_STAGE_2";
                        break;
                    case 4:
                        pdGroup = "COMM_STAGE_1";
                        break;
                    case 5:
                        pdGroup = "COMM_STAGE_2";
                        break;
                    default:
                        break;
                }

                try
                {
                    data.Month = GetIntegerValueFromRowOrNull(worksheet, row, 1, nameof(data.Month), exceptionMessage);
                    data.PdGroup = pdGroup;
                    data.MarginalDefaultRate = GetDoubleValueFromRowOrNull(worksheet, row, i, (data.PdGroup + "; Month: " + data.Month), exceptionMessage);
                }
                catch (Exception exception)
                {
                    data.Exception = exception.Message;
                }
                list.Add(data);
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
