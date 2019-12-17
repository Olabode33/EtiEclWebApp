using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class ModifiedPdassumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsComputed",
                table: "PdInputAssumptionMacroeconomicInputs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsComputed",
                table: "PdInputAssumptionMacroeconomicInputs",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
