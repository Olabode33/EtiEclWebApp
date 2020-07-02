using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Update_overrideType_to_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "WholesaleEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverrideType",
                table: "WholesaleEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "RetailEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverrideType",
                table: "RetailEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ObeEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverrideType",
                table: "ObeEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OverrideType",
                table: "InvestmentEclOverrides",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverrideType",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverrideType",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverrideType",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverrideType",
                table: "InvestmentEclOverrides");

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "WholesaleEclOverrides",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "RetailEclOverrides",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ObeEclOverrides",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
