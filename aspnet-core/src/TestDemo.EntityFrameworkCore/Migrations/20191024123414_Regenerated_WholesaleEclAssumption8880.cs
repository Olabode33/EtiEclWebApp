using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_WholesaleEclAssumption8880 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclAssumptions");
        }
    }
}
