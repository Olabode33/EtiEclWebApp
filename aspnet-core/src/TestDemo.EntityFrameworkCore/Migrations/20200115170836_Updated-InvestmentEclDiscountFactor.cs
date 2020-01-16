using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class UpdatedInvestmentEclDiscountFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclDiscountFactor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    RecordId = table.Column<Guid>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Eir = table.Column<double>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclDiscountFactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclDiscountFactor_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclDiscountFactor_EclId",
                table: "InvestmentEclDiscountFactor",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclDiscountFactor");
        }
    }
}
