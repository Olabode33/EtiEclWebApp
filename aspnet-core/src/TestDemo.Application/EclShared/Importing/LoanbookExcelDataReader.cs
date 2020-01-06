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
    public class LoanbookExcelDataReader : EpPlusExcelImporterBase<ImportLoanbookDto>, ILoanbookExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public LoanbookExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportLoanbookDto> GetImportLoanbookFromExcel(byte[] fileBytes)
        {
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        private ImportLoanbookDto ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var loanbook = new ImportLoanbookDto();

            try
            {
                loanbook.CustomerNo = GetRequiredValueFromRowOrNull(worksheet, row, 1, nameof(loanbook.CustomerNo), exceptionMessage);
                loanbook.AccountNo = GetRequiredValueFromRowOrNull(worksheet, row, 2, nameof(loanbook.AccountNo), exceptionMessage);
                loanbook.ContractNo = GetRequiredValueFromRowOrNull(worksheet, row, 3, nameof(loanbook.ContractNo), exceptionMessage);
                loanbook.CustomerName = GetRequiredValueFromRowOrNull(worksheet, row, 4, nameof(loanbook.CustomerName), exceptionMessage);
                loanbook.SnapshotDate = GetDateTimeValueFromRowOrNull(worksheet, row, 5, nameof(loanbook.SnapshotDate), exceptionMessage);
                loanbook.Segment = GetRequiredValueFromRowOrNull(worksheet, row, 6, nameof(loanbook.Segment), exceptionMessage);
                loanbook.Sector = GetRequiredValueFromRowOrNull(worksheet, row, 7, nameof(loanbook.Sector), exceptionMessage);
                loanbook.Currency = GetRequiredValueFromRowOrNull(worksheet, row, 8, nameof(loanbook.Currency), exceptionMessage);
                loanbook.ProductType = GetRequiredValueFromRowOrNull(worksheet, row, 9, nameof(loanbook.ProductType), exceptionMessage);
                loanbook.ProductMapping = GetRequiredValueFromRowOrNull(worksheet, row, 10, nameof(loanbook.ProductMapping), exceptionMessage);
                loanbook.SpecialisedLending = GetRequiredValueFromRowOrNull(worksheet, row, 11, nameof(loanbook.SpecialisedLending), exceptionMessage);
                loanbook.RatingModel = GetRequiredValueFromRowOrNull(worksheet, row, 12, nameof(loanbook.RatingModel), exceptionMessage);
                loanbook.OriginalRating = GetIntegerValueFromRowOrNull(worksheet, row, 13, nameof(loanbook.OriginalRating), exceptionMessage);
                loanbook.CurrentRating = GetIntegerValueFromRowOrNull(worksheet, row, 14, nameof(loanbook.CurrentRating), exceptionMessage);
                loanbook.LifetimePD = GetDoubleValueFromRowOrNull(worksheet, row, 15, nameof(loanbook.LifetimePD), exceptionMessage);
                loanbook.Month12PD = GetDoubleValueFromRowOrNull(worksheet, row, 16, nameof(loanbook.Month12PD), exceptionMessage);
                loanbook.DaysPastDue = GetIntegerValueFromRowOrNull(worksheet, row, 17, nameof(loanbook.DaysPastDue), exceptionMessage);
                loanbook.WatchlistIndicator = GetBooleanValueFromRowOrNull(worksheet, row, 18, nameof(loanbook.WatchlistIndicator), exceptionMessage);
                loanbook.Classification = GetRequiredValueFromRowOrNull(worksheet, row, 19, nameof(loanbook.Classification), exceptionMessage);
                loanbook.ImpairedDate = GetDateTimeValueFromRowOrNull(worksheet, row, 20, nameof(loanbook.ImpairedDate), exceptionMessage);
                loanbook.DefaultDate = GetDateTimeValueFromRowOrNull(worksheet, row, 21, nameof(loanbook.DefaultDate), exceptionMessage);
                loanbook.CreditLimit = GetDoubleValueFromRowOrNull(worksheet, row, 22, nameof(loanbook.CreditLimit), exceptionMessage);
                loanbook.OriginalBalanceLCY = GetDoubleValueFromRowOrNull(worksheet, row, 23, nameof(loanbook.OriginalBalanceLCY), exceptionMessage);
                loanbook.OutstandingBalanceLCY = GetDoubleValueFromRowOrNull(worksheet, row, 24, nameof(loanbook.OutstandingBalanceLCY), exceptionMessage);
                loanbook.OutstandingBalanceACY = GetDoubleValueFromRowOrNull(worksheet, row, 25, nameof(loanbook.OutstandingBalanceACY), exceptionMessage);
                loanbook.ContractStartDate = GetDateTimeValueFromRowOrNull(worksheet, row, 26, nameof(loanbook.ContractStartDate), exceptionMessage);
                loanbook.ContractEndDate = GetDateTimeValueFromRowOrNull(worksheet, row, 27, nameof(loanbook.ContractEndDate), exceptionMessage);
                loanbook.RestructureIndicator = GetBooleanValueFromRowOrNull(worksheet, row, 28, nameof(loanbook.RestructureIndicator), exceptionMessage);
                loanbook.RestructureRisk = GetRequiredValueFromRowOrNull(worksheet, row, 29, nameof(loanbook.RestructureRisk), exceptionMessage);
                loanbook.RestructureType = GetRequiredValueFromRowOrNull(worksheet, row, 30, nameof(loanbook.RestructureType), exceptionMessage);
                loanbook.RestructureStartDate = GetDateTimeValueFromRowOrNull(worksheet, row, 31, nameof(loanbook.RestructureStartDate), exceptionMessage);
                loanbook.RestructureEndDate = GetDateTimeValueFromRowOrNull(worksheet, row, 32, nameof(loanbook.RestructureEndDate), exceptionMessage);
                loanbook.PrincipalPaymentTermsOrigination = GetRequiredValueFromRowOrNull(worksheet, row, 33, nameof(loanbook.PrincipalPaymentTermsOrigination), exceptionMessage);
                loanbook.PPTOPeriod = GetIntegerValueFromRowOrNull(worksheet, row, 34, nameof(loanbook.PPTOPeriod), exceptionMessage);
                loanbook.InterestPaymentTermsOrigination = GetRequiredValueFromRowOrNull(worksheet, row, 35, nameof(loanbook.InterestPaymentTermsOrigination), exceptionMessage);
                loanbook.IPTOPeriod = GetIntegerValueFromRowOrNull(worksheet, row, 36, nameof(loanbook.IPTOPeriod), exceptionMessage);
                loanbook.PrincipalPaymentStructure = GetRequiredValueFromRowOrNull(worksheet, row, 37, nameof(loanbook.PrincipalPaymentStructure), exceptionMessage);
                loanbook.InterestPaymentStructure = GetRequiredValueFromRowOrNull(worksheet, row, 38, nameof(loanbook.InterestPaymentStructure), exceptionMessage);
                loanbook.InterestRateType = GetRequiredValueFromRowOrNull(worksheet, row, 39, nameof(loanbook.InterestRateType), exceptionMessage);
                loanbook.BaseRate = GetRequiredValueFromRowOrNull(worksheet, row, 40, nameof(loanbook.BaseRate), exceptionMessage);
                loanbook.OriginationContractualInterestRate = GetRequiredValueFromRowOrNull(worksheet, row, 41, nameof(loanbook.OriginationContractualInterestRate), exceptionMessage);
                loanbook.IntroductoryPeriod = GetIntegerValueFromRowOrNull(worksheet, row, 42, nameof(loanbook.IntroductoryPeriod), exceptionMessage);
                loanbook.PostIPContractualInterestRate = GetDoubleValueFromRowOrNull(worksheet, row, 43, nameof(loanbook.PostIPContractualInterestRate), exceptionMessage);
                loanbook.CurrentContractualInterestRate = GetDoubleValueFromRowOrNull(worksheet, row, 44, nameof(loanbook.CurrentContractualInterestRate), exceptionMessage);
                loanbook.EIR = GetDoubleValueFromRowOrNull(worksheet, row, 45, nameof(loanbook.EIR), exceptionMessage);
                loanbook.DebentureOMV = GetDoubleValueFromRowOrNull(worksheet, row, 46, nameof(loanbook.DebentureOMV), exceptionMessage);
                loanbook.DebentureFSV = GetDoubleValueFromRowOrNull(worksheet, row, 47, nameof(loanbook.DebentureFSV), exceptionMessage);
                loanbook.CashOMV = GetDoubleValueFromRowOrNull(worksheet, row, 48, nameof(loanbook.CashOMV), exceptionMessage);
                loanbook.CashFSV = GetDoubleValueFromRowOrNull(worksheet, row, 49, nameof(loanbook.CashFSV), exceptionMessage);
                loanbook.InventoryOMV = GetDoubleValueFromRowOrNull(worksheet, row, 50, nameof(loanbook.InventoryOMV), exceptionMessage);
                loanbook.InventoryFSV = GetDoubleValueFromRowOrNull(worksheet, row, 51, nameof(loanbook.InventoryFSV), exceptionMessage);
                loanbook.PlantEquipmentOMV = GetDoubleValueFromRowOrNull(worksheet, row, 52, nameof(loanbook.PlantEquipmentOMV), exceptionMessage);
                loanbook.PlantEquipmentFSV = GetDoubleValueFromRowOrNull(worksheet, row, 53, nameof(loanbook.PlantEquipmentFSV), exceptionMessage);
                loanbook.ResidentialPropertyOMV = GetDoubleValueFromRowOrNull(worksheet, row, 54, nameof(loanbook.ResidentialPropertyOMV), exceptionMessage);
                loanbook.ResidentialPropertyFSV = GetDoubleValueFromRowOrNull(worksheet, row, 55, nameof(loanbook.ResidentialPropertyFSV), exceptionMessage);
                loanbook.CommercialPropertyOMV = GetDoubleValueFromRowOrNull(worksheet, row, 56, nameof(loanbook.CommercialPropertyOMV), exceptionMessage);
                loanbook.CommercialPropertyFSV = GetDoubleValueFromRowOrNull(worksheet, row, 57, nameof(loanbook.CommercialPropertyFSV), exceptionMessage);
                loanbook.ReceivablesOMV = GetDoubleValueFromRowOrNull(worksheet, row, 58, nameof(loanbook.ReceivablesOMV), exceptionMessage);
                loanbook.ReceivablesFSV = GetDoubleValueFromRowOrNull(worksheet, row, 59, nameof(loanbook.ReceivablesFSV), exceptionMessage);
                loanbook.SharesOMV = GetDoubleValueFromRowOrNull(worksheet, row, 60, nameof(loanbook.SharesOMV), exceptionMessage);
                loanbook.SharesFSV = GetDoubleValueFromRowOrNull(worksheet, row, 61, nameof(loanbook.SharesFSV), exceptionMessage);
                loanbook.VehicleOMV = GetDoubleValueFromRowOrNull(worksheet, row, 62, nameof(loanbook.VehicleOMV), exceptionMessage);
                loanbook.VehicleFSV = GetDoubleValueFromRowOrNull(worksheet, row, 63, nameof(loanbook.VehicleFSV), exceptionMessage);
                loanbook.CureRate = GetDoubleValueFromRowOrNull(worksheet, row, 64, nameof(loanbook.CureRate), exceptionMessage);
                loanbook.GuaranteeIndicator = GetBooleanValueFromRowOrNull(worksheet, row, 65, nameof(loanbook.GuaranteeIndicator), exceptionMessage);
                loanbook.GuarantorPD = GetRequiredValueFromRowOrNull(worksheet, row, 66, nameof(loanbook.GuarantorPD), exceptionMessage);
                loanbook.GuarantorLGD = GetRequiredValueFromRowOrNull(worksheet, row, 67, nameof(loanbook.GuarantorLGD), exceptionMessage);
                loanbook.GuaranteeValue = GetDoubleValueFromRowOrNull(worksheet, row, 68, nameof(loanbook.GuaranteeValue), exceptionMessage);
                loanbook.GuaranteeLevel = GetDoubleValueFromRowOrNull(worksheet, row, 69, nameof(loanbook.GuaranteeLevel), exceptionMessage);
            }
            catch (Exception exception)
            {
                loanbook.Exception = exception.Message;
            }

            return loanbook;
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
