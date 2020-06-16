using Abp.Application.Services;
using System.Threading.Tasks;
using TestDemo.Tenants.Dashboard.Dto;

namespace TestDemo.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        Task<string> GetPowerBiDashboardUrl();
    }
}
