using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedTablesForPdCommsCons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationHistory_Comm_Cons_PD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Current_Rating = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    WI = table.Column<string>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    Serial = table.Column<int>(nullable: false),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_Comm_Cons_PD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationInput_Comm_Cons_PD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Current_Rating = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    WI = table.Column<string>(nullable: true),
                    Serial = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_Comm_Cons_PD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_Comm_Cons_PD",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false),
                    Comm_1 = table.Column<double>(nullable: true),
                    Cons_1 = table.Column<double>(nullable: true),
                    Comm_2 = table.Column<double>(nullable: true),
                    Cons_2 = table.Column<double>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_Comm_Cons_PD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationRunCommConsPD",
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
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ModelType = table.Column<int>(nullable: false),
                    ExceptionComment = table.Column<string>(nullable: true),
                    FriendlyException = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunCommConsPD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunCommConsPD_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackCalibrationPdCommsConsException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Current_Rating = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    WI = table.Column<string>(nullable: true),
                    Serial = table.Column<int>(nullable: false),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCalibrationPdCommsConsException", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationPdCommsConsApprovals",
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
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ReviewedDate = table.Column<DateTime>(nullable: false),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationPdCommsConsApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationPdCommsConsApprovals_CalibrationRunCommConsPD_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunCommConsPD",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationPdCommsConsApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationPdCommsConsApprovals_CalibrationId",
                table: "CalibrationPdCommsConsApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationPdCommsConsApprovals_ReviewedByUserId",
                table: "CalibrationPdCommsConsApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunCommConsPD_CloseByUserId",
                table: "CalibrationRunCommConsPD",
                column: "CloseByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationHistory_Comm_Cons_PD");

            migrationBuilder.DropTable(
                name: "CalibrationInput_Comm_Cons_PD");

            migrationBuilder.DropTable(
                name: "CalibrationPdCommsConsApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationResult_Comm_Cons_PD");

            migrationBuilder.DropTable(
                name: "TrackCalibrationPdCommsConsException");

            migrationBuilder.DropTable(
                name: "CalibrationRunCommConsPD");
        }
    }
}
