using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Ecl_Final_Result_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StageOverride",
                table: "WholesaleEclOverrides",
                newName: "Stage");

            migrationBuilder.RenameColumn(
                name: "OverrideComment",
                table: "WholesaleEclOverrides",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "WholesaleEclOverrides",
                newName: "TtrYears");

            migrationBuilder.RenameColumn(
                name: "StageOverride",
                table: "RetailEclOverrides",
                newName: "Stage");

            migrationBuilder.RenameColumn(
                name: "OverrideComment",
                table: "RetailEclOverrides",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "RetailEclOverrides",
                newName: "TtrYears");

            migrationBuilder.RenameColumn(
                name: "StageOverride",
                table: "ObeEclOverrides",
                newName: "Stage");

            migrationBuilder.RenameColumn(
                name: "OverrideComment",
                table: "ObeEclOverrides",
                newName: "Reason");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "ObeEclOverrides",
                newName: "TtrYears");

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OverlaysPercentage",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OverlaysPercentage",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RetailEclDataLoanBookId",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ObeEclDataLoanBookId",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OverlaysPercentage",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ObeEclFramworkReportDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ContractNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    ECL_Best_Estimate = table.Column<double>(nullable: true),
                    ECL_Optimistic = table.Column<double>(nullable: true),
                    ECL_Downturn = table.Column<double>(nullable: true),
                    Impairment_ModelOutput = table.Column<double>(nullable: true),
                    Overrides_Stage = table.Column<double>(nullable: true),
                    Overrides_TTR_Years = table.Column<double>(nullable: true),
                    Overrides_FSV = table.Column<double>(nullable: true),
                    Overrides_Overlay = table.Column<double>(nullable: true),
                    Overrides_ECL_Best_Estimate = table.Column<double>(nullable: true),
                    Overrides_ECL_Optimistic = table.Column<double>(nullable: true),
                    Overrides_ECL_Downturn = table.Column<double>(nullable: true),
                    Overrides_Impairment_Manual = table.Column<double>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclFramworkReportDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclFramworkReportDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ContractNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    ECL_Best_Estimate = table.Column<double>(nullable: true),
                    ECL_Optimistic = table.Column<double>(nullable: true),
                    ECL_Downturn = table.Column<double>(nullable: true),
                    Impairment_ModelOutput = table.Column<double>(nullable: true),
                    Overrides_Stage = table.Column<double>(nullable: true),
                    Overrides_TTR_Years = table.Column<double>(nullable: true),
                    Overrides_FSV = table.Column<double>(nullable: true),
                    Overrides_Overlay = table.Column<double>(nullable: true),
                    Overrides_ECL_Best_Estimate = table.Column<double>(nullable: true),
                    Overrides_ECL_Optimistic = table.Column<double>(nullable: true),
                    Overrides_ECL_Downturn = table.Column<double>(nullable: true),
                    Overrides_Impairment_Manual = table.Column<double>(nullable: true),
                    RetailEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclFramworkReportDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclFramworkReportDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    ContractNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    ECL_Best_Estimate = table.Column<double>(nullable: true),
                    ECL_Optimistic = table.Column<double>(nullable: true),
                    ECL_Downturn = table.Column<double>(nullable: true),
                    Impairment_ModelOutput = table.Column<double>(nullable: true),
                    Overrides_Stage = table.Column<double>(nullable: true),
                    Overrides_TTR_Years = table.Column<double>(nullable: true),
                    Overrides_FSV = table.Column<double>(nullable: true),
                    Overrides_Overlay = table.Column<double>(nullable: true),
                    Overrides_ECL_Best_Estimate = table.Column<double>(nullable: true),
                    Overrides_ECL_Optimistic = table.Column<double>(nullable: true),
                    Overrides_ECL_Downturn = table.Column<double>(nullable: true),
                    Overrides_Impairment_Manual = table.Column<double>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclFramworkReportDetail", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObeEclFramworkReportDetail");

            migrationBuilder.DropTable(
                name: "RetailEclFramworkReportDetail");

            migrationBuilder.DropTable(
                name: "WholesaleEclFramworkReportDetail");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverlaysPercentage",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverlaysPercentage",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "RetailEclDataLoanBookId",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "ObeEclDataLoanBookId",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverlaysPercentage",
                table: "ObeEclOverrides");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "WholesaleEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "WholesaleEclOverrides",
                newName: "StageOverride");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "WholesaleEclOverrides",
                newName: "OverrideComment");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "RetailEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "RetailEclOverrides",
                newName: "StageOverride");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "RetailEclOverrides",
                newName: "OverrideComment");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "ObeEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "Stage",
                table: "ObeEclOverrides",
                newName: "StageOverride");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "ObeEclOverrides",
                newName: "OverrideComment");
        }
    }
}
