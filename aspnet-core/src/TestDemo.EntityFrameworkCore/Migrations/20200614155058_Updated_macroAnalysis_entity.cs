using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_macroAnalysis_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_CalibrationId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.DropIndex(
                name: "IX_MacroAnalysisApprovals_CalibrationId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.DropColumn(
                name: "NPL_Percentage_Ratio",
                table: "MacroenonomicData");

            migrationBuilder.DropColumn(
                name: "CalibrationId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MacroAnalysisApprovals",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MacroId",
                table: "MacroAnalysisApprovals",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CalibrationRunMacroAnalysis",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_MacroId",
                table: "MacroAnalysisApprovals",
                column: "MacroId");

            migrationBuilder.AddForeignKey(
                name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_MacroId",
                table: "MacroAnalysisApprovals",
                column: "MacroId",
                principalTable: "CalibrationRunMacroAnalysis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_MacroId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.DropIndex(
                name: "IX_MacroAnalysisApprovals_MacroId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.DropColumn(
                name: "MacroId",
                table: "MacroAnalysisApprovals");

            migrationBuilder.AddColumn<double>(
                name: "NPL_Percentage_Ratio",
                table: "MacroenonomicData",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "MacroAnalysisApprovals",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationId",
                table: "MacroAnalysisApprovals",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "CalibrationRunMacroAnalysis",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_CalibrationId",
                table: "MacroAnalysisApprovals",
                column: "CalibrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_CalibrationId",
                table: "MacroAnalysisApprovals",
                column: "CalibrationId",
                principalTable: "CalibrationRunMacroAnalysis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
