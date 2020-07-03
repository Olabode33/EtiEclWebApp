using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_PaymentSchedule_Upload_Exception : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackEclDataPaymentScheduleException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EclId = table.Column<Guid>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ContractRefNo = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    Component = table.Column<string>(nullable: true),
                    NoOfSchedules = table.Column<int>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackEclDataPaymentScheduleException", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackEclDataPaymentScheduleException");
        }
    }
}
