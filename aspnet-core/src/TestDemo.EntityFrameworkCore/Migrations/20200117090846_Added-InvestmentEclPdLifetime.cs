using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclPdLifetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclPdLifetime",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Rating = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    BestValue = table.Column<double>(nullable: true),
                    OptimisticValue = table.Column<double>(nullable: true),
                    DownturnValue = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclPdLifetime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclPdLifetime_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclPdLifetime_EclId",
                table: "InvestmentEclPdLifetime",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclPdLifetime");
        }
    }
}
