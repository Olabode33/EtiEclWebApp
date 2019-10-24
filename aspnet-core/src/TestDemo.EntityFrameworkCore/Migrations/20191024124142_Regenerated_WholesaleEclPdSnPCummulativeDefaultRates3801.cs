using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_WholesaleEclPdSnPCummulativeDefaultRates3801 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses");
        }
    }
}
