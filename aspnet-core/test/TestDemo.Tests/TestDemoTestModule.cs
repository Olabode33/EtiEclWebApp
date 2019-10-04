using Abp.Modules;
using TestDemo.Test.Base;

namespace TestDemo.Tests
{
    [DependsOn(typeof(TestDemoTestBaseModule))]
    public class TestDemoTestModule : AbpModule
    {
       
    }
}
