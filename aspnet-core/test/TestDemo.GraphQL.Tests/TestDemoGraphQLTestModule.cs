using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TestDemo.Configure;
using TestDemo.Startup;
using TestDemo.Test.Base;

namespace TestDemo.GraphQL.Tests
{
    [DependsOn(
        typeof(TestDemoGraphQLModule),
        typeof(TestDemoTestBaseModule))]
    public class TestDemoGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoGraphQLTestModule).GetAssembly());
        }
    }
}