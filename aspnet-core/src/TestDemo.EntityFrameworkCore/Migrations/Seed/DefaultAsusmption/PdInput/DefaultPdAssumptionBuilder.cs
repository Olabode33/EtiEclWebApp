using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclShared;
using TestDemo.EclShared.DefaultAssumptions;
using TestDemo.EntityFrameworkCore;

namespace TestDemo.Migrations.Seed.DefaultAsusmption.PdInput
{
    public class DefaultPdAssumptionBuilder
    {
        private readonly TestDemoDbContext _context;

        public DefaultPdAssumptionBuilder(TestDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            long[] ous = _context.OrganizationUnits.IgnoreQueryFilters().Select(x => x.Id).ToArray();
            CreateMacroeconomicVariable();
            foreach (long ou in ous)
            {
                Create(ou);
            }
        }

        public void Create(long ou)
        {
            //Wholesale
            Create12MonthPdsCreditPds(FrameworkEnum.Wholesale, ou);
            Create12MonthPdsSnPEtiPolicy(FrameworkEnum.Wholesale, ou);
            Create12MonthPdsSnPBestFit(FrameworkEnum.Wholesale, ou);
            CreateSnPCummulativeDefaultRates(FrameworkEnum.Wholesale, ou);
            CreateNonInternalModelInput(FrameworkEnum.Wholesale, ou);
            CreateHistoricalIndex(FrameworkEnum.Wholesale, ou);
            CreateStatisticalIndexWeights(FrameworkEnum.Wholesale, ou);
            CreateStatisticalInputs(FrameworkEnum.Wholesale, ou);
            CreateMacroEconomicPrimeLendingProjection(FrameworkEnum.Wholesale, ou);
            CreateMacroEconomicOilExportProjection(FrameworkEnum.Wholesale, ou);
            CreateMacroEconomicGdpGrowthProjection(FrameworkEnum.Wholesale, ou);
            CreateMacroEconomicDifferencedGdpGrowthProjection(FrameworkEnum.Wholesale, ou);

            //Retail
            Create12MonthPdsCreditPds(FrameworkEnum.Retail, ou);
            Create12MonthPdsSnPEtiPolicy(FrameworkEnum.Retail, ou);
            Create12MonthPdsSnPBestFit(FrameworkEnum.Retail, ou);
            CreateSnPCummulativeDefaultRates(FrameworkEnum.Retail, ou);
            CreateNonInternalModelInput(FrameworkEnum.Retail, ou);
            CreateHistoricalIndex(FrameworkEnum.Retail, ou);
            CreateStatisticalIndexWeights(FrameworkEnum.Retail, ou);
            CreateStatisticalInputs(FrameworkEnum.Retail, ou);
            CreateMacroEconomicPrimeLendingProjection(FrameworkEnum.Retail, ou);
            CreateMacroEconomicOilExportProjection(FrameworkEnum.Retail, ou);
            CreateMacroEconomicGdpGrowthProjection(FrameworkEnum.Retail, ou);
            CreateMacroEconomicDifferencedGdpGrowthProjection(FrameworkEnum.Retail, ou);

            //OBE
            Create12MonthPdsCreditPds(FrameworkEnum.OBE, ou);
            Create12MonthPdsSnPEtiPolicy(FrameworkEnum.OBE, ou);
            Create12MonthPdsSnPBestFit(FrameworkEnum.OBE, ou);
            CreateSnPCummulativeDefaultRates(FrameworkEnum.OBE, ou);
            CreateNonInternalModelInput(FrameworkEnum.OBE, ou);
            CreateHistoricalIndex(FrameworkEnum.OBE, ou);
            CreateStatisticalIndexWeights(FrameworkEnum.OBE, ou);
            CreateStatisticalInputs(FrameworkEnum.OBE, ou);
            CreateMacroEconomicPrimeLendingProjection(FrameworkEnum.OBE, ou);
            CreateMacroEconomicOilExportProjection(FrameworkEnum.OBE, ou);
            CreateMacroEconomicGdpGrowthProjection(FrameworkEnum.OBE, ou);
            CreateMacroEconomicDifferencedGdpGrowthProjection(FrameworkEnum.OBE, ou);
        }

        private void Create12MonthPdsCreditPds(FrameworkEnum framework, long ou)
        {
            string prefix = DefaultPdAssumption.PdAssumptionKey.CreditPdKeyPrefix;
            int maxCredit = DefaultPdAssumption.PdAssumptionKey.MaxCreditPd;
            string[] values = new string[] {DefaultPdAssumption.InputValue.CreditPd1, DefaultPdAssumption.InputValue.CreditPd2, DefaultPdAssumption.InputValue.CreditPd3, DefaultPdAssumption.InputValue.CreditPd4,
                                            DefaultPdAssumption.InputValue.CreditPd5, DefaultPdAssumption.InputValue.CreditPd6, DefaultPdAssumption.InputValue.CreditPd7, DefaultPdAssumption.InputValue.CreditPd8,
                                            DefaultPdAssumption.InputValue.CreditPd9, DefaultPdAssumption.InputValue.CreditPd10};

            for (int i = 0; i < maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == prefix + (i + 1).ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditPD,
                        Key = prefix + (i + 1).ToString(),
                        InputName = (i + 1).ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void Create12MonthPdsSnPEtiPolicy(FrameworkEnum framework, long ou)
        {
            string prefix = DefaultPdAssumption.PdAssumptionKey.CreditPdEtiPolicyKeyPrefix;
            int maxCredit = DefaultPdAssumption.PdAssumptionKey.MaxCreditPd;
            string[] values = new string[] {DefaultPdAssumption.InputValue.CreditPd1_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd2_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd3_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd4_EtiPolicy,
                                            DefaultPdAssumption.InputValue.CreditPd5_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd6_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd7_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd8_EtiPolicy,
                                            DefaultPdAssumption.InputValue.CreditPd9_EtiPolicy, DefaultPdAssumption.InputValue.CreditPd10_EtiPolicy};

            for (int i = 0; i < maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == prefix + (i + 1).ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditEtiPolicy,
                        Key = prefix + (i + 1).ToString(),
                        InputName = (i + 1).ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.StringDropdown,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved,
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void Create12MonthPdsSnPBestFit(FrameworkEnum framework, long ou)
        {
            string prefix = DefaultPdAssumption.PdAssumptionKey.CreditPdBestFitKeyPrefix;
            int maxCredit = DefaultPdAssumption.PdAssumptionKey.MaxCreditPd;
            string[] values = new string[] {DefaultPdAssumption.InputValue.CreditPd1_BestFit, DefaultPdAssumption.InputValue.CreditPd2_BestFit, DefaultPdAssumption.InputValue.CreditPd3_BestFit, DefaultPdAssumption.InputValue.CreditPd4_BestFit,
                                            DefaultPdAssumption.InputValue.CreditPd5_BestFit, DefaultPdAssumption.InputValue.CreditPd6_BestFit, DefaultPdAssumption.InputValue.CreditPd7_BestFit, DefaultPdAssumption.InputValue.CreditPd8_BestFit,
                                            DefaultPdAssumption.InputValue.CreditPd9_BestFit, DefaultPdAssumption.InputValue.CreditPd10_BestFit};

            for (int i = 0; i < maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == prefix + (i + 1).ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditBestFit,
                        Key = prefix + (i + 1).ToString(),
                        InputName = (i + 1).ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.String,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateSnPCummulativeDefaultRates(FrameworkEnum framework, long ou)
        {
            const int maxRating = DefaultPdAssumption.PdAssumptionKey.MaxSnPCummulativeRatesRating;
            const int maxYears = 15;
            string[] ratingKeys = new string[] { DefaultPdAssumption.PdAssumptionKey.SnPMappingAAA, DefaultPdAssumption.PdAssumptionKey.SnPMappingAA, DefaultPdAssumption.PdAssumptionKey.SnPMappingA,
                                                 DefaultPdAssumption.PdAssumptionKey.SnPMappingBBB, DefaultPdAssumption.PdAssumptionKey.SnPMappingBB, DefaultPdAssumption.PdAssumptionKey.SnPMappingB,
                                                 DefaultPdAssumption.PdAssumptionKey.SnPMappingCCC};

            string[] ratingInput = new string[] { DefaultPdAssumption.InputName.RatingsAAA, DefaultPdAssumption.InputName.RatingsAA, DefaultPdAssumption.InputName.RatingsA,
                                                  DefaultPdAssumption.InputName.RatingsBBB, DefaultPdAssumption.InputName.RatingsBB, DefaultPdAssumption.InputName.RatingsB,
                                                  DefaultPdAssumption.InputName.RatingsCCC };

            double[,] values = new double[maxRating, maxYears] {
                                                    { 0.0, 0.0004, 0.0017, 0.0029, 0.0042, 0.0054, 0.0059, 0.0067, 0.0076, 0.0085, 0.009, 0.0094, 0.0099, 0.0109, 0.012 },
                                                    { 0.0003, 0.0008, 0.0018, 0.0031, 0.0045, 0.006, 0.0074, 0.0086, 0.0096, 0.0107, 0.0117, 0.0125, 0.0134, 0.0142, 0.0151 },
                                                    { 0.0007, 0.002, 0.0036, 0.0054, 0.0073, 0.0095, 0.0119, 0.0141, 0.0165, 0.0189, 0.0211, 0.0232, 0.0252, 0.0269, 0.0289 },
                                                    { 0.0022, 0.0058, 0.0099, 0.015, 0.0205, 0.026, 0.0309, 0.0358, 0.0407, 0.0455, 0.0502, 0.0537, 0.0571, 0.0606, 0.0642 },
                                                    { 0.008, 0.0252, 0.0457, 0.0657, 0.0838, 0.1014, 0.1162, 0.1298, 0.1417, 0.1525, 0.1613, 0.1691, 0.1761, 0.1822, 0.1884 },
                                                    { 0.0392, 0.09, 0.1343, 0.1688, 0.1957, 0.2176, 0.2356, 0.2498, 0.2624, 0.2742, 0.2842, 0.292, 0.299, 0.3053, 0.3116 },
                                                    { 0.2885, 0.3923, 0.4494, 0.4855, 0.5131, 0.5253, 0.5395, 0.55, 0.5596, 0.5666, 0.5732, 0.5793, 0.586, 0.5914, 0.5914 }
                                                };


            for (int rating = 0; rating < maxRating; rating++)
            {
                for (int year = 0; year < maxYears; year++)
                {
                    var snp = _context.PdInputSnPCummulativeDefaultRates.IgnoreQueryFilters().FirstOrDefault(x => x.Key == ratingKeys[rating]
                                                                                                           && x.Years == year + 1 && x.Framework == framework && x.OrganizationUnitId == ou);
                    if (snp == null)
                    {
                        _context.PdInputSnPCummulativeDefaultRates.Add(new PdInputAssumptionSnPCummulativeDefaultRate()
                        {
                            Framework = framework,
                            Key = ratingKeys[rating],
                            OrganizationUnitId = ou,
                            Rating = ratingInput[rating],
                            Value = values[rating, year],
                            RequiresGroupApproval = true,
                            Status = GeneralStatusEnum.Approved,
                            Years = year + 1
                        });
                        _context.SaveChanges();
                    }
                }
            }
        }

        private void CreateNonInternalModelInput(FrameworkEnum framework, long ou)
        {
            const int maxPdGroup = DefaultPdAssumption.PdAssumptionKey.MaxNonnInteralPdGroup;
            const int maxProjectionMonths = DefaultPdAssumption.PdAssumptionKey.MaxNonInternalProjectionMonths;

            string[] defaultKeys = new string[] { DefaultPdAssumption.PdAssumptionKey.NonInternalModelConsStage1, DefaultPdAssumption.PdAssumptionKey.NonInternalModelConsStage2,
                                                  DefaultPdAssumption.PdAssumptionKey.NonInternalModelCommStage1, DefaultPdAssumption.PdAssumptionKey.NonInternalModelCommStage2 };
            string[] pdGroups = new string[] { DefaultPdAssumption.InputName.PdGroupConsStage1, DefaultPdAssumption.InputName.PdGroupConsStage2, DefaultPdAssumption.InputName.PdGroupCommStage1, DefaultPdAssumption.InputName.PdGroupCommStage2 };


            double[,] defaultValues = DefaultPdAssumption.InputValue.NonInternalMarginalDefaultValues;
            double[,] cummValues = DefaultPdAssumption.InputValue.NonInternalCummulativeValues;


            for (int pdGroup = 0; pdGroup < maxPdGroup; pdGroup++)
            {
                for (int month = 0; month < maxProjectionMonths; month++)
                {
                    var non = _context.PdInputAssumptionNonInternalModels.IgnoreQueryFilters().FirstOrDefault(x => x.Key == defaultKeys[pdGroup]
                                                                                                           && x.Month == month + 1 && x.Framework == framework && x.OrganizationUnitId == ou);
                    if (non == null)
                    {
                        _context.PdInputAssumptionNonInternalModels.Add(new PdInputAssumptionNonInternalModel()
                        {
                            
                            Framework = framework,
                            Key = defaultKeys[pdGroup],
                            OrganizationUnitId = ou,
                            PdGroup = pdGroups[pdGroup],
                            Month = month + 1,
                            MarginalDefaultRate = defaultValues[month, pdGroup],
                            CummulativeSurvival = cummValues[month, pdGroup],
                            RequiresGroupApproval = true,
                            CanAffiliateEdit = false,
                            IsComputed = true
                        });
                        _context.SaveChanges();
                    }
                }
            }
            
        }

        private void CreateHistoricalIndex(FrameworkEnum framework, long ou)
        {
            string keyPrefix = DefaultPdAssumption.PdAssumptionKey.HistoricalIndexQuarterPrefix;
            string[] quarters = DefaultPdAssumption.InputValue.HistoricalIndexDate;
            double[] actuals = DefaultPdAssumption.InputValue.HistoricalIndexActual;
            double[] standardised = DefaultPdAssumption.InputValue.HistoricalIndexStandardised;
            double[] series = DefaultPdAssumption.InputValue.HistoricalIndexNplSeries;

            for (int i = 0; i < quarters.Length; i++)
            {
                var hi = _context.PdInputAssumptionNplIndexes.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keyPrefix + i.ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if(hi == null)
                {
                    _context.PdInputAssumptionNplIndexes.Add(new PdInputAssumptionNplIndex()
                    {
                        Framework = framework,
                        Key = keyPrefix + i.ToString(),
                        OrganizationUnitId = ou,
                        Date = DateTime.Parse(quarters[i]),
                        Actual = actuals[i],
                        Standardised = standardised[i],
                        EtiNplSeries = series[i],
                        CanAffiliateEdit = false,
                        RequiresGroupApproval = true,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = true
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateStatisticalIndexWeights(FrameworkEnum framework, long ou)
        {
            string[] keys = new string[] { DefaultPdAssumption.PdAssumptionKey.StatisticsIndexWeightsW1, DefaultPdAssumption.PdAssumptionKey.StatisticsIndexWeightsW2,
                                           DefaultPdAssumption.PdAssumptionKey.StatisticsIndexWeightsStandardDeviation, DefaultPdAssumption.PdAssumptionKey.StatisticsIndexWeightsAverage };
            string[] names = new string[] { DefaultPdAssumption.InputName.StatisticalIndexWeightW1, DefaultPdAssumption.InputName.StatisticalIndexWeightW2,
                                            DefaultPdAssumption.InputName.StatisticalIndexWeightStandardDeviation, DefaultPdAssumption.InputName.StatisticalIndexWeightAverage };
            double[] values = new double[] { DefaultPdAssumption.InputValue.StatisticalIndexW1, DefaultPdAssumption.InputValue.StatisticalIndexW2,
                                             DefaultPdAssumption.InputValue.StatisticalIndexStandardDeviatio, DefaultPdAssumption.InputValue.StatisticalIndexAvearage };

            for (int i = 0; i < keys.Length; i++)
            {
                var w = _context.PdInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (w == null)
                {
                    _context.Add(new PdInputAssumption()
                    {
                        Framework = framework,
                        Key = keys[i],
                        OrganizationUnitId = ou,
                        PdGroup = PdInputAssumptionGroupEnum.StatisticsIndexWeights,
                        InputName = names[i],
                        Value = values[i].ToString(),
                        DataType = DataTypeEnum.Double,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = true
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateStatisticalInputs(FrameworkEnum framework, long ou)
        {
            string[] names = new string[] { DefaultPdAssumption.InputName.StatisticalInputsMean, DefaultPdAssumption.InputName.StatisticalInputsStandardDeviation, DefaultPdAssumption.InputName.StatisticalInputsEigenvalues,
                                            DefaultPdAssumption.InputName.StatisticalInputsPrincipalComponentScore1, DefaultPdAssumption.InputName.StatisticalInputsPrincipalComponentScore2 };

            //Prime
            string[] primeLendingkeys = new string[] { DefaultPdAssumption.PdAssumptionKey.StatisticalInputsPrimeLendingMean, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsPrimeLendingStandardDeviation,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsPrimeLendingEigenvalues, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsPrimeLendingPrincipalComponentScore1,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsPrimeLendingPrincipalComponentScore2 };
            double[] primeLendingValues = DefaultPdAssumption.InputValue.StatisticalInputPrimeLending;
            
            //Oil Exports
            string[] oilexportkeys = new string[] { DefaultPdAssumption.PdAssumptionKey.StatisticalInputsOilExportsMean, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsOilExportsStandardDeviation,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsOilExportsEigenvalues, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsOilExportsPrincipalComponentScore1,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsOilExportsPrincipalComponentScore2 };
            double[] oilexportValues = DefaultPdAssumption.InputValue.StatisticalInputOilExports;


            //GDP Growth
            string[] gdpkeys = new string[] { DefaultPdAssumption.PdAssumptionKey.StatisticalInputsRealGdpGrowthRateMean, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsRealGdpGrowthRateStandardDeviation,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsRealGdpGrowthRateEigenvalues, DefaultPdAssumption.PdAssumptionKey.StatisticalInputsRealGdpGrowthRatePrincipalComponentScore1,
                                                       DefaultPdAssumption.PdAssumptionKey.StatisticalInputsRealGdpGrowthRatePrincipalComponentScore2 };
            double[] gdpValues = DefaultPdAssumption.InputValue.StatisticalInputGdpGrowth;


            //Prime
            for (int p = 0; p < names.Length; p++)
            {
                var prime = _context.PdInputAssumptionStatisticals.IgnoreQueryFilters().FirstOrDefault(x => x.Key == primeLendingkeys[p] && x.Framework == framework && x.OrganizationUnitId == ou);
                if(prime == null)
                {
                    _context.Add(new PdInputAssumptionMacroeconomicInput()
                    {
                        CanAffiliateEdit = false,
                        Framework = framework,
                        InputName = names[p],
                        Key = primeLendingkeys[p],
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsPrimeLending,
                        RequiresGroupApproval = true,
                        IsComputed = true,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved,
                        Value = primeLendingValues[p]
                    });
                    _context.SaveChanges();
                }
            }
            //Oil Exports
            for (int p = 0; p < names.Length; p++)
            {
                var prime = _context.PdInputAssumptionStatisticals.IgnoreQueryFilters().FirstOrDefault(x => x.Key == oilexportkeys[p] && x.Framework == framework && x.OrganizationUnitId == ou);
                if(prime == null)
                {
                    _context.Add(new PdInputAssumptionMacroeconomicInput()
                    {
                        CanAffiliateEdit = false,
                        Framework = framework,
                        InputName = names[p],
                        Key = oilexportkeys[p],
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsOilExports,
                        RequiresGroupApproval = true,
                        IsComputed = true,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved,
                        Value = oilexportValues[p]
                    });
                    _context.SaveChanges();
                }
            }
            //Gdp growth rate
            for (int p = 0; p < names.Length; p++)
            {
                var prime = _context.PdInputAssumptionStatisticals.IgnoreQueryFilters().FirstOrDefault(x => x.Key == gdpkeys[p] && x.Framework == framework && x.OrganizationUnitId == ou);
                if(prime == null)
                {
                    _context.PdInputAssumptionStatisticals.Add(new PdInputAssumptionMacroeconomicInput()
                    {
                        CanAffiliateEdit = false,
                        Framework = framework,
                        InputName = names[p],
                        Key = gdpkeys[p],
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsRealGdpGrowthRate,
                        RequiresGroupApproval = true,
                        IsComputed = true,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved,
                        Value = gdpValues[p]
                    });
                    _context.SaveChanges();
                }
            }

        }

        private void CreateMacroEconomicPrimeLendingProjection(FrameworkEnum framework, long ou)
        {
            string[] quarters = DefaultPdAssumption.InputValue.MacroeconomicProjectionDate;
            double[] best = DefaultPdAssumption.InputValue.MacroecoBestProjectionPrimeLending;
            double[] optimistic = DefaultPdAssumption.InputValue.MacroecoOptimisticProjectionPrimeLending;
            double[] downturn = DefaultPdAssumption.InputValue.MacroecoDownturnProjectionPrimeLending;
            
            //Prime
            for (int q = 0; q < quarters.Length; q++)
            {
                var c = _context.PdInputAssumptionMacroeconomicProjections.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionInputsPrimeLendingPrefix + q.ToString()
                                                                                                                 && x.Framework == framework && x.OrganizationUnitId == ou);
                if(c == null)
                {
                    _context.PdInputAssumptionMacroeconomicProjections.Add(new PdInputAssumptionMacroeconomicProjection()
                    {
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsPrimeLending,
                        Key = DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionInputsPrimeLendingPrefix + q.ToString(),
                        Date = DateTime.Parse(quarters[q]),
                        InputName = DefaultPdAssumption.InputName.MacroeconomicProjectionPrimeLending,
                        BestValue = best[q],
                        OptimisticValue = optimistic[q],
                        DownturnValue = downturn[q],
                        Framework = framework,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        RequiresGroupApproval = true,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = false
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateMacroEconomicOilExportProjection(FrameworkEnum framework, long ou)
        {
            string[] quarters = DefaultPdAssumption.InputValue.MacroeconomicProjectionDate;
            double[] best = DefaultPdAssumption.InputValue.MacroecoBestProjectionOilExport;
            double[] optimistic = DefaultPdAssumption.InputValue.MacroecoOptimisticProjectionOilExport;
            double[] downturn = DefaultPdAssumption.InputValue.MacroecoDownturnProjectionOilExport;

            //Oil Exports
            for (int q = 0; q < quarters.Length; q++)
            {
                var c = _context.PdInputAssumptionMacroeconomicProjections.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionOilExportsPrefix + q.ToString()
                                                                                                                 && x.Framework == framework && x.OrganizationUnitId == ou);
                if (c == null)
                {
                    _context.PdInputAssumptionMacroeconomicProjections.Add(new PdInputAssumptionMacroeconomicProjection()
                    {
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsOilExports,
                        Key = DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionOilExportsPrefix + q.ToString(),
                        Date = DateTime.Parse(quarters[q]),
                        InputName = DefaultPdAssumption.InputName.MacroeconomicProjectionOilExport,
                        BestValue = best[q],
                        OptimisticValue = optimistic[q],
                        DownturnValue = downturn[q],
                        Framework = framework,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        RequiresGroupApproval = true,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = false
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateMacroEconomicGdpGrowthProjection(FrameworkEnum framework, long ou)
        {
            string[] quarters = DefaultPdAssumption.InputValue.MacroeconomicProjectionDate;
            double[] best = DefaultPdAssumption.InputValue.MacroecoBestProjectionGdpGrowth;
            double[] optimistic = DefaultPdAssumption.InputValue.MacroecoOptimisticProjectionGdpGrowth;
            double[] downturn = DefaultPdAssumption.InputValue.MacroecoDownturnProjectionGdpGrowth;

            //GDP Growth
            for (int q = 0; q < quarters.Length; q++)
            {
                var c = _context.PdInputAssumptionMacroeconomicProjections.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionRealGdpGrowthRatePrefix + q.ToString()
                                                                                                                 && x.Framework == framework && x.OrganizationUnitId == ou);
                if (c == null)
                {
                    _context.PdInputAssumptionMacroeconomicProjections.Add(new PdInputAssumptionMacroeconomicProjection()
                    {
                        MacroeconomicVariableId = (int)PdInputAssumptionMacroEconomicInputGroupEnum.StatisticalInputsRealGdpGrowthRate,
                        Key = DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionRealGdpGrowthRatePrefix + q.ToString(),
                        Date = DateTime.Parse(quarters[q]),
                        InputName = DefaultPdAssumption.InputName.MacroeconomicProjectionGdpGrowth,
                        BestValue = best[q],
                        OptimisticValue = optimistic[q],
                        DownturnValue = downturn[q],
                        Framework = framework,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        RequiresGroupApproval = true,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = false
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateMacroEconomicDifferencedGdpGrowthProjection(FrameworkEnum framework, long ou)
        {
            string[] quarters = DefaultPdAssumption.InputValue.MacroeconomicProjectionDate;
            double[] best = DefaultPdAssumption.InputValue.MacroecoBestProjectionGdpGrowth;
            double[] optimistic = DefaultPdAssumption.InputValue.MacroecoOptimisticProjectionGdpGrowth;
            double[] downturn = DefaultPdAssumption.InputValue.MacroecoDownturnProjectionGdpGrowth;

            //GDP Growth
            for (int q = 0; q < quarters.Length; q++)
            {
                var c = _context.PdInputAssumptionMacroeconomicProjections.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionDifferencedRealGdpGrowthRatePrefix + q.ToString()
                                                                                                                 && x.Framework == framework && x.OrganizationUnitId == ou);
                if (c == null)
                {
                    _context.PdInputAssumptionMacroeconomicProjections.Add(new PdInputAssumptionMacroeconomicProjection()
                    {
                        MacroeconomicVariableId = 4,
                        Key = DefaultPdAssumption.PdAssumptionKey.MacroeconomicProjectionDifferencedRealGdpGrowthRatePrefix + q.ToString(),
                        Date = DateTime.Parse(quarters[q]),
                        InputName = DefaultPdAssumption.InputName.MacroeconomicProjectionDifferencedGdpGrowth,
                        BestValue = q == 0 ? 0 : best[q] - best[q - 1],
                        OptimisticValue = q == 0 ? 0 : optimistic[q] - optimistic[q - 1],
                        DownturnValue = q == 0 ? 0 : downturn[q] - downturn[q - 1],
                        Framework = framework,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        RequiresGroupApproval = true,
                        Status = GeneralStatusEnum.Approved,
                        IsComputed = false
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateMacroeconomicVariable()
        {
            string[] macros = new string[] { "Prime Lending Rate (%)", "Oil Exports (USD'm)", "Real GDP Growth Rate (%)", "Differenced Real GDP Growth Rate (%)" };

            for (int i = 0; i < macros.Length; i++)
            {
                var c = _context.MacroeconomicVariables.IgnoreQueryFilters().FirstOrDefault(x => x.Name == macros[i]);
                if (c == null)
                {
                    _context.MacroeconomicVariables.Add(new MacroeconomicVariable()
                    {
                        Name = macros[i],
                        Description = macros[i]
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
