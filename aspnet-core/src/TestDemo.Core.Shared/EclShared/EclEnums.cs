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

    public enum UploadStatusEnum
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
}
