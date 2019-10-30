using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_wholesale_to_fullauditedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "WholesaleEcls",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WholesaleEcls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WholesaleEcls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WholesaleEcls");
        }
    }
}
