﻿using System;
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
        All, Wholesale, Retail, OBE
    }

    public enum ModuleEnum
    {
        General, Calibration, Input, Framework
    }

    public enum EclStatusEnum
    {
        Draft, Submitted, Approved, Running, ComputedPD, ComputedSICR, ComputedEAD, ComputedLGD, ComputedECL, Completed, Closed
    }

    public enum UploadDocTypeEnum
    {
        General, LoanBook, PaymentSchedule
    }

    public enum GeneralStatusEnum
    {
        Draft, Submitted, Approved, Rejected
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
        General, CreditPD, CreditEtiPolicy, CreditBestFit,  StatisticsIndexWeights
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

    public static class EclEnums
    {
        public const string Assumption = "Assumption";
        public const string EadInputAssumption = "EadInputAssumption";
        public const string LgdInputAssumption = "LgdInputAssumption";
    }
}
