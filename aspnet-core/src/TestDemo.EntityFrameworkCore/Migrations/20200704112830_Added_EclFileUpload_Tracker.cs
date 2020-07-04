using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_EclFileUpload_Tracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FileUploaded",
                table: "WholesaleEclUploads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FileUploaded",
                table: "RetailEclUploads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FileUploaded",
                table: "ObeEclUploads",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUploaded",
                table: "WholesaleEclUploads");

            migrationBuilder.DropColumn(
                name: "FileUploaded",
                table: "RetailEclUploads");

            migrationBuilder.DropColumn(
                name: "FileUploaded",
                table: "ObeEclUploads");
        }
    }
}
