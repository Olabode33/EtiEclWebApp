using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_override_entities_WhRtObe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObeEclOverrides",
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
                    Stage = table.Column<double>(nullable: true),
                    TtrYears = table.Column<double>(nullable: true),
                    FSV_Cash = table.Column<double>(nullable: true),
                    FSV_CommercialProperty = table.Column<double>(nullable: true),
                    FSV_Debenture = table.Column<double>(nullable: true),
                    FSV_Inventory = table.Column<double>(nullable: true),
                    FSV_PlantAndEquipment = table.Column<double>(nullable: true),
                    FSV_Receivables = table.Column<double>(nullable: true),
                    FSV_ResidentialProperty = table.Column<double>(nullable: true),
                    FSV_Shares = table.Column<double>(nullable: true),
                    FSV_Vehicle = table.Column<double>(nullable: true),
                    OverlaysPercentage = table.Column<double>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    ContractId = table.Column<string>(nullable: false),
                    ObeEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclOverrides_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                        column: x => x.ObeEclDataLoanBookId,
                        principalTable: "ObeEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclOverrides",
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
                    Stage = table.Column<int>(nullable: true),
                    TtrYears = table.Column<int>(nullable: true),
                    FSV_Cash = table.Column<double>(nullable: true),
                    FSV_CommercialProperty = table.Column<double>(nullable: true),
                    FSV_Debenture = table.Column<double>(nullable: true),
                    FSV_Inventory = table.Column<double>(nullable: true),
                    FSV_PlantAndEquipment = table.Column<double>(nullable: true),
                    FSV_Receivables = table.Column<double>(nullable: true),
                    FSV_ResidentialProperty = table.Column<double>(nullable: true),
                    FSV_Shares = table.Column<double>(nullable: true),
                    FSV_Vehicle = table.Column<double>(nullable: true),
                    OverlaysPercentage = table.Column<double>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    ContractId = table.Column<string>(nullable: false),
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclOverrides_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclOverrides",
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
                    Stage = table.Column<int>(nullable: true),
                    TtrYears = table.Column<int>(nullable: true),
                    FSV_Cash = table.Column<double>(nullable: true),
                    FSV_CommercialProperty = table.Column<double>(nullable: true),
                    FSV_Debenture = table.Column<double>(nullable: true),
                    FSV_Inventory = table.Column<double>(nullable: true),
                    FSV_PlantAndEquipment = table.Column<double>(nullable: true),
                    FSV_Receivables = table.Column<double>(nullable: true),
                    FSV_ResidentialProperty = table.Column<double>(nullable: true),
                    FSV_Shares = table.Column<double>(nullable: true),
                    FSV_Vehicle = table.Column<double>(nullable: true),
                    OverlaysPercentage = table.Column<double>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    ContractId = table.Column<string>(nullable: false),
                    WholesaleEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclOverrides_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                        column: x => x.WholesaleEclDataLoanBookId,
                        principalTable: "WholesaleEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclOverrides_ObeEclDataLoanBookId",
                table: "ObeEclOverrides",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclOverrides_RetailEclDataLoanBookId",
                table: "RetailEclOverrides",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclOverrides_WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides",
                column: "WholesaleEclDataLoanBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObeEclOverrides");

            migrationBuilder.DropTable(
                name: "RetailEclOverrides");

            migrationBuilder.DropTable(
                name: "WholesaleEclOverrides");
        }
    }
}
