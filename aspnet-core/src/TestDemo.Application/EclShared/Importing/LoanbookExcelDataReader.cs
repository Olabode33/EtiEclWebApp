using Abp.Localization;
using Abp.Localization.Sources;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public class LoanbookExcelDataReader : EpPlusExcelImporterBase<ImportLoanbookDtoNew>, ILoanbookExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;

        public LoanbookExcelDataReader(ILocalizationManager localizationManager)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public List<ImportLoanbookDtoNew> GetImportLoanbookFromExcel(byte[] fileBytes)
        {
            var loanbook = ProcessExcelFile(fileBytes, ProcessExcelRow);

            var duplicateContract = loanbook.GroupBy(e => e.ContractNo)
                                      .Where(g => g.Count() > 1)
                                      .Select(e => e.Key)
                                      .ToList();

            if (duplicateContract.Count > 0)
            {
                loanbook.ForEach(x =>
                {
                    if (duplicateContract.Contains(x.ContractNo))
                    {
                        x.Exception = _localizationSource.GetString("DuplicateContractError");
                    }
                    
                });
            }


            return loanbook;
        }

        private ImportLoanbookDtoNew ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var loanbook = new ImportLoanbookDtoNew();

            try
            {
                loanbook.CustomerNo = GetStringValueFromRowOrNull(worksheet, row, 1, nameof(loanbook.CustomerNo), exceptionMessage);
                loanbook.AccountNo = GetStringValueFromRowOrNull(worksheet, row, 2, nameof(loanbook.AccountNo), exceptionMessage);
                loanbook.ContractNo = GetStringValueFromRowOrNull(worksheet, row, 3, nameof(loanbook.ContractNo), exceptionMessage);
                loanbook.CustomerName = GetStringValueFromRowOrNull(worksheet, row, 4, nameof(loanbook.CustomerName), exceptionMessage);
                loanbook.SnapshotDate = GetStringValueFromRowOrNull(worksheet, row, 5, nameof(loanbook.SnapshotDate), exceptionMessage);
                loanbook.Segment = GetStringValueFromRowOrNull(worksheet, row, 6, nameof(loanbook.Segment), exceptionMessage);
                loanbook.Sector = GetStringValueFromRowOrNull(worksheet, row, 7, nameof(loanbook.Sector), exceptionMessage);
                loanbook.Currency = GetStringValueFromRowOrNull(worksheet, row, 8, nameof(loanbook.Currency), exceptionMessage);
                loanbook.ProductType = GetStringValueFromRowOrNull(worksheet, row, 9, nameof(loanbook.ProductType), exceptionMessage);
                loanbook.ProductMapping = GetStringValueFromRowOrNull(worksheet, row, 10, nameof(loanbook.ProductMapping), exceptionMessage);
                loanbook.SpecialisedLending = GetStringValueFromRowOrNull(worksheet, row, 11, nameof(loanbook.SpecialisedLending), exceptionMessage);
                loanbook.RatingModel = GetStringValueFromRowOrNull(worksheet, row, 12, nameof(loanbook.RatingModel), exceptionMessage);
                loanbook.OriginalRating = GetStringValueFromRowOrNull(worksheet, row, 13, nameof(loanbook.OriginalRating), exceptionMessage);
                loanbook.CurrentRating = GetStringValueFromRowOrNull(worksheet, row, 14, nameof(loanbook.CurrentRating), exceptionMessage);
                loanbook.LifetimePD = GetStringValueFromRowOrNull(worksheet, row, 15, nameof(loanbook.LifetimePD), exceptionMessage);
                loanbook.Month12PD = GetStringValueFromRowOrNull(worksheet, row, 16, nameof(loanbook.Month12PD), exceptionMessage);
                loanbook.DaysPastDue = GetStringValueFromRowOrNull(worksheet, row, 17, nameof(loanbook.DaysPastDue), exceptionMessage);
                loanbook.WatchlistIndicator = GetStringValueFromRowOrNull(worksheet, row, 18, nameof(loanbook.WatchlistIndicator), exceptionMessage);
                loanbook.Classification = GetStringValueFromRowOrNull(worksheet, row, 19, nameof(loanbook.Classification), exceptionMessage);
                loanbook.ImpairedDate = GetStringValueFromRowOrNull(worksheet, row, 20, nameof(loanbook.ImpairedDate), exceptionMessage);
                loanbook.DefaultDate = GetStringValueFromRowOrNull(worksheet, row, 21, nameof(loanbook.DefaultDate), exceptionMessage);
                loanbook.CreditLimit = GetStringValueFromRowOrNull(worksheet, row, 22, nameof(loanbook.CreditLimit), exceptionMessage);
                loanbook.OriginalBalanceLCY = GetStringValueFromRowOrNull(worksheet, row, 23, nameof(loanbook.OriginalBalanceLCY), exceptionMessage);
                loanbook.OutstandingBalanceLCY = GetStringValueFromRowOrNull(worksheet, row, 24, nameof(loanbook.OutstandingBalanceLCY), exceptionMessage);
                loanbook.OutstandingBalanceACY = GetStringValueFromRowOrNull(worksheet, row, 25, nameof(loanbook.OutstandingBalanceACY), exceptionMessage);
                loanbook.ContractStartDate = GetStringValueFromRowOrNull(worksheet, row, 26, nameof(loanbook.ContractStartDate), exceptionMessage);
                loanbook.ContractEndDate = GetStringValueFromRowOrNull(worksheet, row, 27, nameof(loanbook.ContractEndDate), exceptionMessage);
                loanbook.RestructureIndicator = GetStringValueFromRowOrNull(worksheet, row, 28, nameof(loanbook.RestructureIndicator), exceptionMessage);
                loanbook.RestructureRisk = GetStringValueFromRowOrNull(worksheet, row, 29, nameof(loanbook.RestructureRisk), exceptionMessage);
                loanbook.RestructureType = GetStringValueFromRowOrNull(worksheet, row, 30, nameof(loanbook.RestructureType), exceptionMessage);
                loanbook.RestructureStartDate = GetStringValueFromRowOrNull(worksheet, row, 31, nameof(loanbook.RestructureStartDate), exceptionMessage);
                loanbook.RestructureEndDate = GetStringValueFromRowOrNull(worksheet, row, 32, nameof(loanbook.RestructureEndDate), exceptionMessage);
                loanbook.PrincipalPaymentTermsOrigination = GetStringValueFromRowOrNull(worksheet, row, 33, nameof(loanbook.PrincipalPaymentTermsOrigination), exceptionMessage);
                loanbook.PPTOPeriod = GetStringValueFromRowOrNull(worksheet, row, 34, nameof(loanbook.PPTOPeriod), exceptionMessage);
                loanbook.InterestPaymentTermsOrigination = GetStringValueFromRowOrNull(worksheet, row, 35, nameof(loanbook.InterestPaymentTermsOrigination), exceptionMessage);
                loanbook.IPTOPeriod = GetStringValueFromRowOrNull(worksheet, row, 36, nameof(loanbook.IPTOPeriod), exceptionMessage);
                loanbook.PrincipalPaymentStructure = GetStringValueFromRowOrNull(worksheet, row, 37, nameof(loanbook.PrincipalPaymentStructure), exceptionMessage);
                loanbook.InterestPaymentStructure = GetStringValueFromRowOrNull(worksheet, row, 38, nameof(loanbook.InterestPaymentStructure), exceptionMessage);
                loanbook.InterestRateType = GetStringValueFromRowOrNull(worksheet, row, 39, nameof(loanbook.InterestRateType), exceptionMessage);
                loanbook.BaseRate = GetStringValueFromRowOrNull(worksheet, row, 40, nameof(loanbook.BaseRate), exceptionMessage);
                loanbook.OriginationContractualInterestRate = GetStringValueFromRowOrNull(worksheet, row, 41, nameof(loanbook.OriginationContractualInterestRate), exceptionMessage);
                loanbook.IntroductoryPeriod = GetStringValueFromRowOrNull(worksheet, row, 42, nameof(loanbook.IntroductoryPeriod), exceptionMessage);
                loanbook.PostIPContractualInterestRate = GetStringValueFromRowOrNull(worksheet, row, 43, nameof(loanbook.PostIPContractualInterestRate), exceptionMessage);
                loanbook.CurrentContractualInterestRate = GetStringValueFromRowOrNull(worksheet, row, 44, nameof(loanbook.CurrentContractualInterestRate), exceptionMessage);
                loanbook.EIR = GetStringValueFromRowOrNull(worksheet, row, 45, nameof(loanbook.EIR), exceptionMessage);
                loanbook.DebentureOMV = GetStringValueFromRowOrNull(worksheet, row, 46, nameof(loanbook.DebentureOMV), exceptionMessage);
                loanbook.DebentureFSV = GetStringValueFromRowOrNull(worksheet, row, 47, nameof(loanbook.DebentureFSV), exceptionMessage);
                loanbook.CashOMV = GetStringValueFromRowOrNull(worksheet, row, 48, nameof(loanbook.CashOMV), exceptionMessage);
                loanbook.CashFSV = GetStringValueFromRowOrNull(worksheet, row, 49, nameof(loanbook.CashFSV), exceptionMessage);
                loanbook.InventoryOMV = GetStringValueFromRowOrNull(worksheet, row, 50, nameof(loanbook.InventoryOMV), exceptionMessage);
                loanbook.InventoryFSV = GetStringValueFromRowOrNull(worksheet, row, 51, nameof(loanbook.InventoryFSV), exceptionMessage);
                loanbook.PlantEquipmentOMV = GetStringValueFromRowOrNull(worksheet, row, 52, nameof(loanbook.PlantEquipmentOMV), exceptionMessage);
                loanbook.PlantEquipmentFSV = GetStringValueFromRowOrNull(worksheet, row, 53, nameof(loanbook.PlantEquipmentFSV), exceptionMessage);
                loanbook.ResidentialPropertyOMV = GetStringValueFromRowOrNull(worksheet, row, 54, nameof(loanbook.ResidentialPropertyOMV), exceptionMessage);
                loanbook.ResidentialPropertyFSV = GetStringValueFromRowOrNull(worksheet, row, 55, nameof(loanbook.ResidentialPropertyFSV), exceptionMessage);
                loanbook.CommercialPropertyOMV = GetStringValueFromRowOrNull(worksheet, row, 56, nameof(loanbook.CommercialPropertyOMV), exceptionMessage);
                loanbook.CommercialPropertyFSV = GetStringValueFromRowOrNull(worksheet, row, 57, nameof(loanbook.CommercialPropertyFSV), exceptionMessage);
                loanbook.ReceivablesOMV = GetStringValueFromRowOrNull(worksheet, row, 58, nameof(loanbook.ReceivablesOMV), exceptionMessage);
                loanbook.ReceivablesFSV = GetStringValueFromRowOrNull(worksheet, row, 59, nameof(loanbook.ReceivablesFSV), exceptionMessage);
                loanbook.SharesOMV = GetStringValueFromRowOrNull(worksheet, row, 60, nameof(loanbook.SharesOMV), exceptionMessage);
                loanbook.SharesFSV = GetStringValueFromRowOrNull(worksheet, row, 61, nameof(loanbook.SharesFSV), exceptionMessage);
                loanbook.VehicleOMV = GetStringValueFromRowOrNull(worksheet, row, 62, nameof(loanbook.VehicleOMV), exceptionMessage);
                loanbook.VehicleFSV = GetStringValueFromRowOrNull(worksheet, row, 63, nameof(loanbook.VehicleFSV), exceptionMessage);
                loanbook.CureRate = GetStringValueFromRowOrNull(worksheet, row, 64, nameof(loanbook.CureRate), exceptionMessage);
                loanbook.GuaranteeIndicator = GetStringValueFromRowOrNull(worksheet, row, 65, nameof(loanbook.GuaranteeIndicator), exceptionMessage);
                loanbook.GuarantorPD = GetStringValueFromRowOrNull(worksheet, row, 66, nameof(loanbook.GuarantorPD), exceptionMessage);
                loanbook.GuarantorLGD = GetStringValueFromRowOrNull(worksheet, row, 67, nameof(loanbook.GuarantorLGD), exceptionMessage);
                loanbook.GuaranteeValue = GetStringValueFromRowOrNull(worksheet, row, 68, nameof(loanbook.GuaranteeValue), exceptionMessage);
                loanbook.GuaranteeLevel = GetStringValueFromRowOrNull(worksheet, row, 69, nameof(loanbook.GuaranteeLevel), exceptionMessage);
            }
            catch (Exception exception)
            {
                loanbook.Exception = exception.Message;
            }

            return loanbook;
        }

        private string GetStringValueFromRowOrNull(ExcelWorksheet worksheet, int row, int column, string columnName, StringBuilder exceptionMessage)
        {
            var cellValue = worksheet.Cells[row, column].Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                return cellValue.ToString();
            }
            else
            {
                return "";
            }

            //exceptionMessage.Append(GetLocalizedExceptionMessagePart(columnName));
            //return null;
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
            if (cellValue == null)
            {
                return false;
            }

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
