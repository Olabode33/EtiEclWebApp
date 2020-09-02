namespace TestDemo.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        public const string Pages_ResultSummaryByStages = "Pages.ResultSummaryByStages";
        public const string Pages_ResultSummaryByStages_Create = "Pages.ResultSummaryByStages.Create";
        public const string Pages_ResultSummaryByStages_Edit = "Pages.ResultSummaryByStages.Edit";
        public const string Pages_ResultSummaryByStages_Delete = "Pages.ResultSummaryByStages.Delete";

        public const string Pages_HoldCoInterCompanyResults = "Pages.HoldCoInterCompanyResults";
        public const string Pages_HoldCoInterCompanyResults_Create = "Pages.HoldCoInterCompanyResults.Create";
        public const string Pages_HoldCoInterCompanyResults_Edit = "Pages.HoldCoInterCompanyResults.Edit";
        public const string Pages_HoldCoInterCompanyResults_Delete = "Pages.HoldCoInterCompanyResults.Delete";

        public const string Pages_HoldCoResultSummaries = "Pages.HoldCoResultSummaries";
        public const string Pages_HoldCoResultSummaries_Create = "Pages.HoldCoResultSummaries.Create";
        public const string Pages_HoldCoResultSummaries_Edit = "Pages.HoldCoResultSummaries.Edit";
        public const string Pages_HoldCoResultSummaries_Delete = "Pages.HoldCoResultSummaries.Delete";

        public const string Pages_HoldCoResults = "Pages.HoldCoResults";
        public const string Pages_HoldCoResults_Create = "Pages.HoldCoResults.Create";
        public const string Pages_HoldCoResults_Edit = "Pages.HoldCoResults.Edit";
        public const string Pages_HoldCoResults_Delete = "Pages.HoldCoResults.Delete";

        public const string Pages_AssetBooks = "Pages.AssetBooks";
        public const string Pages_AssetBooks_Create = "Pages.AssetBooks.Create";
        public const string Pages_AssetBooks_Edit = "Pages.AssetBooks.Edit";
        public const string Pages_AssetBooks_Delete = "Pages.AssetBooks.Delete";

        public const string Pages_MacroEconomicCreditIndices = "Pages.MacroEconomicCreditIndices";
        public const string Pages_MacroEconomicCreditIndices_Create = "Pages.MacroEconomicCreditIndices.Create";
        public const string Pages_MacroEconomicCreditIndices_Edit = "Pages.MacroEconomicCreditIndices.Edit";
        public const string Pages_MacroEconomicCreditIndices_Delete = "Pages.MacroEconomicCreditIndices.Delete";

        public const string Pages_HoldCoInputParameters = "Pages.HoldCoInputParameters";
        public const string Pages_HoldCoInputParameters_Create = "Pages.HoldCoInputParameters.Create";
        public const string Pages_HoldCoInputParameters_Edit = "Pages.HoldCoInputParameters.Edit";
        public const string Pages_HoldCoInputParameters_Delete = "Pages.HoldCoInputParameters.Delete";

        public const string Pages_HoldCoRegisters = "Pages.HoldCoRegisters";
        public const string Pages_HoldCoRegisters_Create = "Pages.HoldCoRegisters.Create";
        public const string Pages_HoldCoRegisters_Approve = "Pages.HoldCoRegisters.Approve";
        public const string Pages_HoldCoRegisters_Edit = "Pages.HoldCoRegisters.Edit";
        public const string Pages_HoldCoRegisters_Delete = "Pages.HoldCoRegisters.Delete";

        public const string Pages_Calibration = "Pages.Calibration";
        public const string Pages_Calibration_Create = "Pages.Calibration.Create";
        public const string Pages_Calibration_Edit = "Pages.Calibration.Edit";
        public const string Pages_Calibration_Delete = "Pages.Calibration.Delete";
        public const string Pages_Calibration_Upload = "Pages.Calibration.Upload";
        public const string Pages_Calibration_Submit = "Pages.Calibration.Submit";
        public const string Pages_Calibration_Review = "Pages.Calibration.Review";
        public const string Pages_Calibration_Apply = "Pages.Calibration.Apply";
        public const string Pages_Calibration_Override = "Pages.Calibration.Override";
        public const string Pages_Calibration_ReviewOverride = "Pages.Calibration.Override.Review";
        public const string Pages_Calibration_Erase = "Pages.Calibration.Erase";

        public const string Pages_IVModels = "Pages.IVModels";
        public const string Pages_IVModels_Create = "Pages.IVModels.Create";
        public const string Pages_IVModels_Edit = "Pages.IVModels.Edit";
        public const string Pages_IVModels_Delete = "Pages.IVModels.Delete";
        public const string Pages_IVModels_Upload = "Pages.IVModels.Upload";
        public const string Pages_IVModels_Submit = "Pages.IVModels.Submit";
        public const string Pages_IVModels_Review = "Pages.IVModels.Review";
        public const string Pages_IVModels_Apply = "Pages.IVModels.Apply";
        public const string Pages_IVModels_Override = "Pages.IVModels.Override";
        public const string Pages_IVModels_ReviewOverride = "Pages.IVModels.Override.Review";
        public const string Pages_IVModels_Erase = "Pages.IVModels.Erase";

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
        public const string Pages_EclView_Erase = "Pages.EclView.Erase";

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
        public const string Pages_Administration_Users_Approve = "Pages.Administration.Users.Approve";

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