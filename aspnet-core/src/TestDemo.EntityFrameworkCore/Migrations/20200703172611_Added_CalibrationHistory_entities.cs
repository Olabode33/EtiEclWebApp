using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_CalibrationHistory_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationHistory_EAD_Behavioural_Terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Customer_Name = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Original_Balance_Lcy = table.Column<double>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Outstanding_Balance_Acy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Restructure_Indicator = table.Column<string>(nullable: true),
                    Restructure_Type = table.Column<string>(nullable: true),
                    Restructure_Start_Date = table.Column<DateTime>(nullable: true),
                    Restructure_End_Date = table.Column<DateTime>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_EAD_Behavioural_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationHistory_EAD_CCF_Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<int>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Limit = table.Column<double>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Settlement_Account = table.Column<string>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_EAD_CCF_Summary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationHistory_LGD_HairCut",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Period = table.Column<int>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Debenture_OMV = table.Column<double>(nullable: true),
                    Debenture_FSV = table.Column<double>(nullable: true),
                    Cash_OMV = table.Column<double>(nullable: true),
                    Cash_FSV = table.Column<double>(nullable: true),
                    Inventory_OMV = table.Column<double>(nullable: true),
                    Inventory_FSV = table.Column<double>(nullable: true),
                    Plant_And_Equipment_OMV = table.Column<double>(nullable: true),
                    Plant_And_Equipment_FSV = table.Column<double>(nullable: true),
                    Residential_Property_OMV = table.Column<double>(nullable: true),
                    Residential_Property_FSV = table.Column<double>(nullable: true),
                    Commercial_Property_OMV = table.Column<double>(nullable: true),
                    Commercial_Property_FSV = table.Column<double>(nullable: true),
                    Receivables_OMV = table.Column<double>(nullable: true),
                    Receivables_FSV = table.Column<double>(nullable: true),
                    Shares_OMV = table.Column<double>(nullable: true),
                    Shares_FSV = table.Column<double>(nullable: true),
                    Vehicle_OMV = table.Column<double>(nullable: true),
                    Vehicle_FSV = table.Column<double>(nullable: true),
                    Guarantee_Value = table.Column<double>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_LGD_HairCut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationHistory_LGD_RecoveryRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Account_Name = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Default_Date = table.Column<DateTime>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contractual_Interest_Rate = table.Column<double>(nullable: true),
                    Amount_Recovered = table.Column<double>(nullable: true),
                    Date_Of_Recovery = table.Column<DateTime>(nullable: true),
                    Type_Of_Recovery = table.Column<string>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_LGD_RecoveryRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationHistory_PD_CR_DR",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    RAPP_Date = table.Column<int>(nullable: true),
                    Current_Rating = table.Column<int>(nullable: true),
                    ModelType = table.Column<int>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationHistory_PD_CR_DR", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationHistory_EAD_Behavioural_Terms");

            migrationBuilder.DropTable(
                name: "CalibrationHistory_EAD_CCF_Summary");

            migrationBuilder.DropTable(
                name: "CalibrationHistory_LGD_HairCut");

            migrationBuilder.DropTable(
                name: "CalibrationHistory_LGD_RecoveryRate");

            migrationBuilder.DropTable(
                name: "CalibrationHistory_PD_CR_DR");
        }
    }
}
