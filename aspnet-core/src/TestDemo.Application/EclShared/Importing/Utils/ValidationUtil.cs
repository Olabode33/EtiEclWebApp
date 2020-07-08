using Abp.Dependency;
using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestDemo.EclShared.Importing.Utils
{
    public class ValidationUtil : ITransientDependency, IValidationUtil
    {
        private readonly ILocalizationSource _localizationSource;

        public ValidationUtil(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public int? ValidateIntegerValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            int returnValue;
            double doubleValue;

            if (string.IsNullOrWhiteSpace(value) || string.Equals(value.Trim(), "-"))
            {
                return null;
            }

            if (int.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            if (double.TryParse(value, out doubleValue))
            {
                return Convert.ToInt32(doubleValue);
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingWholeNumber"));
            return null;
        }

        public double? ValidateDoubleValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            double returnValue;
            if (string.IsNullOrWhiteSpace(value) || string.Equals(value.Trim(), "-"))
            {
                return null;
            }
            else if (double.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingNumber"));
            return null;
        }

        public DateTime? ValidateDateTimeValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            DateTime returnValue;
            int dateSerial;

            if (string.IsNullOrWhiteSpace(value) || string.Equals(value.Trim(), "-"))
            {
                return null;
            }

            if (int.TryParse(value, out dateSerial))
            {
                return DateTime.FromOADate(dateSerial);
            }

            if (DateTime.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            if (DateTime.TryParseExact(value, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingDateTime"));
            return null;
        }

        public bool ValidateBooleanValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage)
        {
            bool returnValue;
            if (string.IsNullOrWhiteSpace(value) || string.Equals(value, "0") || string.Equals(value.Trim(), "-"))
            {
                return false;
            }

            if (string.Equals(value, "1"))
            {
                return true;
            }

            if (bool.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "Expecting1or0"));
            return false;
        }

        public string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "Required"));
            return null;
        }

        public string GetTextValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }

            return null;
        }

        public int? GetIntegerValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            int returnValue;
            double doubleValue;

            if (cellValue == null)
            {
                return null;
            }
            else if (int.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            if (double.TryParse(cellValue.ToString(), out doubleValue))
            {
                return Convert.ToInt32(doubleValue);
            }


            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingWholeNumber"));
            return null;
        }

        public double? GetDoubleValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
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

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingNumber"));
            return null;
        }

        public DateTime? GetDateTimeValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
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

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName, "ExpectingDateTime"));
            return null;
        }

        private string GetLocalizedExceptionMessagePart(string parameter, string required)
        {
            return _localizationSource.GetString("{0}IsInvalid", parameter) + " " + _localizationSource.GetString(required) + "; ";
        }
    }
}
