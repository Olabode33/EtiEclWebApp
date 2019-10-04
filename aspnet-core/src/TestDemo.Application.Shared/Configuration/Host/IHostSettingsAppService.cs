using System.Threading.Tasks;
using Abp.Application.Services;
using TestDemo.Configuration.Host.Dto;

namespace TestDemo.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
