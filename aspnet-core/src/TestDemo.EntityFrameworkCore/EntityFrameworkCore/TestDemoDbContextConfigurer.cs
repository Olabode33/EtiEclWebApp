using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TestDemo.EntityFrameworkCore
{
    public static class TestDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TestDemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TestDemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}