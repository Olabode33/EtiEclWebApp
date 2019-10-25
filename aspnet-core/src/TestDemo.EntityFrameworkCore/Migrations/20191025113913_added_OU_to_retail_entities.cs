using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_OU_to_retail_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclUploads",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclUploadApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclSicrs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclSicrApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEcls",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaryTopExposures",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaryKeyInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaries",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclResultDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclLgdAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclDataPaymentSchedules",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclDataLoanBooks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclComputedEadResults",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclAssumptionApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "RetailEclApprovals",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclUploads");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclUploadApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclSicrs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclSicrApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEcls");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaryTopExposures");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaryKeyInputs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclResultSummaries");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclResultDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclDataPaymentSchedules");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclDataLoanBooks");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclComputedEadResults");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "RetailEclApprovals");
        }
    }
}
