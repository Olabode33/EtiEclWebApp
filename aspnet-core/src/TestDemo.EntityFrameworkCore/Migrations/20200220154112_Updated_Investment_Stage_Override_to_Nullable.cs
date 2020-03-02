using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_Investment_Stage_Override_to_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StageOverride",
                table: "InvestmentEclOverrides",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StageOverride",
                table: "InvestmentEclOverrides",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
