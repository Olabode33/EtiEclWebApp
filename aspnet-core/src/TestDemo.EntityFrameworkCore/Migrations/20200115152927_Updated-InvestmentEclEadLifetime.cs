using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class UpdatedInvestmentEclEadLifetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "InvestmentEclEadLifetimes",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "RecordId",
                table: "InvestmentEclEadLifetimes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "InvestmentEclEadLifetimes");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "InvestmentEclEadLifetimes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldDefaultValueSql: "NEWID()");
        }
    }
}
