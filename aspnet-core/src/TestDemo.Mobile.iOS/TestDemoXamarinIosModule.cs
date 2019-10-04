using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestDemo
{
    [DependsOn(typeof(TestDemoXamarinSharedModule))]
    public class TestDemoXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoXamarinIosModule).GetAssembly());
        }
    }
}