using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_PDAssumption__Added_OU_to_Assumptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "PdInputAssumptionSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "LgdInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "LgdInputAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LgdInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PdInputAssumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    PdGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Framework = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumptions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "PdInputAssumptionSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "LgdInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "LgdInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LgdInputAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "EadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "EadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EadInputAssumptions");
        }
    }
}
