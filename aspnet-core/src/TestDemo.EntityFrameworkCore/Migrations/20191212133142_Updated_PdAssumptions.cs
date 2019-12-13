using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_PdAssumptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdInputAssumptionNplIndexes",
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
                    Key = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    NplIndexGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Framework = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumptionNplIndexes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdInputAssumptionNplIndexes");
        }
    }
}
