using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_calibration_inputs__results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationInput_EAD_Behavioural_Terms",
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
                    CalibrationId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Assumption_NonExpired = table.Column<string>(nullable: true),
                    Freq_NonExpired = table.Column<string>(nullable: true),
                    Assumption_Expired = table.Column<string>(nullable: true),
                    Freq_Expired = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_EAD_Behavioural_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationInput_EAD_CCF_Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Limit = table.Column<double>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_EAD_CCF_Summary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationInput_LGD_HairCut",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
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
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_LGD_HairCut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationInput_LGD_RecoveryRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Account_Name = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Default_Date = table.Column<DateTime>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contractual_Interest_Rate = table.Column<double>(nullable: true),
                    Amount_Recovered = table.Column<double>(nullable: true),
                    Date_Of_Recovery = table.Column<DateTime>(nullable: true),
                    Type_Of_Recovery = table.Column<string>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_LGD_RecoveryRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationInput_PD_CR_DR",
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
                    RAPP_Date = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationInput_PD_CR_DR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_EAD_Behavioural_Terms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assumption_NonExpired = table.Column<string>(nullable: true),
                    Freq_NonExpired = table.Column<string>(nullable: true),
                    Assumption_Expired = table.Column<string>(nullable: true),
                    Freq_Expired = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_EAD_Behavioural_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_EAD_CCF_Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OD_TotalLimitOdDefaultedLoan = table.Column<double>(nullable: true),
                    OD_BalanceAtDefault = table.Column<double>(nullable: true),
                    OD_Balance12MonthBeforeDefault = table.Column<double>(nullable: true),
                    OD_TotalConversation = table.Column<double>(nullable: true),
                    OD_CCF = table.Column<double>(nullable: true),
                    Card_TotalLimitOdDefaultedLoan = table.Column<double>(nullable: true),
                    Card_BalanceAtDefault = table.Column<double>(nullable: true),
                    Card_Balance12MonthBeforeDefault = table.Column<double>(nullable: true),
                    Card_TotalConversation = table.Column<double>(nullable: true),
                    Card_CCF = table.Column<double>(nullable: true),
                    Overall_TotalLimitOdDefaultedLoan = table.Column<double>(nullable: true),
                    Overall_BalanceAtDefault = table.Column<double>(nullable: true),
                    Overall_Balance12MonthBeforeDefault = table.Column<double>(nullable: true),
                    Overall_TotalConversation = table.Column<double>(nullable: true),
                    Overall_CCF = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_EAD_CCF_Summary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_LGD_HairCut",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debenture = table.Column<double>(nullable: true),
                    Cash = table.Column<double>(nullable: true),
                    Inventory = table.Column<double>(nullable: true),
                    Plant_And_Equipment = table.Column<double>(nullable: true),
                    Residential_Property = table.Column<double>(nullable: true),
                    Commercial_Property = table.Column<double>(nullable: true),
                    Receivables = table.Column<double>(nullable: true),
                    Shares = table.Column<double>(nullable: true),
                    Vehicle = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_LGD_HairCut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_LGD_RecoveryRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Overall_Exposure_At_Default = table.Column<double>(nullable: true),
                    Overall_PvOfAmountReceived = table.Column<double>(nullable: true),
                    Overall_Count = table.Column<double>(nullable: true),
                    Overall_RecoveryRate = table.Column<double>(nullable: true),
                    Corporate_Exposure_At_Default = table.Column<double>(nullable: true),
                    Corporate_PvOfAmountReceived = table.Column<double>(nullable: true),
                    Corporate_Count = table.Column<double>(nullable: true),
                    Corporate_RecoveryRate = table.Column<double>(nullable: true),
                    Commercial_Exposure_At_Default = table.Column<double>(nullable: true),
                    Commercial_PvOfAmountReceived = table.Column<double>(nullable: true),
                    Commercial_Count = table.Column<double>(nullable: true),
                    Commercial_RecoveryRate = table.Column<double>(nullable: true),
                    Consumer_Exposure_At_Default = table.Column<double>(nullable: true),
                    Consumer_PvOfAmountReceived = table.Column<double>(nullable: true),
                    Consumer_Count = table.Column<double>(nullable: true),
                    Consumer_RecoveryRate = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_LGD_RecoveryRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationResult_PD_12Months_Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Normal_12_Months_PD = table.Column<double>(nullable: true),
                    DefaultedLoansA = table.Column<double>(nullable: true),
                    DefaultedLoansB = table.Column<double>(nullable: true),
                    CuredLoansA = table.Column<double>(nullable: true),
                    CuredLoansB = table.Column<double>(nullable: true),
                    Cure_Rate = table.Column<double>(nullable: true),
                    CuredPopulationA = table.Column<double>(nullable: true),
                    CuredPopulationB = table.Column<double>(nullable: true),
                    RedefaultedLoansA = table.Column<double>(nullable: true),
                    RedefaultedLoansB = table.Column<double>(nullable: true),
                    Redefault_Rate = table.Column<double>(nullable: true),
                    Redefault_Factor = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_PD_12Months_Summary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationInput_EAD_Behavioural_Terms");

            migrationBuilder.DropTable(
                name: "CalibrationInput_EAD_CCF_Summary");

            migrationBuilder.DropTable(
                name: "CalibrationInput_LGD_HairCut");

            migrationBuilder.DropTable(
                name: "CalibrationInput_LGD_RecoveryRate");

            migrationBuilder.DropTable(
                name: "CalibrationInput_PD_CR_DR");

            migrationBuilder.DropTable(
                name: "CalibrationResult_EAD_Behavioural_Terms");

            migrationBuilder.DropTable(
                name: "CalibrationResult_EAD_CCF_Summary");

            migrationBuilder.DropTable(
                name: "CalibrationResult_LGD_HairCut");

            migrationBuilder.DropTable(
                name: "CalibrationResult_LGD_RecoveryRate");

            migrationBuilder.DropTable(
                name: "CalibrationResult_PD_12Months_Summary");
        }
    }
}
