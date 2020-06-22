using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_missing_computation_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ObeEclEadLifetimeProjections",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Contract_no = table.Column<string>(nullable: true),
            //        Eir_Group = table.Column<string>(nullable: true),
            //        Cir_Group = table.Column<string>(nullable: true),
            //        Month = table.Column<int>(nullable: false),
            //        Value = table.Column<double>(nullable: false),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclEadLifetimeProjections", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclEadLifetimeProjections_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ObeEclFrameworkFinalOverrides",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclFrameworkFinalOverrides", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclFrameworkFinalOverrides_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ObeEclFrameworkFinals",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclFrameworkFinals", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclFrameworkFinals_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ObeEclLgdCollateral",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        contract_no = table.Column<string>(nullable: true),
            //        customer_no = table.Column<string>(nullable: true),
            //        debenture_omv = table.Column<double>(nullable: false),
            //        cash_omv = table.Column<double>(nullable: false),
            //        inventory_omv = table.Column<double>(nullable: false),
            //        plant_and_equipment_omv = table.Column<double>(nullable: false),
            //        residential_property_omv = table.Column<double>(nullable: false),
            //        commercial_property_omv = table.Column<double>(nullable: false),
            //        receivables_omv = table.Column<double>(nullable: false),
            //        shares_omv = table.Column<double>(nullable: false),
            //        vehicle_omv = table.Column<double>(nullable: false),
            //        total_omv = table.Column<double>(nullable: false),
            //        debenture_fsv = table.Column<double>(nullable: false),
            //        cash_fsv = table.Column<double>(nullable: false),
            //        inventory_fsv = table.Column<double>(nullable: false),
            //        plant_and_equipment_fsv = table.Column<double>(nullable: false),
            //        residential_property_fsv = table.Column<double>(nullable: false),
            //        commercial_property_fsv = table.Column<double>(nullable: false),
            //        receivables_fsv = table.Column<double>(nullable: false),
            //        shares_fsv = table.Column<double>(nullable: false),
            //        vehicle_fsv = table.Column<double>(nullable: false),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclLgdCollateral", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclLgdCollateral_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ObeEclLgdCollateralProjection",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Month = table.Column<int>(nullable: false),
            //        CollateralProjectionType = table.Column<int>(nullable: false),
            //        Debenture = table.Column<double>(nullable: false),
            //        Cash = table.Column<double>(nullable: false),
            //        Inventory = table.Column<double>(nullable: false),
            //        Plant_And_Equipment = table.Column<double>(nullable: false),
            //        Residential_Property = table.Column<double>(nullable: false),
            //        Commercial_Property = table.Column<double>(nullable: false),
            //        Receivables = table.Column<double>(nullable: false),
            //        Shares = table.Column<double>(nullable: false),
            //        Vehicle = table.Column<double>(nullable: false),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclLgdCollateralProjection", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclLgdCollateralProjection_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ObeEclPdCreditIndex",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        ProjectionMonth = table.Column<int>(nullable: true),
            //        BestEstimate = table.Column<double>(nullable: true),
            //        Optimistic = table.Column<double>(nullable: true),
            //        Downturn = table.Column<double>(nullable: true),
            //        ObeEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ObeEclPdCreditIndex", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ObeEclPdCreditIndex_ObeEcls_ObeEclId",
            //            column: x => x.ObeEclId,
            //            principalTable: "ObeEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailEadLifetimeProjections",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Contract_no = table.Column<string>(nullable: true),
            //        Eir_Group = table.Column<string>(nullable: true),
            //        Cir_Group = table.Column<string>(nullable: true),
            //        Month = table.Column<int>(nullable: false),
            //        Value = table.Column<double>(nullable: false),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailEadLifetimeProjections", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailEadLifetimeProjections_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailECLFrameworkFinal",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailECLFrameworkFinal", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailECLFrameworkFinal_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailECLFrameworkFinalOverride",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailECLFrameworkFinalOverride", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailECLFrameworkFinalOverride_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailLGDCollateral",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        contract_no = table.Column<string>(nullable: true),
            //        customer_no = table.Column<string>(nullable: true),
            //        debenture_omv = table.Column<double>(nullable: false),
            //        cash_omv = table.Column<double>(nullable: false),
            //        inventory_omv = table.Column<double>(nullable: false),
            //        plant_and_equipment_omv = table.Column<double>(nullable: false),
            //        residential_property_omv = table.Column<double>(nullable: false),
            //        commercial_property_omv = table.Column<double>(nullable: false),
            //        receivables_omv = table.Column<double>(nullable: false),
            //        shares_omv = table.Column<double>(nullable: false),
            //        vehicle_omv = table.Column<double>(nullable: false),
            //        total_omv = table.Column<double>(nullable: false),
            //        debenture_fsv = table.Column<double>(nullable: false),
            //        cash_fsv = table.Column<double>(nullable: false),
            //        inventory_fsv = table.Column<double>(nullable: false),
            //        plant_and_equipment_fsv = table.Column<double>(nullable: false),
            //        residential_property_fsv = table.Column<double>(nullable: false),
            //        commercial_property_fsv = table.Column<double>(nullable: false),
            //        receivables_fsv = table.Column<double>(nullable: false),
            //        shares_fsv = table.Column<double>(nullable: false),
            //        vehicle_fsv = table.Column<double>(nullable: false),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailLGDCollateral", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailLGDCollateral_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailLgdCollateralProjection",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Month = table.Column<int>(nullable: false),
            //        CollateralProjectionType = table.Column<int>(nullable: false),
            //        Debenture = table.Column<double>(nullable: false),
            //        Cash = table.Column<double>(nullable: false),
            //        Inventory = table.Column<double>(nullable: false),
            //        Plant_And_Equipment = table.Column<double>(nullable: false),
            //        Residential_Property = table.Column<double>(nullable: false),
            //        Commercial_Property = table.Column<double>(nullable: false),
            //        Receivables = table.Column<double>(nullable: false),
            //        Shares = table.Column<double>(nullable: false),
            //        Vehicle = table.Column<double>(nullable: false),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailLgdCollateralProjection", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailLgdCollateralProjection_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RetailPDCreditIndex",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        ProjectionMonth = table.Column<int>(nullable: true),
            //        BestEstimate = table.Column<double>(nullable: true),
            //        Optimistic = table.Column<double>(nullable: true),
            //        Downturn = table.Column<double>(nullable: true),
            //        RetailEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RetailPDCreditIndex", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RetailPDCreditIndex_RetailEcls_RetailEclId",
            //            column: x => x.RetailEclId,
            //            principalTable: "RetailEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesaleEadLifetimeProjections",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Contract_no = table.Column<string>(nullable: true),
            //        Eir_Group = table.Column<string>(nullable: true),
            //        Cir_Group = table.Column<string>(nullable: true),
            //        Month = table.Column<int>(nullable: false),
            //        Value = table.Column<double>(nullable: false),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesaleEadLifetimeProjections", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesaleEadLifetimeProjections_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesaleECLFrameworkFinal",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesaleECLFrameworkFinal", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesaleECLFrameworkFinal_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesaleECLFrameworkFinalOverride",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        EclMonth = table.Column<int>(nullable: false),
            //        MonthlyEclValue = table.Column<double>(nullable: false),
            //        Stage = table.Column<int>(nullable: false),
            //        FinalEclValue = table.Column<double>(nullable: false),
            //        Scenario = table.Column<int>(nullable: false),
            //        ContractId = table.Column<string>(nullable: true),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesaleECLFrameworkFinalOverride", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesaleECLFrameworkFinalOverride_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesaleLGDCollateral",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        contract_no = table.Column<string>(nullable: true),
            //        customer_no = table.Column<string>(nullable: true),
            //        debenture_omv = table.Column<double>(nullable: false),
            //        cash_omv = table.Column<double>(nullable: false),
            //        inventory_omv = table.Column<double>(nullable: false),
            //        plant_and_equipment_omv = table.Column<double>(nullable: false),
            //        residential_property_omv = table.Column<double>(nullable: false),
            //        commercial_property_omv = table.Column<double>(nullable: false),
            //        receivables_omv = table.Column<double>(nullable: false),
            //        shares_omv = table.Column<double>(nullable: false),
            //        vehicle_omv = table.Column<double>(nullable: false),
            //        total_omv = table.Column<double>(nullable: false),
            //        debenture_fsv = table.Column<double>(nullable: false),
            //        cash_fsv = table.Column<double>(nullable: false),
            //        inventory_fsv = table.Column<double>(nullable: false),
            //        plant_and_equipment_fsv = table.Column<double>(nullable: false),
            //        residential_property_fsv = table.Column<double>(nullable: false),
            //        commercial_property_fsv = table.Column<double>(nullable: false),
            //        receivables_fsv = table.Column<double>(nullable: false),
            //        shares_fsv = table.Column<double>(nullable: false),
            //        vehicle_fsv = table.Column<double>(nullable: false),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesaleLGDCollateral", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesaleLGDCollateral_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesaleLgdCollateralProjection",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        Month = table.Column<int>(nullable: false),
            //        CollateralProjectionType = table.Column<int>(nullable: false),
            //        Debenture = table.Column<double>(nullable: false),
            //        Cash = table.Column<double>(nullable: false),
            //        Inventory = table.Column<double>(nullable: false),
            //        Plant_And_Equipment = table.Column<double>(nullable: false),
            //        Residential_Property = table.Column<double>(nullable: false),
            //        Commercial_Property = table.Column<double>(nullable: false),
            //        Receivables = table.Column<double>(nullable: false),
            //        Shares = table.Column<double>(nullable: false),
            //        Vehicle = table.Column<double>(nullable: false),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesaleLgdCollateralProjection", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesaleLgdCollateralProjection_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WholesalePDCreditIndex",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
            //        ProjectionMonth = table.Column<int>(nullable: true),
            //        BestEstimate = table.Column<double>(nullable: true),
            //        Optimistic = table.Column<double>(nullable: true),
            //        Downturn = table.Column<double>(nullable: true),
            //        WholesaleEclId = table.Column<Guid>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WholesalePDCreditIndex", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_WholesalePDCreditIndex_WholesaleEcls_WholesaleEclId",
            //            column: x => x.WholesaleEclId,
            //            principalTable: "WholesaleEcls",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclEadLifetimeProjections_ObeEclId",
            //    table: "ObeEclEadLifetimeProjections",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclFrameworkFinalOverrides_ObeEclId",
            //    table: "ObeEclFrameworkFinalOverrides",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclFrameworkFinals_ObeEclId",
            //    table: "ObeEclFrameworkFinals",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclLgdCollateral_ObeEclId",
            //    table: "ObeEclLgdCollateral",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclLgdCollateralProjection_ObeEclId",
            //    table: "ObeEclLgdCollateralProjection",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclPdCreditIndex_ObeEclId",
            //    table: "ObeEclPdCreditIndex",
            //    column: "ObeEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailEadLifetimeProjections_RetailEclId",
            //    table: "RetailEadLifetimeProjections",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailECLFrameworkFinal_RetailEclId",
            //    table: "RetailECLFrameworkFinal",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailECLFrameworkFinalOverride_RetailEclId",
            //    table: "RetailECLFrameworkFinalOverride",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailLGDCollateral_RetailEclId",
            //    table: "RetailLGDCollateral",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailLgdCollateralProjection_RetailEclId",
            //    table: "RetailLgdCollateralProjection",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailPDCreditIndex_RetailEclId",
            //    table: "RetailPDCreditIndex",
            //    column: "RetailEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleEadLifetimeProjections_WholesaleEclId",
            //    table: "WholesaleEadLifetimeProjections",
            //    column: "WholesaleEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleECLFrameworkFinal_WholesaleEclId",
            //    table: "WholesaleECLFrameworkFinal",
            //    column: "WholesaleEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleECLFrameworkFinalOverride_WholesaleEclId",
            //    table: "WholesaleECLFrameworkFinalOverride",
            //    column: "WholesaleEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleLGDCollateral_WholesaleEclId",
            //    table: "WholesaleLGDCollateral",
            //    column: "WholesaleEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleLgdCollateralProjection_WholesaleEclId",
            //    table: "WholesaleLgdCollateralProjection",
            //    column: "WholesaleEclId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesalePDCreditIndex_WholesaleEclId",
            //    table: "WholesalePDCreditIndex",
            //    column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObeEclEadLifetimeProjections");

            migrationBuilder.DropTable(
                name: "ObeEclFrameworkFinalOverrides");

            migrationBuilder.DropTable(
                name: "ObeEclFrameworkFinals");

            migrationBuilder.DropTable(
                name: "ObeEclLgdCollateral");

            migrationBuilder.DropTable(
                name: "ObeEclLgdCollateralProjection");

            migrationBuilder.DropTable(
                name: "ObeEclPdCreditIndex");

            migrationBuilder.DropTable(
                name: "RetailEadLifetimeProjections");

            migrationBuilder.DropTable(
                name: "RetailECLFrameworkFinal");

            migrationBuilder.DropTable(
                name: "RetailECLFrameworkFinalOverride");

            migrationBuilder.DropTable(
                name: "RetailLGDCollateral");

            migrationBuilder.DropTable(
                name: "RetailLgdCollateralProjection");

            migrationBuilder.DropTable(
                name: "RetailPDCreditIndex");

            migrationBuilder.DropTable(
                name: "WholesaleEadLifetimeProjections");

            migrationBuilder.DropTable(
                name: "WholesaleECLFrameworkFinal");

            migrationBuilder.DropTable(
                name: "WholesaleECLFrameworkFinalOverride");

            migrationBuilder.DropTable(
                name: "WholesaleLGDCollateral");

            migrationBuilder.DropTable(
                name: "WholesaleLgdCollateralProjection");

            migrationBuilder.DropTable(
                name: "WholesalePDCreditIndex");
        }
    }
}
