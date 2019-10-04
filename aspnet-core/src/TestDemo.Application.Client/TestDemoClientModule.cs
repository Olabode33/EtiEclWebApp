using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestDemo
{
    public class TestDemoClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoClientModule).GetAssembly());
        }
    }
}
