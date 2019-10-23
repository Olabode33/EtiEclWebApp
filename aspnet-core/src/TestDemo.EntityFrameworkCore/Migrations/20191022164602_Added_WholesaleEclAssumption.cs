using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclAssumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclAssumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Datatype = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    AssumptionGroup = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclAssumptions_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclAssumptions_TenantId",
                table: "WholesaleEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclAssumptions_WholesaleEclId",
                table: "WholesaleEclAssumptions",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclAssumptions");
        }
    }
}
