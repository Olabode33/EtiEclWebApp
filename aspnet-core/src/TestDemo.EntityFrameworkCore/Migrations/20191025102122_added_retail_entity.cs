using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_retail_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetailEclAssumptionApprovals",
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
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclAssumptionApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclAssumptionApprovals_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailEclAssumptionApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclAssumptions",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclAssumptions_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclEadInputAssumptions",
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
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclEadInputAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclEadInputAssumptions_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclLgdAssumptions",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclLgdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclLgdAssumptions_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumption12Months",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumption12Months", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumption12Months_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdSnPCummulativeDefaultRates",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdSnPCummulativeDefaultRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdSnPCummulativeDefaultRates_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclResultSummaries",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclResultSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclResultSummaries_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclResultSummaryKeyInputs",
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
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclResultSummaryKeyInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclResultSummaryKeyInputs_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclUploads",
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
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclUploads_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclDataLoanBooks",
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
                    RetailEclUploadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclDataLoanBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclDataLoanBooks_RetailEclUploads_RetailEclUploadId",
                        column: x => x.RetailEclUploadId,
                        principalTable: "RetailEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclDataPaymentSchedules",
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
                    RetailEclUploadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclDataPaymentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclDataPaymentSchedules_RetailEclUploads_RetailEclUploadId",
                        column: x => x.RetailEclUploadId,
                        principalTable: "RetailEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclUploadApprovals",
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
                    RetailEclUploadId = table.Column<Guid>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclUploadApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclUploadApprovals_RetailEclUploads_RetailEclUploadId",
                        column: x => x.RetailEclUploadId,
                        principalTable: "RetailEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetailEclUploadApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclComputedEadResults",
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
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclComputedEadResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclComputedEadResults_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclResultDetails",
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
                    RetailEclId = table.Column<Guid>(nullable: true),
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclResultDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclResultDetails_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailEclResultDetails_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclResultSummaryTopExposures",
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
                    RetailEclId = table.Column<Guid>(nullable: false),
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclResultSummaryTopExposures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclResultSummaryTopExposures_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailEclResultSummaryTopExposures_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclSicrs",
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
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclSicrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrs_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclSicrApprovals",
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
                    RetailEclSicrId = table.Column<Guid>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclSicrApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrApprovals_RetailEclSicrs_RetailEclSicrId",
                        column: x => x.RetailEclSicrId,
                        principalTable: "RetailEclSicrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptionApprovals_RetailEclId",
                table: "RetailEclAssumptionApprovals",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptionApprovals_ReviewedByUserId",
                table: "RetailEclAssumptionApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptionApprovals_TenantId",
                table: "RetailEclAssumptionApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptions_RetailEclId",
                table: "RetailEclAssumptions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptions_TenantId",
                table: "RetailEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclComputedEadResults_RetailEclDataLoanBookId",
                table: "RetailEclComputedEadResults",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclComputedEadResults_TenantId",
                table: "RetailEclComputedEadResults",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclDataLoanBooks_RetailEclUploadId",
                table: "RetailEclDataLoanBooks",
                column: "RetailEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclDataLoanBooks_TenantId",
                table: "RetailEclDataLoanBooks",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclDataPaymentSchedules_RetailEclUploadId",
                table: "RetailEclDataPaymentSchedules",
                column: "RetailEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclDataPaymentSchedules_TenantId",
                table: "RetailEclDataPaymentSchedules",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclEadInputAssumptions_RetailEclId",
                table: "RetailEclEadInputAssumptions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclEadInputAssumptions_TenantId",
                table: "RetailEclEadInputAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclLgdAssumptions_RetailEclId",
                table: "RetailEclLgdAssumptions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclLgdAssumptions_TenantId",
                table: "RetailEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumption12Months_RetailEclId",
                table: "RetailEclPdAssumption12Months",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumption12Months_TenantId",
                table: "RetailEclPdAssumption12Months",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdSnPCummulativeDefaultRates_RetailEclId",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdSnPCummulativeDefaultRates_TenantId",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultDetails_RetailEclDataLoanBookId",
                table: "RetailEclResultDetails",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultDetails_RetailEclId",
                table: "RetailEclResultDetails",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultDetails_TenantId",
                table: "RetailEclResultDetails",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaries_RetailEclId",
                table: "RetailEclResultSummaries",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaries_TenantId",
                table: "RetailEclResultSummaries",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaryKeyInputs_RetailEclId",
                table: "RetailEclResultSummaryKeyInputs",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaryKeyInputs_TenantId",
                table: "RetailEclResultSummaryKeyInputs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaryTopExposures_RetailEclDataLoanBookId",
                table: "RetailEclResultSummaryTopExposures",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaryTopExposures_RetailEclId",
                table: "RetailEclResultSummaryTopExposures",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclResultSummaryTopExposures_TenantId",
                table: "RetailEclResultSummaryTopExposures",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_RetailEclSicrId",
                table: "RetailEclSicrApprovals",
                column: "RetailEclSicrId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_ReviewedByUserId",
                table: "RetailEclSicrApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_TenantId",
                table: "RetailEclSicrApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrs_RetailEclDataLoanBookId",
                table: "RetailEclSicrs",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrs_TenantId",
                table: "RetailEclSicrs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclUploadApprovals_RetailEclUploadId",
                table: "RetailEclUploadApprovals",
                column: "RetailEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclUploadApprovals_ReviewedByUserId",
                table: "RetailEclUploadApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclUploadApprovals_TenantId",
                table: "RetailEclUploadApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclUploads_RetailEclId",
                table: "RetailEclUploads",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclUploads_TenantId",
                table: "RetailEclUploads",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetailEclAssumptionApprovals");

            migrationBuilder.DropTable(
                name: "RetailEclAssumptions");

            migrationBuilder.DropTable(
                name: "RetailEclComputedEadResults");

            migrationBuilder.DropTable(
                name: "RetailEclDataPaymentSchedules");

            migrationBuilder.DropTable(
                name: "RetailEclEadInputAssumptions");

            migrationBuilder.DropTable(
                name: "RetailEclLgdAssumptions");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumption12Months");

            migrationBuilder.DropTable(
                name: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropTable(
                name: "RetailEclResultDetails");

            migrationBuilder.DropTable(
                name: "RetailEclResultSummaries");

            migrationBuilder.DropTable(
                name: "RetailEclResultSummaryKeyInputs");

            migrationBuilder.DropTable(
                name: "RetailEclResultSummaryTopExposures");

            migrationBuilder.DropTable(
                name: "RetailEclSicrApprovals");

            migrationBuilder.DropTable(
                name: "RetailEclUploadApprovals");

            migrationBuilder.DropTable(
                name: "RetailEclSicrs");

            migrationBuilder.DropTable(
                name: "RetailEclDataLoanBooks");

            migrationBuilder.DropTable(
                name: "RetailEclUploads");
        }
    }
}
