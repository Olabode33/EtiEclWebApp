using OfficeOpenXml;
using System;
using System.Text;

namespace TestDemo.EclShared.Importing.Utils
{
    public interface IValidationUtil
    {
        bool ValidateBooleanValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage);
        DateTime? ValidateDateTimeValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage);
        double? ValidateDoubleValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage);
        int? ValidateIntegerValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage);
        string GetRequiredValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage);
        string GetTextValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage);
        int GetIntegerValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage);
        double? GetDoubleValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage);
        DateTime? GetDateTimeValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage);
        int HackValidateIntegerValueFromRowOrNull(string value, string columnName, StringBuilder exceptionMessage);
    }
}