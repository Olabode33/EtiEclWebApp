using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using TestDemo.EntityFrameworkCore;
using TestDemo.Migrations.Seed.DefaultAsusmption.AffiliateAssumptions;
using TestDemo.Migrations.Seed.DefaultAsusmption.EadInput;
using TestDemo.Migrations.Seed.DefaultAsusmption.LgdInput;
using TestDemo.Migrations.Seed.DefaultAsusmption.PdInput;
using TestDemo.Migrations.Seed.DefaultAsusmption.Portfolio;
using TestDemo.Migrations.Seed.Host;
using TestDemo.Migrations.Seed.Tenants;

namespace TestDemo.Migrations.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<TestDemoDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(TestDemoDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            //Host seed
            new InitialHostDbBuilder(context).Create();

            //Default tenant seed (in host database).
            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();

            //Affiliate Assumptions Summary
            //new DefaultAffiliateAssumptionsBuilder(context).Create();

            //SeedAssumptionData(context);
        }

        private static void SeedAssumptionData(TestDemoDbContext context)
        {
            //Direct seeding thats time thanks to EntityFramework
            //Run in debug mode and run each assumption builder one at a time

            //Default framework / portfolio assumption
            new DefaultAssumptionBuilder(context).Create();

            //Default ead input assumption
            new DefaultEadAssumptionBuilder(context).Create();

            //Default lgd input assumption
            new DefaultLgdAssumptionBuilder(context).Create();

            //Default Pd input assumption
            new DefaultPdAssumptionBuilder(context).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
