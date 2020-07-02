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
    }
}