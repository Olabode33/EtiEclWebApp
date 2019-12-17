using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_portfolio_assumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "RetailEclLgdAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "RetailEclEadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "RetailEclAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "ObeEclLgdAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "ObeEclEadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "ObeEclAssumptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "RetailEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "RetailEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "RetailEclAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "ObeEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "ObeEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "ObeEclAssumptions");
        }
    }
}
