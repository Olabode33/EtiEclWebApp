using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_LoanImpairmentRecovery3769 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Recovery",
                table: "LoanImpairmentRecoveries",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Recovery",
                table: "LoanImpairmentRecoveries",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
