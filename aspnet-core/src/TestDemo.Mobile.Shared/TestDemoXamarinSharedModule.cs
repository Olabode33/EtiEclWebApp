using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestDemo
{
    [DependsOn(typeof(TestDemoClientModule), typeof(AbpAutoMapperModule))]
    public class TestDemoXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoXamarinSharedModule).GetAssembly());
        }
    }
}