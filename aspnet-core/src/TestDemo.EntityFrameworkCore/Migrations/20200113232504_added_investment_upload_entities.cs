using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_investment_upload_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclUploads",
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
                    DocType = table.Column<int>(nullable: false),
                    UploadComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclUploads_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentAssetBooks",
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
                    AssetDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    CounterParty = table.Column<string>(nullable: true),
                    SovereignDebt = table.Column<string>(nullable: true),
                    RatingAgency = table.Column<string>(nullable: true),
                    CreditRatingAtPurchaseDate = table.Column<string>(nullable: true),
                    CurrentCreditRating = table.Column<string>(nullable: true),
                    NominalAmount = table.Column<double>(nullable: true),
                    PrincipalAmortisation = table.Column<string>(nullable: true),
                    RepaymentTerms = table.Column<string>(nullable: true),
                    CarryAmountNGAAP = table.Column<double>(nullable: true),
                    CarryingAmountIFRS = table.Column<double>(nullable: true),
                    Coupon = table.Column<double>(nullable: true),
                    Eir = table.Column<double>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    PurchasePrice = table.Column<double>(nullable: true),
                    MaturityDate = table.Column<DateTime>(nullable: true),
                    RedemptionPrice = table.Column<double>(nullable: true),
                    BusinessModelClassification = table.Column<string>(nullable: true),
                    Ias39Impairment = table.Column<double>(nullable: true),
                    PrudentialClassification = table.Column<string>(nullable: true),
                    ForebearanceFlag = table.Column<string>(nullable: true),
                    InvestmentEclUploadId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentAssetBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentAssetBooks_InvestmentEclUploads_InvestmentEclUploadId",
                        column: x => x.InvestmentEclUploadId,
                        principalTable: "InvestmentEclUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentAssetBooks_InvestmentEclUploadId",
                table: "InvestmentAssetBooks",
                column: "InvestmentEclUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclUploads_InvestmentEclId",
                table: "InvestmentEclUploads",
                column: "InvestmentEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentAssetBooks");

            migrationBuilder.DropTable(
                name: "InvestmentEclUploads");
        }
    }
}
