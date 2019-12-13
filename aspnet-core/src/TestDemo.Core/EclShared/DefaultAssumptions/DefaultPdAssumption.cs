using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.DefaultAssumptions
{
    public class DefaultPdAssumption
    {
        public static class PdAssumptionKey
        {
            public const string CreditPdKeyPrefix = "CreditPd";
            public const int MaxCreditPd = 10;
            public const string CreditPdEtiPolicyKeyPrefix = "CreditPdEtiPolicy";
            public const string CreditPdBestFitKeyPrefix = "CreditPdBestFit";
            public const string Upperbound12MonthPdPrefix = "Upperbound12MonthPd";
            public const string StatisticsIndexWeightsW1 = "StatisticsIndexWeightsW1";
            public const string StatisticsIndexWeightsW2 = "StatisticsIndexWeightsW2";
            public const string StatisticsIndexWeightsStandardDeviation = "StatisticsIndexWeightsStandardDeviation";
            public const string StatisticsIndexWeightsAverage = "StatisticsIndexWeightsAverage";

            public const int MaxHistoricalIndex = 32;
            public const string HistoricalIndexActualPrefix = "HistoricalIndexActual";
            public const string HistoricalIndexStandardisedPrefix = "HistoricalIndexStandardised";
            public const string EtiNplSeriesPrefix = "EtiNplSeries";

            public const string StatisticalInputsPrimeLendingMean = "StatisticalInputsPrimeLendingMean";
            public const string StatisticalInputsPrimeLendingStandardDeviation = "StatisticalInputsPrimeLendingStandardDeviation";
            public const string StatisticalInputsPrimeLendingEigenvalues = "StatisticalInputsPrimeLendingEigenvalues";
            public const string StatisticalInputsPrimeLendingPrincipalComponentScore1 = "StatisticalInputsPrimeLendingPrincipalComponentScore1";
            public const string StatisticalInputsPrimeLendingPrincipalComponentScore2 = "StatisticalInputsPrimeLendingPrincipalComponentScore2";
            public const string StatisticalInputsOilExportsMean = "StatisticalInputsOilExportsMean";
            public const string StatisticalInputsOilExportsStandardDeviation = "StatisticalInputsOilExportsStandardDeviation";
            public const string StatisticalInputsOilExportsEigenvalues = "StatisticalInputsOilExportsEigenvalues";
            public const string StatisticalInputsOilExportsPrincipalComponentScore1 = "StatisticalInputsOilExportsPrincipalComponentScore1";
            public const string StatisticalInputsOilExportsPrincipalComponentScore2 = "StatisticalInputsOilExportsPrincipalComponentScore2";
            public const string StatisticalInputsRealGdpGrowthRateMean = "StatisticalInputsRealGdpGrowthRateMean";
            public const string StatisticalInputsRealGdpGrowthRateStandardDeviation = "StatisticalInputsRealGdpGrowthRateStandardDeviation";
            public const string StatisticalInputsRealGdpGrowthRateEigenvalues = "StatisticalInputsRealGdpGrowthRateEigenvalues";
            public const string StatisticalInputsRealGdpGrowthRatePrincipalComponentScore1 = "StatisticalInputsRealGdpGrowthRatePrincipalComponentScore1";
            public const string StatisticalInputsRealGdpGrowthRatePrincipalComponentScore2 = "StatisticalInputsRealGdpGrowthRatePrincipalComponentScore2";

            public const int MaxProjectionMonths = 28;
            public const string BestProjectionInputsPrimeLendingPrefix = "BestProjectionInputsPrimeLending";
            public const string BestProjectionOilExportsPrefix = "BestProjectionOilExports";
            public const string BestProjectionRealGdpGrowthRatePrefix = "BestProjectionRealGdpGrowthRate";
            public const string BestProjectionDifferencedRealGdpGrowthRatePrefix = "BestProjectionDifferencedRealGdpGrowthRate";
            public const string OptimisticProjectionInputsPrimeLendingPrefix = "OptimisticProjectionInputsPrimeLending";
            public const string OptimisticProjectionOilExportsPrefix = "OptimisticProjectionOilExports";
            public const string OptimisticProjectionRealGdpGrowthRatePrefix = "OptimisticProjectionRealGdpGrowthRate";
            public const string OptimisticProjectionDifferencedRealGdpGrowthRatePrefix = "OptimisticProjectionDifferencedRealGdpGrowthRate";
            public const string DownturnProjectionInputsPrimeLendingPrefix = "DownturnProjectionInputsPrimeLending";
            public const string DownturnProjectionOilExportsPrefix = "DownturnProjectionOilExports";
            public const string DownturnProjectionRealGdpGrowthRatePrefix = "DownturnProjectionRealGdpGrowthRate";
            public const string DownturnProjectionDifferencedRealGdpGrowthRatePrefix = "DownturnProjectionDifferencedRealGdpGrowthRate";
        }

        public static class InputName
        {
            //
            public const string StatisticalInputsMean = "Mean";
            public const string StatisticalInputsStandardDeviation = "Standard Deviation";
            public const string StatisticalInputsEigenvalues = "Eigenvalues";
            public const string StatisticalInputsPrincipalComponentScore1 = "Principal Component Score 1";
            public const string StatisticalInputsPrincipalComponentScore2 = "Principal Component Score 2";

            //PD Group
            public const string PdGroupConsStage1 = "CONS_STAGE_1";
            public const string PdGroupConsStage2 = "CONS_STAGE_2";
            public const string PdGroupCommStage1 = "COMM_STAGE_1";
            public const string PdGroupCommStage2 = "COMM_STAGE_2";

            //Ratings
            public const string RatingsAAA = "AAA";
            public const string RatingsAA = "AA";
            public const string RatingsA = "A";
            public const string RatingsBBB = "BBB";
            public const string RatingsBB = "BB";
            public const string RatingsB = "B";
            public const string RatingsCCC = "CCC";
            public const string RatingsD = "D";
        }

        public static class InputValue
        {
            //12-month PDs Credit (1 - 10)
            public const string CreditPd1 = "0.00109820943948866";
            public const string CreditPd2 = "0.00214957684286022";
            public const string CreditPd3 = "0.00420746757149746";
            public const string CreditPd4 = "0.00823547361147019";
            public const string CreditPd5 = "0.01611967875038980";
            public const string CreditPd6 = "0.03155180324466860";
            public const string CreditPd7 = "0.06175782429697670";
            public const string CreditPd8 = "0.12088148599052600";
            public const string CreditPd9 = "0.23660700197291600";
            public const string CreditPd10 = "1.0";

            //12-month PDs SnP ETI Policy
            public const string CreditPd1_EtiPolicy = "AA";
            public const string CreditPd2_EtiPolicy = "AA";
            public const string CreditPd3_EtiPolicy = "A";
            public const string CreditPd4_EtiPolicy = "BBB";
            public const string CreditPd5_EtiPolicy = "BB";
            public const string CreditPd6_EtiPolicy = "B";
            public const string CreditPd7_EtiPolicy = "CCC";
            public const string CreditPd8_EtiPolicy = "CCC";
            public const string CreditPd9_EtiPolicy = "CCC";
            public const string CreditPd10_EtiPolicy = "D";

            //12-month PDs SnP Best Fit
            public const string CreditPd1_BestFit = "A";
            public const string CreditPd2_BestFit = "BBB";
            public const string CreditPd3_BestFit = "BBB";
            public const string CreditPd4_BestFit = "BB";
            public const string CreditPd5_BestFit = "BB";
            public const string CreditPd6_BestFit = "B";
            public const string CreditPd7_BestFit = "B";
            public const string CreditPd8_BestFit = "B";
            public const string CreditPd9_BestFit = "CCC";
            public const string CreditPd10_BestFit = "CCC";
        }
    }
}
