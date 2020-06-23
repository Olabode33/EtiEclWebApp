using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class UpdatedlifetimeScenariovalue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeOptimistics",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeDownturns",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeBests",
                nullable: false,
                defaultValueSql: "0",
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdRedefaultLifetimeBests",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeBests",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdRedefaultLifetimeBests",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailPdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailPdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailPdLifetimeBests",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdRedefaultLifetimeBests",
                nullable: false,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeBests",
                nullable: true,
                oldClrType: typeof(double),
                oldDefaultValueSql: "0");
        }
    }
}
