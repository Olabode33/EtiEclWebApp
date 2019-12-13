using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_SnPAssumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdInputAssumption12Months");

            migrationBuilder.DropTable(
                name: "PdInputSnPCummulativeDefaultRates");

            migrationBuilder.CreateTable(
                name: "PdInputAssumptionSnPCummulativeDefaultRates",
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
                    Rating = table.Column<string>(nullable: true),
                    Years = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Framework = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumptionSnPCummulativeDefaultRates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdInputAssumptionSnPCummulativeDefaultRates");

            migrationBuilder.CreateTable(
                name: "PdInputAssumption12Months",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Framework = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PD = table.Column<double>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    SnPMappingBestFit = table.Column<string>(nullable: true),
                    SnPMappingEtiCreditPolicy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumption12Months", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PdInputSnPCummulativeDefaultRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Framework = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Value = table.Column<double>(nullable: true),
                    Years = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputSnPCummulativeDefaultRates", x => x.Id);
                });
        }
    }
}
