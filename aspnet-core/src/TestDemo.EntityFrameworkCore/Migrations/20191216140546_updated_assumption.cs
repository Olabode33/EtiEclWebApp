using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_assumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "Assumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "Assumptions",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "Assumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "Assumptions");
        }
    }
}
