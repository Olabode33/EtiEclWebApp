using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.DefaultAssumptions
{
    public class DefaultPdAssumption
    {
        public static class PdAssumptionKey
        {
            public const int MaxHistoricalIndex = 32;
            public const int MaxCreditPd = 10;
            public const int MaxProjectionMonths = 28;
            public const int MaxSnPCummulativeRatesRating = 7;
            public const int MaxSnPYears = 15;
            public const int MaxNonnInteralPdGroup = 4;
            public const int MaxNonInternalProjectionMonths = 240;

            public const string CreditPdKeyPrefix = "CreditPd";
            public const string CreditPdEtiPolicyKeyPrefix = "CreditPdEtiPolicy";
            public const string CreditPdBestFitKeyPrefix = "CreditPdBestFit";
            public const string Upperbound12MonthPdPrefix = "Upperbound12MonthPd";

            public const string SnPMappingAAA = "SnPMappingAAA";
            public const string SnPMappingAA = "SnPMappingAA";
            public const string SnPMappingA = "SnPMappingA";
            public const string SnPMappingBBB = "SnPMappingBBB";
            public const string SnPMappingBB = "SnPMappingBB";
            public const string SnPMappingB = "SnPMappingB";
            public const string SnPMappingCCC = "SnPMappingCCC";

            public const string NonInternalModelConsStage1 = "NonInternalModelConsStage1DefaultRate";
            public const string NonInternalModelConsStage2 = "NonInternalModelConsStage2DefaultRate";
            public const string NonInternalModelCommStage1 = "NonInternalModelCommStage1DefaultRate";
            public const string NonInternalModelCommStage2 = "NonInternalModelCommStage2DefaultRate";

            public const string StatisticsIndexWeightsW1 = "StatisticsIndexWeightsW1";
            public const string StatisticsIndexWeightsW2 = "StatisticsIndexWeightsW2";
            public const string StatisticsIndexWeightsStandardDeviation = "StatisticsIndexWeightsStandardDeviation";
            public const string StatisticsIndexWeightsAverage = "StatisticsIndexWeightsAverage";

            public const string HistoricalIndexActualPrefix = "HistoricalIndexActual";
            public const string HistoricalIndexStandardisedPrefix = "HistoricalIndexStandardised";
            public const string EtiNplSeriesPrefix = "EtiNplSeries";
            public const string HistoricalIndexQuarterPrefix = "HistoricalIndexQ";

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

            public const string MacroeconomicProjectionInputsPrimeLendingPrefix = "MacroeconomicProjectionInputsPrimeLending";
            public const string MacroeconomicProjectionOilExportsPrefix = "MacroeconomicProjectionOilExports";
            public const string MacroeconomicProjectionRealGdpGrowthRatePrefix = "MacroeconomicProjectionRealGdpGrowthRate";
            public const string MacroeconomicProjectionDifferencedRealGdpGrowthRatePrefix = "MacroeconomicProjectionDifferencedRealGdpGrowthRate";
        }

        public static class InputName
        {
            //Statistical Index Weight
            public const string StatisticalIndexWeightW1 = "Index Weight W1";
            public const string StatisticalIndexWeightW2 = "Index Weight W1";
            public const string StatisticalIndexWeightStandardDeviation = "Index Weight Standard Deviation";
            public const string StatisticalIndexWeightAverage = "Index Weight Average";

            //Statistical Inputs
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

            //Macroeconomic Projection
            public const string MacroeconomicProjectionPrimeLending = "Prime Lending Rate (%)";
            public const string MacroeconomicProjectionOilExport = "Oil Exports (USD'm)";
            public const string MacroeconomicProjectionGdpGrowth = "Real GDP Growth Rate (%)";
            public const string MacroeconomicProjectionDifferencedGdpGrowth = "Differenced Real GDP Growth Rate (%)";
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

            public const double StatisticalIndexW1 = 0.58;
            public const double StatisticalIndexW2 = 0.42;
            public const double StatisticalIndexStandardDeviatio = 0.84;
            public const double StatisticalIndexAvearage = 0.00;

            //Statistical Inputs
            public static double[] StatisticalInputPrimeLending = new double[] { 16.91, 1.11, 1.52, 0.73, 0.17 };
            public static double[] StatisticalInputOilExports = new double[] { 18606.52, 5573.94, 1.12, -0.66, 0.42 };
            public static double[] StatisticalInputGdpGrowth = new double[] {  -0.26, 1.24, 0.36, 0.17, 0.89 };


            //Non-internal Model
            public static double[,] NonInternalMarginalDefaultValues = new double[PdAssumptionKey.MaxNonInternalProjectionMonths, PdAssumptionKey.MaxNonnInteralPdGroup]{
                                                                                    { 0.000163084122119295, 0.0133646723869717, 0.000292608537635242, 0.0224198726893341 },
                                                                                    { 0.000163084122119295, 0.0133646723869717, 0.000292608537635242, 0.0224198726893341 },
                                                                                    { 0.000163084122119295, 0.0133646723869717, 0.000292608537635242, 0.0224198726893341 },
                                                                                    { 0.000325320737553536, 0.00187139318802987, 0.00308489810166912, 0.0174366933742008 },
                                                                                    { 0.000325320737553536, 0.00187139318802987, 0.00308489810166912, 0.0174366933742008 },
                                                                                    { 0.000325320737553536, 0.00187139318802987, 0.00308489810166912, 0.0174366933742008 },
                                                                                    { 0.000343798913110649, 0.000520529017865479, 0.00480414379813254, 0.0140539059686594 },
                                                                                    { 0.000343798913110649, 0.000520529017865479, 0.00480414379813254, 0.0140539059686594 },
                                                                                    { 0.000343798913110649, 0.000520529017865479, 0.00480414379813254, 0.0140539059686594 },
                                                                                    { 0.000345904316150336, 0.000366049256702028, 0.00587532022929238, 0.0118091974994676 },
                                                                                    { 0.000345904316150336, 0.000366049256702028, 0.00587532022929238, 0.0118091974994676 },
                                                                                    { 0.000345904316150336, 0.000366049256702028, 0.00587532022929238, 0.0118091974994676 },
                                                                                    { 0.000346144216182287, 0.000348439738318662, 0.00654764647337636, 0.0103421228679255 },
                                                                                    { 0.000346144216182287, 0.000348439738318662, 0.00654764647337636, 0.0103421228679255 },
                                                                                    { 0.000346144216182287, 0.000348439738318662, 0.00654764647337636, 0.0103421228679255 },
                                                                                    { 0.000346171551713681, 0.000346433117555112, 0.00697158342180859, 0.00939278737789428 },
                                                                                    { 0.000346171551713681, 0.000346433117555112, 0.00697158342180859, 0.00939278737789428 },
                                                                                    { 0.000346171551713681, 0.000346433117555112, 0.00697158342180859, 0.00939278737789428 },
                                                                                    { 0.000346174666476462, 0.000346204470764611, 0.00723967516535295, 0.00878243328963269 },
                                                                                    { 0.000346174666476462, 0.000346204470764611, 0.00723967516535295, 0.00878243328963269 },
                                                                                    { 0.000346174666476462, 0.000346204470764611, 0.00723967516535295, 0.00878243328963269 },
                                                                                    { 0.000346175021389894, 0.000346178417457099, 0.00740952394588712, 0.00839164867733211 },
                                                                                    { 0.000346175021389894, 0.000346178417457099, 0.00740952394588712, 0.00839164867733211 },
                                                                                    { 0.000346175021389894, 0.000346178417457099, 0.00740952394588712, 0.00839164867733211 },
                                                                                    { 0.000346175061830767, 0.00034617544879767, 0.00751725626693611, 0.00814211153900135 },
                                                                                    { 0.000346175061830767, 0.00034617544879767, 0.00751725626693611, 0.00814211153900135 },
                                                                                    { 0.000346175061830767, 0.00034617544879767, 0.00751725626693611, 0.00814211153900135 },
                                                                                    { 0.000346175066438748, 0.000346175110531921, 0.00758563948878399, 0.00798303978356463 },
                                                                                    { 0.000346175066438748, 0.000346175110531921, 0.00758563948878399, 0.00798303978356463 },
                                                                                    { 0.000346175066438748, 0.000346175110531921, 0.00758563948878399, 0.00798303978356463 },
                                                                                    { 0.000346175066963883, 0.000346175071988086, 0.00762906612157577, 0.00788174685611531 },
                                                                                    { 0.000346175066963883, 0.000346175071988086, 0.00762906612157577, 0.00788174685611531 },
                                                                                    { 0.000346175066963883, 0.000346175071988086, 0.00762906612157577, 0.00788174685611531 },
                                                                                    { 0.000346175067023724, 0.000346175067596155, 0.00765665230942514, 0.00781729066520898 },
                                                                                    { 0.000346175067023724, 0.000346175067596155, 0.00765665230942514, 0.00781729066520898 },
                                                                                    { 0.000346175067023724, 0.000346175067596155, 0.00765665230942514, 0.00781729066520898 },
                                                                                    { 0.000346175067030496, 0.000346175067095777, 0.00767417937131099, 0.00777629302338934 },
                                                                                    { 0.000346175067030496, 0.000346175067095777, 0.00767417937131099, 0.00777629302338934 },
                                                                                    { 0.000346175067030496, 0.000346175067095777, 0.00767417937131099, 0.00777629302338934 },
                                                                                    { 0.000346175067031274, 0.000346175067038712, 0.00768531663706895, 0.00775022359897992 },
                                                                                    { 0.000346175067031274, 0.000346175067038712, 0.00768531663706895, 0.00775022359897992 },
                                                                                    { 0.000346175067031274, 0.000346175067038712, 0.00768531663706895, 0.00775022359897992 },
                                                                                    { 0.000346175067031385, 0.000346175067032273, 0.00769239415710632, 0.00773364962576473 },
                                                                                    { 0.000346175067031385, 0.000346175067032273, 0.00769239415710632, 0.00773364962576473 },
                                                                                    { 0.000346175067031385, 0.000346175067032273, 0.00769239415710632, 0.00773364962576473 },
                                                                                    { 0.000346175067031385, 0.000346175067031496, 0.00769689200373014, 0.0077231137017868 },
                                                                                    { 0.000346175067031385, 0.000346175067031496, 0.00769689200373014, 0.0077231137017868 },
                                                                                    { 0.000346175067031385, 0.000346175067031496, 0.00769689200373014, 0.0077231137017868 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00769975052565819, 0.00771641659304778 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00769975052565819, 0.00771641659304778 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00769975052565819, 0.00771641659304778 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770156724106652, 0.00771215980336271 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770156724106652, 0.00771215980336271 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770156724106652, 0.00771215980336271 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770272185731269, 0.00770945419831248 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770272185731269, 0.00770945419831248 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770272185731269, 0.00770945419831248 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770345568121855, 0.00770773455420071 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770345568121855, 0.00770773455420071 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770345568121855, 0.00770773455420071 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770392207014015, 0.00770664158569312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770392207014015, 0.00770664158569312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770392207014015, 0.00770664158569312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770421848910097, 0.00770594692379989 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770421848910097, 0.00770594692379989 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770421848910097, 0.00770594692379989 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770440688203711, 0.00770550541715864 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770440688203711, 0.00770550541715864 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770440688203711, 0.00770550541715864 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770452661777865, 0.00770522480938673 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770452661777865, 0.00770522480938673 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770452661777865, 0.00770522480938673 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770460271755102, 0.0077050464642312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770460271755102, 0.0077050464642312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770460271755102, 0.0077050464642312 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770465108388074, 0.00770493311400045 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770465108388074, 0.00770493311400045 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770465108388074, 0.00770493311400045 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770468182382322, 0.00770486107243107 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770468182382322, 0.00770486107243107 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770468182382322, 0.00770486107243107 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047013610565, 0.00770481528527101 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047013610565, 0.00770481528527101 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047013610565, 0.00770481528527101 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770471377824078, 0.00770478618452575 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770471377824078, 0.00770478618452575 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770471377824078, 0.00770478618452575 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472167017089, 0.00770476768909578 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472167017089, 0.00770476768909578 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472167017089, 0.00770476768909578 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047266860073, 0.00770475593403985 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047266860073, 0.00770475593403985 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047266860073, 0.00770475593403985 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472987389859, 0.00770474846293334 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472987389859, 0.00770474846293334 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770472987389859, 0.00770474846293334 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473190001153, 0.00770474371455709 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473190001153, 0.00770474371455709 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473190001153, 0.00770474371455709 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473318773857, 0.00770474069665372 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473318773857, 0.00770474069665372 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473318773857, 0.00770474069665372 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473400617322, 0.00770473877857902 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473400617322, 0.00770473877857902 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473400617322, 0.00770473877857902 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473452634179, 0.00770473755951728 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473452634179, 0.00770473755951728 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473452634179, 0.00770473755951728 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047348569429, 0.00770473678472405 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047348569429, 0.00770473678472405 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047348569429, 0.00770473678472405 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473506706149, 0.00770473629229218 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473506706149, 0.00770473629229218 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473506706149, 0.00770473629229218 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473520060566, 0.00770473597931964 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473520060566, 0.00770473597931964 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473520060566, 0.00770473597931964 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473528548155, 0.0077047357804052 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473528548155, 0.0077047357804052 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473528548155, 0.0077047357804052 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473533942584, 0.00770473565398211 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473533942584, 0.00770473565398211 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473533942584, 0.00770473565398211 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473537371086, 0.00770473557363194 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473537371086, 0.00770473557363194 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473537371086, 0.00770473557363194 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473539550121, 0.00770473552256412 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473539550121, 0.00770473552256412 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473539550121, 0.00770473552256412 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473540935057, 0.00770473549010731 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473540935057, 0.00770473549010731 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473540935057, 0.00770473549010731 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473541815264, 0.00770473546947881 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473541815264, 0.00770473546947881 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473541815264, 0.00770473546947881 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542374683, 0.00770473545636807 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542374683, 0.00770473545636807 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542374683, 0.00770473545636807 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542730254, 0.0077047354480354 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542730254, 0.0077047354480354 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542730254, 0.0077047354480354 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542956218, 0.00770473544273942 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542956218, 0.00770473544273942 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473542956218, 0.00770473544273942 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543099859, 0.00770473543937333 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543099859, 0.00770473543937333 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543099859, 0.00770473543937333 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047354319113, 0.00770473543723404 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047354319113, 0.00770473543723404 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.0077047354319113, 0.00770473543723404 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543249151, 0.00770473543587435 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543249151, 0.00770473543587435 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543249151, 0.00770473543587435 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543286021, 0.00770473543501016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543286021, 0.00770473543501016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543286021, 0.00770473543501016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543309447, 0.00770473543446115 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543309447, 0.00770473543446115 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543309447, 0.00770473543446115 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543324357, 0.00770473543411199 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543324357, 0.00770473543411199 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543324357, 0.00770473543411199 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543333805, 0.00770473543389016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543333805, 0.00770473543389016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543333805, 0.00770473543389016 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543339834, 0.00770473543374917 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543339834, 0.00770473543374917 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543339834, 0.00770473543374917 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543343664, 0.00770473543365957 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543343664, 0.00770473543365957 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543343664, 0.00770473543365957 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543346095, 0.00770473543360251 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543346095, 0.00770473543360251 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543346095, 0.00770473543360251 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543347638, 0.00770473543356631 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543347638, 0.00770473543356631 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543347638, 0.00770473543356631 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543348604, 0.00770473543354344 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543348604, 0.00770473543354344 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543348604, 0.00770473543354344 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349226, 0.00770473543352868 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349226, 0.00770473543352868 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349226, 0.00770473543352868 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349637, 0.00770473543351946 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349637, 0.00770473543351946 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349637, 0.00770473543351946 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349881, 0.00770473543351369 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349881, 0.00770473543351369 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543349881, 0.00770473543351369 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350048, 0.00770473543350958 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350048, 0.00770473543350958 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350048, 0.00770473543350958 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350159, 0.00770473543350725 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350159, 0.00770473543350725 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350159, 0.00770473543350725 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350214, 0.0077047354335058 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350214, 0.0077047354335058 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350214, 0.0077047354335058 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350247, 0.00770473543350514 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350247, 0.00770473543350514 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350247, 0.00770473543350514 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350281, 0.00770473543350447 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350281, 0.00770473543350447 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350281, 0.00770473543350447 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350292, 0.00770473543350403 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350292, 0.00770473543350403 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350292, 0.00770473543350403 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350381 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350381 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350381 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350347 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350347 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350303, 0.00770473543350347 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350347, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350347, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350347, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350336 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350314, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350303 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350303 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350325, 0.00770473543350303 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350336, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350336, 0.00770473543350325 },
                                                                                    { 0.000346175067031385, 0.000346175067031385, 0.00770473543350336, 0.00770473543350325 }
            };
            public static double[,] NonInternalCummulativeValues = new double[PdAssumptionKey.MaxNonInternalProjectionMonths, PdAssumptionKey.MaxNonnInteralPdGroup]
            {
                { 0.999836915877881, 0.986635327613028, 0.999707391462365, 0.977580127310666 },
                { 0.999673858352192, 0.973449269694068, 0.999414868544486, 0.955662905312738 },
                { 0.999510827418597, 0.96043943911927, 0.99912243122131, 0.934237064641707 },
                { 0.999185665819029, 0.958642079295386, 0.9960402403299, 0.917947059406736 },
                { 0.998860610001272, 0.956848083038434, 0.992967557683321, 0.901941097998112 },
                { 0.998535659930913, 0.955057444053857, 0.989904353949604, 0.886214227630729 },
                { 0.998192364456326, 0.954560308940498, 0.985148711086833, 0.873759456207518 },
                { 0.997849187006351, 0.954063432600392, 0.980415915016227, 0.861479722970751 },
                { 0.99750612754041, 0.953566814898839, 0.975705855978511, 0.849372567950213 },
                { 0.997161085865507, 0.95321776247503, 0.969973271625042, 0.839342159544659 },
                { 0.996816163542009, 0.952868837821601, 0.96427436804039, 0.829430202212966 },
                { 0.996471360528631, 0.952520040891781, 0.958608947339254, 0.81963529714301 },
                { 0.996126437730593, 0.95218814505799, 0.952332314845861, 0.811158528193068 },
                { 0.995781634325586, 0.951856364869896, 0.946096779523078, 0.80276942702913 },
                { 0.995436950272284, 0.951524700287204, 0.939902072281161, 0.794467086980181 },
                { 0.995092358318575, 0.951195060618852, 0.933349466575923, 0.787004826553441 },
                { 0.994747885652798, 0.950865535148599, 0.926842542907988, 0.779612657552248 },
                { 0.994403532233657, 0.950536123836882, 0.920380982801224, 0.772289921622744 },
                { 0.994059294922543, 0.950207043981186, 0.913717723457375, 0.765507336905837 },
                { 0.993715176777666, 0.949878078054408, 0.907102703946717, 0.758784319786737 },
                { 0.993371177757772, 0.949549226017104, 0.90053557502853, 0.752120347116991 },
                { 0.993027297469064, 0.949220512568744, 0.893863035121233, 0.745808817400912 },
                { 0.992683536223122, 0.948891912913885, 0.887239935558158, 0.739550251824827 },
                { 0.992339893978736, 0.948563427013135, 0.880665910009893, 0.733344205932281 },
                { 0.991996370654581, 0.948235057643075, 0.874045718678794, 0.7273732356111 },
                { 0.991652966249634, 0.94790680194643, 0.867475293022467, 0.72145088159627 },
                { 0.991309680722728, 0.947578659883848, 0.860954258939582, 0.715576748048402 },
                { 0.990966514028142, 0.947250631736525, 0.854423370314933, 0.709864270400538 },
                { 0.99062346612931, 0.946922717144382, 0.847942022656932, 0.704197395688999 },
                { 0.990280536985107, 0.94659491606811, 0.841509840165666, 0.698575759863731 },
                { 0.989937726553903, 0.946267228504896, 0.835089905953086, 0.693069762564667 },
                { 0.989595034795123, 0.945939654378948, 0.828718949843109, 0.687607162142504 },
                { 0.989252461667686, 0.945612193650997, 0.822396598178553, 0.682187616554045 },
                { 0.988910007130464, 0.945284846285941, 0.816099793365846, 0.676854757667236 },
                { 0.988567671142466, 0.94495761224038, 0.80985120099825, 0.671563587288422 },
                { 0.98822545366265, 0.944630491475087, 0.803650451929836, 0.666313779526418 },
                { 0.987883354649987, 0.94430348395132, 0.797483094209892, 0.661132328331298 },
                { 0.987541374063473, 0.943976589629404, 0.791363065899337, 0.655991169618959 },
                { 0.987199511862111, 0.943649808470153, 0.785290003783795, 0.650889990063246 },
                { 0.986857768004919, 0.943323140434444, 0.779254801452791, 0.645845447101918 },
                { 0.98651614245093, 0.942996585483065, 0.77326598156267, 0.640840000476495 },
                { 0.986174635159189, 0.942670143576868, 0.767323187649687, 0.635873347181632 },
                { 0.985833246088759, 0.942343814676726, 0.761420635244398, 0.630955725508167 },
                { 0.985491975198712, 0.942017598743513, 0.755563487598744, 0.626076134997716 },
                { 0.985150822448139, 0.941691495738123, 0.749751395441417, 0.621234281530591 },
                { 0.984809787796142, 0.941365505621462, 0.743980639921058, 0.616436418538882 },
                { 0.984468871201838, 0.941039628354453, 0.73825430128272, 0.611675609988584 },
                { 0.98412807262436, 0.940713863898028, 0.732572037654458, 0.606951569704033 },
                { 0.983787392022851, 0.940388212213136, 0.726931415722445, 0.602268078540392 },
                { 0.983446829356473, 0.940062673260737, 0.721334225172119, 0.59762072714568 },
                { 0.983106384584399, 0.939737247001808, 0.715780131592675, 0.593009236650384 },
                { 0.982766057665817, 0.939411933397335, 0.710267502779394, 0.588435854652466 },
                { 0.982425848559928, 0.939086732408321, 0.704797329847594, 0.583897743307358 },
                { 0.982085757225949, 0.938761643995781, 0.699369285820449, 0.579394630602148 },
                { 0.981745783623111, 0.938436668120744, 0.693982238736227, 0.574927814234773 },
                { 0.981405927710658, 0.938111804744253, 0.688636686577326, 0.570495434583594 },
                { 0.981066189447847, 0.937787053827363, 0.68333230971988, 0.566097226160326 },
                { 0.980726568793953, 0.937462415331143, 0.678068289556408, 0.561733899009212 },
                { 0.980387065708261, 0.937137889216676, 0.67284482053897, 0.557404203225553 },
                { 0.980047680150073, 0.936813475445059, 0.667661590283611, 0.553107879587695 },
                { 0.979708412078703, 0.936489173977401, 0.66251797742284, 0.54884527540149 },
                { 0.979369261453481, 0.936164984774825, 0.657413990554708, 0.54461552157797 },
                { 0.979030228233748, 0.935840907798468, 0.652349324403655, 0.540418364951163 },
                { 0.978691312378864, 0.93551694300948, 0.647323482677231, 0.536253929714202 },
                { 0.978352513848198, 0.935193090369025, 0.64233636113356, 0.532121585394146 },
                { 0.978013832601136, 0.934869349838279, 0.637387661463893, 0.52802108470009 },
                { 0.977675268597078, 0.934545721378433, 0.632476967578385, 0.52395241537156 },
                { 0.977336821795437, 0.934222204950691, 0.627604107676644, 0.519915097196581 },
                { 0.976998492155639, 0.93389880051627, 0.622768790270266, 0.51590888859867 },
                { 0.976660279637128, 0.933575508036401, 0.617970651548907, 0.511933694630856 },
                { 0.976322184199357, 0.933252327472328, 0.613209480215042, 0.507989130426226 },
                { 0.975984205801798, 0.932929258785308, 0.608484991452451, 0.504074959975567 },
                { 0.975646344403933, 0.932606301936612, 0.603796856333717, 0.5001910389875 },
                { 0.97530859996526, 0.932283456887525, 0.59914484143356, 0.496337043791109 },
                { 0.974970972445291, 0.931960723599345, 0.594528668460044, 0.492512743806779 },
                { 0.974633461803551, 0.931638102033382, 0.589948032510195, 0.488717966057955 },
                { 0.97429606799958, 0.931315592150962, 0.585402688762082, 0.484952426817868 },
                { 0.973958790992932, 0.930993193913422, 0.580892365301605, 0.481215900805764 },
                { 0.973621630743174, 0.930670907282113, 0.576416774453068, 0.477508199144211 },
                { 0.973284587209888, 0.930348732218401, 0.571975666607993, 0.473829064808858 },
                { 0.97294766035267, 0.930026668683662, 0.567568776085809, 0.470178277692426 },
                { 0.97261085013113, 0.92970471663929, 0.563195828164207, 0.466555640911659 },
                { 0.97227415650489, 0.929382876046688, 0.558856572500409, 0.462960915878133 },
                { 0.971937579433589, 0.929061146867274, 0.55455074950563, 0.459393887536992 },
                { 0.971601118876878, 0.928739529062481, 0.55027809470518, 0.455854355859042 },
                { 0.971264774794423, 0.928418022593754, 0.546038359487041, 0.452342095515863 },
                { 0.970928547145904, 0.928096627422549, 0.541831290215253, 0.448856896387653 },
                { 0.970592435891013, 0.927775343510339, 0.537656630931955, 0.445398558275338 },
                { 0.970256440989458, 0.92745417081861, 0.533514136236503, 0.441966865854768 },
                { 0.969920562400961, 0.927133109308858, 0.529403558309699, 0.438561613827079 },
                { 0.969584800085256, 0.926812158942596, 0.525324648586323, 0.435182603630503 },
                { 0.969249154002094, 0.926491319681348, 0.521277165747542, 0.43182962788279 },
                { 0.968913624111237, 0.926170591486654, 0.517260867657801, 0.428502485994866 },
                { 0.968578210372463, 0.925849974320063, 0.513275512398159, 0.425200982124534 },
                { 0.968242912745562, 0.925529468143142, 0.509320863224245, 0.421924915511072 },
                { 0.96790773119034, 0.925209072917468, 0.505396683553961, 0.418674090166815 },
                { 0.967572665666615, 0.924888788604633, 0.501502737604023, 0.415448313602154 },
                { 0.96723771613422, 0.924568615166241, 0.497638793463662, 0.412247390819205 },
                { 0.966902882553002, 0.924248552563911, 0.493804619976979, 0.409071130325948 },
                { 0.966568164882821, 0.923928600759273, 0.489999987133184, 0.405919343340299 },
                { 0.966233563083553, 0.923608759713973, 0.486224667970327, 0.402791840056106 },
                { 0.965899077115084, 0.923289029389669, 0.482478436634319, 0.399688433373746 },
                { 0.965564706937318, 0.92296940974803, 0.478761068616337, 0.396608938401782 },
                { 0.965230452510171, 0.922649900750743, 0.475072341930137, 0.393553170134147 },
                { 0.964896313793573, 0.922330502359504, 0.471412035901876, 0.390520945762781 },
                { 0.964562290747467, 0.922011214536025, 0.467779931312729, 0.387512084364185 },
                { 0.964228383331811, 0.921692037242029, 0.464175811125214, 0.384526405453017 },
                { 0.963894591506578, 0.921372970439254, 0.460599459726945, 0.381563730414297 },
                { 0.963560915231752, 0.921054014089452, 0.457050663014498, 0.378623882304857 },
                { 0.963227354467333, 0.920735168154385, 0.453529208839781, 0.375706684951288 },
                { 0.962893909173334, 0.920416432595831, 0.450034886535792, 0.372811963835477 },
                { 0.962560579309782, 0.92009780737558, 0.446567486964098, 0.369939545967513 },
                { 0.962227364836717, 0.919779292455437, 0.443126802787477, 0.367089259321743 },
                { 0.961894265714196, 0.919460887797217, 0.439712628170885, 0.364260933382936 },
                { 0.961561281902285, 0.919142593362751, 0.436324758806466, 0.36145439906364 },
                { 0.961228413361068, 0.918824409113882, 0.432962992078394, 0.358669488350291 },
                { 0.96089566005064, 0.918506335012467, 0.429627126872768, 0.355906034638715 },
                { 0.960563021931112, 0.918188371020375, 0.426316963588751, 0.353163872679172 },
                { 0.960230498962607, 0.91787051709949, 0.42303230423659, 0.350442838352994 },
                { 0.959898091105263, 0.917552773211707, 0.41977295231524, 0.347742768877349 },
                { 0.959565798319231, 0.917235139318935, 0.416538712815001, 0.345063502767565 },
                { 0.959233620564677, 0.916917615383098, 0.413329392274137, 0.342404879694904 },
                { 0.958901557801779, 0.916600201366131, 0.410144798698659, 0.339766740610221 },
                { 0.958569609990731, 0.916282897229982, 0.406984741559782, 0.337148927717105 },
                { 0.958237777091738, 0.915965702936613, 0.403849031824925, 0.334551284380111 },
                { 0.957906059065021, 0.915648618448001, 0.400737481903785, 0.331973655198143 },
                { 0.957574455870815, 0.915331643726132, 0.397649905642657, 0.329415885984383 },
                { 0.957242967469366, 0.915014778733009, 0.394586118339634, 0.326877823705942 },
                { 0.956911593820937, 0.914698023430647, 0.391545936707089, 0.324359316526096 },
                { 0.956580334885803, 0.914381377781072, 0.388529178864155, 0.321860213788511 },
                { 0.956249190624253, 0.914064841746327, 0.385535664342194, 0.319380365976481 },
                { 0.95591816099659, 0.913748415288464, 0.38256521405757, 0.316919624735898 },
                { 0.95558724596313, 0.913432098369552, 0.379617650303067, 0.314477842862222 },
                { 0.955256445484205, 0.91311589095167, 0.376692796747421, 0.312054874271956 },
                { 0.954925759520157, 0.912799792996912, 0.373790478414558, 0.309650574013729 },
                { 0.954595188031345, 0.912483804467385, 0.370910521674458, 0.307264798257041 },
                { 0.954264730978141, 0.912167925325209, 0.368052754239073, 0.304897404271416 },
                { 0.953934388320929, 0.911852155532516, 0.365217005145679, 0.302548250430171 },
                { 0.953604160020108, 0.911536495051451, 0.36240310474748, 0.300217196200341 },
                { 0.953274046036092, 0.911220943844175, 0.359610884707368, 0.297904102126666 },
                { 0.952944046329306, 0.91090550187286, 0.356840177983919, 0.295608829830896 },
                { 0.952614160860191, 0.91059016909969, 0.354090818821915, 0.293331242002511 },
                { 0.9522843895892, 0.910274945486863, 0.351362642744856, 0.291071202385791 },
                { 0.951954732476801, 0.909959830996593, 0.348655486542675, 0.288828575776409 },
                { 0.951625189483475, 0.909644825591101, 0.345969188262297, 0.286603228012721 },
                { 0.951295760569717, 0.909329929232628, 0.343303587199459, 0.284395025964812 },
                { 0.950966445696035, 0.909015141883422, 0.340658523887574, 0.28220383752948 },
                { 0.950637244822951, 0.908700463505748, 0.338033840088395, 0.280029531621943 },
                { 0.950308157911002, 0.908385894061882, 0.335429378783481, 0.277871978166183 },
                { 0.949979184920737, 0.908071433514115, 0.332844984163864, 0.275731048088992 },
                { 0.949650325812719, 0.907757081824749, 0.33028050162085, 0.27360661331201 },
                { 0.949321580547524, 0.9074428389561, 0.32773577773735, 0.271498546742935 },
                { 0.948992949085744, 0.907128704870498, 0.325210660278122, 0.269406722269057 },
                { 0.948664431387982, 0.906814679530283, 0.322704998180733, 0.26733101474956 },
                { 0.948336027414855, 0.906500762897811, 0.320218641546889, 0.265271300007342 },
                { 0.948007737126997, 0.906186954935451, 0.317751441633101, 0.263227454822284 },
                { 0.947679560485051, 0.905873255605584, 0.315303250841833, 0.261199356923792 },
                { 0.947351497449676, 0.905559664870602, 0.312873922712902, 0.259186884983043 },
                { 0.947023547981544, 0.905246182692915, 0.310463311914485, 0.257189918606166 },
                { 0.946695712041341, 0.904932809034941, 0.308071274234455, 0.255208338326985 },
                { 0.946367989589767, 0.904619543859115, 0.305697666571897, 0.253242025599596 },
                { 0.946040380587534, 0.904306387127881, 0.3033423469284, 0.251290862791553 },
                { 0.94571288499537, 0.903993338803701, 0.301005174399589, 0.24935473317679 },
                { 0.945385502774014, 0.903680398849044, 0.298686009166774, 0.247433520928474 },
                { 0.945058233884221, 0.903367567226398, 0.296384712488505, 0.245527111112245 },
                { 0.944731078286757, 0.903054843898259, 0.294101146692277, 0.243635389679312 },
                { 0.944404035942405, 0.90274222882714, 0.291835175166354, 0.241758243459534 },
                { 0.944077106811958, 0.902429721975563, 0.289586662351538, 0.239895560154751 },
                { 0.943750290856224, 0.902117323306068, 0.287355473733067, 0.238047228332049 },
                { 0.943423588036026, 0.901805032781202, 0.285141475832604, 0.236213137417035 },
                { 0.943096998312199, 0.90149285036353, 0.282944536200214, 0.234393177687282 },
                { 0.942770521645591, 0.901180776015627, 0.280764523406448, 0.23258724026576 },
                { 0.942444157997065, 0.900868809700082, 0.2786013070345, 0.23079521711428 },
                { 0.942117907327497, 0.900556951379498, 0.276454757672383, 0.229017001027074 },
                { 0.941791769597776, 0.900245201016488, 0.274324746905191, 0.227252485624372 },
                { 0.941465744768806, 0.899933558573682, 0.272211147307431, 0.225501565346015 },
                { 0.941139832801503, 0.899622024013719, 0.270113832435384, 0.223764135445169 },
                { 0.940814033656797, 0.899310597299253, 0.268032676819545, 0.222040091982049 },
                { 0.940488347295632, 0.898999278392951, 0.265967555957121, 0.220329331817687 },
                { 0.940162773678965, 0.898688067257492, 0.263918346304581, 0.218631752607783 },
                { 0.939837312767766, 0.898376963855569, 0.261884925270259, 0.216947252796571 },
                { 0.93951196452302, 0.898065968149886, 0.259867171207032, 0.215275731610743 },
                { 0.939186728905724, 0.897755080103164, 0.257864963405032, 0.213617089053422 },
                { 0.93886160587689, 0.897444299678131, 0.255878182084428, 0.211971225898187 },
                { 0.938536595397543, 0.897133626837533, 0.253906708388263, 0.210338043683123 },
                { 0.93821169742872, 0.896823061544126, 0.251950424375342, 0.20871744470494 },
                { 0.937886911931473, 0.896512603760681, 0.250009213013172, 0.20710933201313 },
                { 0.937562238866867, 0.89620225344998, 0.248082958170968, 0.205513609404157 },
                { 0.937237678195981, 0.895892010574818, 0.246171544612701, 0.203930181415711 },
                { 0.936913229879907, 0.895581875098005, 0.244274857990204, 0.202358953320996 },
                { 0.936588893879751, 0.89527184698236, 0.242392784836334, 0.200799831123056 },
                { 0.936264670156631, 0.89496192619072, 0.24052521255818, 0.199252721549159 },
                { 0.935940558671681, 0.89465211268593, 0.238672029430333, 0.197717532045217 },
                { 0.935616559386045, 0.894342406430851, 0.236833124588195, 0.196194170770242 },
                { 0.935292672260884, 0.894032807388356, 0.235008388021354, 0.194682546590861 },
                { 0.93496889725737, 0.89372331552133, 0.233197710566995, 0.193182569075857 },
                { 0.93464523433669, 0.893413930792672, 0.231400983903378, 0.191694148490763 },
                { 0.934321683460043, 0.893104653165293, 0.22961810054335, 0.19021719579249 },
                { 0.933998244588642, 0.892795482602118, 0.227848953827921, 0.188751622624006 },
                { 0.933674917683714, 0.892486419066083, 0.226093437919876, 0.187297341309043 },
                { 0.9333517027065, 0.892177462520138, 0.224351447797453, 0.185854264846858 },
                { 0.933028599618252, 0.891868612927246, 0.22262287924805, 0.184422306907025 },
                { 0.932705608380237, 0.891559870250383, 0.220907628861999, 0.183001381824269 },
                { 0.932382728953735, 0.891251234452536, 0.219205594026375, 0.181591404593348 },
                { 0.932059961300041, 0.890942705496708, 0.217516672918858, 0.180192290863958 },
                { 0.93173730538046, 0.890634283345911, 0.215840764501642, 0.178803956935694 },
                { 0.931414761156314, 0.890325967963174, 0.214177768515392, 0.177426319753041 },
                { 0.931092328588937, 0.890017759311534, 0.212527585473243, 0.176059296900403 },
                { 0.930770007639676, 0.889709657354046, 0.21089011665485, 0.174702806597177 },
                { 0.93044779826989, 0.889401662053773, 0.209265264100484, 0.173356767692855 },
                { 0.930125700440955, 0.889093773373793, 0.207652930605168, 0.172021099662174 },
                { 0.929803714114257, 0.888785991277198, 0.206053019712863, 0.170695722600297 },
                { 0.929481839251197, 0.888478315727091, 0.204465435710701, 0.169380557218031 },
                { 0.92916007581319, 0.888170746686589, 0.202890083623254, 0.168075524837087 },
                { 0.928838423761663, 0.887863284118819, 0.201326869206856, 0.16678054738537 },
                { 0.928516883058056, 0.887555927986925, 0.199775698943962, 0.165495547392311 },
                { 0.928195453663823, 0.88724867825406, 0.198236480037555, 0.16422044798423 },
                { 0.927874135540433, 0.886941534883391, 0.196709120405597, 0.16295517287974 },
                { 0.927552928649365, 0.8866344978381, 0.195193528675515, 0.161699646385181 },
                { 0.927231832952115, 0.886327567081379, 0.193689614178738, 0.160453793390092 },
                { 0.926910848410189, 0.886020742576433, 0.192197286945273, 0.159217539362719 },
                { 0.926589974985109, 0.88571402428648, 0.190716457698323, 0.157990810345556 },
                { 0.926269212638408, 0.885407412174752, 0.189247037848942, 0.156773532950919 },
                { 0.925948561331634, 0.885100906204492, 0.187788939490742, 0.155565634356556 },
                { 0.925628021026347, 0.884794506338958, 0.186342075394628, 0.154367042301294 },
                { 0.925307591684122, 0.884488212541417, 0.184906359003582, 0.15317768508071 },
                { 0.924987273266546, 0.884182024775152, 0.183481704427487, 0.151997491542847 },
                { 0.92466706573522, 0.883875943003457, 0.182068026437985, 0.150826391083953 },
                { 0.924346969051757, 0.883569967189641, 0.180665240463381, 0.149664313644261 },
                { 0.924026983177786, 0.883264097297022, 0.17927326258358, 0.148511189703795 },
                { 0.923707108074945, 0.882958333288933, 0.177892009525072, 0.147366950278213 },
                { 0.92338734370489, 0.882652675128721, 0.176521398655948, 0.146231526914677 },
                { 0.923067690029287, 0.882347122779743, 0.175161347980952, 0.145104851687762 },
                { 0.922748147009817, 0.88204167620537, 0.173811776136583, 0.14398685719539 },
                { 0.922428714608173, 0.881736335368985, 0.172472602386223, 0.142877476554798 },
                { 0.922109392786061, 0.881431100233985, 0.171143746615309, 0.141776643398537 },
                { 0.921790181505203, 0.881125970763778, 0.16982512932654, 0.140684291870501 },
                { 0.921471080727332, 0.880820946921785, 0.168516671635118, 0.139600356621989 },
                { 0.921152090414194, 0.880516028671442, 0.167218295264035, 0.138524772807794 },
                { 0.920833210527548, 0.880211215976195, 0.165929922539384, 0.137457476082324 }
            };

            //Macro-economic model
            public static string[] HistoricalIndexDate = new string[] {"2009-03-31","2009-06-30", "2009-09-30", "2009-12-31",
                                                                       "2010-03-31", "2010-06-30", "2010-09-30", "2010-12-31",
                                                                       "2011-03-31", "2011-06-30", "2011-09-30", "2011-12-31",
                                                                       "2012-03-31", "2012-06-30", "2012-09-30", "2012-12-31",
                                                                       "2013-03-31", "2013-06-30", "2013-09-30", "2013-12-31",
                                                                       "2014-03-31", "2014-06-30", "2014-09-30", "2014-12-31",
                                                                       "2015-03-31", "2015-06-30", "2015-09-30", "2015-12-31",
                                                                       "2016-03-31", "2016-06-30", "2016-09-30", "2016-12-31"};
            //Macro-economic projections
            public static string[] MacroeconomicProjectionDate = new string[] {"2016-03-31", "2016-06-30", "2016-09-30", "2016-12-31",
                                                                               "2017-03-31", "2017-06-30", "2017-09-30", "2017-12-31",
                                                                               "2018-03-31", "2018-06-30", "2018-09-30", "2018-12-31",
                                                                               "2019-03-31", "2019-06-30", "2019-09-30", "2019-12-31",
                                                                               "2020-03-31", "2020-06-30", "2020-09-30", "2020-12-31",
                                                                               "2021-03-31", "2021-06-30", "2021-09-30", "2021-12-31",
                                                                               "2022-03-31", "2022-06-30", "2022-09-30", "2022-12-31" };


            public static double[] HistoricalIndexActual = new double[] { -0.625854455275635, -1.80348542962092, 1.81891618871537, 1.33983800967755, 1.47860535986325, 1.26100532849036, 1.21906008055837, 0.757446265471478, 0.22876595759205, -0.763401911305572, -0.669485587226374, -1.61385106240239, -0.191985975567675, -0.609816994702729, 0.257292685936015, 0.438944546914496, -0.888115271200889, 0.0818868961877547, 0.22919644478146, -0.21849265151891, 0.34307295277668, -0.174470387082317, 0.103757311621295, -0.251574098530102, -0.251699768043672, -0.99312496733012, -0.437866399419864, 0.346934352047545, 0.197295482457322, -0.653403225019291, 0.00969657113321359, 0.0349137501071141 };
            public static double[] HistoricalIndexStandardised = new double[] { -0.744846944640842, -2.14637860389136, 2.1647431831703, 1.59457880247295, 1.75972971884019, 1.50075781705868, 1.45083760078339, 0.901458049560751, 0.272260783815197, -0.908546030750988, -0.796773578781807, -1.92068942355498, -0.228487895406711, -0.725760312905968, 0.306211243492205, 0.522400219214975, -1.05697089900988, 0.0974558923647431, 0.27277311869599, -0.260034234061572, 0.408300745496845, -0.207642102176551, 0.123484487314115, -0.299405392200293, -0.299554955013863, -1.18194588426383, -0.521117085642104, 0.412896305094905, 0.234806888500072, -0.777633508352451, 0.0115401613253356, 0.0415518334525543 };
            public static double[] HistoricalIndexNplSeries = new double[] { 0.0465122332294269, 0.105446454657796, 0.285275933021577, 0.382477288211133, 0.405025670714304, 0.322759614200389, 0.219407483504469, 0.24306, 0.206673466481902, 0.0905585349604633, 0.0507602057739988, 0.059, 0.06, 0.04, 0.0372721303261889, 0.0463, 0.0417, 0.0308, 0.0351, 0.0593, 0.0561, 0.0222612203771274, 0.0373466360426162, 0.0468477759607759, 0.0526399855636345, 0.0313021517303864, 0.0390247714906672, 0.101747301755425, 0.119205207441604, 0.0768127013798171, 0.0783279466468615, 0.0911129218938252 };

            //Best
            public static double[] MacroecoBestProjectionPrimeLending = new double[] { 16.69, 16.56, 17.14, 17.08, 17.16, 17.55, 17.64, 17.69, 17.74, 16.7654084302061, 16.8154084302061, 16.8654084302061, 16.9154084302061, 16.9654084302061, 16.7550832269541, 16.8050832269541, 16.8550832269541, 16.9050832269541, 16.9550832269541, 17.0050832269541, 17.0550832269541, 17.1050832269541, 17.1550832269541, 17.2050832269541, 16.8487417635395, 16.8987417635395, 16.9487417635395, 16.9987417635395 };
            public static double[] MacroecoBestProjectionOilExport = new double[] { 7045.39910718547, 8299.34350634626, 7444.756575241, 9239.53978932432, 9247.89, 9062.57622809992, 8760.63959648114, 9810.6804938738, 8345.30722363335, 8366.47830384812, 8320.85805032831, 9319.48481863517, 8220.87688427339, 8036.78205956064, 8204.40356053361, 8977.06994524282, 9250.55846460047, 9365.761842087, 9642.79844619148, 10704.8293564295, 10730.8922105636, 10844.393493311, 11145.4775943678, 12295.143182268, 11530.6959514872, 11163.6658913081, 11480.4672981925, 12656.8612251272 };
            public static double[] MacroecoBestProjectionGdpGrowth = new double[] { -0.665936721292992, -1.48693680701277, -2.34082894664718, -1.7277383960996, -0.517454673429329, 0.55, 2.51756667060183, 2.0044591416164, 1.42162990050316, 1.79945265689749, 3.2313535526417, 3.45527215627088, 3.34002171497845, 3.19558275968215, 3.46140269015902, 3.55050003138226, 5.07498108975128, 5.36734105479701, 5.38325565036062, 5.34434212554074, 5.52676886852792, 5.59725014246844, 5.61553404380724, 5.68456409834313, 5.41456420870219, 5.13534338672421, 5.16252978596372, 5.55524317002063 };

            //Optimistic
            public static double[] MacroecoOptimisticProjectionPrimeLending = new double[] { 16.69, 16.56, 17.14, 17.08, 17.16, 17.55, 16.7654084302061, 16.8154084302061, 16.8654084302061, 16.9154084302061, 16.9654084302061, 17.0154084302061, 16.7550832269541, 16.8050832269541, 16.8550832269541, 16.9050832269541, 16.8487417635395, 16.8987417635395, 16.9487417635395, 16.9987417635395, 16.8844560492537, 16.9344560492537, 16.9844560492537, 17.0344560492537, 16.5487417635395, 16.5987417635395, 16.6487417635395, 16.6987417635395 };
            public static double[] MacroecoOptimisticProjectionOilExport = new double[] { 7045.39910718547, 8299.34350634626, 7444.756575241, 9239.53978932432, 9247.89, 9062.57622809992, 9417.68756621722, 10325.4384210215, 10264.7711623608, 10203.1077136287, 10185.9384915385, 10312.6222865836, 10631.0726584867, 10758.7539175048, 10982.9789503481, 11207.9592308801, 11564.3526092443, 11629.979098868, 11694.6840055426, 11724.3687877405, 12099.4117951461, 12482.3332621595, 12873.2738755593, 13288.2918138001, 13121.1832111729, 13181.3264117017, 13134.0817451861, 13300.2599633376 };
            public static double[] MacroecoOptimisticProjectionGdpGrowth = new double[] { -0.665936721292992, -1.48693680701277, -2.34082894664718, -1.7277383960996, -0.517454673429329, 0.55, 2.51756667060183, 2.0044591416164, 1.42162990050316, 1.79945265689749, 3.2313535526417, 3.45527215627088, 3.34002171497845, 3.19558275968215, 3.46140269015902, 3.55050003138226, 5.07498108975128, 5.36734105479701, 5.38325565036062, 5.34434212554074, 5.52676886852792, 5.59725014246844, 5.61553404380724, 5.68456409834313, 5.41456420870219, 5.13534338672421, 5.16252978596372, 5.55524317002063 };

            //Downturn
            public static double[] MacroecoDownturnProjectionPrimeLending = new double[] { 16.69, 16.56, 17.14, 17.08, 17.16, 17.55, 16.7654084302061, 16.8154084302061, 16.8654084302061, 16.9154084302061, 16.9654084302061, 17.0154084302061, 16.7550832269541, 16.8050832269541, 16.8550832269541, 16.9050832269541, 16.8487417635395, 16.8987417635395, 16.9487417635395, 16.9987417635395, 16.8844560492537, 16.9344560492537, 16.9844560492537, 17.0344560492537, 16.5487417635395, 16.5987417635395, 16.6487417635395, 16.6987417635395 };
            public static double[] MacroecoDownturnProjectionOilExport = new double[] { 7045.39910718547, 8299.34350634626, 7444.756575241, 9239.53978932432, 9247.89, 9062.57622809992, 7008.51167718491, 6540.45366258253, 5263.32365746286, 4232.05825225928, 3263.85212011916, 3143.47385333424, 3293.33055328497, 3341.22792469095, 4028.08892235241, 4087.78353911408, 4152.66899211589, 4218.30025492232, 4713.1592867165, 4815.03183866537, 4890.37842483295, 4966.61668657648, 5043.76254667409, 5127.9735950304, 5918.80411767843, 5840.16266436662, 5702.95654725184, 5564.06691949761 };
            public static double[] MacroecoDownturnProjectionGdpGrowth = new double[] { -0.665936721292992, -1.48693680701277, -2.34082894664718, -1.7277383960996, -0.517454673429329, 0.55, 1.75467944020751, 0.319895304011775, -1.45815234222481, -2.91704379401002, -2.93187518866664, -1.36179889874142, -1.23736034088332, -0.222952159227985, 1.70900322273979, 1.27122211076263, 1.31983834666238, 1.15672648083638, 0.641331341289497, 0.284361406582079, 0.199458655657403, 0.0787867008832155, -0.417476772352177, -0.678649543472543, 0.104983714299456, -0.0342514073057187, -0.00746770905413197, -0.535653007444348 };

        }
    }
}
