using TestDemo.RetailResults;
using TestDemo.RetailComputation;
using TestDemo.RetailInputs;
using TestDemo.RetailAssumption;
using TestDemo.Retail;
using TestDemo.WholesaleResult;
using TestDemo.WholesaleResults;
using TestDemo.WholesaleComputation;
using TestDemo.WholesaleInputs;
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
        public virtual DbSet<RetailEclResultSummaryTopExposure> RetailEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<RetailEclResultSummaryKeyInput> RetailEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<RetailEclResultSummary> RetailEclResultSummaries { get; set; }

        public virtual DbSet<RetailEclResultDetail> RetailEclResultDetails { get; set; }

        public virtual DbSet<RetailEclComputedEadResult> RetailEclComputedEadResults { get; set; }

        public virtual DbSet<RetailEclSicrApproval> RetailEclSicrApprovals { get; set; }

        public virtual DbSet<RetailEclSicr> RetailEclSicrs { get; set; }

        public virtual DbSet<RetailEclDataPaymentSchedule> RetailEclDataPaymentSchedules { get; set; }

        public virtual DbSet<RetailEclDataLoanBook> RetailEclDataLoanBooks { get; set; }

        public virtual DbSet<RetailEclUploadApproval> RetailEclUploadApprovals { get; set; }

        public virtual DbSet<RetailEclUpload> RetailEclUploads { get; set; }

        public virtual DbSet<RetailEclPdSnPCummulativeDefaultRate> RetailEclPdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<RetailEclPdAssumption12Month> RetailEclPdAssumption12Months { get; set; }

        public virtual DbSet<RetailEclLgdAssumption> RetailEclLgdAssumptions { get; set; }

        public virtual DbSet<RetailEclEadInputAssumption> RetailEclEadInputAssumptions { get; set; }

        public virtual DbSet<RetailEclAssumptionApproval> RetailEclAssumptionApprovalses { get; set; }

        public virtual DbSet<RetailEclAssumption> RetailEclAssumptions { get; set; }

        public virtual DbSet<RetailEclApproval> RetailEclApprovals { get; set; }

        public virtual DbSet<RetailEcl> RetailEcls { get; set; }

        public virtual DbSet<WholesaleEclResultSummaryTopExposure> WholesaleEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<WholesaleEclResultSummaryKeyInput> WholesaleEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<WholesaleEclResultSummary> WholesaleEclResultSummaries { get; set; }

        public virtual DbSet<WholesaleEclResultDetail> WholesaleEclResultDetails { get; set; }

        public virtual DbSet<WholesaleEclComputedEadResult> WholesaleEclComputedEadResults { get; set; }

        public virtual DbSet<WholesaleEclSicrApproval> WholesaleEclSicrApprovals { get; set; }

        public virtual DbSet<WholesaleEclSicr> WholesaleEclSicrs { get; set; }

        public virtual DbSet<WholesaleEclDataPaymentSchedule> WholesaleEclDataPaymentSchedules { get; set; }

        public virtual DbSet<WholesaleEclDataLoanBook> WholesaleEclDataLoanBooks { get; set; }

        public virtual DbSet<WholesaleEclUploadApproval> WholesaleEclUploadApprovals { get; set; }

        public virtual DbSet<WholesaleEclUpload> WholesaleEclUploads { get; set; }


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

























           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
            modelBuilder.Entity<RetailEclResultSummaryTopExposure>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclResultSummaryKeyInput>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclResultSummary>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclResultDetail>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclComputedEadResult>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclSicrApproval>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclSicr>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclDataPaymentSchedule>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclDataLoanBook>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclUploadApproval>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclUpload>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclPdSnPCummulativeDefaultRate>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclPdAssumption12Month>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclLgdAssumption>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclEadInputAssumption>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclAssumptionApproval>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclAssumption>(r =>
            {
                r.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<RetailEclApproval>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<RetailEcl>(r =>
                       {
                           r.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclResultSummaryTopExposure>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclResultSummaryKeyInput>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclResultSummary>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclResultDetail>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclComputedEadResult>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclSicrApproval>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclSicr>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclDataPaymentSchedule>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclDataLoanBook>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclUploadApproval>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<WholesaleEclUpload>(w =>
                       {
                           w.HasIndex(e => new { e.TenantId });
                       });
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
