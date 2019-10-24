using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_WholesaleEclLgdAssumption7334 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclAssumptionApprovals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclAssumptionApprovals");
        }
    }
}
