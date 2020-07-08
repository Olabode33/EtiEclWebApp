using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.EclShared.Importing.Utils;

namespace TestDemo.EclShared.Importing
{
    public class LgdRecoveryRateExcelDataReader : EpPlusExcelImporterBase<ImportCalibrationLgdRecoveryRateDto>, ILgdRecoveryRateExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;
        private readonly IValidationUtil _validator;

        public LgdRecoveryRateExcelDataReader(ILocalizationManager localizationManager,
            IValidationUtil validator)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _validator = validator;
        }

        public List<ImportCalibrationLgdRecoveryRateDto> GetImportLgdRecoveryRateFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportCalibrationLgdRecoveryRateDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            //if (IsRowEmpty(worksheet, row))
            //{
            //    return null;
            //}

            var exceptionMessage = new StringBuilder();
            var data = new ImportCalibrationLgdRecoveryRateDto();

            try
            {
                data.Customer_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 1, nameof(data.Customer_No), exceptionMessage);
                data.Account_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 2, nameof(data.Account_No), exceptionMessage);
                data.Account_Name = _validator.GetTextValueFromRowOrNull(worksheet, row, 3, nameof(data.Account_Name), exceptionMessage);
                data.Contract_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 4, nameof(data.Contract_No), exceptionMessage);
                data.Segment = _validator.GetTextValueFromRowOrNull(worksheet, row, 5, nameof(data.Segment), exceptionMessage);
                data.Product_Type = _validator.GetTextValueFromRowOrNull(worksheet, row, 6, nameof(data.Product_Type), exceptionMessage);
                data.Days_Past_Due = _validator.GetIntegerValueFromRowOrNull(worksheet, row, 7, nameof(data.Days_Past_Due), exceptionMessage);
                data.Classification = _validator.GetTextValueFromRowOrNull(worksheet, row, 8, nameof(data.Classification), exceptionMessage);
                data.Default_Date = _validator.GetDateTimeValueFromRowOrNull(worksheet, row, 9, nameof(data.Default_Date), exceptionMessage);
                data.Outstanding_Balance_Lcy = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 10, nameof(data.Outstanding_Balance_Lcy), exceptionMessage);
                data.Contractual_Interest_Rate = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 11, nameof(data.Contractual_Interest_Rate), exceptionMessage);
                data.Amount_Recovered = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 12, nameof(data.Amount_Recovered), exceptionMessage);
                data.Date_Of_Recovery = _validator.GetDateTimeValueFromRowOrNull(worksheet, row, 13, nameof(data.Date_Of_Recovery), exceptionMessage);
                data.Type_Of_Recovery = _validator.GetTextValueFromRowOrNull(worksheet, row, 14, nameof(data.Type_Of_Recovery), exceptionMessage);

            }
            catch (Exception exception)
            {
                data.Exception = exception.Message;
            }

            if (exceptionMessage.Length > 0)
            {
                data.Exception = exceptionMessage.ToString();
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
