using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ProductType_to_RecoveryRateInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Product_Type",
                table: "CalibrationInput_LGD_RecoveryRate",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Snapshot_Date",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_Type",
                table: "CalibrationInput_LGD_RecoveryRate");

            migrationBuilder.AlterColumn<string>(
                name: "Snapshot_Date",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
