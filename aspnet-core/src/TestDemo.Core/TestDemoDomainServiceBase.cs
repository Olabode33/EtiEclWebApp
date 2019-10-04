using Abp.Domain.Services;

namespace TestDemo
{
    public abstract class TestDemoDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected TestDemoDomainServiceBase()
        {
            LocalizationSourceName = TestDemoConsts.LocalizationSourceName;
        }
    }
}
