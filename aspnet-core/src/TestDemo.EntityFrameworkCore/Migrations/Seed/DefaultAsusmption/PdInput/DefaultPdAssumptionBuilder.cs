using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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


        private void Create12MonthPdsCreditPds(FrameworkEnum framework, long ou)
        {
            string prefix = DefaultPdAssumption.PdAssumptionKey.CreditPdKeyPrefix;
            int maxCredit = DefaultPdAssumption.PdAssumptionKey.MaxCreditPd;
            string[] values = new string[] {DefaultPdAssumption.InputValue.CreditPd1, DefaultPdAssumption.InputValue.CreditPd2, DefaultPdAssumption.InputValue.CreditPd3, DefaultPdAssumption.InputValue.CreditPd4,
                                            DefaultPdAssumption.InputValue.CreditPd5, DefaultPdAssumption.InputValue.CreditPd6, DefaultPdAssumption.InputValue.CreditPd7, DefaultPdAssumption.InputValue.CreditPd8,
                                            DefaultPdAssumption.InputValue.CreditPd9, DefaultPdAssumption.InputValue.CreditPd10};

            for (int i = 0; i <= maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstAsync(x => x.Key == prefix + i.ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditPD,
                        Key = prefix + i.ToString(),
                        InputName = i.ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
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

            for (int i = 0; i <= maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstAsync(x => x.Key == prefix + i.ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditEtiPolicy,
                        Key = prefix + i.ToString(),
                        InputName = i.ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.StringDropdown,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
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

            for (int i = 0; i <= maxCredit; i++)
            {
                var cpd = _context.PdInputAssumptions.IgnoreQueryFilters().FirstAsync(x => x.Key == prefix + i.ToString() && x.Framework == framework && x.OrganizationUnitId == ou);
                if (cpd == null)
                {
                    _context.PdInputAssumptions.Add(new PdInputAssumption()
                    {
                        PdGroup = PdInputAssumptionGroupEnum.CreditBestFit,
                        Key = prefix + i.ToString(),
                        InputName = i.ToString(),
                        Value = values[i],
                        DataType = DataTypeEnum.String,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
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
                    var snp = _context.PdInputSnPCummulativeDefaultRates.IgnoreQueryFilters().FirstAsync(x => x.Key == ratingKeys[rating]
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
                    }
                }
            }
        }
    }
}
