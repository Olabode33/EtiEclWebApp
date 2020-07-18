using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class UpdatedBatchEclRegister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FriendlyException",
                table: "BatchEcls",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingleBatch",
                table: "BatchEcls",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendlyException",
                table: "BatchEcls");

            migrationBuilder.DropColumn(
                name: "IsSingleBatch",
                table: "BatchEcls");
        }
    }
}
