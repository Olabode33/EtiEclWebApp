using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Temp_fix_for_macro_identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MacroAnalysisApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationRunMacroAnalysis");

            migrationBuilder.DropColumn(
                name: "NPL_Percentage_Ratio",
                table: "MacroenonomicData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "NPL_Percentage_Ratio",
                table: "MacroenonomicData",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalibrationRunMacroAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExceptionComment = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunMacroAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunMacroAnalysis_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MacroAnalysisApprovals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ReviewComment = table.Column<string>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    ReviewedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroAnalysisApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunMacroAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MacroAnalysisApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunMacroAnalysis_CloseByUserId",
                table: "CalibrationRunMacroAnalysis",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_CalibrationId",
                table: "MacroAnalysisApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_ReviewedByUserId",
                table: "MacroAnalysisApprovals",
                column: "ReviewedByUserId");
        }
    }
}
