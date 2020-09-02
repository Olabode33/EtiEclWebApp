using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.IVModels.Dtos;
using TestDemo.Storage;

namespace TestDemo.Calibration.Exporting
{
    public class InputPdCrDrExporter : EpPlusExcelExporterBase, IInputPdCrDrExporter
    {
        public InputPdCrDrExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<InputPdCrDrDto> inputDtos)
        {
            return CreateExcelPackage(
                "PdCrDrImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PdCrDrImportList"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Customer_No",
                        "Account_No",
                        "Contract_No",
                        "Product_Type",
                        "Current_Rating",
                        "Days_Past_Due",
                        "Classification",
                        "Outstanding_Balance_Lcy",
                        "Contract_Start_Date",
                        "Contract_End_Date",
                        "RAPP_Date"
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.Customer_No,
                        _ => _.Account_No,
                        _ => _.Contract_No,
                        _ => _.Product_Type,
                        _ => _.Current_Rating,
                        _ => _.Days_Past_Due,
                        _ => _.Classification,
                        _ => _.Outstanding_Balance_Lcy,
                        _ => _.Contract_Start_Date,
                        _ => _.Contract_End_Date,
                        _ => _.RAPP_Date
                        );

                    for (var i = 1; i <= 11; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

        public FileDto ExportToFile(List<InputMacroEconomicCreditIndexDto> inputDtos)
        {
            return CreateExcelPackage(
                 "MacroEconomicCreditIndexImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("MacroEconomicCreditIndexImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Month",
                         "Best Estimate",
                         "Optimistic",
                         "Downturn"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Month,
                         _ => _.BestEstimate,
                         _ => _.Optimistic,
                         _ => _.Downturn
                         );

                     for (var i = 1; i <= 4; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputAssetBookDto> inputDtos)
        {
            return CreateExcelPackage(
                 "AssetBooksImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("AssetBooksImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Entity",
                         "Asset_Description",
                         "Asset_Type",
                         "Rating_Agency",
                         "Credit_Rating_At_Purchase_Date",
                         "Current_Credit_Rating",
                         "Nominal_Amount(ACY)",
                         "Nominal_Amount(LCY)",
                         "Principal_Amortisation",
                         "Principal_Repayment_Terms",
                         "Interest_Repayment_Terms",
                         "Outstanding_Balance(ACY)",
                         "Outstanding_Balance(LCY)",
                         "Coupon(%)",
                         "EIR(%)",
                         "Loan_Origination_Date",
                         "Loan_Maturity_Date",
                         "Days_Past_Due",
                         "Prudential_Classification",
                         "Forebearance_Flag"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Entity,
                         _ => _.AssetDescription,
                         _ => _.AssetType,
                         _ => _.RatingAgency,
                         _ => _.PurchaseDateCreditRating,
                         _ => _.CurrentCreditRating,
                         _ => _.NominalAmountACY,
                         _ => _.NominalAmountLCY,
                         _ => _.PrincipalAmortisation,
                         _ => _.PrincipalRepaymentTerms,
                         _ => _.InterestRepaymentTerms,
                         _ => _.OutstandingBalanceACY,
                         _ => _.OutstandingBalanceLCY,
                         _ => _.Coupon,
                         _ => _.EIR,
                         _ => _.LoanOriginationDate,
                         _ => _.LoanMaturityDate,
                         _ => _.DaysPastDue,
                         _ => _.PrudentialClassification,
                         _ => _.ForebearanceFlag
                         );

                     for (var i = 1; i <= 20; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }
    }
}
