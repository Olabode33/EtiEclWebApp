using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_HoldCoApproval7863 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ReviewedByUserId",
                table: "HoldCoApprovals",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReviewedByUserId",
                table: "HoldCoApprovals",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
