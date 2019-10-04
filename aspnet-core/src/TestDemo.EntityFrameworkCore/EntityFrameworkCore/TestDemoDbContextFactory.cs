using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TestDemo.Configuration;
using TestDemo.Web;

namespace TestDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TestDemoDbContextFactory : IDesignTimeDbContextFactory<TestDemoDbContext>
    {
        public TestDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TestDemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            TestDemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TestDemoConsts.ConnectionStringName));

            return new TestDemoDbContext(builder.Options);
        }
    }
}