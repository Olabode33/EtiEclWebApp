using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Fix_override_stage_Result : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Overrides_Stage",
                table: "WholesaleEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Overrides_Stage",
                table: "RetailEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Overrides_Stage",
                table: "ObeEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Overrides_Stage",
                table: "WholesaleEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Overrides_Stage",
                table: "RetailEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Overrides_Stage",
                table: "ObeEclFramworkReportDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
