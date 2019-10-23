using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEcl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEcls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    ReportingDate = table.Column<DateTime>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ClosedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEcls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEcls_AbpUsers_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEcls_ClosedByUserId",
                table: "WholesaleEcls",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEcls_TenantId",
                table: "WholesaleEcls",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEcls");
        }
    }
}
