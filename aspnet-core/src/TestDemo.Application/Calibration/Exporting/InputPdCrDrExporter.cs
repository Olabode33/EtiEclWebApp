using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.HoldCoInterCompanyResults.Dtos;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.IVModels.Dtos;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using TestDemo.LoanImpairmentModelResults.Dtos;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.ReceivablesResults.Dtos;
using TestDemo.Storage;

namespace TestDemo.Calibration.Exporting
{
    public class InputPdCrDrExporter : EpPlusExcelExporterBase, IInputPdCrDrExporter
    {
        public InputPdCrDrExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<InputPdCommsConsDto> inputDtos)
        {
            return CreateExcelPackage(
                "PdCommsConsImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("PdCommsConsImportList"));
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
                        "SnapShot_Date",
                        "WI"
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
                        _ => _.Snapshot_Date,
                        _ => _.WI
                        );

                    for (var i = 1; i <= 11; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
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

        public FileDto ExportToFile(List<InputHoldCoInterCompanyResultDto> inputDtos)
        {
            return CreateExcelPackage(
                 "ResultsImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("ResultsImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Asset Type",
                         "Asset Description",
                         "Stage",
                         "Outstanding Balance(LCY)",
                         "Best Estimate",
                         "Optimistic",
                         "Downturn",
                         "Impairment"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.AssetType,
                         _ => _.AssetDescription,
                         _ => _.Stage,
                         _ => _.OutstandingBalance,
                         _ => _.BestEstimate,
                         _ => _.Optimistic,
                         _ => _.Downturn,
                         _ => _.Impairment
                         );

                     for (var i = 1; i <= 8; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputHoldCoResultSummaryDto> inputDtos)
        {
            return CreateExcelPackage(
                 "ResultSummaryImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("ResultSummaryImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "BestEstimateExposure",
                         "OptimisticExposure",
                         "DownturnExposure",
                         "BestEstimateTotal",
                         "OptimisticTotal",
                         "DownturnTotal",
                         "BestEstimateImpairmentRatio",
                         "OptimisticImpairmentRatio",
                         "DownturnImpairmentRatio",
                         "Exposure",
                         "Total",
                         "ImpairmentRatio"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.BestEstimateExposure,
                         _ => _.OptimisticExposure,
                         _ => _.DownturnExposure,
                         _ => _.BestEstimateTotal,
                         _ => _.OptimisticTotal,
                         _ => _.DownturnTotal,
                         _ => _.BestEstimateImpairmentRatio,
                         _ => _.OptimisticImpairmentRatio,
                         _ => _.DownturnImpairmentRatio,
                         _ => _.Exposure,
                         _ => _.Total,
                         _ => _.ImpairmentRatio
                         );

                     for (var i = 1; i <= 12; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputResultSummaryByStageDto> inputDtos)
        {
            return CreateExcelPackage(
                 "ResultSummaryByStageImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("ResultSummaryByStageImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "StageOneExposure",
                         "StageTwoExposure",
                         "StageThreeExposure",
                         "TotalExposure",
                         "StageOneImpairment",
                         "StageTwoImpairment",
                         "StageThreeImpairment",
                         "StageOneImpairmentRatio",
                         "StageTwoImpairmentRatio",
                         "TotalImpairment",
                         "StageThreeImpairmentRatio",
                         "TotalImpairmentRatio"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.StageOneExposure,
                         _ => _.StageTwoExposure,
                         _ => _.StageThreeExposure,
                         _ => _.TotalExposure,
                         _ => _.StageOneImpairment,
                         _ => _.StageTwoImpairment,
                         _ => _.StageThreeImpairment,
                         _ => _.StageOneImpairmentRatio,
                         _ => _.StageTwoImpairmentRatio,
                         _ => _.TotalImpairment,
                         _ => _.StageThreeImpairmentRatio,
                         _ => _.TotalImpairmentRatio
                         );

                     for (var i = 1; i <= 12; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputCurrentPeriodDateDto> inputDtos)
        {
            return CreateExcelPackage(
                 "CurrentPeriodDataImportList.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("CurrentPeriodDataImportList"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Account",
                         "0-90 Days",
                         "91-180 Days",
                         "180-365 Days",
                         "> 365 Days"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Account,
                         _ => _.ZeroTo90,
                         _ => _.NinetyOneTo180,
                         _ => _.OneEightyOneTo365,
                         _ => _.Over365
                         );

                     for (var i = 1; i <= 5; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputReceivablesForecastDto> inputDtos)
        {
            return CreateExcelPackage(
                 "ForecastImport.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("Forecast"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Period",
                         "Optimistic",
                         "Base",
                         "Downturn"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Period,
                         _ => _.Optimistic,
                         _ => _.Base,
                         _ => _.Downturn
                         );

                     for (var i = 1; i <= 4; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputReceivablesResultDto> inputDtos)
        {
            return CreateExcelPackage(
                "ReceivablesResults.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ReceivablesResults"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "TotalExposure",
                        "TotalImpairment",
                        "AdditionalProvision",
                        "Coverage",
                        "OptimisticExposure",
                        "OptimisticImpairment",
                        "OptimisticCoverageRatio",
                        "BaseExposure",
                        "BaseImpairment",
                        "BaseCoverageRatio",
                        "DownturnExposure",
                        "DownturnImpairment",
                        "DownturnCoverageRatio",
                        "EclTotalExposure",
                        "EclTotalImpairment",
                        "TotalCoverageRatio"
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.TotalExposure,
                        _ => _.TotalImpairment,
                        _ => _.AdditionalProvision,
                        _ => _.Coverage,
                        _ => _.OptimisticExposure,
                        _ => _.OptimisticImpairment,
                        _ => _.OptimisticCoverageRatio,
                        _ => _.BaseExposure,
                        _ => _.BaseImpairment,
                        _ => _.BaseCoverageRatio, 
                        _ => _.DownturnExposure,
                        _ => _.DownturnImpairment,
                        _ => _.DownturnCoverageRatio, 
                        _ => _.ECLTotalExposure,
                        _ => _.ECLTotalImpairment,
                        _ => _.TotalCoverageRatio
                        );

                    for (var i = 1; i <= 4; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

        public FileDto ExportToFile(List<InputLoanImpairmentRecoveryDto> inputDtos)
        {
            return CreateExcelPackage(
                 "ImpairmentRecoveryImport.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("ImpairmentRecovery"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Recovery",
                         "Cash_Recovery",
                         "Property",
                         "Shares",
                         "Loan_Sale"
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Recovery,
                         _ => _.CashRecovery,
                         _ => _.Property,
                         _ => _.Shares,
                         _ => _.LoanSale
                         );

                     for (var i = 1; i <= 5; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputLoanImpairmentScenarioDto> inputDtos)
        {
            return CreateExcelPackage(
                "ImpairmentScenarioOptionsImport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("ImpairmentScenarioOptions"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "Apply_Overrides_Base_Scenario",
                        "Apply_Overrides_Downturn_Scenario",
                        "Apply_Overrides_Optimistic_Scenario",
                        "Base_Scenario",
                        "Best_Scenario_Overrides_Value",
                        "Downturn_Scenario_Overrides_Value",
                        "Optimistic_Scenario",
                        "Optimistic_Scenario_Overrides_Value",
                        "Scenario_Option"
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.ApplyOverridesBaseScenario,
                        _ => _.ApplyOverridesDownturnScenario,
                        _ => _.ApplyOverridesOptimisticScenario,
                        _ => _.BaseScenario,
                        _ => _.BestScenarioOverridesValue,                  
                        _ => _.DownturnScenarioOverridesValue,
                        _ => _.OptimisticScenario,
                        _ => _.OptimisticScenarioOverridesValue,
                        _ => _.ScenarioOption
                        );

                    for (var i = 1; i <= 9; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }

        public FileDto ExportToFile(List<InputLoanImpairmentKeyParameterDto> inputDtos)
        {
            return CreateExcelPackage(
                 "CalibrationOfKeyParametersImport.xlsx",
                 excelPackage =>
                 {
                     var sheet = excelPackage.Workbook.Worksheets.Add(L("CalibrationOfKeyParameters"));
                     sheet.OutLineApplyStyle = true;

                     AddHeader(
                         sheet,
                         "Year",
                         "Expected_Cash_Flow",
                         "Revised_Cash_Flow"
                         
                         );

                     AddObjects(
                         sheet, 2, inputDtos,
                         _ => _.Year,
                         _ => _.ExpectedCashFlow,
                         _ => _.RevisedCashFlow
                         );

                     for (var i = 1; i <= 3; i++)
                     {
                         sheet.Column(i).AutoFit();
                     }
                 });
        }

        public FileDto ExportToFile(List<InputLoanImpairmentModelResultDto> inputDtos)
        {
            return CreateExcelPackage(
                "ResultsImport.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LoanImpairmentModelResult"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        "BaseScenarioExposure",
                        "OptimisticScenarioExposure",
                        "DownturnScenarioExposure",
                        "WeightedECLResultExposure",
                        "BestScenarioPreOverlay",
                        "OptimisticScenarioPreOverlay",
                        "DownturnScenarioPreOverlay",
                        "WeightedECLResultPreOverlay",
                        "BaseScenarioOverrideImpact",
                        "OptimisticScenarioOverrideImpact",
                        "DownturnScenarioOverrideImpact",
                        "WeightedECLResultOverrideImpact",
                        "BaseScenarioImpairmentPostOverride",
                        "OptimisticScenarioImpairmentPostOverride",
                        "DownturnScenarioImpairmentPostOverride",
                        "WeightedECLResultImpairmentPostOverride",
                        "BaseScenarioOverlay",
                        "OptimisticScenarioOverlay",
                        "DownturnScenarioOverlay",
                        "WeightedECLResultOverlay",
                        "BaseScenarioFinalImpairment",
                        "OptimisticScenarioFinalImpairnment",
                        "DownturnScenarioFinalImpairment",
                        "WeightedECLResultFinalImpairment"

                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.BaseScenarioExposure,
                        _ => _.OptimisticScenarioExposure,
                        _ => _.DownturnScenarioExposure,
                        _ => _.ResultsExposure,
                        _ => _.BaseScenarioPreOverlay,
                        _ => _.OptimisticScenarioPreOverlay,
                        _ => _.DownturnScenarioPreOverlay,
                        _ => _.ResultPreOverlay,
                        _ => _.BaseScenarioOverrideImpact,
                        _ => _.OptimisticScenarioOverrideImpact,
                        _ => _.DownturnScenarioOverrideImpact,
                        _ => _.ResultOverrideImpact,
                        _ => _.BaseScenarioIPO,
                        _ => _.OptimisticScenarioIPO,
                        _ => _.DownturnScenarioIPO,
                        _ => _.ResultIPO,
                        _ => _.BaseScenarioOverlay,
                        _ => _.OptimisticScenarioOverlay,
                        _ => _.DownturnScenarioOverlay,
                        _ => _.ResultOverlay,
                        _ => _.BaseScenarioFinalImpairment,
                        _ => _.OptimisticScenarioFinalImpairment,
                        _ => _.DownturnScenarioFinalImpairment,
                        _ => _.ResultFinalImpairment
                        );

                    for (var i = 1; i <= 3; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
