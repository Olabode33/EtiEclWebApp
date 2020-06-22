using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_loanbook_daysPastDue_to_Double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DaysPastDue",
                table: "WholesaleEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DaysPastDue",
                table: "RetailEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DaysPastDue",
                table: "ObeEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DaysPastDue",
                table: "WholesaleEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DaysPastDue",
                table: "RetailEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DaysPastDue",
                table: "ObeEclDataLoanBooks",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
