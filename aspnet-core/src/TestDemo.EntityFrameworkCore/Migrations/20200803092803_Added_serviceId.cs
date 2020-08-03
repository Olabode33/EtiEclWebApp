using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_serviceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "WholesaleEcls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "RetailEcls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ObeEcls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "InvestmentEcls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CalibrationRunPdCrDrs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CalibrationRunLgdHairCut",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CalibrationRunEadCcfSummary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "RetailEcls");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ObeEcls");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "InvestmentEcls");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "CalibrationRunEadBehaviouralTerms");
        }
    }
}
