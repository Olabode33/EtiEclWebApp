using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_CalibrationUpload_Tracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TrackCalibrationUploadSummary",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TrackCalibrationUploadSummary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TrackMacroUploadSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegisterId = table.Column<int>(nullable: false),
                    AllJobs = table.Column<int>(nullable: false),
                    CompletedJobs = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackMacroUploadSummary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackMacroUploadSummary");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TrackCalibrationUploadSummary");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TrackCalibrationUploadSummary");
        }
    }
}
