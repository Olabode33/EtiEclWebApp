using System;
using System.Collections.Generic;
using System.Text;

namespace EclEngine.Utils
{
    public static class TempEclData
    {
        public static DateTime ReportDate = new DateTime(2016, 12, 31);
        public static DateTime TempStartDateForVasicekScenarios = new DateTime(2013, 12, 31);
        public static double Rho = 0.217333590280369;
        public const string ScenarioBest = "Best";
        public const string ScenarioOptimistic = "Optimistic";
        public const string ScenarioDownturn = "Downturn";
        public const double IndexWeight1 = 0.575691023137874;
        public const double IndexWeight2 = 0.424308976862126;
        public const int MaxMarginalLifetimeRedefaultPdMonth = 120;
        public const string CreditQualityCriteriaNone = "None";
        public const string CreditQualityCriteria12MonthPd = "12-month PD";
        public const string CreditQualityCriteriaLifetimePd = "Lifetime PD";
        public const int MaxIrFactorProjectionMonths = 242;
        public const int TempExcelVariable_LIM_MONTH = 204;
        public const int TempExcelVariable_MPD_DEFAULT_CRITERIA = 3;
        public const int TempExcelVariable_LIM_CM = 60;
        public const double CollateralHaircutApplied_Cash  = 0.0;
        public const double CollateralHaircutApplied_CommercialProperty  = 0.32;
        public const double CollateralHaircutApplied_Debenture  = 0.26;
        public const double CollateralHaircutApplied_Invertory  = 0.50;
        public const double CollateralHaircutApplied_PlantEquipment  = 0.32;
        public const double CollateralHaircutApplied_Receivables  = 0.28;
        public const double CollateralHaircutApplied_ResidentialProperty  = 0.24;
        public const double CollateralHaircutApplied_Shares  = 0.0;
        public const double CollateralHaircutApplied_Vehicle  = 0.47;
        public const double MaxPdGroupCount = 15;
        public static string[] TempPdGroupArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "COMM_STAGE_1", "COMM_STAGE_2", "CONS_STAGE_1", "CONS_STAGE_2", "EXP" };
        public static int ProjectionNoMonth = 207;
    }
}
