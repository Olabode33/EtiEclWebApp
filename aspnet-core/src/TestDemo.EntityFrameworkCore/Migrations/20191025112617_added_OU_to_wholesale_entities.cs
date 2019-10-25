using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_OU_to_wholesale_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclUploads",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclUploadApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclSicrs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclSicrApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEcls",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaryTopExposures",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaryKeyInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaries",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclDataPaymentSchedules",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclDataLoanBooks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclComputedEadResults",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclAssumptionApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "WholesaleEclApprovals",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclUploads");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclUploadApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclSicrs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaryTopExposures");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaryKeyInputs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultSummaries");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclResultDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclDataPaymentSchedules");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclDataLoanBooks");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclComputedEadResults");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "WholesaleEclApprovals");
        }
    }
}
