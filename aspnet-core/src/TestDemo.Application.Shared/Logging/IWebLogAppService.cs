using Abp.Application.Services;
using TestDemo.Dto;
using TestDemo.Logging.Dto;

namespace TestDemo.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
