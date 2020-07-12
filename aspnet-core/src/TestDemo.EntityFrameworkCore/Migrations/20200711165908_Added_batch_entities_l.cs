using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_batch_entities_l : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchEclDataLoanBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    CustomerNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    ContractNo = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    ProductMapping = table.Column<string>(nullable: true),
                    SpecialisedLending = table.Column<string>(nullable: true),
                    RatingModel = table.Column<string>(nullable: true),
                    OriginalRating = table.Column<int>(nullable: true),
                    CurrentRating = table.Column<int>(nullable: true),
                    LifetimePD = table.Column<double>(nullable: true),
                    Month12PD = table.Column<double>(nullable: true),
                    DaysPastDue = table.Column<double>(nullable: true),
                    WatchlistIndicator = table.Column<bool>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    ImpairedDate = table.Column<DateTime>(nullable: true),
                    DefaultDate = table.Column<DateTime>(nullable: true),
                    CreditLimit = table.Column<double>(nullable: true),
                    OriginalBalanceLCY = table.Column<double>(nullable: true),
                    OutstandingBalanceLCY = table.Column<double>(nullable: true),
                    OutstandingBalanceACY = table.Column<double>(nullable: true),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    RestructureIndicator = table.Column<bool>(nullable: false),
                    RestructureRisk = table.Column<string>(nullable: true),
                    RestructureType = table.Column<string>(nullable: true),
                    RestructureStartDate = table.Column<DateTime>(nullable: true),
                    RestructureEndDate = table.Column<DateTime>(nullable: true),
                    PrincipalPaymentTermsOrigination = table.Column<string>(nullable: true),
                    PPTOPeriod = table.Column<int>(nullable: true),
                    InterestPaymentTermsOrigination = table.Column<string>(nullable: true),
                    IPTOPeriod = table.Column<int>(nullable: true),
                    PrincipalPaymentStructure = table.Column<string>(nullable: true),
                    InterestPaymentStructure = table.Column<string>(nullable: true),
                    InterestRateType = table.Column<string>(nullable: true),
                    BaseRate = table.Column<string>(nullable: true),
                    OriginationContractualInterestRate = table.Column<string>(nullable: true),
                    IntroductoryPeriod = table.Column<int>(nullable: true),
                    PostIPContractualInterestRate = table.Column<double>(nullable: true),
                    CurrentContractualInterestRate = table.Column<double>(nullable: true),
                    EIR = table.Column<double>(nullable: true),
                    DebentureOMV = table.Column<double>(nullable: true),
                    DebentureFSV = table.Column<double>(nullable: true),
                    CashOMV = table.Column<double>(nullable: true),
                    CashFSV = table.Column<double>(nullable: true),
                    InventoryOMV = table.Column<double>(nullable: true),
                    InventoryFSV = table.Column<double>(nullable: true),
                    PlantEquipmentOMV = table.Column<double>(nullable: true),
                    PlantEquipmentFSV = table.Column<double>(nullable: true),
                    ResidentialPropertyOMV = table.Column<double>(nullable: true),
                    ResidentialPropertyFSV = table.Column<double>(nullable: true),
                    CommercialPropertyOMV = table.Column<double>(nullable: true),
                    CommercialProperty = table.Column<double>(nullable: true),
                    ReceivablesOMV = table.Column<double>(nullable: true),
                    ReceivablesFSV = table.Column<double>(nullable: true),
                    SharesOMV = table.Column<double>(nullable: true),
                    SharesFSV = table.Column<double>(nullable: true),
                    VehicleOMV = table.Column<double>(nullable: true),
                    VehicleFSV = table.Column<double>(nullable: true),
                    CureRate = table.Column<double>(nullable: true),
                    GuaranteeIndicator = table.Column<bool>(nullable: false),
                    GuarantorPD = table.Column<string>(nullable: true),
                    GuarantorLGD = table.Column<string>(nullable: true),
                    GuaranteeValue = table.Column<double>(nullable: true),
                    GuaranteeLevel = table.Column<double>(nullable: true),
                    ContractId = table.Column<string>(nullable: true),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchEclDataLoanBooks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchEclDataPaymentSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ContractRefNo = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Component = table.Column<string>(nullable: true),
                    NoOfSchedules = table.Column<int>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    BatchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchEclDataPaymentSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchEcls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ReportingDate = table.Column<DateTime>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ExceptionComment = table.Column<string>(nullable: true),
                    ClosedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchEcls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchEcls_AbpUsers_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackUploadedLoanBook",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    CustomerNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    ContractNo = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    SnapshotDate = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    ProductMapping = table.Column<string>(nullable: true),
                    SpecialisedLending = table.Column<string>(nullable: true),
                    RatingModel = table.Column<string>(nullable: true),
                    OriginalRating = table.Column<string>(nullable: true),
                    CurrentRating = table.Column<string>(nullable: true),
                    LifetimePD = table.Column<string>(nullable: true),
                    Month12PD = table.Column<string>(nullable: true),
                    DaysPastDue = table.Column<string>(nullable: true),
                    WatchlistIndicator = table.Column<bool>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    ImpairedDate = table.Column<string>(nullable: true),
                    DefaultDate = table.Column<string>(nullable: true),
                    CreditLimit = table.Column<string>(nullable: true),
                    OriginalBalanceLCY = table.Column<string>(nullable: true),
                    OutstandingBalanceLCY = table.Column<string>(nullable: true),
                    OutstandingBalanceACY = table.Column<string>(nullable: true),
                    ContractStartDate = table.Column<string>(nullable: true),
                    ContractEndDate = table.Column<string>(nullable: true),
                    RestructureIndicator = table.Column<bool>(nullable: false),
                    RestructureRisk = table.Column<string>(nullable: true),
                    RestructureType = table.Column<string>(nullable: true),
                    RestructureStartDate = table.Column<string>(nullable: true),
                    RestructureEndDate = table.Column<string>(nullable: true),
                    PrincipalPaymentTermsOrigination = table.Column<string>(nullable: true),
                    PPTOPeriod = table.Column<string>(nullable: true),
                    InterestPaymentTermsOrigination = table.Column<string>(nullable: true),
                    IPTOPeriod = table.Column<string>(nullable: true),
                    PrincipalPaymentStructure = table.Column<string>(nullable: true),
                    InterestPaymentStructure = table.Column<string>(nullable: true),
                    InterestRateType = table.Column<string>(nullable: true),
                    BaseRate = table.Column<string>(nullable: true),
                    OriginationContractualInterestRate = table.Column<string>(nullable: true),
                    IntroductoryPeriod = table.Column<string>(nullable: true),
                    PostIPContractualInterestRate = table.Column<string>(nullable: true),
                    CurrentContractualInterestRate = table.Column<string>(nullable: true),
                    EIR = table.Column<string>(nullable: true),
                    DebentureOMV = table.Column<string>(nullable: true),
                    DebentureFSV = table.Column<string>(nullable: true),
                    CashOMV = table.Column<string>(nullable: true),
                    CashFSV = table.Column<string>(nullable: true),
                    InventoryOMV = table.Column<string>(nullable: true),
                    InventoryFSV = table.Column<string>(nullable: true),
                    PlantEquipmentOMV = table.Column<string>(nullable: true),
                    PlantEquipmentFSV = table.Column<string>(nullable: true),
                    ResidentialPropertyOMV = table.Column<string>(nullable: true),
                    ResidentialPropertyFSV = table.Column<string>(nullable: true),
                    CommercialPropertyOMV = table.Column<string>(nullable: true),
                    CommercialProperty = table.Column<string>(nullable: true),
                    ReceivablesOMV = table.Column<string>(nullable: true),
                    ReceivablesFSV = table.Column<string>(nullable: true),
                    SharesOMV = table.Column<string>(nullable: true),
                    SharesFSV = table.Column<string>(nullable: true),
                    VehicleOMV = table.Column<string>(nullable: true),
                    VehicleFSV = table.Column<string>(nullable: true),
                    CureRate = table.Column<string>(nullable: true),
                    GuaranteeIndicator = table.Column<bool>(nullable: false),
                    GuarantorPD = table.Column<string>(nullable: true),
                    GuarantorLGD = table.Column<string>(nullable: true),
                    GuaranteeValue = table.Column<string>(nullable: true),
                    GuaranteeLevel = table.Column<string>(nullable: true),
                    ContractId = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackUploadedLoanBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchEclApprovals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    BatchEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchEclApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchEclApprovals_BatchEcls_BatchEclId",
                        column: x => x.BatchEclId,
                        principalTable: "BatchEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BatchEclApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BatchEclUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    DocType = table.Column<int>(nullable: false),
                    UploadComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AllJobs = table.Column<int>(nullable: false),
                    CompletedJobs = table.Column<int>(nullable: false),
                    FileUploaded = table.Column<bool>(nullable: false),
                    CountWholesaleData = table.Column<int>(nullable: false),
                    CountRetailData = table.Column<int>(nullable: false),
                    CountObeData = table.Column<int>(nullable: false),
                    CountTotalData = table.Column<int>(nullable: false),
                    BatchId = table.Column<Guid>(nullable: false),
                    BatchEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchEclUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BatchEclUploads_BatchEcls_BatchEclId",
                        column: x => x.BatchEclId,
                        principalTable: "BatchEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchEclApprovals_BatchEclId",
                table: "BatchEclApprovals",
                column: "BatchEclId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchEclApprovals_ReviewedByUserId",
                table: "BatchEclApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchEcls_ClosedByUserId",
                table: "BatchEcls",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchEclUploads_BatchEclId",
                table: "BatchEclUploads",
                column: "BatchEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchEclApprovals");

            migrationBuilder.DropTable(
                name: "BatchEclDataLoanBooks");

            migrationBuilder.DropTable(
                name: "BatchEclDataPaymentSchedules");

            migrationBuilder.DropTable(
                name: "BatchEclUploads");

            migrationBuilder.DropTable(
                name: "TrackUploadedLoanBook");

            migrationBuilder.DropTable(
                name: "BatchEcls");
        }
    }
}
