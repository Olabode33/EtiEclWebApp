import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {

    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) {

    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem('Home', 'Pages.Tenant.Dashboard', 'fa fa-home', '/app/main/home'),
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Calibration', 'Pages.Calibration', 'fa fa-bezier-curve', '', [
                new AppMenuItem('CalibrationEadBehaviouralTerms', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/behavioralTerms'),
                new AppMenuItem('CalibrationEadCcfSummary', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/ccfSummary'),
                new AppMenuItem('CalibrationLgdHairCut', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/haircut'),
                new AppMenuItem('CalibrationLgdRecoveryRate', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/recovery'),
                new AppMenuItem('CalibrationPdCrDr', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/pdcrdr'),
                new AppMenuItem('MacroAnalysis', 'Pages.Calibration', 'flaticon-more', '/app/main/calibration/macroAnalysis'),
            ]),
            new AppMenuItem('Assumptions', 'Pages.Assumption.Affiliates', 'fa fa-list-ul', '/app/main/assumption/affiliates'),
            new AppMenuItem('ECL', 'Pages.EclView', 'fa fa-suitcase', '/app/main/ecl'),
            new AppMenuItem('Configuration', 'Pages.Configurations', 'fa fa-wrench', '', [
                new AppMenuItem('EclConfigurations', 'Pages.Configurations', 'flaticon-more', '/app/main/eclConfig/eclConfigurations'),
                new AppMenuItem('Affiliates', 'Pages.Configurations', 'flaticon-more', '/app/main/eclConfig/affiliates'),
                new AppMenuItem('MacroeconomicVariables', 'Pages.Configurations', 'flaticon-more', '/app/main/config/macroeconomicVariables'),
                new AppMenuItem('AffiliateMacroEconomicVariableOffsets', 'Pages.Configurations', 'flaticon-more', '/app/main/affiliateMacroEconomicVariable/affiliateMacroEconomicVariableOffsets'),
            ]),
            new AppMenuItem('Administration', '', 'fa fa-cogs', '', [
                new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
                new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                //new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
                //new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
                new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ])//,
            //new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {

        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName && this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                return true;
            } else if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            }
        }

        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' && this._appSessionService.tenant && !this._appSessionService.tenant.edition) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }
}
