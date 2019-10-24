using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_PdInputAssumption12Month5999 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_LgdInputAssumption",
            //    table: "LgdInputAssumption");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "PdInputAssumption12Months");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "LgdInputAssumption");

            migrationBuilder.RenameTable(
                name: "LgdInputAssumption",
                newName: "LgdInputAssumptions");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "PdInputAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "LgdInputAssumptions",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_LgdInputAssumptions",
            //    table: "LgdInputAssumptions",
            //    column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_LgdInputAssumptions",
            //    table: "LgdInputAssumptions");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "PdInputAssumption12Months");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "LgdInputAssumptions");

            migrationBuilder.RenameTable(
                name: "LgdInputAssumptions",
                newName: "LgdInputAssumption");

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "PdInputAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "LgdInputAssumption",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_LgdInputAssumption",
            //    table: "LgdInputAssumption",
            //    column: "Id");
        }
    }
}
