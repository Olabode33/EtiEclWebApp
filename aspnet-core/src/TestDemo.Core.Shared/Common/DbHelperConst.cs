using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Common
{
    public class DbHelperConst
    {
        //Table Names
        public const string TB_CalibrationInputPdCommsCons = "CalibrationInput_Comm_Cons_PD";
        public const string TB_CalibrationInputPdCrDr = "CalibrationInput_PD_CR_DR";
        public const string TB_CalibrationInputBehaviouralTerm = "CalibrationInput_EAD_Behavioural_Terms";
        public const string TB_CalibrationInputCcfSummary = "CalibrationInput_EAD_CCF_Summary";
        public const string TB_CalibrationInputHaircut = "CalibrationInput_LGD_HairCut";
        public const string TB_CalibrationInputRecoveryRate = "CalibrationInput_LGD_RecoveryRate";
        public const string TB_CalibrationInputMacroeconomicData = "MacroenonomicData";

        public const string TB_CalibrationResultBehaviouralTerm = "CalibrationResult_EAD_Behavioural_Terms";
        public const string TB_CalibrationResultCcfSummary = "CalibrationResult_EAD_CCF_Summary";
        public const string TB_CalibrationResultHaircut = "CalibrationResult_LGD_HairCut";
        public const string TB_CalibrationResultHaircutSummary = "CalibrationResult_LGD_HairCut_Summary";
        public const string TB_CalibrationResultRecoveryRate = "CalibrationResult_LGD_RecoveryRate";
        public const string TB_CalibrationResultMacroeconomicData = "MacroenonomicData";
        public const string TB_CalibrationResultPd12Months = "CalibrationResult_PD_12Months";
        public const string TB_CalibrationResultPd12MonthsSummary = "CalibrationResult_PD_12Months_Summary";
        public const string TB_MacroResultCorMat = "MacroResult_CorMat";
        public const string TB_MacroResultIndexData = "MacroResult_IndexData";
        public const string TB_MacroResultPrincipalComponent = "MacroResult_PrincipalComponent";
        public const string TB_MacroResultPrincipalComponentSummary = "MacroResult_PrincipalComponentSummary";
        public const string TB_MacroResultSelectedMacroVariables = "MacroResult_SelectedMacroEconomicVariables";
        public const string TB_MacroResultStatistics = "MacroResult_Statistics";

        public const string TB_EclLoanBookWholesale = "WholesaleEclDataLoanBooks";
        public const string TB_EclLoanBookRetail = "RetailEclDataLoanBooks";
        public const string TB_EclLoanBookObe = "ObeEclDataLoanBooks";
        public const string TB_EclPaymentScheduleWholesale = "WholesaleEclDataPaymentSchedules";
        public const string TB_EclPaymentScheduleRetail = "RetailEclDataPaymentSchedules";
        public const string TB_EclPaymentScheduleObe = "ObeEclDataPaymentSchedules";
        public const string TB_EclAssetbookInvestment = "InvestmentAssetBooks";

        public const string TB_TrackEclDataLoanBookException = "TrackEclDataLoanBookException";
        public const string TB_TrackEclDataPaymentScheduleException = "TrackEclDataPaymentScheduleException";
        public const string TB_TrackCalibrationPdCrDrException = "TrackCalibrationPdCrDrException";
        public const string TB_TrackCalibrationPdCommsConsException = "TrackCalibrationPdCommsConsException";
        public const string TB_TrackCalibrationBehaviouralTermException = "TrackCalibrationBehaviouralTermException";

        public const string TB_BatchEclDataLoanBooks = "BatchEclDataLoanBooks";
        public const string TB_BatchEclDataPaymentSchedules = "BatchEclDataPaymentSchedules";

        public const string TB_SUFFIX_EclFrameworkAssumptions = "EclAssumptions";
        public const string TB_SUFFIX_EclEadAssumptions = "EclEadInputAssumptions";
        public const string TB_SUFFIX_EclLgdAssumptions = "EclLgdAssumptions";
        public const string TB_SUFFIX_EclPdAssumption12Months = "EclPdAssumption12Months";
        public const string TB_SUFFIX_EclPdAssumptionMacroeconomicInputs = "EclPdAssumptionMacroeconomicInputs";
        public const string TB_SUFFIX_EclPdAssumptionMacroeconomicProjections = "EclPdAssumptionMacroeconomicProjections";
        public const string TB_SUFFIX_EclPdAssumptionNonInternalModels = "EclPdAssumptionNonInternalModels";
        public const string TB_SUFFIX_EclPdAssumptionNplIndexes = "EclPdAssumptionNplIndexes";
        public const string TB_SUFFIX_EclPdAssumptions = "EclPdAssumptions";
        public const string TB_SUFFIX_EclPdSnPCummulativeDefaultRates = "EclPdSnPCummulativeDefaultRates";
        public const string TB_SUFFIX_PdAssumptionNonInternalModels = "PdAssumptionNonInternalModels";

        public const string TB_SUFFIX_EadCirProjections = "EadCirProjections";
        public const string TB_SUFFIX_EadEirProjections = "EadEirProjections";
        public const string TB_SUFFIX_EadInputs = "EadInputs";
        public const string TB_SUFFIX_EadLifetimeProjections = "EadLifetimeProjections";
        public const string TB_SUFFIX_LGDAccountData = "LGDAccountData";
        public const string TB_SUFFIX_LGDCollateral = "LGDCollateral";
        public const string TB_SUFFIX_LgdCollateralProjection = "LgdCollateralProjection";
        public const string TB_SUFFIX_LgdCollateralTypeDatas = "LgdCollateralTypeDatas";
        public const string TB_SUFFIX_PDCreditIndex = "PDCreditIndex";
        public const string TB_SUFFIX_PdLifetimeBests = "PdLifetimeBests";
        public const string TB_SUFFIX_PdLifetimeDownturns = "PdLifetimeDownturns";
        public const string TB_SUFFIX_PdLifetimeOptimistics = "PdLifetimeOptimistics";
        public const string TB_SUFFIX_PdMappings = "PdMappings";
        public const string TB_SUFFIX_PdRedefaultLifetimeBests = "PdRedefaultLifetimeBests";
        public const string TB_SUFFIX_PdRedefaultLifetimeDownturns = "PdRedefaultLifetimeDownturns";
        public const string TB_SUFFIX_PdRedefaultLifetimeOptimistics = "PdRedefaultLifetimeOptimistics";
        public const string TB_SUFFIX_ECLFrameworkFinal = "ECLFrameworkFinal";
        public const string TB_SUFFIX_ECLFrameworkFinalOverride = "ECLFrameworkFinalOverride";
        public const string TB_SUFFIX_EclFramworkReportDetail = "EclFramworkReportDetail";

        public const string TB_SUFFIX_EclDataLoanBooks = "EclDataLoanBooks";
        public const string TB_SUFFIX_EclDataPaymentSchedules = "EclDataPaymentSchedules";

        public const string TB_INVSEC_AssetBooks = "InvestmentAssetBooks";
        public const string TB_INVSEC_DiscountFactor = "InvestmentEclDiscountFactor";
        public const string TB_INVSEC_EadInputAssumptions = "InvestmentEclEadInputAssumptions";
        public const string TB_INVSEC_EadInputs = "InvestmentEclEadInputs";
        public const string TB_INVSEC_EadLifetimes = "InvestmentEclEadLifetimes";
        public const string TB_INVSEC_FinalPostOverrideResults = "InvestmentEclFinalPostOverrideResults";
        public const string TB_INVSEC_FinalResult = "InvestmentEclFinalResult";
        public const string TB_INVSEC_LgdInputAssumptions = "InvestmentEclLgdInputAssumptions";
        public const string TB_INVSEC_MonthlyPostOverrideResults = "InvestmentEclMonthlyPostOverrideResults";
        public const string TB_INVSEC_MonthlyResults = "InvestmentEclMonthlyResults";
        public const string TB_INVSEC_PdFitchDefaultRates = "InvestmentEclPdFitchDefaultRates";
        public const string TB_INVSEC_PdInputAssumptions = "InvestmentEclPdInputAssumptions";
        public const string TB_INVSEC_PdLifetime = "InvestmentEclPdLifetime";
        public const string TB_INVSEC_Sicr = "InvestmentEclSicr";


        //Column Names
        public const string COL_CalibrationId = "CalibrationId";
        public const string COL_MacroId = "MacroId";
        public const string COL_WholesaleEclUploadId = "WholesaleEclUploadId";
        public const string COL_RetailEclUploadId = "RetailEclUploadId";
        public const string COL_ObeEclUploadId = "ObeEclUploadId";
        public const string COL_InvestmentEclUploadId = "InvestmentEclUploadId";
        public const string COL_EclId = "EclId";
        public const string COL_BatchId = "BatchId";
        public const string COL_EclUploadId = "EclUploadId";
    }
}
