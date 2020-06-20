using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_override_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WholesaleEclId",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "RetailEclId",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "ObeEclId",
                table: "ObeEclOverrides");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WholesaleEclId",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RetailEclId",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ObeEclId",
                table: "ObeEclOverrides",
                nullable: true);
        }
    }
}
