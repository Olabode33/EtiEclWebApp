using Abp.Configuration;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclConfig
{
    public class EclSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(EclSettings.OverrideCutOffDate, DateTime.Now.ToString(), scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.LoanBookSnapshot.Wholesale, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.LoanBookSnapshot.Retail, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.LoanBookSnapshot.Obe, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.PaymentSchedule.Wholesale, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.PaymentSchedule.Retail, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.PaymentSchedule.Obe, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.InputSourceUpload.AssetBook.Investment, "false", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.RequiredNoOfApprovals, "2", scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(EclSettings.PowerBiReportUrl, "", scopes: SettingScopes.Tenant, isVisibleToClients: true)
            };
        }
    }
}
