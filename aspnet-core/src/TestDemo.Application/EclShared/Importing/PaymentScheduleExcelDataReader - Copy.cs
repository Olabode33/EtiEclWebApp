using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public class PaymentScheduleExcelDataReader_ : EpPlusExcelImporterBase<ImportPaymentScheduleDto>//, IPaymentScheduleExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public PaymentScheduleExcelDataReader_(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportPaymentScheduleDto> GetImportPaymentScheduleFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportPaymentScheduleDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var paymentSchedule = new ImportPaymentScheduleDto();

            try
            {
                paymentSchedule.ContractRefNo = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(paymentSchedule.ContractRefNo), exceptionMessage);
                paymentSchedule.StartDate = GetDateTimeValueFromRowOrNull(worksheet, row, 2, nameof(paymentSchedule.StartDate), exceptionMessage);
                paymentSchedule.Component = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(paymentSchedule.Component), exceptionMessage);
                paymentSchedule.NoOfSchedules = GetIntegerValueFromRowOrNull(worksheet, row, 4, nameof(paymentSchedule.NoOfSchedules), exceptionMessage);
                paymentSchedule.Frequency = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(paymentSchedule.Frequency), exceptionMessage);
                paymentSchedule.Amount = GetDoubleValueFromRowOrNull(worksheet, row, 6, nameof(paymentSchedule.Amount), exceptionMessage);
            }
            catch (Exception exception)
            {
                paymentSchedule.Exception = exception.Message;
            }

            return paymentSchedule;
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
