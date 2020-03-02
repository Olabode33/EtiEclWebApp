using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_PostOverride_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclFinalPostOverrideResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RecordId = table.Column<Guid>(nullable: false),
                    AssetDescription = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: false),
                    Exposure = table.Column<double>(nullable: true),
                    BestValue = table.Column<double>(nullable: true),
                    OptimisticValue = table.Column<double>(nullable: true),
                    DownturnValue = table.Column<double>(nullable: true),
                    Impairment = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclFinalPostOverrideResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclFinalPostOverrideResults_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclMonthlyPostOverrideResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StageOverride = table.Column<int>(nullable: true),
                    RecordId = table.Column<Guid>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    BestValue = table.Column<double>(nullable: true),
                    OptimisticValue = table.Column<double>(nullable: true),
                    DownturnValue = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclMonthlyPostOverrideResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclMonthlyPostOverrideResults_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclFinalPostOverrideResults_EclId",
                table: "InvestmentEclFinalPostOverrideResults",
                column: "EclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclMonthlyPostOverrideResults_EclId",
                table: "InvestmentEclMonthlyPostOverrideResults",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclFinalPostOverrideResults");

            migrationBuilder.DropTable(
                name: "InvestmentEclMonthlyPostOverrideResults");
        }
    }
}
