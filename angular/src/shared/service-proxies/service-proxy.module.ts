﻿import { AbpHttpInterceptor } from '@abp/abpHttpInterceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import * as ApiServiceProxies from './service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.InvSecFitchCummulativeDefaultRatesServiceProxy,
        ApiServiceProxies.InvSecMacroEconomicAssumptionsServiceProxy,
        ApiServiceProxies.EclConfigurationsServiceProxy,
        ApiServiceProxies.AffiliateOverrideThresholdsServiceProxy,
        ApiServiceProxies.ObeEclDataPaymentSchedulesServiceProxy,
        ApiServiceProxies.WholesaleEclDataPaymentSchedulesServiceProxy,
        ApiServiceProxies.RetailEclDataPaymentSchedulesServiceProxy,
        ApiServiceProxies.ObeEclDataLoanBooksServiceProxy,
        ApiServiceProxies.WholesaleEclDataLoanBooksServiceProxy,
        ApiServiceProxies.RetailEclDataLoanBooksServiceProxy,
        ApiServiceProxies.RetailEclUploadsServiceProxy,
        ApiServiceProxies.AssumptionApprovalsServiceProxy,
        ApiServiceProxies.PdInputAssumptionMacroeconomicProjectionsServiceProxy,
        ApiServiceProxies.PdInputAssumptionStatisticalsServiceProxy,
        ApiServiceProxies.PdInputAssumptionNplIndexesServiceProxy,
        ApiServiceProxies.PdInputAssumptionNonInternalModelsServiceProxy,
        ApiServiceProxies.PdInputAssumptionsServiceProxy,
        ApiServiceProxies.LgdAssumptionUnsecuredRecoveriesServiceProxy,
        ApiServiceProxies.EadInputAssumptionsServiceProxy,
        ApiServiceProxies.MacroeconomicVariablesServiceProxy,
        ApiServiceProxies.WholesaleEclPdAssumptionMacroeconomicInputsServiceProxy,
        ApiServiceProxies.RetailEclsServiceProxy,
        ApiServiceProxies.EclSharedServiceProxy,
        ApiServiceProxies.WholesaleEclResultSummaryTopExposuresServiceProxy,
        ApiServiceProxies.WholesaleEclResultSummaryKeyInputsServiceProxy,
        ApiServiceProxies.WholesaleEclResultSummariesServiceProxy,
        ApiServiceProxies.WholesaleEclSicrsServiceProxy,
        ApiServiceProxies.WholesaleEclDataPaymentSchedulesServiceProxy,
        ApiServiceProxies.WholesaleEclDataLoanBooksServiceProxy,
        ApiServiceProxies.WholesaleEclUploadsServiceProxy,
        ApiServiceProxies.WholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy,
        ApiServiceProxies.WholesaleEclPdAssumption12MonthsesServiceProxy,
        ApiServiceProxies.WholesaleEclsServiceProxy,
        ApiServiceProxies.AssumptionsServiceProxy,
        ApiServiceProxies.PdInputSnPCummulativeDefaultRatesServiceProxy,
        ApiServiceProxies.AuditLogServiceProxy,
        ApiServiceProxies.CachingServiceProxy,
        ApiServiceProxies.ChatServiceProxy,
        ApiServiceProxies.CommonLookupServiceProxy,
        ApiServiceProxies.EditionServiceProxy,
        ApiServiceProxies.FriendshipServiceProxy,
        ApiServiceProxies.HostSettingsServiceProxy,
        ApiServiceProxies.InstallServiceProxy,
        ApiServiceProxies.LanguageServiceProxy,
        ApiServiceProxies.NotificationServiceProxy,
        ApiServiceProxies.OrganizationUnitServiceProxy,
        ApiServiceProxies.PermissionServiceProxy,
        ApiServiceProxies.ProfileServiceProxy,
        ApiServiceProxies.RoleServiceProxy,
        ApiServiceProxies.SessionServiceProxy,
        ApiServiceProxies.TenantServiceProxy,
        ApiServiceProxies.TenantDashboardServiceProxy,
        ApiServiceProxies.TenantSettingsServiceProxy,
        ApiServiceProxies.TimingServiceProxy,
        ApiServiceProxies.UserServiceProxy,
        ApiServiceProxies.UserLinkServiceProxy,
        ApiServiceProxies.UserLoginServiceProxy,
        ApiServiceProxies.WebLogServiceProxy,
        ApiServiceProxies.AccountServiceProxy,
        ApiServiceProxies.TokenAuthServiceProxy,
        ApiServiceProxies.TenantRegistrationServiceProxy,
        ApiServiceProxies.HostDashboardServiceProxy,
        ApiServiceProxies.PaymentServiceProxy,
        ApiServiceProxies.DemoUiComponentsServiceProxy,
        ApiServiceProxies.InvoiceServiceProxy,
        ApiServiceProxies.SubscriptionServiceProxy,
        ApiServiceProxies.InstallServiceProxy,
        ApiServiceProxies.UiCustomizationSettingsServiceProxy,
        ApiServiceProxies.PayPalPaymentServiceProxy,
        ApiServiceProxies.StripePaymentServiceProxy,
        { provide: HTTP_INTERCEPTORS, useClass: AbpHttpInterceptor, multi: true }
    ]
})
export class ServiceProxyModule { }
