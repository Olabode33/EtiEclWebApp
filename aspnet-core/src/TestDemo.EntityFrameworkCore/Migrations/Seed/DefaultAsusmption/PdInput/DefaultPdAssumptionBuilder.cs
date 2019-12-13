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
    }
}
