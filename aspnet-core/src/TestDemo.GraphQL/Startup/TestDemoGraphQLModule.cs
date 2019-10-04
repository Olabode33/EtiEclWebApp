using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestDemo.Startup
{
    [DependsOn(typeof(TestDemoCoreModule))]
    public class TestDemoGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestDemoGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}