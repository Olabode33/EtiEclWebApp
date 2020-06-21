using TestDemo.Calibration;
using TestDemo.InvestmentComputation;
using TestDemo.InvestmentInputs;
using TestDemo.InvestmentAssumption;
using TestDemo.Investment;
using TestDemo.EclConfig;
using TestDemo.LgdCalibrationResult;
using TestDemo.LgdCalibrationResult;
using TestDemo.EadCalibrationResult;
using TestDemo.GeneralCalibrationResult;
using TestDemo.PdCalibrationResult;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs;
using TestDemo.ObeAssumption;
using TestDemo.OBE;
using TestDemo.RetailComputation;
using TestDemo.RetailInputs;
using TestDemo.RetailAssumption;
using TestDemo.Retail;
using TestDemo.WholesaleComputation;
using TestDemo.WholesaleInputs;
using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale;
using TestDemo.EclShared;
using System;
using System.Linq;
using Abp.Organizations;
using TestDemo.Authorization.Roles;
using TestDemo.MultiTenancy;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.CalibrationInput;

namespace TestDemo.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(WholesaleEclOverrideApproval),
            typeof(RetailEclOverrideApproval),
            typeof(ObeEclOverrideApproval),
            typeof(InvestmentEclOverrideApproval),

            typeof(WholesaleEclOverride),
            typeof(RetailEclOverride),
            typeof(ObeEclOverride),
            typeof(InvestmentEclOverride),

            //typeof(WholesaleEclDataLoanBook),
            //typeof(RetailEclDataLoanBook),
            //typeof(ObeEclDataLoanBook),
            //typeof(WholesaleEclDataPaymentSchedule),
            //typeof(RetailEclDataPaymentSchedule),
            //typeof(ObeEclDataPaymentSchedule),
            //typeof(InvestmentAssetBook),

            typeof(RetailEclUpload),
            typeof(ObeEclUpload),
            typeof(WholesaleEclUpload),
            typeof(InvestmentEclUpload),

            typeof(WholesaleEclApproval),
            typeof(ObeEclApproval),
            typeof(RetailEclApproval),
            typeof(InvestmentEclApproval),

            typeof(WholesaleEcl),
            typeof(RetailEcl),
            typeof(ObeEcl),
            typeof(InvestmentEcl),

            typeof(CalibrationInputEadBehaviouralTerms),
            typeof(CalibrationInputEadCcfSummary),
            typeof(CalibrationInputLgdHairCut),
            typeof(CalibrationInputLgdRecoveryRate),
            typeof(CalibrationInputPdCrDr),
            typeof(MacroeconomicData),

            typeof(CalibrationEadBehaviouralTerm),
            typeof(CalibrationEadCcfSummary),
            typeof(CalibrationLgdHairCut),
            typeof(CalibrationLgdRecoveryRate),
            typeof(CalibrationPdCrDr),
            typeof(MacroAnalysis),

            typeof(AssumptionApproval),

            typeof(InvSecFitchCummulativeDefaultRate),
            typeof(InvSecMacroEconomicAssumption),
            typeof(PdInputAssumptionMacroeconomicInput),
            typeof(PdInputAssumptionMacroeconomicProjection),
            typeof(PdInputAssumptionNonInternalModel),
            typeof(PdInputAssumptionNplIndex),
            typeof(PdInputAssumptionSnPCummulativeDefaultRate),
            typeof(PdInputAssumption),
            typeof(LgdInputAssumption),
            typeof(EadInputAssumption),
            typeof(Assumption),

            typeof(AffiliateOverrideThreshold),
            typeof(MacroeconomicVariable),
            typeof(AffiliateMacroEconomicVariableOffset),
            typeof(EclConfiguration),

            typeof(Affiliate),
            typeof(AffiliateAssumption),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}
