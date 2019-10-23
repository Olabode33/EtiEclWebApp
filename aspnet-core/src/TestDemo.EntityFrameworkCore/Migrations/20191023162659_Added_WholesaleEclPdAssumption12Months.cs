using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclPdAssumption12Months : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclPdAssumption12Monthses",
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
                    TenantId = table.Column<int>(nullable: true),
                    Credit = table.Column<int>(nullable: false),
                    PD = table.Column<double>(nullable: true),
                    SnPMappingEtiCreditPolicy = table.Column<string>(nullable: true),
                    SnPMappingBestFit = table.Column<string>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdAssumption12Monthses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdAssumption12Monthses_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumption12Monthses_TenantId",
                table: "WholesaleEclPdAssumption12Monthses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumption12Monthses_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Monthses",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclPdAssumption12Monthses");
        }
    }
}
