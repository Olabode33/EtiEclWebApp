using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclEadLifetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclEadLifetimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclEadLifetimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclEadLifetimes_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclEadLifetimes_EclId",
                table: "InvestmentEclEadLifetimes",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclEadLifetimes");
        }
    }
}
