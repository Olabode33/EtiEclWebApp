using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale;
using TestDemo.EclShared;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Users;
using TestDemo.Chat;
using TestDemo.Editions;
using TestDemo.Friendships;
using TestDemo.MultiTenancy;
using TestDemo.MultiTenancy.Accounting;
using TestDemo.MultiTenancy.Payments;
using TestDemo.Storage;

namespace TestDemo.EntityFrameworkCore
{
    public class TestDemoDbContext : AbpZeroDbContext<Tenant, Role, User, TestDemoDbContext>, IAbpPersistedGrantDbContext
    {

        public virtual DbSet<WholesaleEclEadInputAssumption> WholesaleEadInputAssumptions { get; set; }

        public virtual DbSet<WholesaleEclApproval> WholesaleEclApprovals { get; set; }

        public virtual DbSet<WholesaleEclAssumptionApproval> WholesaleEclAssumptionApprovals { get; set; }

        public virtual DbSet<WholesaleEclPdSnPCummulativeDefaultRate> WholesaleEclPdSnPCummulativeDefaultRateses { get; set; }

        public virtual DbSet<WholesaleEclPdAssumption12Month> WholesaleEclPdAssumption12Monthses { get; set; }

        public virtual DbSet<WholesaleEclLgdAssumption> WholesaleEclLgdAssumptions { get; set; }

        public virtual DbSet<WholesaleEclAssumption> WholesaleEclAssumptions { get; set; }

        public virtual DbSet<WholesaleEcl> WholesaleEcls { get; set; }

        public virtual DbSet<Assumption> Assumptions { get; set; }

        public virtual DbSet<PdInputSnPCummulativeDefaultRate> PdInputSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<PdInputAssumption12Month> PdInputAssumption12Months { get; set; }

        public virtual DbSet<LgdInputAssumption> LgdAssumption { get; set; }

        public virtual DbSet<EadInputAssumption> EadInputAssumptions { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public TestDemoDbContext(DbContextOptions<TestDemoDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
           
           
           
           
           
           
           
           
            modelBuilder.Entity<WholesaleEclEadInputAssumption>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclApproval>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclPdSnPCummulativeDefaultRate>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclPdAssumption12Month>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclLgdAssumption>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclEadInputAssumption>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEclAssumption>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<WholesaleEcl>(w =>
            {
                w.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<EadInputAssumption>(x =>
            {
                x.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
