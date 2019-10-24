using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_PdInputSnPCummulativeDefaultRate1227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Framework",
                table: "PdInputSnPCummulativeDefaultRates");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "PdInputSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "PdInputSnPCummulativeDefaultRates");

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "PdInputSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);
        }
    }
}
