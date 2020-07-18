using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedIsSingleBatchtoEclRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSingleBatch",
                table: "RetailEcls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingleBatch",
                table: "ObeEcls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSingleBatch",
                table: "RetailEcls");

            migrationBuilder.DropColumn(
                name: "IsSingleBatch",
                table: "ObeEcls");
        }
    }
}
