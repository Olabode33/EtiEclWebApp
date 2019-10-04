using System.Threading.Tasks;
using Abp.Application.Services;
using TestDemo.Install.Dto;

namespace TestDemo.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}