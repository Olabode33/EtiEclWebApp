﻿using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public class EadCcfSummaryExcelDataReader : EpPlusExcelImporterBase<ImportCalibrationCcfSummaryDto>, IEadCcfSummaryExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public EadCcfSummaryExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportCalibrationCcfSummaryDto> GetImportCcfSummaryFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportCalibrationCcfSummaryDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var data = new ImportCalibrationCcfSummaryDto();

            try
            {
                data.Customer_No = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(data.Customer_No), exceptionMessage);
                data.Account_No = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(data.Account_No), exceptionMessage);
                data.Product_Type = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(data.Product_Type), exceptionMessage);
                data.Settlement_Account = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(data.Settlement_Account), exceptionMessage);
                data.Snapshot_Date = GetIntegerValueFromRowOrNull(worksheet, row, 5, nameof(data.Snapshot_Date), exceptionMessage);
                data.Contract_Start_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 6, nameof(data.Contract_Start_Date), exceptionMessage);
                data.Contract_End_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 7, nameof(data.Contract_End_Date), exceptionMessage);
                data.Limit = GetIntegerValueFromRowOrNull(worksheet, row, 8, nameof(data.Limit), exceptionMessage);
                data.Outstanding_Balance = GetDoubleValueFromRowOrNull(worksheet, row, 9, nameof(data.Outstanding_Balance), exceptionMessage);
                data.Classification = GetRequiredValueFromRowOrNull(worksheet, row, 10, nameof(data.Classification), exceptionMessage);
            }
            catch (Exception exception)
            {
                data.Exception = exception.Message;
            }

            return data;
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