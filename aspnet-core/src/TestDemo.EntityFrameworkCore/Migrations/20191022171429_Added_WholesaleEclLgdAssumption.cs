using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclLgdAssumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclLgdAssumptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    LgdGroup = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclLgdAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclLgdAssumptions_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclLgdAssumptions_TenantId",
                table: "WholesaleEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclLgdAssumptions_WholesaleEclId",
                table: "WholesaleEclLgdAssumptions",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclLgdAssumptions");
        }
    }
}
