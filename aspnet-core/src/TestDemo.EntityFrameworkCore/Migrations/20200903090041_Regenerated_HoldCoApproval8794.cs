using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_HoldCoApproval8794 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "HoldCoApprovals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "HoldCoApprovals",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "HoldCoApprovals",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "HoldCoApprovals",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HoldCoApprovals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "HoldCoApprovals",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "HoldCoApprovals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "HoldCoApprovals");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "HoldCoApprovals");
        }
    }
}
