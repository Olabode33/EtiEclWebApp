using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_EadInputAssumption8219 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEadInputAssumptions_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadInputAssumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEadInputAssumptions",
                table: "WholesaleEadInputAssumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LgdAssumptionUnsecuredRecoveries",
                table: "LgdAssumptionUnsecuredRecoveries");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "EadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "Assumptions");

            migrationBuilder.RenameTable(
                name: "WholesaleEadInputAssumptions",
                newName: "WholesaleEclEadInputAssumptions");

            migrationBuilder.RenameTable(
                name: "LgdAssumptionUnsecuredRecoveries",
                newName: "LgdInputAssumption");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEadInputAssumptions_WholesaleEclId",
                table: "WholesaleEclEadInputAssumptions",
                newName: "IX_WholesaleEclEadInputAssumptions_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEadInputAssumptions_TenantId",
                table: "WholesaleEclEadInputAssumptions",
                newName: "IX_WholesaleEclEadInputAssumptions_TenantId");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "Assumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEclEadInputAssumptions",
                table: "WholesaleEclEadInputAssumptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LgdAssumption",
                table: "LgdInputAssumption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclEadInputAssumptions_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclEadInputAssumptions",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclEadInputAssumptions_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEclEadInputAssumptions",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LgdAssumption",
                table: "LgdInputAssumption");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "EadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "Assumptions");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.RenameTable(
                name: "WholesaleEclEadInputAssumptions",
                newName: "WholesaleEadInputAssumptions");

            migrationBuilder.RenameTable(
                name: "LgdInputAssumption",
                newName: "LgdAssumptionUnsecuredRecoveries");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclEadInputAssumptions_WholesaleEclId",
                table: "WholesaleEadInputAssumptions",
                newName: "IX_WholesaleEadInputAssumptions_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclEadInputAssumptions_TenantId",
                table: "WholesaleEadInputAssumptions",
                newName: "IX_WholesaleEadInputAssumptions_TenantId");

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "Assumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEadInputAssumptions",
                table: "WholesaleEadInputAssumptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LgdAssumptionUnsecuredRecoveries",
                table: "LgdAssumptionUnsecuredRecoveries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEadInputAssumptions_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadInputAssumptions",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
