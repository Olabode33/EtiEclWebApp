using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_assumptionApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssumptionEntity",
                table: "AssumptionApprovals",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssumptionId",
                table: "AssumptionApprovals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssumptionEntity",
                table: "AssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "AssumptionId",
                table: "AssumptionApprovals");
        }
    }
}
