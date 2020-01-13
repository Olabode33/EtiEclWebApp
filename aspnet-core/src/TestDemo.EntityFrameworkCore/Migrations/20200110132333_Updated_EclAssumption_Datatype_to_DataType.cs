using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_EclAssumption_Datatype_to_DataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "WholesaleEclEadInputAssumptions",
                newName: "DataType");

            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "WholesaleEclAssumptions",
                newName: "DataType");

            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "RetailEclEadInputAssumptions",
                newName: "DataType");

            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "RetailEclAssumptions",
                newName: "DataType");

            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "ObeEclEadInputAssumptions",
                newName: "DataType");

            migrationBuilder.RenameColumn(
                name: "Datatype",
                table: "ObeEclAssumptions",
                newName: "DataType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "WholesaleEclEadInputAssumptions",
                newName: "Datatype");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "WholesaleEclAssumptions",
                newName: "Datatype");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "RetailEclEadInputAssumptions",
                newName: "Datatype");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "RetailEclAssumptions",
                newName: "Datatype");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "ObeEclEadInputAssumptions",
                newName: "Datatype");

            migrationBuilder.RenameColumn(
                name: "DataType",
                table: "ObeEclAssumptions",
                newName: "Datatype");
        }
    }
}
