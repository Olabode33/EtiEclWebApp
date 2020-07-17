using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedFriendlyExceptionToEcl__Calibration_Register : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "RetailEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "ObeEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "InvestmentEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunPdCrDrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunMacroAnalysis",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunLgdHairCut",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunEadCcfSummary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "RetailEcls");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "ObeEcls");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "InvestmentEcls");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunMacroAnalysis");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "CalibrationRunEadBehaviouralTerms");
        }
    }
}
