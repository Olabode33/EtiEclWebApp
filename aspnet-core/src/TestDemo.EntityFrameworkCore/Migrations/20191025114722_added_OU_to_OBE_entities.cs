using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class added_OU_to_OBE_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObesaleEclResultSummaries",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclUploads",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclUploadApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclSicrs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclSicrApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEcls",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclResultSummaryTopExposures",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclResultSummaryKeyInputs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclResultDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclLgdAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclDataPaymentSchedules",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclDataLoanBooks",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclComputedEadResults",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclAssumptions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclAssumptionApprovals",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitId",
                table: "ObeEclApprovals",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObesaleEclResultSummaries");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclUploads");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclUploadApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclSicrs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclSicrApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEcls");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclResultSummaryTopExposures");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclResultSummaryKeyInputs");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclResultDetails");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclDataPaymentSchedules");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclDataLoanBooks");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclComputedEadResults");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclAssumptions");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitId",
                table: "ObeEclApprovals");
        }
    }
}
