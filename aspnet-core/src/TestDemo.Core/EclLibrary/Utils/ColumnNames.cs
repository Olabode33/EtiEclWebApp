using System;
using System.Collections.Generic;
using System.Text;

namespace EclEngine.Utils
{
    public static class LoanBookColumns
    {
        public const string ContractID = "ContractID";
        public const string CustomerNo = "CustomerNo";
        public const string AccountNo = "AccountNo";
        public const string ContractNo = "ContractNo";
        public const string CustomerName = "CustomerName";
        public const string SnapshotDate = "SnapshotDate";
        public const string Segment = "Segment";
        public const string Sector = "Sector";
        public const string Currency = "Currency";
        public const string ProductType = "ProductType";
        public const string ProductMapping = "ProductMapping";
        public const string SpecialisedLending = "SpecialisedLending";
        public const string RatingModel = "RatingModel";
        public const string OriginalRating = "OriginalRating";
        public const string CurrentRating = "CurrentRating";
        public const string LifetimePD = "LifetimePD";
        public const string Month12PD = "Month12PD";
        public const string DaysPastDue = "DaysPastDue";
        public const string WatchlistIndicator = "WatchlistIndicator";
        public const string Classification = "Classification";
        public const string ImpairedDate = "ImpairedDate";
        public const string DefaultDate = "DefaultDate";
        public const string CreditLimit = "CreditLimit";
        public const string OriginalBalanceLCY = "OriginalBalanceLCY";
        public const string OutstandingBalanceLCY = "OutstandingBalanceLCY";
        public const string OutstandingBalanceACY = "OutstandingBalanceACY";
        public const string ContractStartDate = "ContractStartDate";
        public const string ContractEndDate = "ContractEndDate";
        public const string RestructureIndicator = "RestructureIndicator";
        public const string RestructureRisk = "RestructureRisk";
        public const string RestructureType = "RestructureType";
        public const string RestructureStartDate = "RestructureStartDate";
        public const string RestructureEndDate = "RestructureEndDate";
        public const string PrincipalPaymentTermsOrigination = "PrincipalPaymentTermsOrigination";
        public const string PPTOPeriod = "PPTOPeriod";
        public const string InterestPaymentTermsOrigination = "InterestPaymentTermsOrigination";
        public const string IPTOPeriod = "IPTOPeriod";
        public const string PrincipalPaymentStructure = "PrincipalPaymentStructure";
        public const string InterestPaymentStructure = "InterestPaymentStructure";
        public const string InterestRateType = "InterestRateType";
        public const string BaseRate = "BaseRate";
        public const string OriginationContractualInterestRate = "OriginationContractualInterestRate";
        public const string IntroductoryPeriod = "IntroductoryPeriod";
        public const string PostIPContractualInterestRate = "PostIPContractualInterestRate";
        public const string CurrentContractualInterestRate = "CurrentContractualInterestRate";
        public const string EIR = "EIR";
        public const string DebentureOMV = "DebentureOMV";
        public const string DebentureFSV = "DebentureFSV";
        public const string CashOMV = "CashOMV";
        public const string CashFSV = "CashFSV";
        public const string InventoryOMV = "InventoryOMV";
        public const string InventoryFSV = "InventoryFSV";
        public const string PlantEquipmentOMV = "PlantEquipmentOMV";
        public const string PlantEquipmentFSV = "PlantEquipmentFSV";
        public const string ResidentialPropertyOMV = "ResidentialPropertyOMV";
        public const string ResidentialPropertyFSV = "ResidentialPropertyFSV";
        public const string CommercialPropertyOMV = "CommercialPropertyOMV";
        public const string CommercialProperty = "CommercialProperty";
        public const string ReceivablesOMV = "ReceivablesOMV";
        public const string ReceivablesFSV = "ReceivablesFSV";
        public const string SharesOMV = "SharesOMV";
        public const string SharesFSV = "SharesFSV";
        public const string VehicleOMV = "VehicleOMV";
        public const string VehicleFSV = "VehicleFSV";
        public const string CureRate = "CureRate";
        public const string GuaranteeIndicator = "GuaranteeIndicator";
        public const string GuarantorPD = "GuarantorPD";
        public const string GuarantorLGD = "GuarantorLGD";
        public const string GuaranteeValue = "GuaranteeValue";
        public const string GuaranteeLevel = "GuaranteeLevel";
    }

    public static class SnPCummlativeDefaultRateColumns
    {
        public const string Rating = "Rating";
        public const string Month1 = "1";
        public const string Month2 = "2";
        public const string Month3 = "3";
        public const string Month4 = "4";
        public const string Month5 = "5";
        public const string Month6 = "6";
        public const string Month7 = "7";
        public const string Month8 = "8";
        public const string Month9 = "9";
        public const string Month10 = "10";
        public const string Month11 = "11";
        public const string Month12 = "12";
        public const string Month13 = "13";
        public const string Month14 = "14";
        public const string Month15 = "15";
        public const string Pd12Months = "[12 Month PD]";
    }

    public static class Pd12MonthColumns
    {
        public const string CreditRating = "Credit Rating";
        public const string Pd = "PD";
        public const string SnpMappingEtiCreditPolicy = "S&P Mapping (ETI Credit Policy)";
        public const string SnpMappingBestFit = "S&P Mapping (Best Fit)";
    }

    public static class LogOddRatioColumns
    {
        public const string Rank = "Rank";
        public const string Rating = "Rating";
        public const string Year = "Year";
        public const string LogOddsRatio = "LogOddsRatio";
    }

    public static class LogRateColumns
    {
        public const string LogOddsRatio = "LogRate";
    }

    public static class PdAssumptionsRowKey
    {
        public const string AssumptionsColumn = "Assumptions";
        public const string ValuesColumn = "Value";
        public const string ReDefaultAdjustmentFactor = "ReDefaultAdjustmentFactor";
        public const string SnpMapping = "SnpMapping";
        public const string NonExpired = "NonExpired"; ///OD_PERFORMANCE_PAST_EXPIRY
        public const string Expired = "Expired"; ///EXP_OD_PERFORMANCE_PAST_REPORTING
        public const string SnpMappingValueBestFit = "Best Fit";
        public const string SnpMappingValueEtiCreditPolicy = "ETI Credit Policy";
    }

    public static class MonthlyLogOddsRatioColumns
    {
        public const string Month = "Month";
        public const string Rank = "Rank";
        public const string Rating = "Rating";
        public const string CreditRating = "CreditRating";
    }

    public static class PdMappingColumns
    {
        public const string PdGroup = "PdGroup";
        public const string RatingUsed = "RatingUsed";
        public const string ClassificationScore = "ClassificationScore";
        public const string MaxDpd = "MaxDpd";
        public const string MaxClassificationScore = "MaxClassificationScore";
        public const string RestructureEndDate = "RestructureEndDate";
        public const string TimeToMaturityMonths = "TtmMonths";
    }

    public static class EtiNplColumns
    {
        public const string Date = "Date";
        public const string Series = "Series";
    }

    public static class HistoricIndexColumns
    {
        public const string Date = "Date";
        public const string Actual = "Actual";
        public const string Standardised = "Standardised";
    }
    public static class VasicekEtiNplIndexColumns
    {
        public const string Date = "Date";
        public const string Month = "Month";
        public const string EtiNpl = "ETI NPL";
        public const string Index = "Index";
        public const string Fitted = "Fitted";
        public const string Residuals = "Residuals";
        public const string ScenarioPd = "PD";
        public const string ScenarioIndex = "Index";
        public const string ScenarioFactor = "Factor";
    }

    public static class MacroeconomicProjectionColumns
    {
        public const string Date = "Date";
        public const string PrimeLendingRate = "Prime Lending Rate (%)";
        public const string OilExports = "Oil Exports (USD'm)";
        public const string RealGdpGrowthRate = "Real GDP Growth Rate (%)";
        public const string DifferencedGdpGrowthRate = "Differenced Real GDP Growth Rate (%)";
    }

    public static class IndexForecastColumns
    {
        public const string Date = "Date";
        public const string PrimeLendingRate = "PrimeLendingRate";
        public const string OilExports = "OilExports";
        public const string DifferencedGdpGrowthRate = "DifferencedRealGdpGrowthRate";
        public const string Principal1 = "Principal1";
        public const string Principal2 = "Principal2";
        public const string Actual = "Actual";
        public const string Standardised = "Standardised";
    }

    public static class StatisticalInputsColumns
    {
        public const string Mode = "Mode";
        public const string PrimeLendingRate = "Prime Lending Rate (%)";
        public const string OilExports = "Oil Exports (USD'm)";
        public const string RealGdpGrowthRate = "Real GDP Growth Rate (%)";
    }

    public static class StatisticalInputsRowKeys
    {
        public const string Mean = "Mean";
        public const string StandardDeviation = "Standard Deviation";
        public const string Eigenvalues = "Eigenvalues";
        public const string PrincipalScore1 = "Principal Component Score 1";
        public const string PrincipalScore2 = "Principal Component Score 2";
    }

    public static class CreditIndexColumns
    {
        public const string ProjectionMonth = "ProjectionMonth";
        public const string BestEstimate = "BestEstimate";
        public const string Optimistic = "Optimistic";
        public const string Downturn = "Downturn";
    }

    public static class MarginalLifetimeRedefaultPdColumns
    {
        public const string PdGroup = "PdGroup";
        public const string ProjectionMonth = "Month";
        public const string Value = "Value";
    }

    public static class NonInternalModelInputColumns
    {
        public const string Month = "Month";
        public const string ConsStage1 = "CONS_STAGE_1";
        public const string ConsStage2 = "CONS_STAGE_2";
        public const string CommStage1 = "COMM_STAGE_1";
        public const string CommStage2 = "COMM_STAGE_2";
    }

    public static class SicrInputsColumns
    {
        public const string ContractId = "ContractId";
        public const string RestructureIndicator = "RestructureIndicator";
        public const string RestructureRisk = "RestructureRisk";
        public const string WatchlistIndicator = "WatchlistIndicator";
        public const string CurrentRating = "CurrentRating";
        public const string Pd12Month = "Pd12Month";
        public const string LifetimePd = "LifetimePd";
        public const string RedefaultLifetimePd = "RedefaultLifetimePD";
        public const string Stage1Transition = "Stage1Transition";
        public const string Stage2Transition = "Stage2Transition";
        public const string DaysPastDue = "DaysPastDue";
        public const string OriginationRating = "OriginationRating";
        public const string Origination12MonthPd = "Origination12MonthPd";
        public const string OriginationLifetimePd = "OriginationLifetimePd";
        public const string ImpairedDate = "ImpairedDate";
        public const string DefaultDate = "DefaultDate";
    }

    public static class ImpairmentRowKeys
    {
        public static string CreditIndexThreshold = "Credit Index Threshold for Downturn Recoveries";
        public static string BestScenarioLikelihood = "Best Estimate Scenario(Likelihood)";
        public static string OptimisticScenarioLikelihood = "Optimistic Scenario(Likelihood)";
        public static string DownturnScenarioLikelihood = "Downturn Scenario(Likelihood)";
        public static string AbsoluteCreditQualityCriteria = "Absolute Credit Quality Criteria";
        public static string AbsoluteCreditQualityThreshold = "Absolute Credit Quality Threshold";
        public static string RelativeCreditQualityCriteria = "Relative Credit Quality Criteria";
        public static string RelativeCreditQualityThreshold = "Relative Credit Quality Threshold";
        public static string CreditRatingRankLowHighRisk = "Credit Rating Rank (Low/High Risk)";
        public static string CreditRatingRankLowRisk = "Credit Rating Rank (Notches) - Low Risk";
        public static string CreditRatingRankHighRisk = "Credit Rating Rank (Notches) - High Risk";
        public static string CreditRatingDefaultIndicator = "Credit Rating Default Indicator";
        public static string UseWatchlistIndicator = "Use Watchlist Indicator?";
        public static string UseRestructureIndicator = "Use Restructure Indicator?";
        public static string ForwardTransitionStage1to1 = "Forward Transitions Stage 1 -> Stage 2 (dpd)";
        public static string ForwardTransitionStage2to3 = "Forward Transitions Stage 2 -> Stage 3 (dpd)";
        public static string BackwardTransitionsStage2to1 = "Backward Transitions (Probation Period) Stage 2 -> Stage 1 (days)";
        public static string BackwardTransitionsStage3to2 = "Backward Transitions (Probation Period) Stage 3 -> Stage 2 (days)";
        public static string CreditRatingRank = "Credit Rating Rank ";
        public static string ColumnAssumption = "Assumption";
        public static string ColumnValue = "Value";
    }

    public static class LgdInputAssumptionColumns
    {
        public static string SegementProductType = "SEGMENT_PRODUCT_TYPE";
        public static string CureRate = "CURE_RATE";
        public static string Scenario = "Scenario";
        public static string UnsecuredRecoveries0Days = "0";
        public static string UnsecuredRecoveries90Days = "90";
        public static string UnsecuredRecoveries180Days = "180";
        public static string UnsecuredRecoveries270Days = "270";
        public static string UnsecuredRecoveries360Days = "360";
    }

    public static class TempLgdContractDataColumns
    {
        public static string ContractNo = "CONTRACT_NO";
        public static string AccountNo = "ACCOUNT_NO";
        public static string CustomerNo = "CUSTOMER_NO";
        public static string ProductType = "PRODUCT_TYPE";
        public static string TtrYears = "TTR_YEARS";
        public static string CostOfRecovery = "COST_OF_RECOVERY_%";
        public static string GuaranteePd = "GUARANTOR_PD";
        public static string GuaranteeLgd = "GUARANTOR_LGD";
        public static string GuaranteeValue = "GUARANTEE_VALUE";
        public static string GuaranteeLevel = "GUARANTEE_LEVEL";
    }

    public static class TypeCollateralTypeColumns
    {
        public static string ContractNo = "CONTRACT_NO";
        public static string Cash = "CASH";
        public static string CommercialProperty = "COMMERCIAL_PROPERTY";
        public static string Debenture = "DEBENTURE";
        public static string Inventory = "INVENTORY";
        public static string PlantAndEquipment = "PLANT_AND_EQUIPMENT";
        public static string Receivables = "RECEIVABLES";
        public static string ResidentialProperty = "RESIDENTIAL_PROPERTY";
        public static string Shares = "SHARES";
        public static string Vehicle = "VEHICLE";
        public static string ProjectionMonth = "MONTH";
    }

    public static class EadInputsColumns
    {
        public static string ContractId = "CONTRACT_ID";
        public static string EirGroup = "EIR_GROUP";
        public static string CirGroup = "CIR_GROUP";
        public static string EirProjectionGroups = "EIR_GROUPS";
        public static string CirProjectionGroups = "CIR_GROUPS";
    }

    public static class StageClassificationColumns
    {
        public static string ContractId = "Contract Id";
        public static string Stage = "Stage";
    }

    public static class IrFactorColumns
    {
        public static string GroupRank = "Rank";
        public static string CirGroup = "CIR Group";
        public static string EirGroup = "EIR Group";
        public static string ProjectionMonth = "Month";
        public static string ProjectionValue = "Value";
    }

    public static class LifetimeEadColumns
    {
        public static string ContractId = "Contract Id";
        public static string CirIndex = "CIR Index";
        public static string ProductType = "Product Type";
        public static string MonthsPastDue = "Months Past Due";
        public static string ProjectionMonth = "Month";
        public static string ProjectionValue = "Value";
    }

    public static class LifetimeCollateralColumns
    {
        public static string ContractId = "Contract Id";
        public static string EirIndex = "EIR Index";
        public static string TtrMonths = "TTR Months";
        public static string ProjectionMonth = "Month";
        public static string ProjectionValue = "Value";
    }

    public static class LifetimeLgdColumns
    {
        public static string ContractId = "CONTRACT_ID";
        public static string PdIndex = "PD_INDEX";
        public static string LgdIndex = "LGD_INDEX";
        public static string RedefaultLifetimePD = "RE-DEFAULTED_LIFETIME_PD";
        public static string CureRate = "CURE_RATE";
        public static string UrBest = "UR (%) - BE";
        public static string URDownturn = "UR (%) - D";
        public static string Cor = "CoR (%)";
        public static string GPd = "G_PD";
        public static string GuarantorLgd = "GUARANTOR_LGD";
        public static string GuaranteeValue = "GUARANTEE_VALUE";
        public static string GuaranteeLevel = "GUARANTEE_LEVEL";
        public static string Stage = "STAGE";
        public static string Month = "Month";
        public static string Value = "Value";
    }

    public static class EclColumns
    {
        public static string ContractId = "Contract Id";
        public static string EclMonth = "Month";
        public static string MonthlyEclValue = "Ecl";
        public static string FinalEclValue = "Ecl";
        public static string Stage = "Stage";
    }
}
