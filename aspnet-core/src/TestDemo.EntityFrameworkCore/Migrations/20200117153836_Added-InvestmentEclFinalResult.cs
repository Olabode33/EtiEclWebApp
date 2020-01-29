using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclFinalResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclFinalResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    RecordId = table.Column<Guid>(nullable: false),
                    Stage = table.Column<int>(nullable: false),
                    BestValue = table.Column<double>(nullable: true),
                    OptimisticValue = table.Column<double>(nullable: true),
                    DownturnValue = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclFinalResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclFinalResult_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclFinalResult_EclId",
                table: "InvestmentEclFinalResult",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclFinalResult");
        }
    }
}
