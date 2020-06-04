using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updatedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclDataLoanBooks_ObeEclUploads_ObeEclUploadId",
                table: "ObeEclDataLoanBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclDataPaymentSchedules_ObeEclUploads_ObeEclUploadId",
                table: "ObeEclDataPaymentSchedules");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_RetailEclDataLoanBooks_RetailEclUploads_RetailEclUploadId",
            //    table: "RetailEclDataLoanBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclDataPaymentSchedules_RetailEclUploads_RetailEclUploadId",
                table: "RetailEclDataPaymentSchedules");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_WholesaleEclDataLoanBooks_WholesaleEclUploads_WholesaleEclUploadId",
            //    table: "WholesaleEclDataLoanBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclDataPaymentSchedules_WholesaleEclUploads_WholesaleEclUploadId",
                table: "WholesaleEclDataPaymentSchedules");

            //migrationBuilder.DropIndex(
            //    name: "IX_WholesaleEclDataPaymentSchedules_WholesaleEclUploadId",
            //    table: "WholesaleEclDataPaymentSchedules");

            //migrationBuilder.DropIndex(
            //    name: "IX_WholesaleEclDataLoanBooks_WholesaleEclUploadId",
            //    table: "WholesaleEclDataLoanBooks");

            //migrationBuilder.DropIndex(
            //    name: "IX_RetailEclDataPaymentSchedules_RetailEclUploadId",
            //    table: "RetailEclDataPaymentSchedules");

            //migrationBuilder.DropIndex(
            //    name: "IX_RetailEclDataLoanBooks_RetailEclUploadId",
            //    table: "RetailEclDataLoanBooks");

            //migrationBuilder.DropIndex(
            //    name: "IX_ObeEclDataPaymentSchedules_ObeEclUploadId",
            //    table: "ObeEclDataPaymentSchedules");

            //migrationBuilder.DropIndex(
            //    name: "IX_ObeEclDataLoanBooks_ObeEclUploadId",
            //    table: "ObeEclDataLoanBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleEclDataPaymentSchedules_WholesaleEclUploadId",
            //    table: "WholesaleEclDataPaymentSchedules",
            //    column: "WholesaleEclUploadId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_WholesaleEclDataLoanBooks_WholesaleEclUploadId",
            //    table: "WholesaleEclDataLoanBooks",
            //    column: "WholesaleEclUploadId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailEclDataPaymentSchedules_RetailEclUploadId",
            //    table: "RetailEclDataPaymentSchedules",
            //    column: "RetailEclUploadId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RetailEclDataLoanBooks_RetailEclUploadId",
            //    table: "RetailEclDataLoanBooks",
            //    column: "RetailEclUploadId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclDataPaymentSchedules_ObeEclUploadId",
            //    table: "ObeEclDataPaymentSchedules",
            //    column: "ObeEclUploadId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ObeEclDataLoanBooks_ObeEclUploadId",
            //    table: "ObeEclDataLoanBooks",
            //    column: "ObeEclUploadId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ObeEclDataLoanBooks_ObeEclUploads_ObeEclUploadId",
            //    table: "ObeEclDataLoanBooks",
            //    column: "ObeEclUploadId",
            //    principalTable: "ObeEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ObeEclDataPaymentSchedules_ObeEclUploads_ObeEclUploadId",
            //    table: "ObeEclDataPaymentSchedules",
            //    column: "ObeEclUploadId",
            //    principalTable: "ObeEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RetailEclDataLoanBooks_RetailEclUploads_RetailEclUploadId",
            //    table: "RetailEclDataLoanBooks",
            //    column: "RetailEclUploadId",
            //    principalTable: "RetailEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RetailEclDataPaymentSchedules_RetailEclUploads_RetailEclUploadId",
            //    table: "RetailEclDataPaymentSchedules",
            //    column: "RetailEclUploadId",
            //    principalTable: "RetailEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WholesaleEclDataLoanBooks_WholesaleEclUploads_WholesaleEclUploadId",
            //    table: "WholesaleEclDataLoanBooks",
            //    column: "WholesaleEclUploadId",
            //    principalTable: "WholesaleEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_WholesaleEclDataPaymentSchedules_WholesaleEclUploads_WholesaleEclUploadId",
            //    table: "WholesaleEclDataPaymentSchedules",
            //    column: "WholesaleEclUploadId",
            //    principalTable: "WholesaleEclUploads",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
