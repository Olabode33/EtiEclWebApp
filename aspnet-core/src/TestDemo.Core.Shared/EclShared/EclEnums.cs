using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared
{
    public enum DataTypeEnum
    {
        String = 1, Datetime, Double, Int, DoublePercentage, DoubleMoney, StringDropdown, DoubleDropDown, IntDropdown
    }

    public enum FrameworkEnum
    {
        All, Wholesale, Retail, OBE, Investments
    }

    public enum ModuleEnum
    {
        General, Calibration, Input, Framework
    }

    public enum EclStatusEnum
    {
        Draft, Submitted, Approved, Running, PreOverrideComplete, PostOverrideComplete, ComputedEAD, ComputedLGD, ComputedECL, Completed, Closed, AwaitngAdditionApproval, QueuePostOverride
    }

    public enum UploadDocTypeEnum
    {
        General, LoanBook, PaymentSchedule, AssetBook
    }

    public enum GeneralStatusEnum
    {
        Draft, Submitted, Approved, Rejected, Processing, Completed, AwaitngAdditionApproval
    }

    public enum ResultSummaryTypeEnum
    {
        ByScenario, ByStage, ByProductType, BySegementStage
    }

    public enum EadInputAssumptionGroupEnum
    {
        General, CreditConversionFactors, VariableInterestRateProjections, ExchangeRateProjections, BehaviouralLife
    }

    public enum LdgInputAssumptionGroupEnum
    {
        General, UnsecuredRecoveriesCureRate, UnsecuredRecoveriesTimeInDefault, CostOfRecoveryHigh, CostOfRecoveryLow, CollateralGrowthBest, CollateralGrowthOptimistic, CollateralGrowthDownturn, CollateralTTR, CollateralProjectionBest, CollateralProjectionOptimistic, CollateralProjectionDownturn, Haircut, PdAssumptions
    }

    public enum PdInputAssumptionGroupEnum
    {
        General, CreditPD, CreditEtiPolicy, CreditBestFit,  StatisticsIndexWeights, InvestmentAssumption, InvestmentMacroeconomicScenario
    }

    public enum PdInputAssumptionHistoricalIndexEtiNplEnum
    {
        Actual, Standarised, NplSeries
    }

    public enum PdInputAssumptionMacroEconomicInputGroupEnum
    {
        General, StatisticalInputsPrimeLending, StatisticalInputsOilExports, StatisticalInputsRealGdpGrowthRate, DifferencedRealGdpGrowthRate,
        ProjectionPrimeLending, ProjectionOilExports, ProjectionRealGdpGrowthRate, ProjectionDifferencedRealGdpGrowthRate,
    }

    public enum AssumptionGroupEnum
    {
        General, ScenarioInputs, AbsoluteCreditQuality, RelativeCreditQuality, ForwardTransitions, BackwardTransitions, CreditRatingRank
    }

    public enum AssumptionTypeEnum
    {
        General, EadInputAssumption, LgdInputAssumption, PdInputAssumption, PdSnPAssumption, PdMacroeconomicProjection, PdMacroeconomicInput, PdNonInternalModel, PdNplIndex
    }

    public enum CalibrationResultGroupEnum
    {
        IndexWeight, PdInputs, EadInputs, LgdInputs, Haircuts
    }

    public enum CollateralProjectionType
    {
        Best, Optimistic, Downturn
    }

    public enum EclScenarioEnum
    {
        Best,
        Optimistic,
        Downturn
    }

    public enum EclType
    {
        None = -1,
        Retail,
        Wholesale,
        Obe,
        Investment
    }

    public static class EclEnums
    {
        public const string Assumption = "Assumption";
        public const string EadInputAssumption = "EadInputAssumption";
        public const string LgdInputAssumption = "LgdInputAssumption";
        public const string PdInputAssumption = "PdInputAssumption";
        public const string PdMacroProjectionAssumption = "PdMacroProjectionAssumption";
        public const string PdMacroInputAssumption = "PdMacroInputAssumption";
        public const string PdNonInternalAssumption = "PdNonInternalAssumption";
        public const string PdNplIndexAssumption = "PdNplIndexAssumption";
        public const string PdSnpAssumption = "PdSnpAssumption";
    }
}
