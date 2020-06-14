using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ExceptionComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "RetailEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "ObeEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "InvestmentEcls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "CalibrationRunPdCrDrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "CalibrationRunLgdHairCut",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "CalibrationRunEadCcfSummary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExceptionComment",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "RetailEcls");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "ObeEcls");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "InvestmentEcls");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "ExceptionComment",
                table: "CalibrationRunEadBehaviouralTerms");
        }
    }
}
