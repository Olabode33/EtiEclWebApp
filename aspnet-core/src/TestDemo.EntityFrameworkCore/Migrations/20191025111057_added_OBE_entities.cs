using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_OBE_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObeEcls",
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
                    ReportingDate = table.Column<DateTime>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ClosedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEcls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEcls_AbpUsers_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclApprovals",
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
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclApprovals_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclAssumptionApprovals",
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
                    AssumptionType = table.Column<int>(nullable: false),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    DateReviewed = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclAssumptionApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclAssumptionApprovals_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclAssumptionApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Datatype = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    AssumptionGroup = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclAssumptions_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclEadInputAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Datatype = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    EadGroup = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclEadInputAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclEadInputAssumptions_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclLgdAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    LgdGroup = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclLgdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclLgdAssumptions_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumption12Months",
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
                    Credit = table.Column<int>(nullable: false),
                    PD = table.Column<double>(nullable: true),
                    SnPMappingEtiCreditPolicy = table.Column<string>(nullable: true),
                    SnPMappingBestFit = table.Column<string>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumption12Months", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumption12Months_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdSnPCummulativeDefaultRates",
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
                    Key = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Years = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdSnPCummulativeDefaultRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdSnPCummulativeDefaultRates_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclResultSummaryKeyInputs",
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
                    PDGrouping = table.Column<string>(nullable: true),
                    Exposure = table.Column<double>(nullable: true),
                    Collateral = table.Column<double>(nullable: true),
                    UnsecuredPercentage = table.Column<double>(nullable: true),
                    PercentageOfBook = table.Column<double>(nullable: true),
                    Months6CummulativeBestPDs = table.Column<double>(nullable: true),
                    Months12CummulativeBestPDs = table.Column<double>(nullable: true),
                    Months24CummulativeBestPDs = table.Column<double>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclResultSummaryKeyInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclResultSummaryKeyInputs_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclUploads",
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
                    DocType = table.Column<int>(nullable: false),
                    UploadComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclUploads_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObesaleEclResultSummaries",
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
                    SummaryType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PreOverrideExposure = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PreOverrideCoverageRatio = table.Column<double>(nullable: true),
                    PostOverrideExposure = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideCoverageRatio = table.Column<double>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObesaleEclResultSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObesaleEclResultSummaries_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclDataLoanBooks",
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
                    DaysPastDue = table.Column<int>(nullable: true),
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
                    ObeEclUploadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclDataLoanBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclDataLoanBooks_ObeEclUploads_ObeEclUploadId",
                        column: x => x.ObeEclUploadId,
                        principalTable: "ObeEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclDataPaymentSchedules",
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
                    ContractRefNo = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Component = table.Column<string>(nullable: true),
                    NoOfSchedules = table.Column<int>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    ObeEclUploadId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclDataPaymentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclDataPaymentSchedules_ObeEclUploads_ObeEclUploadId",
                        column: x => x.ObeEclUploadId,
                        principalTable: "ObeEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclUploadApprovals",
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
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ObeEclUploadId = table.Column<Guid>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclUploadApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclUploadApprovals_ObeEclUploads_ObeEclUploadId",
                        column: x => x.ObeEclUploadId,
                        principalTable: "ObeEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclUploadApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclComputedEadResults",
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
                    LifetimeEAD = table.Column<string>(nullable: true),
                    ObeEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclComputedEadResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclComputedEadResults_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                        column: x => x.ObeEclDataLoanBookId,
                        principalTable: "ObeEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclResultDetails",
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
                    ContractID = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    OutstandingBalance = table.Column<double>(nullable: true),
                    PreOverrideEclBest = table.Column<double>(nullable: true),
                    PreOverrideEclOptimistic = table.Column<double>(nullable: true),
                    PreOverrideEclDownturn = table.Column<double>(nullable: true),
                    OverrideStage = table.Column<int>(nullable: true),
                    OverrideTTRYears = table.Column<double>(nullable: true),
                    OverrideFSV = table.Column<double>(nullable: true),
                    OverrideOverlay = table.Column<double>(nullable: true),
                    PostOverrideEclBest = table.Column<double>(nullable: true),
                    PostOverrideEclOptimistic = table.Column<double>(nullable: true),
                    PostOverrideEclDownturn = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: true),
                    ObeEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclResultDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclResultDetails_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                        column: x => x.ObeEclDataLoanBookId,
                        principalTable: "ObeEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclResultDetails_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclResultSummaryTopExposures",
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
                    PreOverrideExposure = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PreOverrideCoverageRatio = table.Column<double>(nullable: true),
                    PostOverrideExposure = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideCoverageRatio = table.Column<double>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: true),
                    ObeEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclResultSummaryTopExposures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclResultSummaryTopExposures_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                        column: x => x.ObeEclDataLoanBookId,
                        principalTable: "ObeEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclResultSummaryTopExposures_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclSicrs",
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
                    ComputedSICR = table.Column<int>(nullable: false),
                    OverrideSICR = table.Column<string>(nullable: true),
                    OverrideComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ObeEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclSicrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclSicrs_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                        column: x => x.ObeEclDataLoanBookId,
                        principalTable: "ObeEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclSicrApprovals",
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
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    ObeEclSicrId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclSicrApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclSicrApprovals_ObeEclSicrs_ObeEclSicrId",
                        column: x => x.ObeEclSicrId,
                        principalTable: "ObeEclSicrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ObeEclSicrApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclApprovals_ObeEclId",
                table: "ObeEclApprovals",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclApprovals_ReviewedByUserId",
                table: "ObeEclApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclApprovals_TenantId",
                table: "ObeEclApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptionApprovals_ObeEclId",
                table: "ObeEclAssumptionApprovals",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptionApprovals_ReviewedByUserId",
                table: "ObeEclAssumptionApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptionApprovals_TenantId",
                table: "ObeEclAssumptionApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptions_ObeEclId",
                table: "ObeEclAssumptions",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptions_TenantId",
                table: "ObeEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclComputedEadResults_ObeEclDataLoanBookId",
                table: "ObeEclComputedEadResults",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclComputedEadResults_TenantId",
                table: "ObeEclComputedEadResults",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclDataLoanBooks_ObeEclUploadId",
                table: "ObeEclDataLoanBooks",
                column: "ObeEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclDataLoanBooks_TenantId",
                table: "ObeEclDataLoanBooks",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclDataPaymentSchedules_ObeEclUploadId",
                table: "ObeEclDataPaymentSchedules",
                column: "ObeEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclDataPaymentSchedules_TenantId",
                table: "ObeEclDataPaymentSchedules",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclEadInputAssumptions_ObeEclId",
                table: "ObeEclEadInputAssumptions",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclEadInputAssumptions_TenantId",
                table: "ObeEclEadInputAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclLgdAssumptions_ObeEclId",
                table: "ObeEclLgdAssumptions",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclLgdAssumptions_TenantId",
                table: "ObeEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumption12Months_ObeEclId",
                table: "ObeEclPdAssumption12Months",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumption12Months_TenantId",
                table: "ObeEclPdAssumption12Months",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdSnPCummulativeDefaultRates_ObeEclId",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdSnPCummulativeDefaultRates_TenantId",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultDetails_ObeEclDataLoanBookId",
                table: "ObeEclResultDetails",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultDetails_ObeEclId",
                table: "ObeEclResultDetails",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultDetails_TenantId",
                table: "ObeEclResultDetails",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultSummaryKeyInputs_ObeEclId",
                table: "ObeEclResultSummaryKeyInputs",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultSummaryKeyInputs_TenantId",
                table: "ObeEclResultSummaryKeyInputs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultSummaryTopExposures_ObeEclDataLoanBookId",
                table: "ObeEclResultSummaryTopExposures",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultSummaryTopExposures_ObeEclId",
                table: "ObeEclResultSummaryTopExposures",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclResultSummaryTopExposures_TenantId",
                table: "ObeEclResultSummaryTopExposures",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEcls_ClosedByUserId",
                table: "ObeEcls",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEcls_TenantId",
                table: "ObeEcls",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclSicrApprovals_ObeEclSicrId",
                table: "ObeEclSicrApprovals",
                column: "ObeEclSicrId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclSicrApprovals_ReviewedByUserId",
                table: "ObeEclSicrApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclSicrApprovals_TenantId",
                table: "ObeEclSicrApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclSicrs_ObeEclDataLoanBookId",
                table: "ObeEclSicrs",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclSicrs_TenantId",
                table: "ObeEclSicrs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclUploadApprovals_ObeEclUploadId",
                table: "ObeEclUploadApprovals",
                column: "ObeEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclUploadApprovals_ReviewedByUserId",
                table: "ObeEclUploadApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclUploadApprovals_TenantId",
                table: "ObeEclUploadApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclUploads_ObeEclId",
                table: "ObeEclUploads",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclUploads_TenantId",
                table: "ObeEclUploads",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObesaleEclResultSummaries_ObeEclId",
                table: "ObesaleEclResultSummaries",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObesaleEclResultSummaries_TenantId",
                table: "ObesaleEclResultSummaries",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObeEclApprovals");

            migrationBuilder.DropTable(
                name: "ObeEclAssumptionApprovals");

            migrationBuilder.DropTable(
                name: "ObeEclAssumptions");

            migrationBuilder.DropTable(
                name: "ObeEclComputedEadResults");

            migrationBuilder.DropTable(
                name: "ObeEclDataPaymentSchedules");

            migrationBuilder.DropTable(
                name: "ObeEclEadInputAssumptions");

            migrationBuilder.DropTable(
                name: "ObeEclLgdAssumptions");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumption12Months");

            migrationBuilder.DropTable(
                name: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropTable(
                name: "ObeEclResultDetails");

            migrationBuilder.DropTable(
                name: "ObeEclResultSummaryKeyInputs");

            migrationBuilder.DropTable(
                name: "ObeEclResultSummaryTopExposures");

            migrationBuilder.DropTable(
                name: "ObeEclSicrApprovals");

            migrationBuilder.DropTable(
                name: "ObeEclUploadApprovals");

            migrationBuilder.DropTable(
                name: "ObesaleEclResultSummaries");

            migrationBuilder.DropTable(
                name: "ObeEclSicrs");

            migrationBuilder.DropTable(
                name: "ObeEclDataLoanBooks");

            migrationBuilder.DropTable(
                name: "ObeEclUploads");

            migrationBuilder.DropTable(
                name: "ObeEcls");
        }
    }
}
