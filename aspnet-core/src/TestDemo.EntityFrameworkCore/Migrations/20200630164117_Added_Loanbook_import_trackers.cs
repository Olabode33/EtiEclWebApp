using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Loanbook_import_trackers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllJobs",
                table: "WholesaleEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedJobs",
                table: "WholesaleEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllJobs",
                table: "RetailEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedJobs",
                table: "RetailEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AllJobs",
                table: "ObeEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletedJobs",
                table: "ObeEclUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrackEclDataLoanBookException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    CustomerNo = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    ContractNo = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    ProductMapping = table.Column<string>(nullable: true),
                    SpecialisedLending = table.Column<string>(nullable: true),
                    RatingModel = table.Column<string>(nullable: true),
                    OriginalRating = table.Column<int>(nullable: true),
                    CurrentRating = table.Column<int>(nullable: true),
                    LifetimePD = table.Column<double>(nullable: true),
                    Month12PD = table.Column<double>(nullable: true),
                    DaysPastDue = table.Column<double>(nullable: true),
                    WatchlistIndicator = table.Column<bool>(nullable: false),
                    Classification = table.Column<string>(nullable: true),
                    ImpairedDate = table.Column<DateTime>(nullable: true),
                    DefaultDate = table.Column<DateTime>(nullable: true),
                    CreditLimit = table.Column<double>(nullable: true),
                    OriginalBalanceLCY = table.Column<double>(nullable: true),
                    OutstandingBalanceLCY = table.Column<double>(nullable: true),
                    OutstandingBalanceACY = table.Column<double>(nullable: true),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    RestructureIndicator = table.Column<bool>(nullable: false),
                    RestructureRisk = table.Column<string>(nullable: true),
                    RestructureType = table.Column<string>(nullable: true),
                    RestructureStartDate = table.Column<DateTime>(nullable: true),
                    RestructureEndDate = table.Column<DateTime>(nullable: true),
                    PrincipalPaymentTermsOrigination = table.Column<string>(nullable: true),
                    PPTOPeriod = table.Column<int>(nullable: true),
                    InterestPaymentTermsOrigination = table.Column<string>(nullable: true),
                    IPTOPeriod = table.Column<int>(nullable: true),
                    PrincipalPaymentStructure = table.Column<string>(nullable: true),
                    InterestPaymentStructure = table.Column<string>(nullable: true),
                    InterestRateType = table.Column<string>(nullable: true),
                    BaseRate = table.Column<string>(nullable: true),
                    OriginationContractualInterestRate = table.Column<string>(nullable: true),
                    IntroductoryPeriod = table.Column<int>(nullable: true),
                    PostIPContractualInterestRate = table.Column<double>(nullable: true),
                    CurrentContractualInterestRate = table.Column<double>(nullable: true),
                    EIR = table.Column<double>(nullable: true),
                    DebentureOMV = table.Column<double>(nullable: true),
                    DebentureFSV = table.Column<double>(nullable: true),
                    CashOMV = table.Column<double>(nullable: true),
                    CashFSV = table.Column<double>(nullable: true),
                    InventoryOMV = table.Column<double>(nullable: true),
                    InventoryFSV = table.Column<double>(nullable: true),
                    PlantEquipmentOMV = table.Column<double>(nullable: true),
                    PlantEquipmentFSV = table.Column<double>(nullable: true),
                    ResidentialPropertyOMV = table.Column<double>(nullable: true),
                    ResidentialPropertyFSV = table.Column<double>(nullable: true),
                    CommercialPropertyOMV = table.Column<double>(nullable: true),
                    CommercialProperty = table.Column<double>(nullable: true),
                    ReceivablesOMV = table.Column<double>(nullable: true),
                    ReceivablesFSV = table.Column<double>(nullable: true),
                    SharesOMV = table.Column<double>(nullable: true),
                    SharesFSV = table.Column<double>(nullable: true),
                    VehicleOMV = table.Column<double>(nullable: true),
                    VehicleFSV = table.Column<double>(nullable: true),
                    CureRate = table.Column<double>(nullable: true),
                    GuaranteeIndicator = table.Column<bool>(nullable: false),
                    GuarantorPD = table.Column<string>(nullable: true),
                    GuarantorLGD = table.Column<string>(nullable: true),
                    GuaranteeValue = table.Column<double>(nullable: true),
                    GuaranteeLevel = table.Column<double>(nullable: true),
                    ContractId = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackEclDataLoanBookException", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackEclDataLoanBookException");

            migrationBuilder.DropColumn(
                name: "AllJobs",
                table: "WholesaleEclUploads");

            migrationBuilder.DropColumn(
                name: "CompletedJobs",
                table: "WholesaleEclUploads");

            migrationBuilder.DropColumn(
                name: "AllJobs",
                table: "RetailEclUploads");

            migrationBuilder.DropColumn(
                name: "CompletedJobs",
                table: "RetailEclUploads");

            migrationBuilder.DropColumn(
                name: "AllJobs",
                table: "ObeEclUploads");

            migrationBuilder.DropColumn(
                name: "CompletedJobs",
                table: "ObeEclUploads");
        }
    }
}
