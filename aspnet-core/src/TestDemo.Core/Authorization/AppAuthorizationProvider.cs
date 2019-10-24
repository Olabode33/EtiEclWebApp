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
