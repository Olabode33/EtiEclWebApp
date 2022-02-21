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
    public class PdCommsConsExcelDataReader : EpPlusExcelImporterBase<ImportCalibrationPdCommsConsAsStringDto>, IPdCommsConsExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public PdCommsConsExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportCalibrationPdCommsConsAsStringDto> GetImportPdCommsConsFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportCalibrationPdCommsConsAsStringDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            //if (IsRowEmpty(worksheet, row))
            //{
            //    return null;
            //}

            var exceptionMessage = new StringBuilder();
            var data = new ImportCalibrationPdCommsConsAsStringDto();

            try
            {
                data.Customer_No = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(data.Customer_No), exceptionMessage);
                data.Account_No = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(data.Account_No), exceptionMessage);
                data.Contract_No = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(data.Contract_No), exceptionMessage);
                data.Product_Type = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(data.Product_Type), exceptionMessage);
                data.Current_Rating = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(data.Current_Rating), exceptionMessage);
                data.Days_Past_Due = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(data.Days_Past_Due), exceptionMessage);
                data.Classification = GetRequiredValueFromRowOrNull(worksheet, row, 7, nameof(data.Classification), exceptionMessage);
                data.Outstanding_Balance_Lcy = GetRequiredValueFromRowOrNull(worksheet, row, 8, nameof(data.Outstanding_Balance_Lcy), exceptionMessage);
                data.Contract_Start_Date = GetRequiredValueFromRowOrNull(worksheet, row, 9, nameof(data.Contract_Start_Date), exceptionMessage);
                data.Contract_End_Date = GetRequiredValueFromRowOrNull(worksheet, row, 10, nameof(data.Contract_End_Date), exceptionMessage);
                data.Snapshot_Date = GetRequiredValueFromRowOrNull(worksheet, row, 11, nameof(data.Snapshot_Date), exceptionMessage);
                data.Segment = GetRequiredValueFromRowOrNull(worksheet, row, 12, nameof(data.Segment), exceptionMessage);
                data.WI = GetRequiredValueFromRowOrNull(worksheet, row, 13, nameof(data.WI), exceptionMessage);
                data.Serial = row;// GetIntegerValueFromRowOrNull(worksheet, row, 13, nameof(data.Serial), exceptionMessage);
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

        private int GetIntegerValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            int returnValue;

            if (cellValue == null)
            {
                return 0;
            }
            else if (int.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return 0;
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