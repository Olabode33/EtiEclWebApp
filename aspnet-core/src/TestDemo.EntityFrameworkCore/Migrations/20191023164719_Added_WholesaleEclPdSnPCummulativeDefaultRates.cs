using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclPdSnPCummulativeDefaultRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclPdSnPCummulativeDefaultRateses",
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
                    Key = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Years = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclPdSnPCummulativeDefaultRateses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclPdSnPCummulativeDefaultRateses");
        }
    }
}
