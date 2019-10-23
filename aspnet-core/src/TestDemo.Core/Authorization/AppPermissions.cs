namespace TestDemo.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_WholesaleEclLgdAssumptions = "Pages.WholesaleEclLgdAssumptions";
        public const string Pages_WholesaleEclLgdAssumptions_Create = "Pages.WholesaleEclLgdAssumptions.Create";
        public const string Pages_WholesaleEclLgdAssumptions_Edit = "Pages.WholesaleEclLgdAssumptions.Edit";
        public const string Pages_WholesaleEclLgdAssumptions_Delete = "Pages.WholesaleEclLgdAssumptions.Delete";

        public const string Pages_WholesaleEadInputAssumptions = "Pages.WholesaleEadInputAssumptions";
        public const string Pages_WholesaleEadInputAssumptions_Create = "Pages.WholesaleEadInputAssumptions.Create";
        public const string Pages_WholesaleEadInputAssumptions_Edit = "Pages.WholesaleEadInputAssumptions.Edit";
        public const string Pages_WholesaleEadInputAssumptions_Delete = "Pages.WholesaleEadInputAssumptions.Delete";

        public const string Pages_WholesaleEclAssumptions = "Pages.WholesaleEclAssumptions";
        public const string Pages_WholesaleEclAssumptions_Create = "Pages.WholesaleEclAssumptions.Create";
        public const string Pages_WholesaleEclAssumptions_Edit = "Pages.WholesaleEclAssumptions.Edit";
        public const string Pages_WholesaleEclAssumptions_Delete = "Pages.WholesaleEclAssumptions.Delete";

        public const string Pages_WholesaleEcls = "Pages.WholesaleEcls";
        public const string Pages_WholesaleEcls_Create = "Pages.WholesaleEcls.Create";
        public const string Pages_WholesaleEcls_Edit = "Pages.WholesaleEcls.Edit";
        public const string Pages_WholesaleEcls_Delete = "Pages.WholesaleEcls.Delete";

        public const string Pages_Assumptions = "Pages.Assumptions";
        public const string Pages_Assumptions_Create = "Pages.Assumptions.Create";
        public const string Pages_Assumptions_Edit = "Pages.Assumptions.Edit";
        public const string Pages_Assumptions_Delete = "Pages.Assumptions.Delete";

        public const string Pages_PdInputSnPCummulativeDefaultRates = "Pages.PdInputSnPCummulativeDefaultRates";
        public const string Pages_PdInputSnPCummulativeDefaultRates_Create = "Pages.PdInputSnPCummulativeDefaultRates.Create";
        public const string Pages_PdInputSnPCummulativeDefaultRates_Edit = "Pages.PdInputSnPCummulativeDefaultRates.Edit";
        public const string Pages_PdInputSnPCummulativeDefaultRates_Delete = "Pages.PdInputSnPCummulativeDefaultRates.Delete";

        public const string Pages_PdInputAssumption12Months = "Pages.PdInputAssumption12Months";
        public const string Pages_PdInputAssumption12Months_Create = "Pages.PdInputAssumption12Months.Create";
        public const string Pages_PdInputAssumption12Months_Edit = "Pages.PdInputAssumption12Months.Edit";
        public const string Pages_PdInputAssumption12Months_Delete = "Pages.PdInputAssumption12Months.Delete";

        public const string Pages_LgdAssumptionUnsecuredRecoveries = "Pages.LgdAssumptionUnsecuredRecoveries";
        public const string Pages_LgdAssumptionUnsecuredRecoveries_Create = "Pages.LgdAssumptionUnsecuredRecoveries.Create";
        public const string Pages_LgdAssumptionUnsecuredRecoveries_Edit = "Pages.LgdAssumptionUnsecuredRecoveries.Edit";
        public const string Pages_LgdAssumptionUnsecuredRecoveries_Delete = "Pages.LgdAssumptionUnsecuredRecoveries.Delete";

        public const string Pages_EadInputAssumptions = "Pages.EadInputAssumptions";
        public const string Pages_EadInputAssumptions_Create = "Pages.EadInputAssumptions.Create";
        public const string Pages_EadInputAssumptions_Edit = "Pages.EadInputAssumptions.Edit";
        public const string Pages_EadInputAssumptions_Delete = "Pages.EadInputAssumptions.Delete";

        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
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