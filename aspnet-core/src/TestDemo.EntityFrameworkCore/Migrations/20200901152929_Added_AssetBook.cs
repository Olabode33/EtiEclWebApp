using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_AssetBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetBooks",
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
                    Entity = table.Column<string>(nullable: true),
                    AssetDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    RatingAgency = table.Column<string>(nullable: true),
                    PurchaseDateCreditRating = table.Column<string>(nullable: true),
                    CurrentCreditRating = table.Column<string>(nullable: true),
                    NominalAmountACY = table.Column<double>(nullable: false),
                    NominalAmountLCY = table.Column<double>(nullable: false),
                    PrincipalAmortisation = table.Column<string>(nullable: true),
                    PrincipalRepaymentTerms = table.Column<string>(nullable: true),
                    InterestRepaymentTerms = table.Column<string>(nullable: true),
                    OutstandingBalanceACY = table.Column<double>(nullable: false),
                    OutstandingBalanceLCY = table.Column<double>(nullable: false),
                    Coupon = table.Column<double>(nullable: false),
                    EIR = table.Column<double>(nullable: false),
                    LoanOriginationDate = table.Column<DateTime>(nullable: false),
                    LoanMaturityDate = table.Column<DateTime>(nullable: false),
                    DaysPastDue = table.Column<int>(nullable: false),
                    PrudentialClassification = table.Column<string>(nullable: true),
                    ForebearanceFlag = table.Column<string>(nullable: true),
                    RegistrationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetBooks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetBooks");
        }
    }
}
