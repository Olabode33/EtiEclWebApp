using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedIsSingleBatchtoInvestmentEclRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BatchId",
                table: "InvestmentEcls",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingleBatch",
                table: "InvestmentEcls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "InvestmentEcls");

            migrationBuilder.DropColumn(
                name: "IsSingleBatch",
                table: "InvestmentEcls");
        }
    }
}
