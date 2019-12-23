using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_affiliateAssumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffiliateAssumption",
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
                    LastAssumptionUpdate = table.Column<DateTime>(nullable: false),
                    LastWholesaleReportingDate = table.Column<DateTime>(nullable: false),
                    LastRetailReportingDate = table.Column<DateTime>(nullable: false),
                    LastObeReportingDate = table.Column<DateTime>(nullable: false),
                    LastSecuritiesReportingDate = table.Column<DateTime>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateAssumption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliateAssumption_AbpOrganizationUnits_OrganizationUnitId",
                        column: x => x.OrganizationUnitId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumptionMacroeconomicInputses",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    MacroeconomicGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumptionMacroeconomicInputses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumptionMacroeconomicInputses_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumptionMacroeconomicProjections",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InputName = table.Column<string>(nullable: true),
                    BestValue = table.Column<double>(nullable: false),
                    OptimisticValue = table.Column<double>(nullable: false),
                    DownturnValue = table.Column<double>(nullable: false),
                    MacroeconomicGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumptionMacroeconomicProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumptionMacroeconomicProjections_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumptionNonInternalModels",
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
                    Key = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    MarginalDefaultRate = table.Column<double>(nullable: false),
                    CummulativeSurvival = table.Column<double>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumptionNonInternalModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumptionNonInternalModels_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumptionNplIndexes",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Actual = table.Column<double>(nullable: false),
                    Standardised = table.Column<double>(nullable: false),
                    EtiNplSeries = table.Column<double>(nullable: false),
                    Statue = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumptionNplIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumptionNplIndexes_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEclPdAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    PdGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclPdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclPdAssumptions_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumptionMacroeconomicInputs",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    MacroeconomicGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumptionMacroeconomicInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumptionMacroeconomicInputs_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumptionMacroeconomicProjections",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InputName = table.Column<string>(nullable: true),
                    BestValue = table.Column<double>(nullable: false),
                    OptimisticValue = table.Column<double>(nullable: false),
                    DownturnValue = table.Column<double>(nullable: false),
                    MacroeconomicGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumptionMacroeconomicProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumptionMacroeconomicProjections_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumptionNonInteralModels",
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
                    Key = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    MarginalDefaultRate = table.Column<double>(nullable: false),
                    CummulativeSurvival = table.Column<double>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumptionNonInteralModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumptionNonInteralModels_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumptionNplIndexes",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Actual = table.Column<double>(nullable: false),
                    Standardised = table.Column<double>(nullable: false),
                    EtiNplSeries = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumptionNplIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumptionNplIndexes_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclPdAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    PdGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclPdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclPdAssumptions_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumptionMacroeconomicInputs",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    MacroEconomicInputGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumptionMacroeconomicInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumptionMacroeconomicInputs_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumptionMacroeconomicProjections",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    InputName = table.Column<string>(nullable: true),
                    BestValue = table.Column<double>(nullable: false),
                    OptimisticValue = table.Column<double>(nullable: false),
                    DownturnValue = table.Column<double>(nullable: false),
                    MacroeconomicGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumptionMacroeconomicProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumptionMacroeconomicProjections_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumptionNonInternalModels",
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
                    Key = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    MarginalDefaultRate = table.Column<double>(nullable: false),
                    CummulativeSurvival = table.Column<double>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumptionNonInternalModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumptionNonInternalModels_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumptionNplIndexes",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Actual = table.Column<double>(nullable: false),
                    Standardised = table.Column<double>(nullable: false),
                    EtiNplSeries = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumptionNplIndexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumptionNplIndexes_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    PdGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumptions_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliateAssumption_OrganizationUnitId",
                table: "AffiliateAssumption",
                column: "OrganizationUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputses_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicProjections_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionNonInternalModels_ObeEclId",
                table: "ObeEclPdAssumptionNonInternalModels",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionNplIndexes_ObeEclId",
                table: "ObeEclPdAssumptionNplIndexes",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptions_ObeEclId",
                table: "ObeEclPdAssumptions",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicInputs_RetailEclId",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicProjections_RetailEclId",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionNonInteralModels_RetailEclId",
                table: "RetailEclPdAssumptionNonInteralModels",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionNplIndexes_RetailEclId",
                table: "RetailEclPdAssumptionNplIndexes",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptions_RetailEclId",
                table: "RetailEclPdAssumptions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicInputs_WholesaleEclId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicProjections_WholesaleEclId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionNonInternalModels_WholesaleEclId",
                table: "WholesaleEclPdAssumptionNonInternalModels",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionNplIndexes_WholesaleEclId",
                table: "WholesaleEclPdAssumptionNplIndexes",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptions_WholesaleEclId",
                table: "WholesaleEclPdAssumptions",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliateAssumption");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumptionNonInternalModels");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumptionNplIndexes");

            migrationBuilder.DropTable(
                name: "ObeEclPdAssumptions");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumptionNplIndexes");

            migrationBuilder.DropTable(
                name: "RetailEclPdAssumptions");

            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumptionNonInternalModels");

            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumptionNplIndexes");

            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumptions");
        }
    }
}
