using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestDemo.Authorization;

namespace TestDemo
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(TestDemoCoreModule)
        )]
    public class TestDemoApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoApplicationModule).GetAssembly());
        }
    }
}