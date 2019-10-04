using Abp.AspNetCore.Mvc.Views;

namespace TestDemo.Web.Views
{
    public abstract class TestDemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected TestDemoRazorPage()
        {
            LocalizationSourceName = TestDemoConsts.LocalizationSourceName;
        }
    }
}
