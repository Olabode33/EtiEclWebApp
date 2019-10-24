using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_WholesaleEclPdAssumption12Months3758 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclPdAssumption12Monthses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclPdAssumption12Monthses");
        }
    }
}
