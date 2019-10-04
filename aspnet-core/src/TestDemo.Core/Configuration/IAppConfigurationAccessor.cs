using Microsoft.Extensions.Configuration;

namespace TestDemo.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
