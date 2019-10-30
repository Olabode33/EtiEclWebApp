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
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Home', 'Pages.Tenant.Dashboard', 'fa fa-home', '/app/main/workspace'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),

            // new AppMenuItem('PdInputSnPCummulativeDefaultRates', 'Pages.PdInputSnPCummulativeDefaultRates', 'flaticon-more', '/app/main/eclShared/pdInputSnPCummulativeDefaultRates'),

            // new AppMenuItem('Assumptions', 'Pages.Assumptions', 'flaticon-more', '/app/main/eclShared/assumptions'),

            // new AppMenuItem('WholesaleEcls', 'Pages.WholesaleEcls', 'flaticon-more', '/app/main/wholesale/wholesaleEcls'),

            // new AppMenuItem('WholesaleEclPdAssumption12Monthses', 'Pages.WholesaleEclPdAssumption12Monthses', 'flaticon-more', '/app/main/wholesaleAssumption/wholesaleEclPdAssumption12Monthses'),

            // new AppMenuItem('WholesaleEclPdSnPCummulativeDefaultRateses', 'Pages.WholesaleEclPdSnPCummulativeDefaultRateses', 'flaticon-more', '/app/main/wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses'),

            // new AppMenuItem('WholesaleEclUploads', 'Pages.WholesaleEclUploads', 'flaticon-more', '/app/main/wholesaleInputs/wholesaleEclUploads'),

            // new AppMenuItem('WholesaleEclDataLoanBooks', 'Pages.WholesaleEclDataLoanBooks', 'flaticon-more', '/app/main/wholesaleInputs/wholesaleEclDataLoanBooks'),

            // new AppMenuItem('WholesaleEclDataPaymentSchedules', 'Pages.WholesaleEclDataPaymentSchedules', 'flaticon-more', '/app/main/wholesaleInputs/wholesaleEclDataPaymentSchedules'),

            // new AppMenuItem('WholesaleEclSicrs', 'Pages.WholesaleEclSicrs', 'flaticon-more', '/app/main/wholesaleComputation/wholesaleEclSicrs'),

            // new AppMenuItem('WholesaleEclResultSummaries', 'Pages.WholesaleEclResultSummaries', 'flaticon-more', '/app/main/wholesaleResult/wholesaleEclResultSummaries'),

            // new AppMenuItem('WholesaleEclResultSummaryKeyInputs', 'Pages.WholesaleEclResultSummaryKeyInputs', 'flaticon-more', '/app/main/wholesaleResults/wholesaleEclResultSummaryKeyInputs'),

            // new AppMenuItem('WholesaleEclResultSummaryTopExposures', 'Pages.WholesaleEclResultSummaryTopExposures', 'flaticon-more', '/app/main/wholesaleResults/wholesaleEclResultSummaryTopExposures'),
            new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
                new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
                new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
                new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
                new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ]),
            new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')
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
