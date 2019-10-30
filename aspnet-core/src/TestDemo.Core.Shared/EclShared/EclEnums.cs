using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared
{
    public enum DataTypeEnum
    {
        String = 1, Datetime, Double
    }

    public enum FrameworkEnum
    {
        General, Wholesale, Retail, OBE
    }

    public enum ModuleEnum
    {
        General, Calibration, Input, Framework
    }

    public enum EclStatusEnum
    {
        Draft, Submitted, ApprovedAssumption, ComputedPD, ComputedSICR, ComputedEAD, ComputedLGD, ComputedECL, Completed, Closed
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

    public enum EadInputGroupEnum
    {
        General, CreditConversionFactors, VariableInterestRateProjections, ExchangeRateProjections
    }

    public enum LdgInputAssumptionEnum
    {
        General, UnsecuredRecoveries, CostOfRecoveryHigh, CostOfRecoveryLow, CollateralGrowthBest, CollateralGrowthOptimistic, CollateralGrowthDownturn, CollateralTTR, CollateralProjectionBest, CollateralProjectionOptimistic, CollateralProjectionDownturn
    }

    public enum AssumptionGroupEnum
    {
        General, ScenarioInputs, AbsoluteCreditQuality, RelativeCreditQuality, ForwardTransitions, BackwardTransitions
    }

    public enum AssumptionTypeEnum
    {
        General, EadInputAssumption, LgdInputAssumption, Pd12MonthsAssumption, PdSnPAssumption
    }

    public static class EclEnums
    {
        
    }
}
