using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class ModelEnumAdditionToCalibration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunPdCrDrs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunMacroAnalysis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunLgdHairCut",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunEadCcfSummary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModelType",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunMacroAnalysis");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "CalibrationRunEadBehaviouralTerms");
        }
    }
}
