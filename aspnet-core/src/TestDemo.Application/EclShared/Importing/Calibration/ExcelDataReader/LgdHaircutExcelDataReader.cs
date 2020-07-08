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
    public class LgdHaircutExcelDataReader : EpPlusExcelImporterBase<ImportCalibrationLgdHaircutDto>, ILgdHaircutExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;
        private readonly IValidationUtil _validator;

        public LgdHaircutExcelDataReader(ILocalizationManager localizationManager,
            IValidationUtil validator)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _validator = validator;
        }

        public List<ImportCalibrationLgdHaircutDto> GetImportLgdHaircutFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportCalibrationLgdHaircutDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            //if (IsRowEmpty(worksheet, row))
            //{
            //    return null;
            //}

            var exceptionMessage = new StringBuilder();
            var data = new ImportCalibrationLgdHaircutDto();

            try
            {
                data.Customer_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 1, nameof(data.Customer_No), exceptionMessage);
                data.Account_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 2, nameof(data.Account_No), exceptionMessage);
                data.Contract_No = _validator.GetTextValueFromRowOrNull(worksheet, row, 3, nameof(data.Contract_No), exceptionMessage);
                data.Snapshot_Date = _validator.GetDateTimeValueFromRowOrNull(worksheet, row, 4, nameof(data.Snapshot_Date), exceptionMessage);
                data.Outstanding_Balance_Lcy = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 5, nameof(data.Outstanding_Balance_Lcy), exceptionMessage);
                data.Debenture_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 6, nameof(data.Debenture_OMV), exceptionMessage);
                data.Debenture_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 7, nameof(data.Debenture_FSV), exceptionMessage);
                data.Cash_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 8, nameof(data.Cash_OMV), exceptionMessage);
                data.Cash_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 9, nameof(data.Cash_FSV), exceptionMessage);
                data.Inventory_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 10, nameof(data.Inventory_OMV), exceptionMessage);
                data.Inventory_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 11, nameof(data.Inventory_FSV), exceptionMessage);
                data.Plant_And_Equipment_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 12, nameof(data.Plant_And_Equipment_OMV), exceptionMessage);
                data.Plant_And_Equipment_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 13, nameof(data.Plant_And_Equipment_FSV), exceptionMessage);
                data.Residential_Property_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 14, nameof(data.Residential_Property_OMV), exceptionMessage);
                data.Residential_Property_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 15, nameof(data.Residential_Property_FSV), exceptionMessage);
                data.Commercial_Property_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 16, nameof(data.Commercial_Property_OMV), exceptionMessage);
                data.Commercial_Property_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 17, nameof(data.Commercial_Property_FSV), exceptionMessage);
                data.Receivables_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 18, nameof(data.Receivables_OMV), exceptionMessage);
                data.Receivables_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 19, nameof(data.Receivables_FSV), exceptionMessage);
                data.Shares_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 20, nameof(data.Shares_OMV), exceptionMessage);
                data.Shares_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 21, nameof(data.Shares_FSV), exceptionMessage);
                data.Vehicle_OMV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 22, nameof(data.Vehicle_OMV), exceptionMessage);
                data.Vehicle_FSV = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 23, nameof(data.Vehicle_FSV), exceptionMessage);
                data.Guarantee_Value = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 24, nameof(data.Guarantee_Value), exceptionMessage);
                data.Period = _validator.GetIntegerValueFromRowOrNull(worksheet, row, 25, nameof(data.Period), exceptionMessage);
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
