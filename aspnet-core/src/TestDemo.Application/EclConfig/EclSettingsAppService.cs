using System.Threading.Tasks;
using TestDemo.EclConfig.Dtos;
using TestDemo.Authorization;
using Abp.Authorization;
using Abp.Configuration;
using System;
using Abp.Runtime.Session;

namespace TestDemo.EclConfig
{
    [AbpAuthorize(AppPermissions.Pages_EclConfigurations)]
    public class EclSettingsAppService : TestDemoAppServiceBase, IEclSettingsAppService
    {
        #region Get Settings
        public async Task<EclSettingsEditDto> GetAllSettings()
        {
            return new EclSettingsEditDto
            {
                OverrideCutOffTime = await SettingManager.GetSettingValueAsync<DateTime>(EclSettings.OverrideCutOffDate),
                RequiredNumberOfApprovals = await SettingManager.GetSettingValueAsync<int>(EclSettings.RequiredNoOfApprovals),
                PowerBiReportUrl = await SettingManager.GetSettingValueAsync(EclSettings.PowerBiReportUrl),
                LoanBookSnapshot = await GetLoanBookSnapshotSettingsAsync(),
                PaymentSchedule = await GetPaymentScheduleSettingsAsync(),
                AssetBook = await GetAssetBookSettingsAsync()
            };
        }

        private async Task<LoanBookSnapshotEditDto> GetLoanBookSnapshotSettingsAsync()
        {
            return new LoanBookSnapshotEditDto
            {
                WholesaleStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.LoanBookSnapshot.Wholesale),
                RetailStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.LoanBookSnapshot.Retail),
                ObeStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.LoanBookSnapshot.Obe)
            };
        }

        private async Task<PaymentScheduleEditDto> GetPaymentScheduleSettingsAsync()
        {
            return new PaymentScheduleEditDto
            {
                WholesaleStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.PaymentSchedule.Wholesale),
                RetailStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.PaymentSchedule.Retail),
                ObeStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.PaymentSchedule.Obe)
            };
        }

        private async Task<AssetBookSettingsEditDto> GetAssetBookSettingsAsync()
        {
            return new AssetBookSettingsEditDto
            {
                InvesmentStaging = await SettingManager.GetSettingValueAsync<bool>(EclSettings.InputSourceUpload.AssetBook.Investment)
            };
        }
        #endregion

        #region Update Settings
        public async Task UpdateAllSettings(EclSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.OverrideCutOffDate, input.OverrideCutOffTime.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.RequiredNoOfApprovals, input.RequiredNumberOfApprovals.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.PowerBiReportUrl, input.PowerBiReportUrl.ToString());
            await UpdateLoanBookSnapshotSettingAsync(input.LoanBookSnapshot);
            await UpdatePaymentScheduleSettingAsync(input.PaymentSchedule);
            await UpdateAssetBookSettingAsync(input.AssetBook);
        }

        private async Task UpdateLoanBookSnapshotSettingAsync(LoanBookSnapshotEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.LoanBookSnapshot.Wholesale, input.WholesaleStaging.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.LoanBookSnapshot.Retail, input.RetailStaging.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.LoanBookSnapshot.Obe, input.ObeStaging.ToString().ToLowerInvariant());
        }
        private async Task UpdatePaymentScheduleSettingAsync(PaymentScheduleEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.PaymentSchedule.Wholesale, input.WholesaleStaging.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.PaymentSchedule.Retail, input.RetailStaging.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.PaymentSchedule.Obe, input.ObeStaging.ToString().ToLowerInvariant());
        }
        private async Task UpdateAssetBookSettingAsync(AssetBookSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EclSettings.InputSourceUpload.AssetBook.Investment, input.InvesmentStaging.ToString().ToLowerInvariant());
        }
        #endregion
    }
}