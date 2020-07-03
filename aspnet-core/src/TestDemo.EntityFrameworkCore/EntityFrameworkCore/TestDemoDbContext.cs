using TestDemo.Calibration;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.InvestmentInputs;
using TestDemo.InvestmentAssumption;
using TestDemo.Investment;
using TestDemo.EclConfig;
using TestDemo.WholesaleComputatoin;
using TestDemo.LgdCalibrationResult;
using TestDemo.EadCalibrationResult;
using TestDemo.GeneralCalibrationResult;
using TestDemo.PdCalibrationResult;
using TestDemo.ObeResults;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs;
using TestDemo.ObeAssumption;
using TestDemo.OBE;
using TestDemo.RetailResults;
using TestDemo.RetailComputation;
using TestDemo.RetailInputs;
using TestDemo.RetailAssumption;
using TestDemo.Retail;
using TestDemo.WholesaleResult;
using TestDemo.WholesaleResults;
using TestDemo.WholesaleComputation;
using TestDemo.WholesaleInputs;
using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale;
using TestDemo.EclShared;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Users;
using TestDemo.Chat;
using TestDemo.Editions;
using TestDemo.Friendships;
using TestDemo.MultiTenancy;
using TestDemo.MultiTenancy.Accounting;
using TestDemo.MultiTenancy.Payments;
using TestDemo.Storage;
using TestDemo.InvestmentComputation;
using TestDemo.CalibrationInput;
using TestDemo.CalibrationResult;
using TestDemo.Calibration.Approvals;
using TestDemo.EclLibrary.Workers.Trackers;

namespace TestDemo.EntityFrameworkCore
{
    public class TestDemoDbContext : AbpZeroDbContext<Tenant, Role, User, TestDemoDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<OverrideType> OverrideTypes { get; set; }
        
        public virtual DbSet<TrackEclDataPaymentScheduleException> TrackEclDataPaymentScheduleException { get; set; }
        public virtual DbSet<TrackCalibrationBehaviouralTermException> TrackCalibrationBehaviouralTermException { get; set; }
        public virtual DbSet<TrackCalibrationPdCrDrException> TrackCalibrationPdCrDrException { get; set; }
        public virtual DbSet<TrackCalibrationUploadSummary> TrackCalibrationUploadSummary { get; set; }
        public virtual DbSet<TrackRunningUploadJobs> TrackRunningUploadJobs { get; set; }
        public virtual DbSet<TrackEclDataLoanBookException> TrackEclDataLoanBookException { get; set; }
        public virtual DbSet<TrackFacilityStage> TrackFacilityStage { get; set; }
        public virtual DbSet<TrackRunningGuidRegister> TrackRunningGuidRegister { get; set; }
        public virtual DbSet<TrackRunningIntRegister> TrackRunningIntRegister { get; set; }
        
        public virtual DbSet<RetailEclFramworkReportDetail> RetailEclFramworkReportDetail { get; set; }
        public virtual DbSet<WholesaleEclFramworkReportDetail> WholesaleEclFramworkReportDetail { get; set; }
        public virtual DbSet<ObeEclFramworkReportDetail> ObeEclFramworkReportDetail { get; set; }

        public virtual DbSet<CalibrationEadBehaviouralTermApproval> CalibrationEadBehaviouralTermApprovals { get; set; }
        public virtual DbSet<CalibrationEadCcfSummaryApproval> CalibrationEadCcfSummaryApproval { get; set; }
        public virtual DbSet<CalibrationLgdHairCutApproval> CalibrationLgdHairCutApproval { get; set; }
        public virtual DbSet<CalibrationLgdRecoveryRateApproval> CalibrationLgdRecoveryRateApproval { get; set; }
        public virtual DbSet<CalibrationPdCrDrApproval> CalibrationPdCrDrApproval { get; set; }
        public virtual DbSet<MacroAnalysisApproval> MacroAnalysisApproval { get; set; }
        public virtual DbSet<CalibrationEadBehaviouralTerm> CalibrationEadBehaviouralTerms { get; set; }
        public virtual DbSet<CalibrationEadCcfSummary> CalibrationEadCcfSummary { get; set; }
        public virtual DbSet<CalibrationLgdHairCut> CalibrationLgdHairCut { get; set; }
        public virtual DbSet<CalibrationLgdRecoveryRate> CalibrationLgdRecoveryRate { get; set; }
        public virtual DbSet<CalibrationPdCrDr> CalibrationPdCrDr { get; set; }
        public virtual DbSet<MacroAnalysis> MacroAnalysis { get; set; }

        public virtual DbSet<CalibrationHistoryPdCrDr> CalibrationHistoryPdCrDr { get; set; }
        public virtual DbSet<CalibrationHistoryLgdRecoveryRate> CalibrationHistoryLgdRecoveryRate { get; set; }
        public virtual DbSet<CalibrationHistoryLgdHairCut> CalibrationHistoryLgdHairCut { get; set; }
        public virtual DbSet<CalibrationHistoryEadCcfSummary> CalibrationHistoryEadCcfSummary { get; set; }
        public virtual DbSet<CalibrationHistoryEadBehaviouralTerms> CalibrationHistoryEadBehaviouralTerms { get; set; }

        public virtual DbSet<CalibrationInputEadBehaviouralTerms> CalibrationInputEadBehaviouralTerms { get; set; }
        public virtual DbSet<CalibrationInputEadCcfSummary> CalibrationInputEadCcfSummary { get; set; }
        public virtual DbSet<CalibrationInputLgdHairCut> CalibrationInputLgdHairCut { get; set; }
        public virtual DbSet<CalibrationInputLgdRecoveryRate> CalibrationInputLgdRecoveryRate { get; set; }
        public virtual DbSet<CalibrationInputPdCrDr> CalibrationInputPdCrDr { get; set; }
        public virtual DbSet<MacroeconomicData> MacroeconomicData { get; set; }

        public virtual DbSet<CalibrationResultEadBehaviouralTerms> CalibrationResultEadBehaviouralTerms { get; set; }
        public virtual DbSet<CalibrationResultEadCcfSummary> CalibrationResultEadCcfSummary { get; set; }
        public virtual DbSet<CalibrationResultLgdHairCut> CalibrationResultLgdHairCut { get; set; }
        public virtual DbSet<CalibrationResultLgdHairCutSummary> CalibrationResultLgdHairCutSummary { get; set; }
        public virtual DbSet<CalibrationResultLgdRecoveryRate> CalibrationResultLgdRecoveryRate { get; set; }
        public virtual DbSet<CalibrationResultPd12Months> CalibrationResult12MonthPd { get; set; }
        public virtual DbSet<CalibrationResultPd12MonthsSummary> CalibrationResultPd12MonthsSummary { get; set; }
        public virtual DbSet<MacroResult_PrincipalComponent> MacroResult_PrincipalComponent { get; set; }
        public virtual DbSet<MacroResult_Statistics> MacroResult_Statistics { get; set; }
        public virtual DbSet<MacroResult_CorMat> MacroResult_CorMat { get; set; }
        public virtual DbSet<MacroResult_IndexData> MacroResult_IndexData { get; set; }
        public virtual DbSet<MacroResult_PrincipalComponentSummary> MacroResult_PrincipalComponentSummary { get; set; }
        public virtual DbSet<MacroResult_SelectedMacroEconomicVariables> MacroResult_SelectedMacroEconomicVariables { get; set; }
        
        public virtual DbSet<WholesaleEclOverrideApproval> WholesaleEclOverrideApprovals { get; set; }
        public virtual DbSet<RetailEclOverrideApproval> RetailEclOverrideApprovals { get; set; }
        public virtual DbSet<ObeEclOverrideApproval> ObeEclOverrideApprovals { get; set; }

        public virtual DbSet<RetailEclEadLifetimeProjection> RetailEclEadLifetimeProjections { get; set; }
        public virtual DbSet<RetailEclFrameworkFinal> RetailEclFrameworkFinals { get; set; }
        public virtual DbSet<RetailEclFrameworkFinalOverride> RetailEclFrameworkFinalOverrides { get; set; }
        public virtual DbSet<RetailEclLgdCollateral> RetailEclLgdCollateral { get; set; }
        public virtual DbSet<RetailEclLgdCollateralProjection> RetailEclLgdCollateralProjection { get; set; }
        public virtual DbSet<RetailEclPdCreditIndex> RetailEclPdCreditIndex { get; set; }

        public virtual DbSet<WholesaleEclEadLifetimeProjection> WholesaleEclEadLifetimeProjections { get; set; }
        public virtual DbSet<WholesaleEclFrameworkFinal> WholesaleEclFrameworkFinals { get; set; }
        public virtual DbSet<WholesaleEclFrameworkFinalOverride> WholesaleEclFrameworkFinalOverrides { get; set; }
        public virtual DbSet<WholesaleEclLgdCollateral> WholesaleEclLgdCollateral { get; set; }
        public virtual DbSet<WholesaleEclLgdCollateralProjection> WholesaleEclLgdCollateralProjection { get; set; }
        public virtual DbSet<WholesaleEcIPdCreditIndex> WholesaleEclPdCreditIndex { get; set; }

        public virtual DbSet<ObeEclEadLifetimeProjection> ObeEclEadLifetimeProjections { get; set; }
        public virtual DbSet<ObeEclFrameworkFinal> ObeEclFrameworkFinals { get; set; }
        public virtual DbSet<ObeEclFrameworkFinalOverride> ObeEclFrameworkFinalOverrides { get; set; }
        public virtual DbSet<ObeEclLgdCollateral> ObeEclLgdCollateral { get; set; }
        public virtual DbSet<ObeEclLgdCollateralProjection> ObeEclLgdCollateralProjection { get; set; }
        public virtual DbSet<ObeEclPdCreditIndex> ObeEclPdCreditIndex { get; set; }



        public virtual DbSet<AffiliateMacroEconomicVariableOffset> AffiliateMacroEconomicVariableOffsets { get; set; }

        public virtual DbSet<Affiliate> AffiliateConfigurations { get; set; }
        public virtual DbSet<InvestmentEclFinalPostOverrideResult> InvestmentEclFinalPostOverrideResults { get; set; }
        public virtual DbSet<InvestmentEclFinalResult> InvestmentEclFinalResults { get; set; }
        public virtual DbSet<InvestmentEclMonthlyPostOverrideResult> InvestmentEclMonthlyPostOverrideResults { get; set; }
        public virtual DbSet<InvestmentEclMonthlyResult> InvestmentEclMonthlyResults { get; set; }
        public virtual DbSet<InvestmentEclPdLifetime> InvestmentEclPdLifetime { get; set; }
        public virtual DbSet<InvestmentEclOverrideApproval> InvestmentEclOverrideApprovals { get; set; }
        public virtual DbSet<InvestmentEclOverride> InvestmentEclOverrides { get; set; }
        public virtual DbSet<InvestmentEclSicr> InvestmentEclSicr { get; set; }
        public virtual DbSet<InvestmentEclDiscountFactor> InvestmentEclDiscountFactors { get; set; }
        public virtual DbSet<InvestmentEclEadLifetime> InvestmentEclEadLifetimes { get; set; }
        public virtual DbSet<InvestmentEclEadInput> InvestmentEclEadInput { get; set; }
        public virtual DbSet<InvestmentAssetBook> InvestmentAssetBooks { get; set; }
        public virtual DbSet<InvestmentEclUpload> InvestmentEclUploads { get; set; }
        public virtual DbSet<ObeEclOverride> ObeEclOverrides { get; set; }
        public virtual DbSet<WholesaleEclOverride> WholesaleEclOverrides { get; set; }
        public virtual DbSet<RetailEclOverride> RetailEclOverrides { get; set; }
        public virtual DbSet<InvestmentPdInputMacroEconomicAssumption> InvestmentPdInputMacroEconomicAssumptions { get; set; }
        public virtual DbSet<InvestmentEclPdFitchDefaultRate> InvestmentEclPdFitchDefaultRates { get; set; }
        public virtual DbSet<InvestmentEclPdInputAssumption> InvestmentEclPdInputAssumptions { get; set; }
        public virtual DbSet<InvestmentEclLgdInputAssumption> InvestmentEclLgdInputAssumptions { get; set; }
        public virtual DbSet<InvestmentEclEadInputAssumption> InvestmentEclEadInputAssumptions { get; set; }
        public virtual DbSet<InvSecFitchCummulativeDefaultRate> InvSecFitchCummulativeDefaultRates { get; set; }
        public virtual DbSet<InvSecMacroEconomicAssumption> InvSecMacroEconomicAssumptions { get; set; }
        public virtual DbSet<InvestmentEclApproval> InvestmentEclApprovals { get; set; }
        public virtual DbSet<InvestmentEcl> InvestmentEcls { get; set; }

        public virtual DbSet<EclConfiguration> EclConfigurations { get; set; }
        public virtual DbSet<AffiliateOverrideThreshold> AffiliateOverrideThresholds { get; set; }
        public virtual DbSet<AssumptionApproval> AssumptionApprovals { get; set; }
        public virtual DbSet<MacroeconomicVariable> MacroeconomicVariables { get; set; }
        public virtual DbSet<AffiliateAssumption> AffiliateAssumptions { get; set; }

        public virtual DbSet<ObeEclPdAssumptionNonInternalModel> ObeEclPdAssumptionNonInternalModels { get; set; }
        public virtual DbSet<RetailEclPdAssumptionNonInteralModel> RetailEclPdAssumptionNonInteralModels { get; set; }
        public virtual DbSet<ObeEclPdAssumptionNplIndex> ObeEclPdAssumptionNplIndexes { get; set; }

        public virtual DbSet<RetailEclPdAssumptionNplIndex> RetailEclPdAssumptionNplIndexes { get; set; }

        public virtual DbSet<ObeEclPdAssumptionMacroeconomicProjection> ObeEclPdAssumptionMacroeconomicProjections { get; set; }

        public virtual DbSet<RetailEclPdAssumptionMacroeconomicProjection> RetailEclPdAssumptionMacroeconomicProjections { get; set; }

        public virtual DbSet<ObeEclPdAssumptionMacroeconomicInputs> ObeEclPdAssumptionMacroeconomicInputses { get; set; }

        public virtual DbSet<RetailEclPdAssumptionMacroeconomicInput> RetailEclPdAssumptionMacroeconomicInputs { get; set; }

        public virtual DbSet<ObeEclPdAssumption> ObeEclPdAssumptions { get; set; }

        public virtual DbSet<RetailEclPdAssumption> RetailEclPdAssumptions { get; set; }

        public virtual DbSet<WholesaleEclPdAssumptionNplIndex> WholesaleEclPdAssumptionNplIndexes { get; set; }

        public virtual DbSet<WholesaleEclPdAssumptionNonInternalModel> WholesalePdAssumptionNonInternalModels { get; set; }

        public virtual DbSet<WholesaleEclPdAssumptionMacroeconomicProjection> WholesaleEclPdAssumptionMacroeconomicProjections { get; set; }

        public virtual DbSet<WholesaleEclPdAssumptionMacroeconomicInput> WholesaleEclPdAssumptionMacroeconomicInputs { get; set; }

        public virtual DbSet<WholesaleEclPdAssumption> WholesaleEclPdAssumptions { get; set; }

        public virtual DbSet<PdInputAssumptionMacroeconomicProjection> PdInputAssumptionMacroeconomicProjections { get; set; }

        public virtual DbSet<PdInputAssumptionNplIndex> PdInputAssumptionNplIndexes { get; set; }

        public virtual DbSet<PdInputAssumptionMacroeconomicInput> PdInputAssumptionStatisticals { get; set; }

        public virtual DbSet<PdInputAssumptionNonInternalModel> PdInputAssumptionNonInternalModels { get; set; }

        public virtual DbSet<PdInputAssumption> PdInputAssumptions { get; set; }

        public virtual DbSet<ObePdMapping> ObePdMappings { get; set; }

        public virtual DbSet<RetailPdMapping> RetailPdMappings { get; set; }

        public virtual DbSet<ObePdRedefaultLifetimeDownturn> ObePdRedefaultLifetimeDownturns { get; set; }

        public virtual DbSet<ObePdRedefaultLifetimeOptimistic> ObePdRedefaultLifetimeOptimistics { get; set; }

        public virtual DbSet<ObePdRedefaultLifetimeBest> ObePdRedefaultLifetimeBests { get; set; }

        public virtual DbSet<ObePdLifetimeDownturn> ObePdLifetimeDownturns { get; set; }

        public virtual DbSet<ObePdLifetimeOptimistic> ObePdLifetimeOptimistics { get; set; }

        public virtual DbSet<ObeLgdContractData> ObeLgdContractDatas { get; set; }

        public virtual DbSet<ObeLgdCollateralTypeData> ObeLgdCollateralTypeDatas { get; set; }

        public virtual DbSet<ObeEadInput> ObeEadInputs { get; set; }

        public virtual DbSet<ObeEadEirProjection> ObeEadEirProjections { get; set; }

        public virtual DbSet<ObeEadCirProjection> ObeEadCirProjections { get; set; }

        public virtual DbSet<ObePdLifetimeBest> ObePdLifetimeBests { get; set; }

        public virtual DbSet<RetailPdRedefaultLifetimeDownturn> RetailPdRedefaultLifetimeDownturns { get; set; }

        public virtual DbSet<RetailPdRedefaultLifetimeOptimistic> RetailPdRedefaultLifetimeOptimistics { get; set; }

        public virtual DbSet<RetailPdRedefaultLifetimeBest> RetailPdRedefaultLifetimeBests { get; set; }

        public virtual DbSet<RetailPdLifetimeDownturn> RetailPdLifetimeDownturns { get; set; }

        public virtual DbSet<RetailPdLifetimeOptimistic> RetailPdLifetimeOptimistics { get; set; }

        public virtual DbSet<RetailPdLifetimeBest> RetailPdLifetimeBests { get; set; }

        public virtual DbSet<WholesalePdRedefaultLifetimeDownturn> WholesalePdRedefaultLifetimeDownturns { get; set; }

        public virtual DbSet<WholesalePdRedefaultLifetimeOptimistic> WholesalePdRedefaultLifetimeOptimistics { get; set; }

        public virtual DbSet<WholesalePdLifetimeDownturn> WholesalePdLifetimeDownturns { get; set; }

        public virtual DbSet<WholesalePdLifetimeOptimistic> WholesalePdLifetimeOptimistics { get; set; }

        public virtual DbSet<RetailLgdContractData> RetailLgdContractDatas { get; set; }

        public virtual DbSet<RetailLgdCollateralTypeData> RetailLgdCollateralTypeDatas { get; set; }

        public virtual DbSet<RetailEadInput> RetailEadInputs { get; set; }

        public virtual DbSet<RetailEadEirProjection> RetailEadEirProjetions { get; set; }

        public virtual DbSet<RetailEadCirProjection> RetailEadCirProjections { get; set; }

        public virtual DbSet<WholesalePdRedefaultLifetimeBest> WholesalePdRedefaultLifetimes { get; set; }

        public virtual DbSet<WholesalePdLifetimeBest> WholesalePdLifetimeBests { get; set; }

        public virtual DbSet<WholesalePdMapping> WholesalePdMappings { get; set; }

        public virtual DbSet<WholesaleLgdCollateralTypeData> WholesaleLgdCollateralTypeDatas { get; set; }

        public virtual DbSet<WholesaleLgdContractData> WholesaleLgdContractDatas { get; set; }

        public virtual DbSet<WholesaleEadEirProjection> WholesaleEadEirProjections { get; set; }

        public virtual DbSet<WholesaleEadCirProjection> WholesaleEadCirProjections { get; set; }

        public virtual DbSet<WholesaleEadInput> WholesaleEadInputs { get; set; }

        public virtual DbSet<CalibrationResultLgd> CalibrationResultLgds { get; set; }

        //public virtual DbSet<CalibrationResult> CalibrationResults { get; set; }

        public virtual DbSet<CalibrationResultPdScenarioMacroeconomicProjection> PdScenarioMacroeconomicProjections { get; set; }

        public virtual DbSet<CalibrationResultPdStatisticalInput> PdStatisticalInputs { get; set; }

        public virtual DbSet<CalibrationResultPdEtiNpl> PdEtiNpls { get; set; }

        public virtual DbSet<CalibrationResultPdHistoricIndex> PdHistoricIndexes { get; set; }

        public virtual DbSet<CalibrationResultPdCummulativeSurvival> PdCummulativeSurvivals { get; set; }

        public virtual DbSet<CalibrationResultPdMarginalDefaultRate> PdMarginalDefaultRates { get; set; }

        public virtual DbSet<CalibrationResultPdUpperbound> PdUpperbounds { get; set; }

        public virtual DbSet<CalibrationResultPdSnPCummulativeDefaultRate> PdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<CalibrationResult12MonthPd> Pd12MonthPds { get; set; }

        public virtual DbSet<ObeEclResultSummaryTopExposure> ObeEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<ObeEclResultSummaryKeyInput> ObeEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<ObesaleEclResultSummary> ObesaleEclResultSummaries { get; set; }

        public virtual DbSet<ObeEclResultDetail> ObeEclResultDetails { get; set; }

        public virtual DbSet<ObeEclComputedEadResult> ObeEclComputedEadResults { get; set; }

        public virtual DbSet<ObeEclSicrApproval> ObeEclSicrApprovals { get; set; }

        public virtual DbSet<ObeEclSicr> ObeEclSicrs { get; set; }

        public virtual DbSet<ObeEclDataPaymentSchedule> ObeEclDataPaymentSchedules { get; set; }

        public virtual DbSet<ObeEclDataLoanBook> ObeEclDataLoanBooks { get; set; }

        public virtual DbSet<ObeEclUploadApproval> ObeEclUploadApprovals { get; set; }

        public virtual DbSet<ObeEclUpload> ObeEclUploads { get; set; }

        public virtual DbSet<ObeEclPdSnPCummulativeDefaultRate> ObeEclPdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<ObeEclPdAssumption12Month> ObeEclPdAssumption12Months { get; set; }

        public virtual DbSet<ObeEclLgdAssumption> ObeEclLgdAssumptions { get; set; }

        public virtual DbSet<ObeEclEadInputAssumption> ObeEclEadInputAssumptions { get; set; }

        public virtual DbSet<ObeEclAssumptionApproval> ObeEclAssumptionApprovals { get; set; }

        public virtual DbSet<ObeEclAssumption> ObeEclAssumptions { get; set; }

        public virtual DbSet<ObeEclApproval> ObeEclApprovals { get; set; }

        public virtual DbSet<ObeEcl> ObeEcls { get; set; }

        public virtual DbSet<RetailEclResultSummaryTopExposure> RetailEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<RetailEclResultSummaryKeyInput> RetailEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<RetailEclResultSummary> RetailEclResultSummaries { get; set; }

        public virtual DbSet<RetailEclResultDetail> RetailEclResultDetails { get; set; }

        public virtual DbSet<RetailEclDataPaymentSchedule> RetailEclDataPaymentSchedules { get; set; }

        public virtual DbSet<RetailEclDataLoanBook> RetailEclDataLoanBooks { get; set; }

        public virtual DbSet<RetailEclUploadApproval> RetailEclUploadApprovals { get; set; }

        public virtual DbSet<RetailEclUpload> RetailEclUploads { get; set; }

        public virtual DbSet<RetailEclPdSnPCummulativeDefaultRate> RetailEclPdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<RetailEclPdAssumption12Month> RetailEclPdAssumption12Months { get; set; }

        public virtual DbSet<RetailEclLgdAssumption> RetailEclLgdAssumptions { get; set; }

        public virtual DbSet<RetailEclEadInputAssumption> RetailEclEadInputAssumptions { get; set; }

        public virtual DbSet<RetailEclAssumptionApproval> RetailEclAssumptionApprovalses { get; set; }

        public virtual DbSet<RetailEclAssumption> RetailEclAssumptions { get; set; }

        public virtual DbSet<RetailEclApproval> RetailEclApprovals { get; set; }

        public virtual DbSet<RetailEcl> RetailEcls { get; set; }

        public virtual DbSet<WholesaleEclResultSummaryTopExposure> WholesaleEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<WholesaleEclResultSummaryKeyInput> WholesaleEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<WholesaleEclResultSummary> WholesaleEclResultSummaries { get; set; }

        public virtual DbSet<WholesaleEclResultDetail> WholesaleEclResultDetails { get; set; }

        public virtual DbSet<WholesaleEclComputedEadResult> WholesaleEclComputedEadResults { get; set; }

        public virtual DbSet<WholesaleEclSicrApproval> WholesaleEclSicrApprovals { get; set; }

        public virtual DbSet<WholesaleEclSicr> WholesaleEclSicrs { get; set; }

        public virtual DbSet<WholesaleEclDataPaymentSchedule> WholesaleEclDataPaymentSchedules { get; set; }

        public virtual DbSet<WholesaleEclDataLoanBook> WholesaleEclDataLoanBooks { get; set; }

        public virtual DbSet<WholesaleEclUploadApproval> WholesaleEclUploadApprovals { get; set; }

        public virtual DbSet<WholesaleEclUpload> WholesaleEclUploads { get; set; }


        public virtual DbSet<WholesaleEclEadInputAssumption> WholesaleEadInputAssumptions { get; set; }

        public virtual DbSet<WholesaleEclApproval> WholesaleEclApprovals { get; set; }

        public virtual DbSet<WholesaleEclAssumptionApproval> WholesaleEclAssumptionApprovals { get; set; }

        public virtual DbSet<WholesaleEclPdSnPCummulativeDefaultRate> WholesaleEclPdSnPCummulativeDefaultRateses { get; set; }

        public virtual DbSet<WholesaleEclPdAssumption12Month> WholesaleEclPdAssumption12Monthses { get; set; }

        public virtual DbSet<WholesaleEclLgdAssumption> WholesaleEclLgdAssumptions { get; set; }

        public virtual DbSet<WholesaleEclAssumption> WholesaleEclAssumptions { get; set; }

        public virtual DbSet<WholesaleEcl> WholesaleEcls { get; set; }

        public virtual DbSet<Assumption> Assumptions { get; set; }

        public virtual DbSet<PdInputAssumptionSnPCummulativeDefaultRate> PdInputSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<LgdInputAssumption> LgdAssumption { get; set; }

        public virtual DbSet<EadInputAssumption> EadInputAssumptions { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public TestDemoDbContext(DbContextOptions<TestDemoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalibrationHistoryEadBehaviouralTerms>(o => o.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()"));
            modelBuilder.Entity<CalibrationHistoryEadCcfSummary>(o => o.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()"));
            modelBuilder.Entity<CalibrationHistoryLgdHairCut>(o => o.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()"));
            modelBuilder.Entity<CalibrationHistoryLgdRecoveryRate>(o => o.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()"));
            modelBuilder.Entity<CalibrationHistoryPdCrDr>(o => o.Property(x => x.DateCreated).HasDefaultValueSql("GETDATE()"));

            //Investment
            modelBuilder.Entity<InvestmentEclMonthlyPostOverrideResult>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclFinalPostOverrideResult>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclFinalResult>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclMonthlyResult>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclEadLifetime>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclDiscountFactor>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclSicr>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<InvestmentEclPdLifetime>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));

            //Wholeslale
            modelBuilder.Entity<WholesalePdLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdRedefaultLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdRedefaultLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdRedefaultLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<WholesalePdMapping>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEadCirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEadEirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEadInput>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleLgdCollateralTypeData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleLgdContractData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclEadLifetimeProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclFrameworkFinal>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclFrameworkFinalOverride>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclLgdCollateral>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclLgdCollateralProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEcIPdCreditIndex>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<WholesaleEclFramworkReportDetail>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));

            //Retail
            modelBuilder.Entity<RetailPdLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdRedefaultLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdRedefaultLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdRedefaultLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<RetailPdMapping>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEadCirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEadEirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEadInput>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailLgdCollateralTypeData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailLgdContractData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclEadLifetimeProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclFrameworkFinal>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclFrameworkFinalOverride>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclLgdCollateral>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclLgdCollateralProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclPdCreditIndex>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<RetailEclFramworkReportDetail>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));

            //OBE
            modelBuilder.Entity<ObePdLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdRedefaultLifetimeBest>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdRedefaultLifetimeOptimistic>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdRedefaultLifetimeDownturn>(o =>
            {
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                o.Property(x => x.Value).HasDefaultValueSql("0");
            });
            modelBuilder.Entity<ObePdMapping>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEadCirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEadEirProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEadInput>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeLgdCollateralTypeData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeLgdContractData>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()")); 
            modelBuilder.Entity<ObeEclEadLifetimeProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclFrameworkFinal>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclFrameworkFinalOverride>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclLgdCollateral>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclLgdCollateralProjection>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclPdCreditIndex>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));
            modelBuilder.Entity<ObeEclFramworkReportDetail>(o => o.Property(x => x.Id).HasDefaultValueSql("NEWID()"));

            modelBuilder.Entity<ObeEclResultSummaryTopExposure>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
                o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            });
            modelBuilder.Entity<ObeEclResultSummaryKeyInput>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                           o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<ObesaleEclResultSummary>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                           o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<ObeEclResultDetail>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                           o.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<ObeEclComputedEadResult>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclSicrApproval>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclSicr>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclDataPaymentSchedule>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclDataLoanBook>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclUploadApproval>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclUpload>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEclApproval>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<ObeEcl>(o =>
                       {
                           o.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEclResultSummaryTopExposure>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                           r.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<RetailEclResultSummaryKeyInput>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                           r.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<RetailEclResultSummary>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                           r.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<RetailEclResultDetail>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                           r.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<RetailEclDataPaymentSchedule>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEclDataLoanBook>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEclUploadApproval>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEclUpload>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEclApproval>(r =>
                                  {
                                      r.HasIndex(e => new { e.TenantId });
                                  });
            modelBuilder.Entity<RetailEcl>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclResultSummaryTopExposure>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                           w.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<WholesaleEclResultSummaryKeyInput>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                           w.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<WholesaleEclResultSummary>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                           w.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<WholesaleEclResultDetail>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                           w.Property(x => x.Id).HasDefaultValueSql("NEWID()");
                       });
            modelBuilder.Entity<WholesaleEclComputedEadResult>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclSicrApproval>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclSicr>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclDataPaymentSchedule>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclDataLoanBook>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclUploadApproval>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclUpload>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclApproval>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            
            modelBuilder.Entity<WholesaleEcl>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            //modelBuilder.Entity<EadInputAssumption>(x =>
            //           {
            //               x.HasIndex(e => new { e.TenantId });
            //           });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
