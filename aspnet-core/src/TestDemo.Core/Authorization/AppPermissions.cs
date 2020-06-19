namespace TestDemo.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_Calibration = "Pages.Calibration";
        public const string Pages_Calibration_Create = "Pages.Calibration.Create";
        public const string Pages_Calibration_Edit = "Pages.Calibration.Edit";
        public const string Pages_Calibration_Delete = "Pages.Calibration.Delete";
        public const string Pages_Calibration_Upload = "Pages.Calibration.Upload";
        public const string Pages_Calibration_Submit = "Pages.Calibration.Submit";
        public const string Pages_Calibration_Review = "Pages.Calibration.Review";
        public const string Pages_Calibration_Apply = "Pages.Calibration.Apply";

        //Final 
        //Assumptions
        public const string Pages_AssumptionsUpdate = "Pages.Assumption.Affiliates";
        public const string Pages_AssumptionsEdit = "Pages.Assumption.Affiliates.Edit";
        public const string Pages_AssumptionsUpdate_Review = "Pages.Assumption.Affiliates.Review";
        public const string Pages_AssumptionsUpdate_Copy = "Pages.Assumption.Affiliates.Copy";

        //Workspace
        public const string Pages_Workspace = "Pages.Workspace";
        public const string Pages_Workspace_CreateEcl = "Pages.Workspace.CreateEcl";
        public const string Pages_Workspace_Dashboard = "Pages.Workspace.Dashboard";

        //Ecl View
        public const string Pages_EclView = "Pages.EclView";
        public const string Pages_EclView_Edit = "Pages.EclView.Edit";
        public const string Pages_EclView_Upload = "Pages.EclView.Upload";
        public const string Pages_EclView_Submit = "Pages.EclView.Submit";
        public const string Pages_EclView_Review = "Pages.EclView.Review";
        public const string Pages_EclView_Run = "Pages.EclView.Run";
        public const string Pages_EclView_Override = "Pages.EclView.Override";
        public const string Pages_EclView_Override_Review = "Pages.EclView.Override.Review";
        public const string Pages_EclView_Close = "Pages.EclView.Close";
        public const string Pages_EclView_Reopen = "Pages.EclView.Reopen";
        public const string Pages_EclView_Delete = "Pages.EclView.Delete";

        //Configuration
        public const string Pages_Configuration = "Pages.Configurations";
        public const string Pages_Configuration_View = "Pages.Configurations.View";
        public const string Pages_Configuration_Update = "Pages.Configurations.Update";


        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

    }
}