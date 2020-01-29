using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ECL_id_to_InvestmentEclOverride : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EclId",
                table: "InvestmentEclOverrides",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclOverrides_EclId",
                table: "InvestmentEclOverrides",
                column: "EclId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentEclOverrides_InvestmentEcls_EclId",
                table: "InvestmentEclOverrides",
                column: "EclId",
                principalTable: "InvestmentEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentEclOverrides_InvestmentEcls_EclId",
                table: "InvestmentEclOverrides");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentEclOverrides_EclId",
                table: "InvestmentEclOverrides");

            migrationBuilder.DropColumn(
                name: "EclId",
                table: "InvestmentEclOverrides");
        }
    }
}
