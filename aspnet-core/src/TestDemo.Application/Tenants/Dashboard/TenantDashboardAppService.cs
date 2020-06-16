using Abp.Auditing;
using Abp.Authorization;
using System.Threading.Tasks;
using TestDemo.Authorization;
using TestDemo.EclConfig;
using TestDemo.Tenants.Dashboard.Dto;

namespace TestDemo.Tenants.Dashboard
{
    [DisableAuditing]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class TenantDashboardAppService : TestDemoAppServiceBase, ITenantDashboardAppService
    {
        public async Task<string> GetPowerBiDashboardUrl()
        {
            return await SettingManager.GetSettingValueAsync(EclSettings.PowerBiReportUrl);
        }
    }
}