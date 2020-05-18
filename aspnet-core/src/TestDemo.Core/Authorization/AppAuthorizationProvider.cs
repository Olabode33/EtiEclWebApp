using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace TestDemo.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var affiliateMacroEconomicVariableOffsets = pages.CreateChildPermission(AppPermissions.Pages_AffiliateMacroEconomicVariableOffsets, L("AffiliateMacroEconomicVariableOffsets"));
            affiliateMacroEconomicVariableOffsets.CreateChildPermission(AppPermissions.Pages_AffiliateMacroEconomicVariableOffsets_Create, L("CreateNewAffiliateMacroEconomicVariableOffset"));
            affiliateMacroEconomicVariableOffsets.CreateChildPermission(AppPermissions.Pages_AffiliateMacroEconomicVariableOffsets_Edit, L("EditAffiliateMacroEconomicVariableOffset"));
            affiliateMacroEconomicVariableOffsets.CreateChildPermission(AppPermissions.Pages_AffiliateMacroEconomicVariableOffsets_Delete, L("DeleteAffiliateMacroEconomicVariableOffset"));




            //Final Permissions List
            var assumptionsUpdate = pages.CreateChildPermission(AppPermissions.Pages_AssumptionsUpdate, L("Assumptions"));
            assumptionsUpdate.CreateChildPermission(AppPermissions.Pages_AssumptionsUpdate_Review, L("ReviewUpdatedAssumption"));

            var workspace = pages.CreateChildPermission(AppPermissions.Pages_Workspace, L("Workspace"));
            workspace.CreateChildPermission(AppPermissions.Pages_Workspace_CreateEcl, L("CreateNewEcl"));
            workspace.CreateChildPermission(AppPermissions.Pages_Workspace_Dashboard, L("ViewDashboard"));

            var eclView = pages.CreateChildPermission(AppPermissions.Pages_EclView, L("ViewEcl"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Edit, L("EditEclRecord"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Upload, L("UploadEclData"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Submit, L("SubmtEclForApproval"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Review, L("ReviewSubmittedEcl"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Run, L("RunEclComputation"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Override, L("ApplyOverride"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Override_Review, L("ReviewAppliedOverrides"));

            var configuration = pages.CreateChildPermission(AppPermissions.Pages_Configuration, L("EditEclConfiguration"));

            //Rad Permission

            var investmentEclOverrideApprovals = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrideApprovals, L("InvestmentEclOverrideApprovals"));
            investmentEclOverrideApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrideApprovals_Create, L("CreateNewInvestmentEclOverrideApproval"));
            investmentEclOverrideApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrideApprovals_Edit, L("EditInvestmentEclOverrideApproval"));
            investmentEclOverrideApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrideApprovals_Delete, L("DeleteInvestmentEclOverrideApproval"));



            var investmentEclOverrides = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrides, L("InvestmentEclOverrides"));
            investmentEclOverrides.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrides_Create, L("CreateNewInvestmentEclOverride"));
            investmentEclOverrides.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrides_Edit, L("EditInvestmentEclOverride"));
            investmentEclOverrides.CreateChildPermission(AppPermissions.Pages_InvestmentEclOverrides_Delete, L("DeleteInvestmentEclOverride"));



            var investmentAssetBooks = pages.CreateChildPermission(AppPermissions.Pages_InvestmentAssetBooks, L("InvestmentAssetBooks"));
            investmentAssetBooks.CreateChildPermission(AppPermissions.Pages_InvestmentAssetBooks_Create, L("CreateNewInvestmentAssetBook"));
            investmentAssetBooks.CreateChildPermission(AppPermissions.Pages_InvestmentAssetBooks_Edit, L("EditInvestmentAssetBook"));
            investmentAssetBooks.CreateChildPermission(AppPermissions.Pages_InvestmentAssetBooks_Delete, L("DeleteInvestmentAssetBook"));



            var investmentEclUploads = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclUploads, L("InvestmentEclUploads"));
            investmentEclUploads.CreateChildPermission(AppPermissions.Pages_InvestmentEclUploads_Create, L("CreateNewInvestmentEclUpload"));
            investmentEclUploads.CreateChildPermission(AppPermissions.Pages_InvestmentEclUploads_Edit, L("EditInvestmentEclUpload"));
            investmentEclUploads.CreateChildPermission(AppPermissions.Pages_InvestmentEclUploads_Delete, L("DeleteInvestmentEclUpload"));



            var obeEclOverrides = pages.CreateChildPermission(AppPermissions.Pages_ObeEclOverrides, L("ObeEclOverrides"));
            obeEclOverrides.CreateChildPermission(AppPermissions.Pages_ObeEclOverrides_Create, L("CreateNewObeEclOverride"));
            obeEclOverrides.CreateChildPermission(AppPermissions.Pages_ObeEclOverrides_Edit, L("EditObeEclOverride"));
            obeEclOverrides.CreateChildPermission(AppPermissions.Pages_ObeEclOverrides_Delete, L("DeleteObeEclOverride"));



            var wholesaleEclOverrides = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclOverrides, L("WholesaleEclOverrides"));
            wholesaleEclOverrides.CreateChildPermission(AppPermissions.Pages_WholesaleEclOverrides_Create, L("CreateNewWholesaleEclOverride"));
            wholesaleEclOverrides.CreateChildPermission(AppPermissions.Pages_WholesaleEclOverrides_Edit, L("EditWholesaleEclOverride"));
            wholesaleEclOverrides.CreateChildPermission(AppPermissions.Pages_WholesaleEclOverrides_Delete, L("DeleteWholesaleEclOverride"));



            var retailEclOverrides = pages.CreateChildPermission(AppPermissions.Pages_RetailEclOverrides, L("RetailEclOverrides"));
            retailEclOverrides.CreateChildPermission(AppPermissions.Pages_RetailEclOverrides_Create, L("CreateNewRetailEclOverride"));
            retailEclOverrides.CreateChildPermission(AppPermissions.Pages_RetailEclOverrides_Edit, L("EditRetailEclOverride"));
            retailEclOverrides.CreateChildPermission(AppPermissions.Pages_RetailEclOverrides_Delete, L("DeleteRetailEclOverride"));



            var investmentPdInputMacroEconomicAssumptions = pages.CreateChildPermission(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions, L("InvestmentPdInputMacroEconomicAssumptions"));
            investmentPdInputMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Create, L("CreateNewInvestmentPdInputMacroEconomicAssumption"));
            investmentPdInputMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Edit, L("EditInvestmentPdInputMacroEconomicAssumption"));
            investmentPdInputMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentPdInputMacroEconomicAssumptions_Delete, L("DeleteInvestmentPdInputMacroEconomicAssumption"));



            var investmentEclPdFitchDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates, L("InvestmentEclPdFitchDefaultRates"));
            investmentEclPdFitchDefaultRates.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Create, L("CreateNewInvestmentEclPdFitchDefaultRate"));
            investmentEclPdFitchDefaultRates.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Edit, L("EditInvestmentEclPdFitchDefaultRate"));
            investmentEclPdFitchDefaultRates.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdFitchDefaultRates_Delete, L("DeleteInvestmentEclPdFitchDefaultRate"));



            var investmentEclPdInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdInputAssumptions, L("InvestmentEclPdInputAssumptions"));
            investmentEclPdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdInputAssumptions_Create, L("CreateNewInvestmentEclPdInputAssumption"));
            investmentEclPdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdInputAssumptions_Edit, L("EditInvestmentEclPdInputAssumption"));
            investmentEclPdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclPdInputAssumptions_Delete, L("DeleteInvestmentEclPdInputAssumption"));



            var investmentEclLgdInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclLgdInputAssumptions, L("InvestmentEclLgdInputAssumptions"));
            investmentEclLgdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Create, L("CreateNewInvestmentEclLgdInputAssumption"));
            investmentEclLgdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Edit, L("EditInvestmentEclLgdInputAssumption"));
            investmentEclLgdInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclLgdInputAssumptions_Delete, L("DeleteInvestmentEclLgdInputAssumption"));



            var investmentEclEadInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclEadInputAssumptions, L("InvestmentEclEadInputAssumptions"));
            investmentEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Create, L("CreateNewInvestmentEclEadInputAssumption"));
            investmentEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Edit, L("EditInvestmentEclEadInputAssumption"));
            investmentEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_InvestmentEclEadInputAssumptions_Delete, L("DeleteInvestmentEclEadInputAssumption"));



            var invSecFitchCummulativeDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates, L("InvSecFitchCummulativeDefaultRates"));
            invSecFitchCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Create, L("CreateNewInvSecFitchCummulativeDefaultRate"));
            invSecFitchCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Edit, L("EditInvSecFitchCummulativeDefaultRate"));
            invSecFitchCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_InvSecFitchCummulativeDefaultRates_Delete, L("DeleteInvSecFitchCummulativeDefaultRate"));



            var invSecMacroEconomicAssumptions = pages.CreateChildPermission(AppPermissions.Pages_InvSecMacroEconomicAssumptions, L("InvSecMacroEconomicAssumptions"));
            invSecMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvSecMacroEconomicAssumptions_Create, L("CreateNewInvSecMacroEconomicAssumption"));
            invSecMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvSecMacroEconomicAssumptions_Edit, L("EditInvSecMacroEconomicAssumption"));
            invSecMacroEconomicAssumptions.CreateChildPermission(AppPermissions.Pages_InvSecMacroEconomicAssumptions_Delete, L("DeleteInvSecMacroEconomicAssumption"));



            var investmentEclApprovals = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEclApprovals, L("InvestmentEclApprovals"));
            investmentEclApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclApprovals_Create, L("CreateNewInvestmentEclApproval"));
            investmentEclApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclApprovals_Edit, L("EditInvestmentEclApproval"));
            investmentEclApprovals.CreateChildPermission(AppPermissions.Pages_InvestmentEclApprovals_Delete, L("DeleteInvestmentEclApproval"));



            var investmentEcls = pages.CreateChildPermission(AppPermissions.Pages_InvestmentEcls, L("InvestmentEcls"));
            investmentEcls.CreateChildPermission(AppPermissions.Pages_InvestmentEcls_Create, L("CreateNewInvestmentEcl"));
            investmentEcls.CreateChildPermission(AppPermissions.Pages_InvestmentEcls_Edit, L("EditInvestmentEcl"));
            investmentEcls.CreateChildPermission(AppPermissions.Pages_InvestmentEcls_Delete, L("DeleteInvestmentEcl"));



            var eclConfigurations = pages.CreateChildPermission(AppPermissions.Pages_EclConfigurations, L("EclConfigurations"));
            eclConfigurations.CreateChildPermission(AppPermissions.Pages_EclConfigurations_Create, L("CreateNewEclConfiguration"));
            eclConfigurations.CreateChildPermission(AppPermissions.Pages_EclConfigurations_Edit, L("EditEclConfiguration"));
            eclConfigurations.CreateChildPermission(AppPermissions.Pages_EclConfigurations_Delete, L("DeleteEclConfiguration"));



            var affiliateOverrideThresholds = pages.CreateChildPermission(AppPermissions.Pages_AffiliateOverrideThresholds, L("AffiliateOverrideThresholds"));
            affiliateOverrideThresholds.CreateChildPermission(AppPermissions.Pages_AffiliateOverrideThresholds_Create, L("CreateNewAffiliateOverrideThreshold"));
            affiliateOverrideThresholds.CreateChildPermission(AppPermissions.Pages_AffiliateOverrideThresholds_Edit, L("EditAffiliateOverrideThreshold"));
            affiliateOverrideThresholds.CreateChildPermission(AppPermissions.Pages_AffiliateOverrideThresholds_Delete, L("DeleteAffiliateOverrideThreshold"));



            var assumptionApprovals = pages.CreateChildPermission(AppPermissions.Pages_AssumptionApprovals, L("AssumptionApprovals"));
            assumptionApprovals.CreateChildPermission(AppPermissions.Pages_AssumptionApprovals_Create, L("CreateNewAssumptionApproval"));
            assumptionApprovals.CreateChildPermission(AppPermissions.Pages_AssumptionApprovals_Edit, L("EditAssumptionApproval"));
            assumptionApprovals.CreateChildPermission(AppPermissions.Pages_AssumptionApprovals_Delete, L("DeleteAssumptionApproval"));



            var macroeconomicVariables = pages.CreateChildPermission(AppPermissions.Pages_MacroeconomicVariables, L("MacroeconomicVariables"));
            macroeconomicVariables.CreateChildPermission(AppPermissions.Pages_MacroeconomicVariables_Create, L("CreateNewMacroeconomicVariable"));
            macroeconomicVariables.CreateChildPermission(AppPermissions.Pages_MacroeconomicVariables_Edit, L("EditMacroeconomicVariable"));
            macroeconomicVariables.CreateChildPermission(AppPermissions.Pages_MacroeconomicVariables_Delete, L("DeleteMacroeconomicVariable"));


            var obeEclPdAssumptionNonInternalModels = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNonInternalModels, L("ObeEclPdAssumptionNonInternalModels"));
            obeEclPdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNonInternalModels_Create, L("CreateNewObeEclPdAssumptionNonInternalModel"));
            obeEclPdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNonInternalModels_Edit, L("EditObeEclPdAssumptionNonInternalModel"));
            obeEclPdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNonInternalModels_Delete, L("DeleteObeEclPdAssumptionNonInternalModel"));



            var retailEclPdAssumptionNonInteralModels = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels, L("RetailEclPdAssumptionNonInteralModels"));
            retailEclPdAssumptionNonInteralModels.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Create, L("CreateNewRetailEclPdAssumptionNonInteralModel"));
            retailEclPdAssumptionNonInteralModels.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Edit, L("EditRetailEclPdAssumptionNonInteralModel"));
            retailEclPdAssumptionNonInteralModels.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNonInteralModels_Delete, L("DeleteRetailEclPdAssumptionNonInteralModel"));



            var obeEclPdAssumptionNplIndexes = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes, L("ObeEclPdAssumptionNplIndexes"));
            obeEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Create, L("CreateNewObeEclPdAssumptionNplIndex"));
            obeEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Edit, L("EditObeEclPdAssumptionNplIndex"));
            obeEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionNplIndexes_Delete, L("DeleteObeEclPdAssumptionNplIndex"));



            var retailEclPdAssumptionNplIndexes = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNplIndexes, L("RetailEclPdAssumptionNplIndexes"));
            retailEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNplIndexes_Create, L("CreateNewRetailEclPdAssumptionNplIndex"));
            retailEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNplIndexes_Edit, L("EditRetailEclPdAssumptionNplIndex"));
            retailEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionNplIndexes_Delete, L("DeleteRetailEclPdAssumptionNplIndex"));



            var obeEclPdAssumptionMacroeconomicProjections = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections, L("ObeEclPdAssumptionMacroeconomicProjections"));
            obeEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Create, L("CreateNewObeEclPdAssumptionMacroeconomicProjection"));
            obeEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Edit, L("EditObeEclPdAssumptionMacroeconomicProjection"));
            obeEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicProjections_Delete, L("DeleteObeEclPdAssumptionMacroeconomicProjection"));



            var retailEclPdAssumptionMacroeconomicProjections = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections, L("RetailEclPdAssumptionMacroeconomicProjections"));
            retailEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Create, L("CreateNewRetailEclPdAssumptionMacroeconomicProjection"));
            retailEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Edit, L("EditRetailEclPdAssumptionMacroeconomicProjection"));
            retailEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicProjections_Delete, L("DeleteRetailEclPdAssumptionMacroeconomicProjection"));



            var obeEclPdAssumptionMacroeconomicInputses = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicInputses, L("ObeEclPdAssumptionMacroeconomicInputses"));
            obeEclPdAssumptionMacroeconomicInputses.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicInputses_Create, L("CreateNewObeEclPdAssumptionMacroeconomicInputs"));
            obeEclPdAssumptionMacroeconomicInputses.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicInputses_Edit, L("EditObeEclPdAssumptionMacroeconomicInputs"));
            obeEclPdAssumptionMacroeconomicInputses.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptionMacroeconomicInputses_Delete, L("DeleteObeEclPdAssumptionMacroeconomicInputs"));



            var retailEclPdAssumptionMacroeconomicInputs = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs, L("RetailEclPdAssumptionMacroeconomicInputs"));
            retailEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Create, L("CreateNewRetailEclPdAssumptionMacroeconomicInput"));
            retailEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Edit, L("EditRetailEclPdAssumptionMacroeconomicInput"));
            retailEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptionMacroeconomicInputs_Delete, L("DeleteRetailEclPdAssumptionMacroeconomicInput"));



            var obeEclPdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptions, L("ObeEclPdAssumptions"));
            obeEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptions_Create, L("CreateNewObeEclPdAssumption"));
            obeEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptions_Edit, L("EditObeEclPdAssumption"));
            obeEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumptions_Delete, L("DeleteObeEclPdAssumption"));



            var retailEclPdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptions, L("RetailEclPdAssumptions"));
            retailEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptions_Create, L("CreateNewRetailEclPdAssumption"));
            retailEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptions_Edit, L("EditRetailEclPdAssumption"));
            retailEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumptions_Delete, L("DeleteRetailEclPdAssumption"));



            var wholesaleEclPdAssumptionNplIndexes = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes, L("WholesaleEclPdAssumptionNplIndexes"));
            wholesaleEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Create, L("CreateNewWholesaleEclPdAssumptionNplIndex"));
            wholesaleEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Edit, L("EditWholesaleEclPdAssumptionNplIndex"));
            wholesaleEclPdAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionNplIndexes_Delete, L("DeleteWholesaleEclPdAssumptionNplIndex"));



            var wholesalePdAssumptionNonInternalModels = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdAssumptionNonInternalModels, L("WholesalePdAssumptionNonInternalModels"));
            wholesalePdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_WholesalePdAssumptionNonInternalModels_Create, L("CreateNewWholesalePdAssumptionNonInternalModel"));
            wholesalePdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_WholesalePdAssumptionNonInternalModels_Edit, L("EditWholesalePdAssumptionNonInternalModel"));
            wholesalePdAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_WholesalePdAssumptionNonInternalModels_Delete, L("DeleteWholesalePdAssumptionNonInternalModel"));



            var wholesaleEclPdAssumptionMacroeconomicProjections = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections, L("WholesaleEclPdAssumptionMacroeconomicProjections"));
            wholesaleEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Create, L("CreateNewWholesaleEclPdAssumptionMacroeconomicProjection"));
            wholesaleEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Edit, L("EditWholesaleEclPdAssumptionMacroeconomicProjection"));
            wholesaleEclPdAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicProjections_Delete, L("DeleteWholesaleEclPdAssumptionMacroeconomicProjection"));



            var wholesaleEclPdAssumptionMacroeconomicInputs = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs, L("WholesaleEclPdAssumptionMacroeconomicInputs"));
            wholesaleEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Create, L("CreateNewWholesaleEclPdAssumptionMacroeconomicInput"));
            wholesaleEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Edit, L("EditWholesaleEclPdAssumptionMacroeconomicInput"));
            wholesaleEclPdAssumptionMacroeconomicInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptionMacroeconomicInputs_Delete, L("DeleteWholesaleEclPdAssumptionMacroeconomicInput"));



            var wholesaleEclPdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptions, L("WholesaleEclPdAssumptions"));
            wholesaleEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptions_Create, L("CreateNewWholesaleEclPdAssumption"));
            wholesaleEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptions_Edit, L("EditWholesaleEclPdAssumption"));
            wholesaleEclPdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumptions_Delete, L("DeleteWholesaleEclPdAssumption"));



            var pdInputAssumptionMacroeconomicProjections = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections, L("PdInputAssumptionMacroeconomicProjections"));
            pdInputAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Create, L("CreateNewPdInputAssumptionMacroeconomicProjection"));
            pdInputAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Edit, L("EditPdInputAssumptionMacroeconomicProjection"));
            pdInputAssumptionMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionMacroeconomicProjections_Delete, L("DeletePdInputAssumptionMacroeconomicProjection"));



            var pdInputAssumptionNplIndexes = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNplIndexes, L("PdInputAssumptionNplIndexes"));
            pdInputAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNplIndexes_Create, L("CreateNewPdInputAssumptionNplIndex"));
            pdInputAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNplIndexes_Edit, L("EditPdInputAssumptionNplIndex"));
            pdInputAssumptionNplIndexes.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNplIndexes_Delete, L("DeletePdInputAssumptionNplIndex"));



            var pdInputAssumptionStatisticals = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionStatisticals, L("PdInputAssumptionStatisticals"));
            pdInputAssumptionStatisticals.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionStatisticals_Create, L("CreateNewPdInputAssumptionStatistical"));
            pdInputAssumptionStatisticals.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionStatisticals_Edit, L("EditPdInputAssumptionStatistical"));
            pdInputAssumptionStatisticals.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionStatisticals_Delete, L("DeletePdInputAssumptionStatistical"));



            var pdInputAssumptionNonInternalModels = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNonInternalModels, L("PdInputAssumptionNonInternalModels"));
            pdInputAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Create, L("CreateNewPdInputAssumptionNonInternalModel"));
            pdInputAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Edit, L("EditPdInputAssumptionNonInternalModel"));
            pdInputAssumptionNonInternalModels.CreateChildPermission(AppPermissions.Pages_PdInputAssumptionNonInternalModels_Delete, L("DeletePdInputAssumptionNonInternalModel"));



            var pdInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumptions, L("PdInputAssumptions"));
            pdInputAssumptions.CreateChildPermission(AppPermissions.Pages_PdInputAssumptions_Create, L("CreateNewPdInputAssumption"));
            pdInputAssumptions.CreateChildPermission(AppPermissions.Pages_PdInputAssumptions_Edit, L("EditPdInputAssumption"));
            pdInputAssumptions.CreateChildPermission(AppPermissions.Pages_PdInputAssumptions_Delete, L("DeletePdInputAssumption"));



            var obePdMappings = pages.CreateChildPermission(AppPermissions.Pages_ObePdMappings, L("ObePdMappings"));
            obePdMappings.CreateChildPermission(AppPermissions.Pages_ObePdMappings_Create, L("CreateNewObePdMapping"));
            obePdMappings.CreateChildPermission(AppPermissions.Pages_ObePdMappings_Edit, L("EditObePdMapping"));
            obePdMappings.CreateChildPermission(AppPermissions.Pages_ObePdMappings_Delete, L("DeleteObePdMapping"));



            var retailPdMappings = pages.CreateChildPermission(AppPermissions.Pages_RetailPdMappings, L("RetailPdMappings"));
            retailPdMappings.CreateChildPermission(AppPermissions.Pages_RetailPdMappings_Create, L("CreateNewRetailPdMapping"));
            retailPdMappings.CreateChildPermission(AppPermissions.Pages_RetailPdMappings_Edit, L("EditRetailPdMapping"));
            retailPdMappings.CreateChildPermission(AppPermissions.Pages_RetailPdMappings_Delete, L("DeleteRetailPdMapping"));



            var obePdRedefaultLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns, L("ObePdRedefaultLifetimeDownturns"));
            obePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Create, L("CreateNewObePdRedefaultLifetimeDownturn"));
            obePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Edit, L("EditObePdRedefaultLifetimeDownturn"));
            obePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeDownturns_Delete, L("DeleteObePdRedefaultLifetimeDownturn"));



            var obePdRedefaultLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics, L("ObePdRedefaultLifetimeOptimistics"));
            obePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Create, L("CreateNewObePdRedefaultLifetimeOptimistic"));
            obePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Edit, L("EditObePdRedefaultLifetimeOptimistic"));
            obePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeOptimistics_Delete, L("DeleteObePdRedefaultLifetimeOptimistic"));



            var obePdRedefaultLifetimeBests = pages.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeBests, L("ObePdRedefaultLifetimeBests"));
            obePdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Create, L("CreateNewObePdRedefaultLifetimeBest"));
            obePdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Edit, L("EditObePdRedefaultLifetimeBest"));
            obePdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdRedefaultLifetimeBests_Delete, L("DeleteObePdRedefaultLifetimeBest"));



            var obePdLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeDownturns, L("ObePdLifetimeDownturns"));
            obePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeDownturns_Create, L("CreateNewObePdLifetimeDownturn"));
            obePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeDownturns_Edit, L("EditObePdLifetimeDownturn"));
            obePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeDownturns_Delete, L("DeleteObePdLifetimeDownturn"));



            var obePdLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeOptimistics, L("ObePdLifetimeOptimistics"));
            obePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeOptimistics_Create, L("CreateNewObePdLifetimeOptimistic"));
            obePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeOptimistics_Edit, L("EditObePdLifetimeOptimistic"));
            obePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeOptimistics_Delete, L("DeleteObePdLifetimeOptimistic"));



            var obeLgdContractDatas = pages.CreateChildPermission(AppPermissions.Pages_ObeLgdContractDatas, L("ObeLgdContractDatas"));
            obeLgdContractDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdContractDatas_Create, L("CreateNewObeLgdContractData"));
            obeLgdContractDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdContractDatas_Edit, L("EditObeLgdContractData"));
            obeLgdContractDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdContractDatas_Delete, L("DeleteObeLgdContractData"));



            var obeLgdCollateralTypeDatas = pages.CreateChildPermission(AppPermissions.Pages_ObeLgdCollateralTypeDatas, L("ObeLgdCollateralTypeDatas"));
            obeLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Create, L("CreateNewObeLgdCollateralTypeData"));
            obeLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Edit, L("EditObeLgdCollateralTypeData"));
            obeLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_ObeLgdCollateralTypeDatas_Delete, L("DeleteObeLgdCollateralTypeData"));



            var obeEadInputs = pages.CreateChildPermission(AppPermissions.Pages_ObeEadInputs, L("ObeEadInputs"));
            obeEadInputs.CreateChildPermission(AppPermissions.Pages_ObeEadInputs_Create, L("CreateNewObeEadInput"));
            obeEadInputs.CreateChildPermission(AppPermissions.Pages_ObeEadInputs_Edit, L("EditObeEadInput"));
            obeEadInputs.CreateChildPermission(AppPermissions.Pages_ObeEadInputs_Delete, L("DeleteObeEadInput"));



            var obeEadEirProjections = pages.CreateChildPermission(AppPermissions.Pages_ObeEadEirProjections, L("ObeEadEirProjections"));
            obeEadEirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadEirProjections_Create, L("CreateNewObeEadEirProjection"));
            obeEadEirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadEirProjections_Edit, L("EditObeEadEirProjection"));
            obeEadEirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadEirProjections_Delete, L("DeleteObeEadEirProjection"));



            var obeEadCirProjections = pages.CreateChildPermission(AppPermissions.Pages_ObeEadCirProjections, L("ObeEadCirProjections"));
            obeEadCirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadCirProjections_Create, L("CreateNewObeEadCirProjection"));
            obeEadCirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadCirProjections_Edit, L("EditObeEadCirProjection"));
            obeEadCirProjections.CreateChildPermission(AppPermissions.Pages_ObeEadCirProjections_Delete, L("DeleteObeEadCirProjection"));



            var obePdLifetimeBests = pages.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeBests, L("ObePdLifetimeBests"));
            obePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeBests_Create, L("CreateNewObePdLifetimeBest"));
            obePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeBests_Edit, L("EditObePdLifetimeBest"));
            obePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_ObePdLifetimeBests_Delete, L("DeleteObePdLifetimeBest"));



            var retailPdRedefaultLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns, L("RetailPdRedefaultLifetimeDownturns"));
            retailPdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Create, L("CreateNewRetailPdRedefaultLifetimeDownturn"));
            retailPdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Edit, L("EditRetailPdRedefaultLifetimeDownturn"));
            retailPdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeDownturns_Delete, L("DeleteRetailPdRedefaultLifetimeDownturn"));



            var retailPdRedefaultLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics, L("RetailPdRedefaultLifetimeOptimistics"));
            retailPdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Create, L("CreateNewRetailPdRedefaultLifetimeOptimistic"));
            retailPdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Edit, L("EditRetailPdRedefaultLifetimeOptimistic"));
            retailPdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeOptimistics_Delete, L("DeleteRetailPdRedefaultLifetimeOptimistic"));



            var retailPdRedefaultLifetimeBests = pages.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeBests, L("RetailPdRedefaultLifetimeBests"));
            retailPdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Create, L("CreateNewRetailPdRedefaultLifetimeBest"));
            retailPdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Edit, L("EditRetailPdRedefaultLifetimeBest"));
            retailPdRedefaultLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdRedefaultLifetimeBests_Delete, L("DeleteRetailPdRedefaultLifetimeBest"));



            var retailPdLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeDownturns, L("RetailPdLifetimeDownturns"));
            retailPdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeDownturns_Create, L("CreateNewRetailPdLifetimeDownturn"));
            retailPdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeDownturns_Edit, L("EditRetailPdLifetimeDownturn"));
            retailPdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeDownturns_Delete, L("DeleteRetailPdLifetimeDownturn"));



            var retailPdLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeOptimistics, L("RetailPdLifetimeOptimistics"));
            retailPdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeOptimistics_Create, L("CreateNewRetailPdLifetimeOptimistic"));
            retailPdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeOptimistics_Edit, L("EditRetailPdLifetimeOptimistic"));
            retailPdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeOptimistics_Delete, L("DeleteRetailPdLifetimeOptimistic"));



            var retailPdLifetimeBests = pages.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeBests, L("RetailPdLifetimeBests"));
            retailPdLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeBests_Create, L("CreateNewRetailPdLifetimeBest"));
            retailPdLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeBests_Edit, L("EditRetailPdLifetimeBest"));
            retailPdLifetimeBests.CreateChildPermission(AppPermissions.Pages_RetailPdLifetimeBests_Delete, L("DeleteRetailPdLifetimeBest"));



            var wholesalePdRedefaultLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns, L("WholesalePdRedefaultLifetimeDownturns"));
            wholesalePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Create, L("CreateNewWholesalePdRedefaultLifetimeDownturn"));
            wholesalePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Edit, L("EditWholesalePdRedefaultLifetimeDownturn"));
            wholesalePdRedefaultLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeDownturns_Delete, L("DeleteWholesalePdRedefaultLifetimeDownturn"));



            var wholesalePdRedefaultLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics, L("WholesalePdRedefaultLifetimeOptimistics"));
            wholesalePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Create, L("CreateNewWholesalePdRedefaultLifetimeOptimistic"));
            wholesalePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Edit, L("EditWholesalePdRedefaultLifetimeOptimistic"));
            wholesalePdRedefaultLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimeOptimistics_Delete, L("DeleteWholesalePdRedefaultLifetimeOptimistic"));



            var wholesalePdLifetimeDownturns = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeDownturns, L("WholesalePdLifetimeDownturns"));
            wholesalePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeDownturns_Create, L("CreateNewWholesalePdLifetimeDownturn"));
            wholesalePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeDownturns_Edit, L("EditWholesalePdLifetimeDownturn"));
            wholesalePdLifetimeDownturns.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeDownturns_Delete, L("DeleteWholesalePdLifetimeDownturn"));



            var wholesalePdLifetimeOptimistics = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeOptimistics, L("WholesalePdLifetimeOptimistics"));
            wholesalePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Create, L("CreateNewWholesalePdLifetimeOptimistic"));
            wholesalePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Edit, L("EditWholesalePdLifetimeOptimistic"));
            wholesalePdLifetimeOptimistics.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeOptimistics_Delete, L("DeleteWholesalePdLifetimeOptimistic"));



            var retailLgdContractDatas = pages.CreateChildPermission(AppPermissions.Pages_RetailLgdContractDatas, L("RetailLgdContractDatas"));
            retailLgdContractDatas.CreateChildPermission(AppPermissions.Pages_RetailLgdContractDatas_Create, L("CreateNewRetailLgdContractData"));
            retailLgdContractDatas.CreateChildPermission(AppPermissions.Pages_RetailLgdContractDatas_Edit, L("EditRetailLgdContractData"));
            retailLgdContractDatas.CreateChildPermission(AppPermissions.Pages_RetailLgdContractDatas_Delete, L("DeleteRetailLgdContractData"));



            var retailEadInputs = pages.CreateChildPermission(AppPermissions.Pages_RetailEadInputs, L("RetailEadInputs"));
            retailEadInputs.CreateChildPermission(AppPermissions.Pages_RetailEadInputs_Create, L("CreateNewRetailEadInput"));
            retailEadInputs.CreateChildPermission(AppPermissions.Pages_RetailEadInputs_Edit, L("EditRetailEadInput"));
            retailEadInputs.CreateChildPermission(AppPermissions.Pages_RetailEadInputs_Delete, L("DeleteRetailEadInput"));



            var retailEadEirProjetions = pages.CreateChildPermission(AppPermissions.Pages_RetailEadEirProjetions, L("RetailEadEirProjetions"));
            retailEadEirProjetions.CreateChildPermission(AppPermissions.Pages_RetailEadEirProjetions_Create, L("CreateNewRetailEadEirProjetion"));
            retailEadEirProjetions.CreateChildPermission(AppPermissions.Pages_RetailEadEirProjetions_Edit, L("EditRetailEadEirProjetion"));
            retailEadEirProjetions.CreateChildPermission(AppPermissions.Pages_RetailEadEirProjetions_Delete, L("DeleteRetailEadEirProjetion"));



            var retailEadCirProjections = pages.CreateChildPermission(AppPermissions.Pages_RetailEadCirProjections, L("RetailEadCirProjections"));
            retailEadCirProjections.CreateChildPermission(AppPermissions.Pages_RetailEadCirProjections_Create, L("CreateNewRetailEadCirProjection"));
            retailEadCirProjections.CreateChildPermission(AppPermissions.Pages_RetailEadCirProjections_Edit, L("EditRetailEadCirProjection"));
            retailEadCirProjections.CreateChildPermission(AppPermissions.Pages_RetailEadCirProjections_Delete, L("DeleteRetailEadCirProjection"));



            var wholesalePdRedefaultLifetimes = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimes, L("WholesalePdRedefaultLifetimes"));
            wholesalePdRedefaultLifetimes.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Create, L("CreateNewWholesalePdRedefaultLifetime"));
            wholesalePdRedefaultLifetimes.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Edit, L("EditWholesalePdRedefaultLifetime"));
            wholesalePdRedefaultLifetimes.CreateChildPermission(AppPermissions.Pages_WholesalePdRedefaultLifetimes_Delete, L("DeleteWholesalePdRedefaultLifetime"));



            var wholesalePdLifetimeBests = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeBests, L("WholesalePdLifetimeBests"));
            wholesalePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeBests_Create, L("CreateNewWholesalePdLifetimeBest"));
            wholesalePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeBests_Edit, L("EditWholesalePdLifetimeBest"));
            wholesalePdLifetimeBests.CreateChildPermission(AppPermissions.Pages_WholesalePdLifetimeBests_Delete, L("DeleteWholesalePdLifetimeBest"));



            var wholesalePdMappings = pages.CreateChildPermission(AppPermissions.Pages_WholesalePdMappings, L("WholesalePdMappings"));
            wholesalePdMappings.CreateChildPermission(AppPermissions.Pages_WholesalePdMappings_Create, L("CreateNewWholesalePdMapping"));
            wholesalePdMappings.CreateChildPermission(AppPermissions.Pages_WholesalePdMappings_Edit, L("EditWholesalePdMapping"));
            wholesalePdMappings.CreateChildPermission(AppPermissions.Pages_WholesalePdMappings_Delete, L("DeleteWholesalePdMapping"));



            var wholesaleLgdCollateralTypeDatas = pages.CreateChildPermission(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas, L("WholesaleLgdCollateralTypeDatas"));
            wholesaleLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Create, L("CreateNewWholesaleLgdCollateralTypeData"));
            wholesaleLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Edit, L("EditWholesaleLgdCollateralTypeData"));
            wholesaleLgdCollateralTypeDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdCollateralTypeDatas_Delete, L("DeleteWholesaleLgdCollateralTypeData"));



            var wholesaleLgdContractDatas = pages.CreateChildPermission(AppPermissions.Pages_WholesaleLgdContractDatas, L("WholesaleLgdContractDatas"));
            wholesaleLgdContractDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdContractDatas_Create, L("CreateNewWholesaleLgdContractData"));
            wholesaleLgdContractDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdContractDatas_Edit, L("EditWholesaleLgdContractData"));
            wholesaleLgdContractDatas.CreateChildPermission(AppPermissions.Pages_WholesaleLgdContractDatas_Delete, L("DeleteWholesaleLgdContractData"));



            var wholesaleEadEirProjections = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEadEirProjections, L("WholesaleEadEirProjections"));
            wholesaleEadEirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadEirProjections_Create, L("CreateNewWholesaleEadEirProjection"));
            wholesaleEadEirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadEirProjections_Edit, L("EditWholesaleEadEirProjection"));
            wholesaleEadEirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadEirProjections_Delete, L("DeleteWholesaleEadEirProjection"));



            var wholesaleEadCirProjections = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEadCirProjections, L("WholesaleEadCirProjections"));
            wholesaleEadCirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadCirProjections_Create, L("CreateNewWholesaleEadCirProjection"));
            wholesaleEadCirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadCirProjections_Edit, L("EditWholesaleEadCirProjection"));
            wholesaleEadCirProjections.CreateChildPermission(AppPermissions.Pages_WholesaleEadCirProjections_Delete, L("DeleteWholesaleEadCirProjection"));



            var wholesaleEadInputs = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputs, L("WholesaleEadInputs"));
            wholesaleEadInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputs_Create, L("CreateNewWholesaleEadInput"));
            wholesaleEadInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputs_Edit, L("EditWholesaleEadInput"));
            wholesaleEadInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputs_Delete, L("DeleteWholesaleEadInput"));



            var calibrationResultLgds = pages.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgds, L("CalibrationResultLgds"));
            calibrationResultLgds.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgds_Create, L("CreateNewCalibrationResultLgd"));
            calibrationResultLgds.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgds_Edit, L("EditCalibrationResultLgd"));
            calibrationResultLgds.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgds_Delete, L("DeleteCalibrationResultLgd"));



            var calibrationResultLgdCureRates = pages.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgdCureRates, L("CalibrationResultLgdCureRates"));
            calibrationResultLgdCureRates.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgdCureRates_Create, L("CreateNewCalibrationResultLgdCureRate"));
            calibrationResultLgdCureRates.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgdCureRates_Edit, L("EditCalibrationResultLgdCureRate"));
            calibrationResultLgdCureRates.CreateChildPermission(AppPermissions.Pages_CalibrationResultLgdCureRates_Delete, L("DeleteCalibrationResultLgdCureRate"));



            var eadCirProjections = pages.CreateChildPermission(AppPermissions.Pages_EadCirProjections, L("EadCirProjections"));
            eadCirProjections.CreateChildPermission(AppPermissions.Pages_EadCirProjections_Create, L("CreateNewEadCirProjection"));
            eadCirProjections.CreateChildPermission(AppPermissions.Pages_EadCirProjections_Edit, L("EditEadCirProjection"));
            eadCirProjections.CreateChildPermission(AppPermissions.Pages_EadCirProjections_Delete, L("DeleteEadCirProjection"));



            var calibrationResults = pages.CreateChildPermission(AppPermissions.Pages_CalibrationResults, L("CalibrationResults"));
            calibrationResults.CreateChildPermission(AppPermissions.Pages_CalibrationResults_Create, L("CreateNewCalibrationResult"));
            calibrationResults.CreateChildPermission(AppPermissions.Pages_CalibrationResults_Edit, L("EditCalibrationResult"));
            calibrationResults.CreateChildPermission(AppPermissions.Pages_CalibrationResults_Delete, L("DeleteCalibrationResult"));



            var pdScenarioMacroeconomicProjections = pages.CreateChildPermission(AppPermissions.Pages_PdScenarioMacroeconomicProjections, L("PdScenarioMacroeconomicProjections"));
            pdScenarioMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Create, L("CreateNewPdScenarioMacroeconomicProjection"));
            pdScenarioMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Edit, L("EditPdScenarioMacroeconomicProjection"));
            pdScenarioMacroeconomicProjections.CreateChildPermission(AppPermissions.Pages_PdScenarioMacroeconomicProjections_Delete, L("DeletePdScenarioMacroeconomicProjection"));



            var pdStatisticalInputs = pages.CreateChildPermission(AppPermissions.Pages_PdStatisticalInputs, L("PdStatisticalInputs"));
            pdStatisticalInputs.CreateChildPermission(AppPermissions.Pages_PdStatisticalInputs_Create, L("CreateNewPdStatisticalInput"));
            pdStatisticalInputs.CreateChildPermission(AppPermissions.Pages_PdStatisticalInputs_Edit, L("EditPdStatisticalInput"));
            pdStatisticalInputs.CreateChildPermission(AppPermissions.Pages_PdStatisticalInputs_Delete, L("DeletePdStatisticalInput"));



            var pdEtiNpls = pages.CreateChildPermission(AppPermissions.Pages_PdEtiNpls, L("PdEtiNpls"));
            pdEtiNpls.CreateChildPermission(AppPermissions.Pages_PdEtiNpls_Create, L("CreateNewPdEtiNpl"));
            pdEtiNpls.CreateChildPermission(AppPermissions.Pages_PdEtiNpls_Edit, L("EditPdEtiNpl"));
            pdEtiNpls.CreateChildPermission(AppPermissions.Pages_PdEtiNpls_Delete, L("DeletePdEtiNpl"));



            var pdHistoricIndexes = pages.CreateChildPermission(AppPermissions.Pages_PdHistoricIndexes, L("PdHistoricIndexes"));
            pdHistoricIndexes.CreateChildPermission(AppPermissions.Pages_PdHistoricIndexes_Create, L("CreateNewPdHistoricIndex"));
            pdHistoricIndexes.CreateChildPermission(AppPermissions.Pages_PdHistoricIndexes_Edit, L("EditPdHistoricIndex"));
            pdHistoricIndexes.CreateChildPermission(AppPermissions.Pages_PdHistoricIndexes_Delete, L("DeletePdHistoricIndex"));



            var pdCummulativeSurvivals = pages.CreateChildPermission(AppPermissions.Pages_PdCummulativeSurvivals, L("PdCummulativeSurvivals"));
            pdCummulativeSurvivals.CreateChildPermission(AppPermissions.Pages_PdCummulativeSurvivals_Create, L("CreateNewPdCummulativeSurvival"));
            pdCummulativeSurvivals.CreateChildPermission(AppPermissions.Pages_PdCummulativeSurvivals_Edit, L("EditPdCummulativeSurvival"));
            pdCummulativeSurvivals.CreateChildPermission(AppPermissions.Pages_PdCummulativeSurvivals_Delete, L("DeletePdCummulativeSurvival"));



            var pdMarginalDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_PdMarginalDefaultRates, L("PdMarginalDefaultRates"));
            pdMarginalDefaultRates.CreateChildPermission(AppPermissions.Pages_PdMarginalDefaultRates_Create, L("CreateNewPdMarginalDefaultRate"));
            pdMarginalDefaultRates.CreateChildPermission(AppPermissions.Pages_PdMarginalDefaultRates_Edit, L("EditPdMarginalDefaultRate"));
            pdMarginalDefaultRates.CreateChildPermission(AppPermissions.Pages_PdMarginalDefaultRates_Delete, L("DeletePdMarginalDefaultRate"));



            var pdUpperbounds = pages.CreateChildPermission(AppPermissions.Pages_PdUpperbounds, L("PdUpperbounds"));
            pdUpperbounds.CreateChildPermission(AppPermissions.Pages_PdUpperbounds_Create, L("CreateNewPdUpperbound"));
            pdUpperbounds.CreateChildPermission(AppPermissions.Pages_PdUpperbounds_Edit, L("EditPdUpperbound"));
            pdUpperbounds.CreateChildPermission(AppPermissions.Pages_PdUpperbounds_Delete, L("DeletePdUpperbound"));



            var pdSnPCummulativeDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_PdSnPCummulativeDefaultRates, L("PdSnPCummulativeDefaultRates"));
            pdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Create, L("CreateNewPdSnPCummulativeDefaultRate"));
            pdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Edit, L("EditPdSnPCummulativeDefaultRate"));
            pdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdSnPCummulativeDefaultRates_Delete, L("DeletePdSnPCummulativeDefaultRate"));



            var pd12MonthPds = pages.CreateChildPermission(AppPermissions.Pages_Pd12MonthPds, L("Pd12MonthPds"));
            pd12MonthPds.CreateChildPermission(AppPermissions.Pages_Pd12MonthPds_Create, L("CreateNewPd12MonthPd"));
            pd12MonthPds.CreateChildPermission(AppPermissions.Pages_Pd12MonthPds_Edit, L("EditPd12MonthPd"));
            pd12MonthPds.CreateChildPermission(AppPermissions.Pages_Pd12MonthPds_Delete, L("DeletePd12MonthPd"));



            var obeEclResultSummaryTopExposures = pages.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryTopExposures, L("ObeEclResultSummaryTopExposures"));
            obeEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Create, L("CreateNewObeEclResultSummaryTopExposure"));
            obeEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Edit, L("EditObeEclResultSummaryTopExposure"));
            obeEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryTopExposures_Delete, L("DeleteObeEclResultSummaryTopExposure"));



            var obeEclResultSummaryKeyInputs = pages.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryKeyInputs, L("ObeEclResultSummaryKeyInputs"));
            obeEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Create, L("CreateNewObeEclResultSummaryKeyInput"));
            obeEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Edit, L("EditObeEclResultSummaryKeyInput"));
            obeEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_ObeEclResultSummaryKeyInputs_Delete, L("DeleteObeEclResultSummaryKeyInput"));



            var obesaleEclResultSummaries = pages.CreateChildPermission(AppPermissions.Pages_ObesaleEclResultSummaries, L("ObesaleEclResultSummaries"));
            obesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_ObesaleEclResultSummaries_Create, L("CreateNewObesaleEclResultSummary"));
            obesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_ObesaleEclResultSummaries_Edit, L("EditObesaleEclResultSummary"));
            obesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_ObesaleEclResultSummaries_Delete, L("DeleteObesaleEclResultSummary"));



            var obeEclResultDetails = pages.CreateChildPermission(AppPermissions.Pages_ObeEclResultDetails, L("ObeEclResultDetails"));
            obeEclResultDetails.CreateChildPermission(AppPermissions.Pages_ObeEclResultDetails_Create, L("CreateNewObeEclResultDetail"));
            obeEclResultDetails.CreateChildPermission(AppPermissions.Pages_ObeEclResultDetails_Edit, L("EditObeEclResultDetail"));
            obeEclResultDetails.CreateChildPermission(AppPermissions.Pages_ObeEclResultDetails_Delete, L("DeleteObeEclResultDetail"));



            var obeEclComputedEadResults = pages.CreateChildPermission(AppPermissions.Pages_ObeEclComputedEadResults, L("ObeEclComputedEadResults"));
            obeEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_ObeEclComputedEadResults_Create, L("CreateNewObeEclComputedEadResult"));
            obeEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_ObeEclComputedEadResults_Edit, L("EditObeEclComputedEadResult"));
            obeEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_ObeEclComputedEadResults_Delete, L("DeleteObeEclComputedEadResult"));



            var obeEclSicrApprovals = pages.CreateChildPermission(AppPermissions.Pages_ObeEclSicrApprovals, L("ObeEclSicrApprovals"));
            obeEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclSicrApprovals_Create, L("CreateNewObeEclSicrApproval"));
            obeEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclSicrApprovals_Edit, L("EditObeEclSicrApproval"));
            obeEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclSicrApprovals_Delete, L("DeleteObeEclSicrApproval"));



            var obeEclSicrs = pages.CreateChildPermission(AppPermissions.Pages_ObeEclSicrs, L("ObeEclSicrs"));
            obeEclSicrs.CreateChildPermission(AppPermissions.Pages_ObeEclSicrs_Create, L("CreateNewObeEclSicr"));
            obeEclSicrs.CreateChildPermission(AppPermissions.Pages_ObeEclSicrs_Edit, L("EditObeEclSicr"));
            obeEclSicrs.CreateChildPermission(AppPermissions.Pages_ObeEclSicrs_Delete, L("DeleteObeEclSicr"));



            var obeEclDataPaymentSchedules = pages.CreateChildPermission(AppPermissions.Pages_ObeEclDataPaymentSchedules, L("ObeEclDataPaymentSchedules"));
            obeEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_ObeEclDataPaymentSchedules_Create, L("CreateNewObeEclDataPaymentSchedule"));
            obeEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_ObeEclDataPaymentSchedules_Edit, L("EditObeEclDataPaymentSchedule"));
            obeEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_ObeEclDataPaymentSchedules_Delete, L("DeleteObeEclDataPaymentSchedule"));



            var obeEclDataLoanBooks = pages.CreateChildPermission(AppPermissions.Pages_ObeEclDataLoanBooks, L("ObeEclDataLoanBooks"));
            obeEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_ObeEclDataLoanBooks_Create, L("CreateNewObeEclDataLoanBook"));
            obeEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_ObeEclDataLoanBooks_Edit, L("EditObeEclDataLoanBook"));
            obeEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_ObeEclDataLoanBooks_Delete, L("DeleteObeEclDataLoanBook"));



            var obeEclUploadApprovals = pages.CreateChildPermission(AppPermissions.Pages_ObeEclUploadApprovals, L("ObeEclUploadApprovals"));
            obeEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclUploadApprovals_Create, L("CreateNewObeEclUploadApproval"));
            obeEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclUploadApprovals_Edit, L("EditObeEclUploadApproval"));
            obeEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclUploadApprovals_Delete, L("DeleteObeEclUploadApproval"));



            var obeEclUploads = pages.CreateChildPermission(AppPermissions.Pages_ObeEclUploads, L("ObeEclUploads"));
            obeEclUploads.CreateChildPermission(AppPermissions.Pages_ObeEclUploads_Create, L("CreateNewObeEclUpload"));
            obeEclUploads.CreateChildPermission(AppPermissions.Pages_ObeEclUploads_Edit, L("EditObeEclUpload"));
            obeEclUploads.CreateChildPermission(AppPermissions.Pages_ObeEclUploads_Delete, L("DeleteObeEclUpload"));



            var obeEclPdSnPCummulativeDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates, L("ObeEclPdSnPCummulativeDefaultRates"));
            obeEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Create, L("CreateNewObeEclPdSnPCummulativeDefaultRate"));
            obeEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Edit, L("EditObeEclPdSnPCummulativeDefaultRate"));
            obeEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_ObeEclPdSnPCummulativeDefaultRates_Delete, L("DeleteObeEclPdSnPCummulativeDefaultRate"));



            var obeEclPdAssumption12Months = pages.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumption12Months, L("ObeEclPdAssumption12Months"));
            obeEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumption12Months_Create, L("CreateNewObeEclPdAssumption12Month"));
            obeEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumption12Months_Edit, L("EditObeEclPdAssumption12Month"));
            obeEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_ObeEclPdAssumption12Months_Delete, L("DeleteObeEclPdAssumption12Month"));



            var obeEclLgdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_ObeEclLgdAssumptions, L("ObeEclLgdAssumptions"));
            obeEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclLgdAssumptions_Create, L("CreateNewObeEclLgdAssumption"));
            obeEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclLgdAssumptions_Edit, L("EditObeEclLgdAssumption"));
            obeEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclLgdAssumptions_Delete, L("DeleteObeEclLgdAssumption"));



            var obeEclEadInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_ObeEclEadInputAssumptions, L("ObeEclEadInputAssumptions"));
            obeEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclEadInputAssumptions_Create, L("CreateNewObeEclEadInputAssumption"));
            obeEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclEadInputAssumptions_Edit, L("EditObeEclEadInputAssumption"));
            obeEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclEadInputAssumptions_Delete, L("DeleteObeEclEadInputAssumption"));



            var obeEclAssumptionApprovals = pages.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptionApprovals, L("ObeEclAssumptionApprovals"));
            obeEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptionApprovals_Create, L("CreateNewObeEclAssumptionApproval"));
            obeEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptionApprovals_Edit, L("EditObeEclAssumptionApproval"));
            obeEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptionApprovals_Delete, L("DeleteObeEclAssumptionApproval"));



            var obeEclAssumptions = pages.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptions, L("ObeEclAssumptions"));
            obeEclAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptions_Create, L("CreateNewObeEclAssumption"));
            obeEclAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptions_Edit, L("EditObeEclAssumption"));
            obeEclAssumptions.CreateChildPermission(AppPermissions.Pages_ObeEclAssumptions_Delete, L("DeleteObeEclAssumption"));



            var obeEclApprovals = pages.CreateChildPermission(AppPermissions.Pages_ObeEclApprovals, L("ObeEclApprovals"));
            obeEclApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclApprovals_Create, L("CreateNewObeEclApproval"));
            obeEclApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclApprovals_Edit, L("EditObeEclApproval"));
            obeEclApprovals.CreateChildPermission(AppPermissions.Pages_ObeEclApprovals_Delete, L("DeleteObeEclApproval"));



            var obeEcls = pages.CreateChildPermission(AppPermissions.Pages_ObeEcls, L("ObeEcls"));
            obeEcls.CreateChildPermission(AppPermissions.Pages_ObeEcls_Create, L("CreateNewObeEcl"));
            obeEcls.CreateChildPermission(AppPermissions.Pages_ObeEcls_Edit, L("EditObeEcl"));
            obeEcls.CreateChildPermission(AppPermissions.Pages_ObeEcls_Delete, L("DeleteObeEcl"));



            var retailEclResultSummaryTopExposures = pages.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryTopExposures, L("RetailEclResultSummaryTopExposures"));
            retailEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryTopExposures_Create, L("CreateNewRetailEclResultSummaryTopExposure"));
            retailEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryTopExposures_Edit, L("EditRetailEclResultSummaryTopExposure"));
            retailEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryTopExposures_Delete, L("DeleteRetailEclResultSummaryTopExposure"));



            var retailEclResultSummaryKeyInputs = pages.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryKeyInputs, L("RetailEclResultSummaryKeyInputs"));
            retailEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Create, L("CreateNewRetailEclResultSummaryKeyInput"));
            retailEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Edit, L("EditRetailEclResultSummaryKeyInput"));
            retailEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaryKeyInputs_Delete, L("DeleteRetailEclResultSummaryKeyInput"));



            var retailEclResultSummaries = pages.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaries, L("RetailEclResultSummaries"));
            retailEclResultSummaries.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaries_Create, L("CreateNewRetailEclResultSummary"));
            retailEclResultSummaries.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaries_Edit, L("EditRetailEclResultSummary"));
            retailEclResultSummaries.CreateChildPermission(AppPermissions.Pages_RetailEclResultSummaries_Delete, L("DeleteRetailEclResultSummary"));



            var retailEclResultDetails = pages.CreateChildPermission(AppPermissions.Pages_RetailEclResultDetails, L("RetailEclResultDetails"));
            retailEclResultDetails.CreateChildPermission(AppPermissions.Pages_RetailEclResultDetails_Create, L("CreateNewRetailEclResultDetail"));
            retailEclResultDetails.CreateChildPermission(AppPermissions.Pages_RetailEclResultDetails_Edit, L("EditRetailEclResultDetail"));
            retailEclResultDetails.CreateChildPermission(AppPermissions.Pages_RetailEclResultDetails_Delete, L("DeleteRetailEclResultDetail"));



            var retailEclComputedEadResults = pages.CreateChildPermission(AppPermissions.Pages_RetailEclComputedEadResults, L("RetailEclComputedEadResults"));
            retailEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_RetailEclComputedEadResults_Create, L("CreateNewRetailEclComputedEadResult"));
            retailEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_RetailEclComputedEadResults_Edit, L("EditRetailEclComputedEadResult"));
            retailEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_RetailEclComputedEadResults_Delete, L("DeleteRetailEclComputedEadResult"));



            var retailEclSicrApprovals = pages.CreateChildPermission(AppPermissions.Pages_RetailEclSicrApprovals, L("RetailEclSicrApprovals"));
            retailEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclSicrApprovals_Create, L("CreateNewRetailEclSicrApproval"));
            retailEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclSicrApprovals_Edit, L("EditRetailEclSicrApproval"));
            retailEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclSicrApprovals_Delete, L("DeleteRetailEclSicrApproval"));



            var retailEclSicrs = pages.CreateChildPermission(AppPermissions.Pages_RetailEclSicrs, L("RetailEclSicrs"));
            retailEclSicrs.CreateChildPermission(AppPermissions.Pages_RetailEclSicrs_Create, L("CreateNewRetailEclSicr"));
            retailEclSicrs.CreateChildPermission(AppPermissions.Pages_RetailEclSicrs_Edit, L("EditRetailEclSicr"));
            retailEclSicrs.CreateChildPermission(AppPermissions.Pages_RetailEclSicrs_Delete, L("DeleteRetailEclSicr"));



            var retailEclDataPaymentSchedules = pages.CreateChildPermission(AppPermissions.Pages_RetailEclDataPaymentSchedules, L("RetailEclDataPaymentSchedules"));
            retailEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_RetailEclDataPaymentSchedules_Create, L("CreateNewRetailEclDataPaymentSchedule"));
            retailEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_RetailEclDataPaymentSchedules_Edit, L("EditRetailEclDataPaymentSchedule"));
            retailEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_RetailEclDataPaymentSchedules_Delete, L("DeleteRetailEclDataPaymentSchedule"));



            var retailEclDataLoanBooks = pages.CreateChildPermission(AppPermissions.Pages_RetailEclDataLoanBooks, L("RetailEclDataLoanBooks"));
            retailEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_RetailEclDataLoanBooks_Create, L("CreateNewRetailEclDataLoanBook"));
            retailEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_RetailEclDataLoanBooks_Edit, L("EditRetailEclDataLoanBook"));
            retailEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_RetailEclDataLoanBooks_Delete, L("DeleteRetailEclDataLoanBook"));



            var retailEclUploadApprovals = pages.CreateChildPermission(AppPermissions.Pages_RetailEclUploadApprovals, L("RetailEclUploadApprovals"));
            retailEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclUploadApprovals_Create, L("CreateNewRetailEclUploadApproval"));
            retailEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclUploadApprovals_Edit, L("EditRetailEclUploadApproval"));
            retailEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclUploadApprovals_Delete, L("DeleteRetailEclUploadApproval"));



            var retailEclUploads = pages.CreateChildPermission(AppPermissions.Pages_RetailEclUploads, L("RetailEclUploads"));
            retailEclUploads.CreateChildPermission(AppPermissions.Pages_RetailEclUploads_Create, L("CreateNewRetailEclUpload"));
            retailEclUploads.CreateChildPermission(AppPermissions.Pages_RetailEclUploads_Edit, L("EditRetailEclUpload"));
            retailEclUploads.CreateChildPermission(AppPermissions.Pages_RetailEclUploads_Delete, L("DeleteRetailEclUpload"));



            var retailEclPdSnPCummulativeDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates, L("RetailEclPdSnPCummulativeDefaultRates"));
            retailEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Create, L("CreateNewRetailEclPdSnPCummulativeDefaultRate"));
            retailEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Edit, L("EditRetailEclPdSnPCummulativeDefaultRate"));
            retailEclPdSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_RetailEclPdSnPCummulativeDefaultRates_Delete, L("DeleteRetailEclPdSnPCummulativeDefaultRate"));



            var retailEclPdAssumption12Months = pages.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumption12Months, L("RetailEclPdAssumption12Months"));
            retailEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumption12Months_Create, L("CreateNewRetailEclPdAssumption12Month"));
            retailEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumption12Months_Edit, L("EditRetailEclPdAssumption12Month"));
            retailEclPdAssumption12Months.CreateChildPermission(AppPermissions.Pages_RetailEclPdAssumption12Months_Delete, L("DeleteRetailEclPdAssumption12Month"));



            var retailEclLgdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_RetailEclLgdAssumptions, L("RetailEclLgdAssumptions"));
            retailEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclLgdAssumptions_Create, L("CreateNewRetailEclLgdAssumption"));
            retailEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclLgdAssumptions_Edit, L("EditRetailEclLgdAssumption"));
            retailEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclLgdAssumptions_Delete, L("DeleteRetailEclLgdAssumption"));



            var retailEclEadInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_RetailEclEadInputAssumptions, L("RetailEclEadInputAssumptions"));
            retailEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclEadInputAssumptions_Create, L("CreateNewRetailEclEadInputAssumption"));
            retailEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclEadInputAssumptions_Edit, L("EditRetailEclEadInputAssumption"));
            retailEclEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclEadInputAssumptions_Delete, L("DeleteRetailEclEadInputAssumption"));



            var retailEclAssumptionApprovalses = pages.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptionApprovalses, L("RetailEclAssumptionApprovalses"));
            retailEclAssumptionApprovalses.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptionApprovalses_Create, L("CreateNewRetailEclAssumptionApprovals"));
            retailEclAssumptionApprovalses.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptionApprovalses_Edit, L("EditRetailEclAssumptionApprovals"));
            retailEclAssumptionApprovalses.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptionApprovalses_Delete, L("DeleteRetailEclAssumptionApprovals"));



            var retailEclAssumptions = pages.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptions, L("RetailEclAssumptions"));
            retailEclAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptions_Create, L("CreateNewRetailEclAssumption"));
            retailEclAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptions_Edit, L("EditRetailEclAssumption"));
            retailEclAssumptions.CreateChildPermission(AppPermissions.Pages_RetailEclAssumptions_Delete, L("DeleteRetailEclAssumption"));



            var retailEclApprovals = pages.CreateChildPermission(AppPermissions.Pages_RetailEclApprovals, L("RetailEclApprovals"));
            retailEclApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclApprovals_Create, L("CreateNewRetailEclApproval"));
            retailEclApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclApprovals_Edit, L("EditRetailEclApproval"));
            retailEclApprovals.CreateChildPermission(AppPermissions.Pages_RetailEclApprovals_Delete, L("DeleteRetailEclApproval"));



            var retailEcls = pages.CreateChildPermission(AppPermissions.Pages_RetailEcls, L("RetailEcls"));
            retailEcls.CreateChildPermission(AppPermissions.Pages_RetailEcls_Create, L("CreateNewRetailEcl"));
            retailEcls.CreateChildPermission(AppPermissions.Pages_RetailEcls_Edit, L("EditRetailEcl"));
            retailEcls.CreateChildPermission(AppPermissions.Pages_RetailEcls_Delete, L("DeleteRetailEcl"));



            var wholesaleEclResultSummaryTopExposures = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures, L("WholesaleEclResultSummaryTopExposures"));
            wholesaleEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Create, L("CreateNewWholesaleEclResultSummaryTopExposure"));
            wholesaleEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Edit, L("EditWholesaleEclResultSummaryTopExposure"));
            wholesaleEclResultSummaryTopExposures.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryTopExposures_Delete, L("DeleteWholesaleEclResultSummaryTopExposure"));



            var wholesaleEclResultSummaryKeyInputs = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryKeyInputs, L("WholesaleEclResultSummaryKeyInputs"));
            wholesaleEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryKeyInputs_Create, L("CreateNewWholesaleEclResultSummaryKeyInput"));
            wholesaleEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryKeyInputs_Edit, L("EditWholesaleEclResultSummaryKeyInput"));
            wholesaleEclResultSummaryKeyInputs.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaryKeyInputs_Delete, L("DeleteWholesaleEclResultSummaryKeyInput"));



            var wholesaleEclResultSummaries = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaries, L("WholesaleEclResultSummaries"));
            wholesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaries_Create, L("CreateNewWholesaleEclResultSummary"));
            wholesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaries_Edit, L("EditWholesaleEclResultSummary"));
            wholesaleEclResultSummaries.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultSummaries_Delete, L("DeleteWholesaleEclResultSummary"));



            var wholesaleEclResultDetails = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultDetails, L("WholesaleEclResultDetails"));
            wholesaleEclResultDetails.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultDetails_Create, L("CreateNewWholesaleEclResultDetail"));
            wholesaleEclResultDetails.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultDetails_Edit, L("EditWholesaleEclResultDetail"));
            wholesaleEclResultDetails.CreateChildPermission(AppPermissions.Pages_WholesaleEclResultDetails_Delete, L("DeleteWholesaleEclResultDetail"));



            var wholesaleEclComputedEadResults = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclComputedEadResults, L("WholesaleEclComputedEadResults"));
            wholesaleEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_WholesaleEclComputedEadResults_Create, L("CreateNewWholesaleEclComputedEadResult"));
            wholesaleEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_WholesaleEclComputedEadResults_Edit, L("EditWholesaleEclComputedEadResult"));
            wholesaleEclComputedEadResults.CreateChildPermission(AppPermissions.Pages_WholesaleEclComputedEadResults_Delete, L("DeleteWholesaleEclComputedEadResult"));



            var wholesaleEclSicrApprovals = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrApprovals, L("WholesaleEclSicrApprovals"));
            wholesaleEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrApprovals_Create, L("CreateNewWholesaleEclSicrApproval"));
            wholesaleEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrApprovals_Edit, L("EditWholesaleEclSicrApproval"));
            wholesaleEclSicrApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrApprovals_Delete, L("DeleteWholesaleEclSicrApproval"));



            var wholesaleEclSicrs = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrs, L("WholesaleEclSicrs"));
            wholesaleEclSicrs.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrs_Create, L("CreateNewWholesaleEclSicr"));
            wholesaleEclSicrs.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrs_Edit, L("EditWholesaleEclSicr"));
            wholesaleEclSicrs.CreateChildPermission(AppPermissions.Pages_WholesaleEclSicrs_Delete, L("DeleteWholesaleEclSicr"));



            var wholesaleEclDataPaymentSchedules = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataPaymentSchedules, L("WholesaleEclDataPaymentSchedules"));
            wholesaleEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Create, L("CreateNewWholesaleEclDataPaymentSchedule"));
            wholesaleEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Edit, L("EditWholesaleEclDataPaymentSchedule"));
            wholesaleEclDataPaymentSchedules.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataPaymentSchedules_Delete, L("DeleteWholesaleEclDataPaymentSchedule"));



            var wholesaleEclDataLoanBooks = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataLoanBooks, L("WholesaleEclDataLoanBooks"));
            wholesaleEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataLoanBooks_Create, L("CreateNewWholesaleEclDataLoanBook"));
            wholesaleEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataLoanBooks_Edit, L("EditWholesaleEclDataLoanBook"));
            wholesaleEclDataLoanBooks.CreateChildPermission(AppPermissions.Pages_WholesaleEclDataLoanBooks_Delete, L("DeleteWholesaleEclDataLoanBook"));



            var wholesaleEclUploadApprovals = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploadApprovals, L("WholesaleEclUploadApprovals"));
            wholesaleEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploadApprovals_Create, L("CreateNewWholesaleEclUploadApproval"));
            wholesaleEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploadApprovals_Edit, L("EditWholesaleEclUploadApproval"));
            wholesaleEclUploadApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploadApprovals_Delete, L("DeleteWholesaleEclUploadApproval"));



            var wholesaleEclUploads = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploads, L("WholesaleEclUploads"));
            wholesaleEclUploads.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploads_Create, L("CreateNewWholesaleEclUpload"));
            wholesaleEclUploads.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploads_Edit, L("EditWholesaleEclUpload"));
            wholesaleEclUploads.CreateChildPermission(AppPermissions.Pages_WholesaleEclUploads_Delete, L("DeleteWholesaleEclUpload"));



            var wholesaleEclApprovals = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclApprovals, L("WholesaleEclApprovals"));
            wholesaleEclApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclApprovals_Create, L("CreateNewWholesaleEclApproval"));
            wholesaleEclApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclApprovals_Edit, L("EditWholesaleEclApproval"));
            wholesaleEclApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclApprovals_Delete, L("DeleteWholesaleEclApproval"));



            var wholesaleEclAssumptionApprovals = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptionApprovals, L("WholesaleEclAssumptionApprovals"));
            wholesaleEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Create, L("CreateNewWholesaleEclAssumptionApproval"));
            wholesaleEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Edit, L("EditWholesaleEclAssumptionApproval"));
            wholesaleEclAssumptionApprovals.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptionApprovals_Delete, L("DeleteWholesaleEclAssumptionApproval"));



            var wholesaleEclPdSnPCummulativeDefaultRateses = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdSnPCummulativeDefaultRateses, L("WholesaleEclPdSnPCummulativeDefaultRateses"));
            wholesaleEclPdSnPCummulativeDefaultRateses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdSnPCummulativeDefaultRateses_Create, L("CreateNewWholesaleEclPdSnPCummulativeDefaultRates"));
            wholesaleEclPdSnPCummulativeDefaultRateses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdSnPCummulativeDefaultRateses_Edit, L("EditWholesaleEclPdSnPCummulativeDefaultRates"));
            wholesaleEclPdSnPCummulativeDefaultRateses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdSnPCummulativeDefaultRateses_Delete, L("DeleteWholesaleEclPdSnPCummulativeDefaultRates"));



            var wholesaleEclPdAssumption12Monthses = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumption12Monthses, L("WholesaleEclPdAssumption12Monthses"));
            wholesaleEclPdAssumption12Monthses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumption12Monthses_Create, L("CreateNewWholesaleEclPdAssumption12Months"));
            wholesaleEclPdAssumption12Monthses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumption12Monthses_Edit, L("EditWholesaleEclPdAssumption12Months"));
            wholesaleEclPdAssumption12Monthses.CreateChildPermission(AppPermissions.Pages_WholesaleEclPdAssumption12Monthses_Delete, L("DeleteWholesaleEclPdAssumption12Months"));



            var wholesaleEclLgdAssumptions = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclLgdAssumptions, L("WholesaleEclLgdAssumptions"));
            wholesaleEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclLgdAssumptions_Create, L("CreateNewWholesaleEclLgdAssumption"));
            wholesaleEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclLgdAssumptions_Edit, L("EditWholesaleEclLgdAssumption"));
            wholesaleEclLgdAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclLgdAssumptions_Delete, L("DeleteWholesaleEclLgdAssumption"));



            var wholesaleEadInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputAssumptions, L("WholesaleEadInputAssumptions"));
            wholesaleEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputAssumptions_Create, L("CreateNewWholesaleEadInputAssumption"));
            wholesaleEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputAssumptions_Edit, L("EditWholesaleEadInputAssumption"));
            wholesaleEadInputAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEadInputAssumptions_Delete, L("DeleteWholesaleEadInputAssumption"));



            var wholesaleEclAssumptions = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptions, L("WholesaleEclAssumptions"));
            wholesaleEclAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptions_Create, L("CreateNewWholesaleEclAssumption"));
            wholesaleEclAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptions_Edit, L("EditWholesaleEclAssumption"));
            wholesaleEclAssumptions.CreateChildPermission(AppPermissions.Pages_WholesaleEclAssumptions_Delete, L("DeleteWholesaleEclAssumption"));



            var wholesaleEcls = pages.CreateChildPermission(AppPermissions.Pages_WholesaleEcls, L("WholesaleEcls"));
            wholesaleEcls.CreateChildPermission(AppPermissions.Pages_WholesaleEcls_Create, L("CreateNewWholesaleEcl"));
            wholesaleEcls.CreateChildPermission(AppPermissions.Pages_WholesaleEcls_Edit, L("EditWholesaleEcl"));
            wholesaleEcls.CreateChildPermission(AppPermissions.Pages_WholesaleEcls_Delete, L("DeleteWholesaleEcl"));



            var assumptions = pages.CreateChildPermission(AppPermissions.Pages_Assumptions, L("Assumptions"));
            assumptions.CreateChildPermission(AppPermissions.Pages_Assumptions_Create, L("CreateNewAssumption"));
            assumptions.CreateChildPermission(AppPermissions.Pages_Assumptions_Edit, L("EditAssumption"));
            assumptions.CreateChildPermission(AppPermissions.Pages_Assumptions_Delete, L("DeleteAssumption"));



            var pdInputSnPCummulativeDefaultRates = pages.CreateChildPermission(AppPermissions.Pages_PdInputSnPCummulativeDefaultRates, L("PdInputSnPCummulativeDefaultRates"));
            pdInputSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdInputSnPCummulativeDefaultRates_Create, L("CreateNewPdInputSnPCummulativeDefaultRate"));
            pdInputSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdInputSnPCummulativeDefaultRates_Edit, L("EditPdInputSnPCummulativeDefaultRate"));
            pdInputSnPCummulativeDefaultRates.CreateChildPermission(AppPermissions.Pages_PdInputSnPCummulativeDefaultRates_Delete, L("DeletePdInputSnPCummulativeDefaultRate"));



            var pdInputAssumption12Months = pages.CreateChildPermission(AppPermissions.Pages_PdInputAssumption12Months, L("PdInputAssumption12Months"));
            pdInputAssumption12Months.CreateChildPermission(AppPermissions.Pages_PdInputAssumption12Months_Create, L("CreateNewPdInputAssumption12Month"));
            pdInputAssumption12Months.CreateChildPermission(AppPermissions.Pages_PdInputAssumption12Months_Edit, L("EditPdInputAssumption12Month"));
            pdInputAssumption12Months.CreateChildPermission(AppPermissions.Pages_PdInputAssumption12Months_Delete, L("DeletePdInputAssumption12Month"));



            var lgdAssumptionUnsecuredRecoveries = pages.CreateChildPermission(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries, L("LgdAssumptionUnsecuredRecoveries"));
            lgdAssumptionUnsecuredRecoveries.CreateChildPermission(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Create, L("CreateNewLgdAssumptionUnsecuredRecovery"));
            lgdAssumptionUnsecuredRecoveries.CreateChildPermission(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Edit, L("EditLgdAssumptionUnsecuredRecovery"));
            lgdAssumptionUnsecuredRecoveries.CreateChildPermission(AppPermissions.Pages_LgdAssumptionUnsecuredRecoveries_Delete, L("DeleteLgdAssumptionUnsecuredRecovery"));



            var eadInputAssumptions = pages.CreateChildPermission(AppPermissions.Pages_EadInputAssumptions, L("EadInputAssumptions"));
            eadInputAssumptions.CreateChildPermission(AppPermissions.Pages_EadInputAssumptions_Create, L("CreateNewEadInputAssumption"));
            eadInputAssumptions.CreateChildPermission(AppPermissions.Pages_EadInputAssumptions_Edit, L("EditEadInputAssumption"));
            eadInputAssumptions.CreateChildPermission(AppPermissions.Pages_EadInputAssumptions_Delete, L("DeleteEadInputAssumption"));


            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TestDemoConsts.LocalizationSourceName);
        }
    }
}
