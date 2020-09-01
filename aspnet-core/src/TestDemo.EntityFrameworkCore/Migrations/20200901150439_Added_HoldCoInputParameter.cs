using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_HoldCoInputParameter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoldCoInputParameters",
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
                    RegistrationId = table.Column<Guid>(nullable: false),
                    ValuationDate = table.Column<DateTime>(nullable: false),
                    Optimistic = table.Column<double>(nullable: false),
                    BestEstimate = table.Column<double>(nullable: false),
                    Downturn = table.Column<double>(nullable: false),
                    AssumedRating = table.Column<string>(nullable: true),
                    DefaultLoanRating = table.Column<string>(nullable: true),
                    RecoveryRate = table.Column<double>(nullable: false),
                    AssumedStartDate = table.Column<DateTime>(nullable: false),
                    AssumedMaturityDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldCoInputParameters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoldCoInputParameters");
        }
    }
}
