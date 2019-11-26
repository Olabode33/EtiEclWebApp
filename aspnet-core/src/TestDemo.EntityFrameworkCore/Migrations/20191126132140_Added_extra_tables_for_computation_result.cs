using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_extra_tables_for_computation_result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationResult12MonthPds",
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
                    Credit = table.Column<int>(nullable: false),
                    Pd = table.Column<double>(nullable: true),
                    SnPMappingEtiCreditPolicy = table.Column<string>(nullable: true),
                    SnPMappingBestFit = table.Column<string>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult12MonthPds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultLgds",
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
                    ResultGroup = table.Column<int>(nullable: false),
                    InputName = table.Column<string>(nullable: true),
                    InputValue = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultLgds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdCummulativeSurvivals",
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
                    Month = table.Column<int>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdCummulativeSurvivals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdEtiNpls",
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
                    Date = table.Column<DateTime>(nullable: false),
                    Series = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdEtiNpls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdHistoricIndexes",
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
                    Date = table.Column<DateTime>(nullable: false),
                    Actual = table.Column<double>(nullable: false),
                    Standardised = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdHistoricIndexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdMarginalDefaultRates",
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
                    Month = table.Column<int>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdMarginalDefaultRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdScenarioMacroeconomicProjections",
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
                    Module = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PrimeLendingRate = table.Column<double>(nullable: false),
                    OilExport = table.Column<double>(nullable: false),
                    RealGdpGrowthRate = table.Column<double>(nullable: false),
                    DifferencedRealGdp = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdScenarioMacroeconomicProjections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdSnPCummulativeDefaultRates",
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
                    Rating = table.Column<string>(nullable: true),
                    Years = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdSnPCummulativeDefaultRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdStatisticalInputs",
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
                    StatisticalInputs = table.Column<string>(nullable: true),
                    PrimeLendingRate = table.Column<double>(nullable: false),
                    OilExport = table.Column<double>(nullable: false),
                    RealGdpGrowthRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdStatisticalInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResultPdUpperbounds",
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
                    Rating = table.Column<string>(nullable: true),
                    Upperbound12MonthPd = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResultPdUpperbounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObeEadCirProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CIR_EFFECTIVE = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEadCirProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEadCirProjections_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEadEirProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EIR_GROUP = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEadEirProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEadEirProjections_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeEadInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    EIR_GROUP = table.Column<string>(nullable: true),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEadInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEadInputs_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObeLgdCollateralTypeDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    DEBENTURE_OMV = table.Column<double>(nullable: false),
                    CASH_OMV = table.Column<double>(nullable: false),
                    INVENTORY_OMV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_OMV = table.Column<double>(nullable: false),
                    RESIDENTIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    RECEIVABLES_OMV = table.Column<double>(nullable: false),
                    SHARE_OMV = table.Column<double>(nullable: false),
                    VEHICLE_OMV = table.Column<double>(nullable: false),
                    DEBENTURE_FSV = table.Column<double>(nullable: false),
                    CASH_FSV = table.Column<double>(nullable: false),
                    INVENTORY_FSV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_FSV = table.Column<double>(nullable: false),
                    RESIDENTIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    RECEIVABLES_FSV = table.Column<double>(nullable: false),
                    SHARES_FSV = table.Column<double>(nullable: false),
                    VEHICLE_FSV = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeLgdCollateralTypeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeLgdCollateralTypeDatas_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObeLgdContractDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    TTR_YEARS = table.Column<double>(nullable: false),
                    COST_OF_RECOVERY = table.Column<double>(nullable: false),
                    GUARANTOR_PD = table.Column<double>(nullable: false),
                    GUARANTOR_LGD = table.Column<double>(nullable: false),
                    GUARANTEE_VALUE = table.Column<double>(nullable: false),
                    GUARANTEE_LEVEL = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeLgdContractDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeLgdContractDatas_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObePdLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdLifetimeBests_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObePdLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdLifetimeDownturns_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObePdLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdLifetimeOptimistics_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObePdMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    PdGroup = table.Column<string>(nullable: true),
                    TtmMonths = table.Column<int>(nullable: false),
                    MaxDpd = table.Column<int>(nullable: false),
                    MaxClassificationScore = table.Column<int>(nullable: false),
                    Pd12Month = table.Column<double>(nullable: false),
                    LifetimePd = table.Column<double>(nullable: false),
                    RedefaultLifetimePd = table.Column<double>(nullable: false),
                    Stage1Transition = table.Column<int>(nullable: false),
                    Stage2Transition = table.Column<int>(nullable: false),
                    DaysPastDue = table.Column<int>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdMappings_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObePdRedefaultLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdRedefaultLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdRedefaultLifetimeBests_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObePdRedefaultLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdRedefaultLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdRedefaultLifetimeDownturns_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ObePdRedefaultLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObePdRedefaultLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObePdRedefaultLifetimeOptimistics_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEadCirProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CIR_EFFECTIVE = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEadCirProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEadCirProjections_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEadEirProjetions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EIR_Group = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEadEirProjetions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEadEirProjetions_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEadInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    EIR_GROUP = table.Column<string>(nullable: true),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEadInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEadInputs_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailLgdCollateralTypeDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    DEBENTURE_OMV = table.Column<double>(nullable: false),
                    CASH_OMV = table.Column<double>(nullable: false),
                    INVENTORY_OMV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_OMV = table.Column<string>(nullable: true),
                    RESIDENTIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    RECEIVABLES_OMV = table.Column<double>(nullable: false),
                    SHARES_OMV = table.Column<double>(nullable: false),
                    VEHICLE_OMV = table.Column<double>(nullable: false),
                    DEBENTURE_FSV = table.Column<double>(nullable: false),
                    CASH_FSV = table.Column<double>(nullable: false),
                    INVENTORY_FSV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_FSV = table.Column<double>(nullable: false),
                    RESIDENTIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    RECEIVABLES_FSV = table.Column<double>(nullable: false),
                    SHARES_FSV = table.Column<double>(nullable: false),
                    VEHICLE_FSV = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailLgdCollateralTypeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailLgdCollateralTypeDatas_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailLgdContractDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    TTR_YEARS = table.Column<double>(nullable: false),
                    COST_OF_RECOVERY = table.Column<double>(nullable: false),
                    GUARANTOR_PD = table.Column<double>(nullable: false),
                    GUARANTOR_LGD = table.Column<double>(nullable: false),
                    GUARANTEE_VALUE = table.Column<double>(nullable: false),
                    GUARANTEE_LEVEL = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailLgdContractDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailLgdContractDatas_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdLifetimeBests_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdLifetimeDownturns_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdLifetimeOptimistics_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    PdGroup = table.Column<string>(nullable: true),
                    TtmMonths = table.Column<int>(nullable: false),
                    MaxDpd = table.Column<int>(nullable: false),
                    MaxClassificationScore = table.Column<int>(nullable: false),
                    Pd12Month = table.Column<double>(nullable: false),
                    LifetimePd = table.Column<double>(nullable: false),
                    RedefaultLifetimePd = table.Column<double>(nullable: false),
                    Stage1Transition = table.Column<int>(nullable: false),
                    Stage2Transition = table.Column<int>(nullable: false),
                    DaysPastDue = table.Column<int>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdMappings_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdRedefaultLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdRedefaultLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdRedefaultLifetimeBests_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdRedefaultLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdRedefaultLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdRedefaultLifetimeDownturns_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailPdRedefaultLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailPdRedefaultLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailPdRedefaultLifetimeOptimistics_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEadCirProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    CIR_EFFECTIVE = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEadCirProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEadCirProjections_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEadEirProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EIR_GROUP = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEadEirProjections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEadEirProjections_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEadInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    EIR_GROUP = table.Column<string>(nullable: true),
                    CIR_GROUP = table.Column<string>(nullable: true),
                    Months = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEadInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEadInputs_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleLgdCollateralTypeDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    DEBENTURE_OMV = table.Column<double>(nullable: false),
                    CASH_OMV = table.Column<double>(nullable: false),
                    INVENTORY_OMV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_OMV = table.Column<double>(nullable: false),
                    RESIDENTIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_OMV = table.Column<double>(nullable: false),
                    RECEIVABLES_OMV = table.Column<double>(nullable: false),
                    SHARES_OMV = table.Column<double>(nullable: false),
                    VEHICLE_OMV = table.Column<double>(nullable: false),
                    DEBENTURE_FSV = table.Column<double>(nullable: false),
                    CASH_FSV = table.Column<double>(nullable: false),
                    INVENTORY_FSV = table.Column<double>(nullable: false),
                    PLANT_AND_EQUIPMENT_FSV = table.Column<double>(nullable: false),
                    RESIDENTIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    COMMERCIAL_PROPERTY_FSV = table.Column<double>(nullable: false),
                    RECEIVABLES_FSV = table.Column<double>(nullable: false),
                    SHARES_FSV = table.Column<double>(nullable: false),
                    VEHICLE_FSV = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleLgdCollateralTypeDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleLgdCollateralTypeDatas_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleLgdContractDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CONTRACT_NO = table.Column<string>(nullable: true),
                    TTR_YEARS = table.Column<double>(nullable: false),
                    COST_OF_RECOVERY = table.Column<double>(nullable: false),
                    GUARANTOR_PD = table.Column<double>(nullable: false),
                    GUARANTOR_LGD = table.Column<double>(nullable: false),
                    GUARANTEE_VALUE = table.Column<double>(nullable: false),
                    GUARANTEE_LEVEL = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleLgdContractDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleLgdContractDatas_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdLifetimeBests_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<string>(nullable: true),
                    PdGroup = table.Column<string>(nullable: true),
                    TtmMonths = table.Column<int>(nullable: false),
                    MaxDpd = table.Column<int>(nullable: false),
                    MaxClassificationScore = table.Column<int>(nullable: false),
                    Pd12Month = table.Column<double>(nullable: false),
                    LifetimePd = table.Column<double>(nullable: false),
                    RedefaultLifetimePD = table.Column<double>(nullable: false),
                    Stage1Transition = table.Column<int>(nullable: false),
                    Stage2Transition = table.Column<int>(nullable: false),
                    DaysPastDue = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdMappings_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdRedefaultLifetimeBests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdRedefaultLifetimeBests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdRedefaultLifetimeBests_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdRedefaultLifetimeDownturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdRedefaultLifetimeDownturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdRedefaultLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesalePdRedefaultLifetimeOptimistics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PdGroup = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesalePdRedefaultLifetimeOptimistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesalePdRedefaultLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadCirProjections_ObeEclId",
                table: "ObeEadCirProjections",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadEirProjections_ObeEclId",
                table: "ObeEadEirProjections",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadInputs_ObeEclId",
                table: "ObeEadInputs",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeLgdCollateralTypeDatas_ObeEclId",
                table: "ObeLgdCollateralTypeDatas",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeLgdContractDatas_ObeEclId",
                table: "ObeLgdContractDatas",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeBests_ObeEclId",
                table: "ObePdLifetimeBests",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeDownturns_ObeEclId",
                table: "ObePdLifetimeDownturns",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeOptimistics_ObeEclId",
                table: "ObePdLifetimeOptimistics",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdMappings_ObeEclId",
                table: "ObePdMappings",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeBests_ObeEclId",
                table: "ObePdRedefaultLifetimeBests",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeDownturns_ObeEclId",
                table: "ObePdRedefaultLifetimeDownturns",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeOptimistics_ObeEclId",
                table: "ObePdRedefaultLifetimeOptimistics",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadCirProjections_RetailEclId",
                table: "RetailEadCirProjections",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadEirProjetions_RetailEclId",
                table: "RetailEadEirProjetions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadInputs_RetailEclId",
                table: "RetailEadInputs",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLgdCollateralTypeDatas_RetailEclId",
                table: "RetailLgdCollateralTypeDatas",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLgdContractDatas_RetailEclId",
                table: "RetailLgdContractDatas",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeBests_RetailEclId",
                table: "RetailPdLifetimeBests",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeDownturns_RetailEclId",
                table: "RetailPdLifetimeDownturns",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeOptimistics_RetailEclId",
                table: "RetailPdLifetimeOptimistics",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdMappings_RetailEclId",
                table: "RetailPdMappings",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeBests_RetailEclId",
                table: "RetailPdRedefaultLifetimeBests",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeDownturns_RetailEclId",
                table: "RetailPdRedefaultLifetimeDownturns",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeOptimistics_RetailEclId",
                table: "RetailPdRedefaultLifetimeOptimistics",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadCirProjections_WholesaleEclId",
                table: "WholesaleEadCirProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadEirProjections_WholesaleEclId",
                table: "WholesaleEadEirProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadInputs_WholesaleEclId",
                table: "WholesaleEadInputs",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLgdCollateralTypeDatas_WholesaleEclId",
                table: "WholesaleLgdCollateralTypeDatas",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLgdContractDatas_WholesaleEclId",
                table: "WholesaleLgdContractDatas",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeBests_WholesaleEclId",
                table: "WholesalePdLifetimeBests",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdLifetimeDownturns",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdLifetimeOptimistics",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdMappings_WholesaleEclId",
                table: "WholesalePdMappings",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeBests_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeBests",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeDownturns",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeOptimistics",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationResult12MonthPds");

            migrationBuilder.DropTable(
                name: "CalibrationResultLgds");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdCummulativeSurvivals");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdEtiNpls");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdHistoricIndexes");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdMarginalDefaultRates");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdScenarioMacroeconomicProjections");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdSnPCummulativeDefaultRates");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdStatisticalInputs");

            migrationBuilder.DropTable(
                name: "CalibrationResultPdUpperbounds");

            migrationBuilder.DropTable(
                name: "ObeEadCirProjections");

            migrationBuilder.DropTable(
                name: "ObeEadEirProjections");

            migrationBuilder.DropTable(
                name: "ObeEadInputs");

            migrationBuilder.DropTable(
                name: "ObeLgdCollateralTypeDatas");

            migrationBuilder.DropTable(
                name: "ObeLgdContractDatas");

            migrationBuilder.DropTable(
                name: "ObePdLifetimeBests");

            migrationBuilder.DropTable(
                name: "ObePdLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "ObePdLifetimeOptimistics");

            migrationBuilder.DropTable(
                name: "ObePdMappings");

            migrationBuilder.DropTable(
                name: "ObePdRedefaultLifetimeBests");

            migrationBuilder.DropTable(
                name: "ObePdRedefaultLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "ObePdRedefaultLifetimeOptimistics");

            migrationBuilder.DropTable(
                name: "RetailEadCirProjections");

            migrationBuilder.DropTable(
                name: "RetailEadEirProjetions");

            migrationBuilder.DropTable(
                name: "RetailEadInputs");

            migrationBuilder.DropTable(
                name: "RetailLgdCollateralTypeDatas");

            migrationBuilder.DropTable(
                name: "RetailLgdContractDatas");

            migrationBuilder.DropTable(
                name: "RetailPdLifetimeBests");

            migrationBuilder.DropTable(
                name: "RetailPdLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "RetailPdLifetimeOptimistics");

            migrationBuilder.DropTable(
                name: "RetailPdMappings");

            migrationBuilder.DropTable(
                name: "RetailPdRedefaultLifetimeBests");

            migrationBuilder.DropTable(
                name: "RetailPdRedefaultLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "RetailPdRedefaultLifetimeOptimistics");

            migrationBuilder.DropTable(
                name: "WholesaleEadCirProjections");

            migrationBuilder.DropTable(
                name: "WholesaleEadEirProjections");

            migrationBuilder.DropTable(
                name: "WholesaleEadInputs");

            migrationBuilder.DropTable(
                name: "WholesaleLgdCollateralTypeDatas");

            migrationBuilder.DropTable(
                name: "WholesaleLgdContractDatas");

            migrationBuilder.DropTable(
                name: "WholesalePdLifetimeBests");

            migrationBuilder.DropTable(
                name: "WholesalePdLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "WholesalePdLifetimeOptimistics");

            migrationBuilder.DropTable(
                name: "WholesalePdMappings");

            migrationBuilder.DropTable(
                name: "WholesalePdRedefaultLifetimeBests");

            migrationBuilder.DropTable(
                name: "WholesalePdRedefaultLifetimeDownturns");

            migrationBuilder.DropTable(
                name: "WholesalePdRedefaultLifetimeOptimistics");
        }
    }
}
