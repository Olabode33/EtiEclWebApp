using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclSicr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclSicrs",
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
                    ComputedSICR = table.Column<int>(nullable: false),
                    OverrideSICR = table.Column<string>(nullable: true),
                    OverrideComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    WholesaleEclDataLoanBookId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclSicrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclSicrs_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                        column: x => x.WholesaleEclDataLoanBookId,
                        principalTable: "WholesaleEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrs_TenantId",
                table: "WholesaleEclSicrs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrs_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrs",
                column: "WholesaleEclDataLoanBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclSicrs");
        }
    }
}
