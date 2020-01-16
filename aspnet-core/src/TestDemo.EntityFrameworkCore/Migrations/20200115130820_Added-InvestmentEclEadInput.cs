using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclEadInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclEadInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetDescription = table.Column<string>(nullable: true),
                    BusinessModelClassification = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    CounterParty = table.Column<string>(nullable: true),
                    NominalAmount = table.Column<double>(nullable: true),
                    CarryingAmountIFRS = table.Column<double>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: true),
                    MaturityDate = table.Column<DateTime>(nullable: true),
                    EIR = table.Column<double>(nullable: true),
                    PrincpalRepayment = table.Column<string>(nullable: true),
                    RepaymentTerms = table.Column<string>(nullable: true),
                    MonthlyInt = table.Column<double>(nullable: true),
                    TermInForceMonths = table.Column<double>(nullable: true),
                    ContractualTermMonths = table.Column<double>(nullable: true),
                    PaymentStartMonth = table.Column<int>(nullable: true),
                    PrincipalAmortisation = table.Column<double>(nullable: true),
                    Coupon = table.Column<double>(nullable: true),
                    Month0 = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclEadInputs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclEadInputs");
        }
    }
}
