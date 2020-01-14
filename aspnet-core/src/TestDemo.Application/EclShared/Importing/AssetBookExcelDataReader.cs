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
    public class AssetBookExcelDataReader : EpPlusExcelImporterBase<ImportAssetBookDto>, IAssetBookExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public AssetBookExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportAssetBookDto> GetImportAssetBookFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportAssetBookDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var assetBook = new ImportAssetBookDto();

            try
            {
                assetBook.AssetDescription = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(assetBook.AssetDescription), exceptionMessage);
                assetBook.AssetType = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(assetBook.AssetType), exceptionMessage);
                assetBook.CounterParty = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(assetBook.CounterParty), exceptionMessage);
                assetBook.SovereignDebt = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(assetBook.SovereignDebt), exceptionMessage);
                assetBook.RatingAgency = GetRequiredValueFromRowOrNull(worksheet, row, 5, nameof(assetBook.RatingAgency), exceptionMessage);
                assetBook.CreditRatingAtPurchaseDate = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(assetBook.CreditRatingAtPurchaseDate), exceptionMessage);
                assetBook.CurrentCreditRating = GetRequiredValueFromRowOrNull(worksheet, row, 7, nameof(assetBook.CurrentCreditRating), exceptionMessage);
                assetBook.NominalAmount = GetDoubleValueFromRowOrNull(worksheet, row, 8, nameof(assetBook.NominalAmount), exceptionMessage);
                assetBook.PrincipalAmortisation = GetRequiredValueFromRowOrNull(worksheet, row, 9, nameof(assetBook.PrincipalAmortisation), exceptionMessage);
                assetBook.RepaymentTerms = GetRequiredValueFromRowOrNull(worksheet, row, 10, nameof(assetBook.RepaymentTerms), exceptionMessage);
                assetBook.CarryAmountNGAAP = GetDoubleValueFromRowOrNull(worksheet, row, 11, nameof(assetBook.CarryAmountNGAAP), exceptionMessage);
                assetBook.CarryingAmountIFRS = GetDoubleValueFromRowOrNull(worksheet, row, 12, nameof(assetBook.CarryingAmountIFRS), exceptionMessage);
                assetBook.Coupon = GetIntegerValueFromRowOrNull(worksheet, row, 13, nameof(assetBook.Coupon), exceptionMessage);
                assetBook.Eir = GetIntegerValueFromRowOrNull(worksheet, row, 14, nameof(assetBook.Eir), exceptionMessage);
                assetBook.PurchaseDate = GetDateTimeValueFromRowOrNull(worksheet, row, 15, nameof(assetBook.PurchaseDate), exceptionMessage);
                assetBook.IssueDate = GetDateTimeValueFromRowOrNull(worksheet, row, 16, nameof(assetBook.IssueDate), exceptionMessage);
                assetBook.PurchasePrice = GetIntegerValueFromRowOrNull(worksheet, row, 17, nameof(assetBook.PurchasePrice), exceptionMessage);
                assetBook.MaturityDate = GetDateTimeValueFromRowOrNull(worksheet, row, 18, nameof(assetBook.MaturityDate), exceptionMessage);
                assetBook.RedemptionPrice = GetDoubleValueFromRowOrNull(worksheet, row, 19, nameof(assetBook.RedemptionPrice), exceptionMessage);
                assetBook.BusinessModelClassification = GetRequiredValueFromRowOrNull(worksheet, row, 20, nameof(assetBook.BusinessModelClassification), exceptionMessage);
                assetBook.Ias39Impairment = GetDoubleValueFromRowOrNull(worksheet, row, 21, nameof(assetBook.Ias39Impairment), exceptionMessage);
                assetBook.PrudentialClassification = GetRequiredValueFromRowOrNull(worksheet, row, 22, nameof(assetBook.PrudentialClassification), exceptionMessage);
                assetBook.ForebearanceFlag = GetRequiredValueFromRowOrNull(worksheet, row, 23, nameof(assetBook.ForebearanceFlag), exceptionMessage);
            }
            catch (Exception exception)
            {
                assetBook.Exception = exception.Message;
            }

            return assetBook;
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

        private bool GetBooleanValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            bool returnValue;

            if (bool.TryParse(cellValue.ToString(), out returnValue))
            {
                return returnValue;
            }

            exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            return false;
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
