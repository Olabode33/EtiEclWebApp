using Abp.AspNetCore.Mvc.ViewComponents;

namespace TestDemo.Web.Public.Views
{
    public abstract class TestDemoViewComponent : AbpViewComponent
    {
        protected TestDemoViewComponent()
        {
            LocalizationSourceName = TestDemoConsts.LocalizationSourceName;
        }
    }
}