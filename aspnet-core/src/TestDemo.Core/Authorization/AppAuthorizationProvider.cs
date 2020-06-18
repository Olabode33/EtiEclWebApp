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

            var calibration = pages.CreateChildPermission(AppPermissions.Pages_Calibration, L("Calibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Create, L("NewCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Edit, L("EditCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Delete, L("DeleteCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Upload, L("UploadCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Submit, L("SubmitCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Review, L("ReviewCalibration"));
            calibration.CreateChildPermission(AppPermissions.Pages_Calibration_Apply, L("ApplyCalibration"));

            //Final Permissions List
            var assumptionsUpdate = pages.CreateChildPermission(AppPermissions.Pages_AssumptionsUpdate, L("Assumptions"));
            assumptionsUpdate.CreateChildPermission(AppPermissions.Pages_AssumptionsEdit, L("UpdatedAssumption"));
            assumptionsUpdate.CreateChildPermission(AppPermissions.Pages_AssumptionsUpdate_Review, L("ReviewUpdatedAssumption"));
            assumptionsUpdate.CreateChildPermission(AppPermissions.Pages_AssumptionsUpdate_Copy, L("CopyAssumption"));

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
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Close, L("CloseEcl"));
            eclView.CreateChildPermission(AppPermissions.Pages_EclView_Reopen, L("ReopenEcl"));

            var configuration = pages.CreateChildPermission(AppPermissions.Pages_Configuration, L("EditEclConfiguration"));
            configuration.CreateChildPermission(AppPermissions.Pages_Configuration_View, L("View"));
            configuration.CreateChildPermission(AppPermissions.Pages_Configuration_Update, L("Update"));

            //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

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

            //administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            //administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            //var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            //editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            //editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            //editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            //editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            //var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

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
