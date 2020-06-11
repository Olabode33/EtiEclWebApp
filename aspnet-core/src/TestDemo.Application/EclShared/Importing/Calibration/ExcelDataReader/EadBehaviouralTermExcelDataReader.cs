using Abp.Localization;
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
    public class EadBehaviouralTermExcelDataReader : EpPlusExcelImporterBase<ImportCalibrationBehaviouralTermDto>, IEadBehaviouralTermExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public EadBehaviouralTermExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportCalibrationBehaviouralTermDto> GetImportBehaviouralTermFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportCalibrationBehaviouralTermDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var data = new ImportCalibrationBehaviouralTermDto();

            try
            {
                data.Customer_No = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(data.Customer_No), exceptionMessage);
                data.Account_No = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(data.Account_No), exceptionMessage);
                data.Contract_No = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(data.Contract_No), exceptionMessage);
                data.Customer_Name = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(data.Customer_Name), exceptionMessage);
                data.Snapshot_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 5, nameof(data.Snapshot_Date), exceptionMessage);
                data.Classification = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(data.Classification), exceptionMessage);
                data.Original_Balance_Lcy = GetDoubleValueFromRowOrNull(worksheet, row, 7, nameof(data.Original_Balance_Lcy), exceptionMessage);
                data.Outstanding_Balance_Lcy = GetDoubleValueFromRowOrNull(worksheet, row, 8, nameof(data.Outstanding_Balance_Lcy), exceptionMessage);
                data.Outstanding_Balance_Acy = GetDoubleValueFromRowOrNull(worksheet, row, 9, nameof(data.Outstanding_Balance_Acy), exceptionMessage);
                data.Contract_Start_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 10, nameof(data.Contract_Start_Date), exceptionMessage);
                data.Contract_End_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 11, nameof(data.Contract_End_Date), exceptionMessage);
                data.Restructure_Indicator = GetRequiredValueFromRowOrNull(worksheet, row, 12, nameof(data.Restructure_Indicator), exceptionMessage);
                data.Restructure_Type = GetRequiredValueFromRowOrNull(worksheet, row, 13, nameof(data.Restructure_Type), exceptionMessage);
                data.Restructure_Start_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 14, nameof(data.Restructure_Start_Date), exceptionMessage);
                data.Restructure_End_Date = GetDateTimeValueFromRowOrNull(worksheet, row, 15, nameof(data.Restructure_End_Date), exceptionMessage);
                data.Assumption_NonExpired = GetRequiredValueFromRowOrNull(worksheet, row, 16, nameof(data.Assumption_NonExpired), exceptionMessage);
                data.Freq_NonExpired = GetRequiredValueFromRowOrNull(worksheet, row, 17, nameof(data.Freq_NonExpired), exceptionMessage);
                data.Assumption_Expired = GetRequiredValueFromRowOrNull(worksheet, row, 18, nameof(data.Assumption_Expired), exceptionMessage);
                data.Freq_Expired = GetRequiredValueFromRowOrNull(worksheet, row, 19, nameof(data.Freq_Expired), exceptionMessage);
                data.Comment = GetRequiredValueFromRowOrNull(worksheet, row, 20, nameof(data.Comment), exceptionMessage);
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
