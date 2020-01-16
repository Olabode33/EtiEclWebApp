using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class UpdatedInvestmentEclEadInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EclId",
                table: "InvestmentEclEadInputs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclEadInputs_EclId",
                table: "InvestmentEclEadInputs",
                column: "EclId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentEclEadInputs_InvestmentEcls_EclId",
                table: "InvestmentEclEadInputs",
                column: "EclId",
                principalTable: "InvestmentEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentEclEadInputs_InvestmentEcls_EclId",
                table: "InvestmentEclEadInputs");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentEclEadInputs_EclId",
                table: "InvestmentEclEadInputs");

            migrationBuilder.DropColumn(
                name: "EclId",
                table: "InvestmentEclEadInputs");
        }
    }
}
