using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclDataPaymentSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclDataPaymentSchedules",
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
                    ContractRefNo = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Component = table.Column<string>(nullable: true),
                    NoOfSchedules = table.Column<int>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    WholesaleEclUploadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclDataPaymentSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclDataPaymentSchedules_WholesaleEclUploads_WholesaleEclUploadId",
                        column: x => x.WholesaleEclUploadId,
                        principalTable: "WholesaleEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclDataPaymentSchedules_TenantId",
                table: "WholesaleEclDataPaymentSchedules",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclDataPaymentSchedules_WholesaleEclUploadId",
                table: "WholesaleEclDataPaymentSchedules",
                column: "WholesaleEclUploadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclDataPaymentSchedules");
        }
    }
}
