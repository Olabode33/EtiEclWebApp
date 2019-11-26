using TestDemo.LgdCalibrationResult;
using TestDemo.LgdCalibrationResult;
using TestDemo.EadCalibrationResult;
using TestDemo.GeneralCalibrationResult;
using TestDemo.PdCalibrationResult;
using TestDemo.ObeResults;
using TestDemo.ObeComputation;
using TestDemo.ObeInputs;
using TestDemo.ObeAssumption;
using TestDemo.OBE;
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
        public virtual DbSet<WholesalePdScenarioRedefaultLifetime> WholesalePdRedefaultLifetimes { get; set; }

        public virtual DbSet<WholesalePdScenarioLifetime> WholesalePdLifetimeBests { get; set; }

        public virtual DbSet<WholesalePdMapping> WholesalePdMappings { get; set; }

        public virtual DbSet<WholesaleLgdCollateralTypeData> WholesaleLgdCollateralTypeDatas { get; set; }

        public virtual DbSet<WholesaleLgdContractData> WholesaleLgdContractDatas { get; set; }

        public virtual DbSet<WholesaleEadEirProjection> WholesaleEadEirProjections { get; set; }

        public virtual DbSet<WholesaleEadCirProjection> WholesaleEadCirProjections { get; set; }

        public virtual DbSet<WholesaleEadInput> WholesaleEadInputs { get; set; }

        public virtual DbSet<CalibrationResultLgd> CalibrationResultLgds { get; set; }

        //public virtual DbSet<CalibrationResult> CalibrationResults { get; set; }

        public virtual DbSet<CalibrationResultPdScenarioMacroeconomicProjection> PdScenarioMacroeconomicProjections { get; set; }

        public virtual DbSet<CalibrationResultPdStatisticalInput> PdStatisticalInputs { get; set; }

        public virtual DbSet<CalibrationResultPdEtiNpl> PdEtiNpls { get; set; }

        public virtual DbSet<CalibrationResultPdHistoricIndex> PdHistoricIndexes { get; set; }

        public virtual DbSet<CalibrationResultPdCummulativeSurvival> PdCummulativeSurvivals { get; set; }

        public virtual DbSet<CalibrationResultPdMarginalDefaultRate> PdMarginalDefaultRates { get; set; }

        public virtual DbSet<CalibrationResultPdUpperbound> PdUpperbounds { get; set; }

        public virtual DbSet<CalibrationResultPdSnPCummulativeDefaultRate> PdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<CalibrationResult12MonthPd> Pd12MonthPds { get; set; }

        public virtual DbSet<ObeEclResultSummaryTopExposure> ObeEclResultSummaryTopExposures { get; set; }

        public virtual DbSet<ObeEclResultSummaryKeyInput> ObeEclResultSummaryKeyInputs { get; set; }

        public virtual DbSet<ObesaleEclResultSummary> ObesaleEclResultSummaries { get; set; }

        public virtual DbSet<ObeEclResultDetail> ObeEclResultDetails { get; set; }

        public virtual DbSet<ObeEclComputedEadResult> ObeEclComputedEadResults { get; set; }

        public virtual DbSet<ObeEclSicrApproval> ObeEclSicrApprovals { get; set; }

        public virtual DbSet<ObeEclSicr> ObeEclSicrs { get; set; }

        public virtual DbSet<ObeEclDataPaymentSchedule> ObeEclDataPaymentSchedules { get; set; }

        public virtual DbSet<ObeEclDataLoanBook> ObeEclDataLoanBooks { get; set; }

        public virtual DbSet<ObeEclUploadApproval> ObeEclUploadApprovals { get; set; }

        public virtual DbSet<ObeEclUpload> ObeEclUploads { get; set; }

        public virtual DbSet<ObeEclPdSnPCummulativeDefaultRate> ObeEclPdSnPCummulativeDefaultRates { get; set; }

        public virtual DbSet<ObeEclPdAssumption12Month> ObeEclPdAssumption12Months { get; set; }

        public virtual DbSet<ObeEclLgdAssumption> ObeEclLgdAssumptions { get; set; }

        public virtual DbSet<ObeEclEadInputAssumption> ObeEclEadInputAssumptions { get; set; }

        public virtual DbSet<ObeEclAssumptionApproval> ObeEclAssumptionApprovals { get; set; }

        public virtual DbSet<ObeEclAssumption> ObeEclAssumptions { get; set; }

        public virtual DbSet<ObeEclApproval> ObeEclApprovals { get; set; }

        public virtual DbSet<ObeEcl> ObeEcls { get; set; }

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

























           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
           
            modelBuilder.Entity<ObeEclResultSummaryTopExposure>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclResultSummaryKeyInput>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObesaleEclResultSummary>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclResultDetail>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclComputedEadResult>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclSicrApproval>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclSicr>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclDataPaymentSchedule>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclDataLoanBook>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclUploadApproval>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclUpload>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclPdSnPCummulativeDefaultRate>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclPdAssumption12Month>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclLgdAssumption>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclEadInputAssumption>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclAssumptionApproval>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclAssumption>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEclApproval>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<ObeEcl>(o =>
            {
                o.HasIndex(e => new { e.TenantId });
            });
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
