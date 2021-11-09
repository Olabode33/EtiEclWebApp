using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class CloningBusinessUnitFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CalibrationFilesCreated",
                table: "AbpOrganizationUnits",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SourceAffiliateId",
                table: "AbpOrganizationUnits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalibrationFilesCreated",
                table: "AbpOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "SourceAffiliateId",
                table: "AbpOrganizationUnits");
        }
    }
}
