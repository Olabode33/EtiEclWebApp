using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclShared;
using TestDemo.EclShared.DefaultAssumptions;
using TestDemo.EntityFrameworkCore;

namespace TestDemo.Migrations.Seed.DefaultAsusmption.LgdInput
{
    public class DefaultLgdAssumptionBuilder
    {
        private readonly TestDemoDbContext _context;

        public DefaultLgdAssumptionBuilder(TestDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            long[] ous = _context.OrganizationUnits.IgnoreQueryFilters().Select(x => x.Id).ToArray();

            foreach (long ou in ous)
            {
                Create(ou);
            }
        }

        public void Create(long ou)
        {
            //Retail
            CreateRetailUnsecuredRecoveryCureRate(ou);
            CreateRetailUnsecuredRecoveryTimeInDefault(ou);
            CreateCostOfRecovery(FrameworkEnum.Retail, ou);
            CreateCollateralGrowth(FrameworkEnum.Retail, ou);
            CreateTTR(FrameworkEnum.Retail, ou);
            CreateHaircut(FrameworkEnum.Retail, ou);
            CreateCollateralProjection(FrameworkEnum.Retail, ou);

            //Wholesale
            CreateWholesaleUnsecuredRecoveryCureRate(ou);
            CreateWholesaleUnsecuredRecoveryTimeInDefault(ou);
            CreateCostOfRecovery(FrameworkEnum.Wholesale, ou);
            CreateCollateralGrowth(FrameworkEnum.Wholesale, ou);
            CreateTTR(FrameworkEnum.Wholesale, ou);
            CreateHaircut(FrameworkEnum.Wholesale, ou);
            CreateCollateralProjection(FrameworkEnum.Wholesale, ou);

            //OBE
            CreateObeUnsecuredRecoveryCureRate(ou);
            CreateObeUnsecuredRecoveryTimeInDefault(ou);
            CreateCostOfRecovery(FrameworkEnum.OBE, ou);
            CreateCollateralGrowth(FrameworkEnum.OBE, ou);
            CreateTTR(FrameworkEnum.OBE, ou);
            CreateHaircut(FrameworkEnum.OBE, ou);
            CreateCollateralProjection(FrameworkEnum.OBE, ou);
            CreatePdAssumption(FrameworkEnum.OBE, ou);
        }

        private void CreateRetailUnsecuredRecoveryCureRate(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.Retail;

            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdCureRate };
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CustomerCard, DefaultLgdAssumptions.InputName.CustomerLease, DefaultLgdAssumptions.InputName.CustomerLoan,
                                             DefaultLgdAssumptions.InputName.CustomerMortgage, DefaultLgdAssumptions.InputName.CustomerOd};

            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.CureRate,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateWholesaleUnsecuredRecoveryCureRate(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.Wholesale;

            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdCureRate };
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CommercialCard, DefaultLgdAssumptions.InputName.CommercialLease, DefaultLgdAssumptions.InputName.CommercialLoan,
                                             DefaultLgdAssumptions.InputName.CommercialMortgage, DefaultLgdAssumptions.InputName.CommercialOd};

            string[] copkeys = new string[] {  DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdCureRate };
            string[] copName = new string[] {DefaultLgdAssumptions.InputName.CorporateLease, DefaultLgdAssumptions.InputName.CorporateLoan,
                                             DefaultLgdAssumptions.InputName.CorporateMortgage, DefaultLgdAssumptions.InputName.CorporateOd};

            //Commercial
            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                        DataType = DataTypeEnum.DoublePercentage,
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

            //Corporate
            for (int i = 0; i < copkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == copkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                        Key = copkeys[i],
                        InputName = copName[i],
                        Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateObeUnsecuredRecoveryCureRate(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.OBE;

            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondCureRate};
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CommercialAdvancePaymentGuarantee, DefaultLgdAssumptions.InputName.CommercialBidBond, DefaultLgdAssumptions.InputName.CommercialCustomsBond,
                                          DefaultLgdAssumptions.InputName.CommercialLetterOfCredit, DefaultLgdAssumptions.InputName.CommercialOtherBondGuarantee, DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                                          DefaultLgdAssumptions.InputName.CommercialTenderBond };

            string[] copkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeCureRate, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondCureRate,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondCureRate};
            string[] copName = new string[] {DefaultLgdAssumptions.InputName.CorporateAdvancePaymentGuarantee, DefaultLgdAssumptions.InputName.CorporateBidBond, DefaultLgdAssumptions.InputName.CorporateCustomsBond,
                                              DefaultLgdAssumptions.InputName.CorporateLetterOfCredit, DefaultLgdAssumptions.InputName.CorporateOtherBondGuarantee, DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                                              DefaultLgdAssumptions.InputName.CorporateTenderBond };

            //Commercial
            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                        DataType = DataTypeEnum.DoublePercentage,
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


            //Customer
            var customerObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeCureRate
                                                                                           && x.Framework == framework && x.OrganizationUnitId == ou);
            if (customerObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                    CanAffiliateEdit = false,
                    OrganizationUnitId = ou,
                    Status = GeneralStatusEnum.Approved
                });
                _context.SaveChanges();
            }

            //Corporate
            for (int i = 0; i < copkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == copkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate,
                        Key = copkeys[i],
                        InputName = copName[i],
                        Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateRetailUnsecuredRecoveryTimeInDefault(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.Retail;
            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardBestTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanBestTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdBestTimeInDefault };
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CustomerCard, DefaultLgdAssumptions.InputName.CustomerLease, DefaultLgdAssumptions.InputName.CustomerLoan,
                                             DefaultLgdAssumptions.InputName.CustomerMortgage, DefaultLgdAssumptions.InputName.CustomerOd};

            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateWholesaleUnsecuredRecoveryTimeInDefault(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.Wholesale;

            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardBestTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanBestTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdBestTimeInDefault };
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CommercialCard, DefaultLgdAssumptions.InputName.CommercialLease, DefaultLgdAssumptions.InputName.CommercialLoan,
                                             DefaultLgdAssumptions.InputName.CommercialMortgage, DefaultLgdAssumptions.InputName.CommercialOd};

            string[] copkeys = new string[] {  DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanBestTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdBestTimeInDefault };
            string[] copName = new string[] {DefaultLgdAssumptions.InputName.CorporateLease, DefaultLgdAssumptions.InputName.CorporateLoan,
                                             DefaultLgdAssumptions.InputName.CorporateMortgage, DefaultLgdAssumptions.InputName.CorporateOd};

            //Commercial
            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                        DataType = DataTypeEnum.DoublePercentage,
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

            //Corporate
            for (int i = 0; i < copkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == copkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                        Key = copkeys[i],
                        InputName = copName[i],
                        Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateObeUnsecuredRecoveryTimeInDefault(long ou)
        {
            FrameworkEnum framework = FrameworkEnum.OBE;

            string[] commkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondTimeInDefault};
            string[] cusName = new string[] {DefaultLgdAssumptions.InputName.CommercialAdvancePaymentGuarantee, DefaultLgdAssumptions.InputName.CommercialBidBond, DefaultLgdAssumptions.InputName.CommercialCustomsBond,
                                          DefaultLgdAssumptions.InputName.CommercialLetterOfCredit, DefaultLgdAssumptions.InputName.CommercialOtherBondGuarantee, DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                                          DefaultLgdAssumptions.InputName.CommercialTenderBond };

            string[] copkeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditBestTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeTimeInDefault, DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondTimeInDefault,
                                               DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondTimeInDefault};
            string[] copName = new string[] {DefaultLgdAssumptions.InputName.CorporateAdvancePaymentGuarantee, DefaultLgdAssumptions.InputName.CorporateBidBond, DefaultLgdAssumptions.InputName.CorporateCustomsBond,
                                              DefaultLgdAssumptions.InputName.CorporateLetterOfCredit, DefaultLgdAssumptions.InputName.CorporateOtherBondGuarantee, DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                                              DefaultLgdAssumptions.InputName.CorporateTenderBond };

            //Commercial
            for (int i = 0; i < commkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == commkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                        Key = commkeys[i],
                        InputName = cusName[i],
                        Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                        DataType = DataTypeEnum.DoublePercentage,
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

            
            //Customer
            var customerObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeTimeInDefault
                                                                                           && x.Framework == framework && x.OrganizationUnitId == ou);
            if (customerObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                    CanAffiliateEdit = false,
                    OrganizationUnitId = ou,
                    Status = GeneralStatusEnum.Approved
                });
                _context.SaveChanges();
            }

            //Corporate
            for (int i = 0; i < copkeys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == copkeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault,
                        Key = copkeys[i],
                        InputName = copName[i],
                        Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateCostOfRecovery(FrameworkEnum framework, long ou)
        {
            string[] corHighKeys = new string[] {DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighCollateralValue, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighDebenture,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighCash, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighInventory,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighPlantEquipment, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighResidentialProperty,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighCommercialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighReceivables,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighShares, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryHighVehicle};
            string[] corLowKeys = new string[] {DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowCollateralValue, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowDebenture,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowCash, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowInventory,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowPlantEquipment, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowResidentialProperty,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowCommercialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowReceivables,
                                          DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowShares, DefaultLgdAssumptions.LgdAssumptionKey.CostOfRecoveryLowVehicle};

            string[] corName = new string[] {DefaultLgdAssumptions.InputName.Debenture, DefaultLgdAssumptions.InputName.Cash, DefaultLgdAssumptions.InputName.Inventory,
                                              DefaultLgdAssumptions.InputName.PlantEquipment, DefaultLgdAssumptions.InputName.ResidentialProperty, DefaultLgdAssumptions.InputName.CommercialProperty,
                                              DefaultLgdAssumptions.InputName.Receivables, DefaultLgdAssumptions.InputName.Shares, DefaultLgdAssumptions.InputName.Vehicle};

            for (int i = 0; i < corHighKeys.Length; i++)
            {
                var corHigh = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == corHighKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (corHigh == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CostOfRecoveryHigh,
                        Key = corHighKeys[i],
                        InputName = i == 0 ? DefaultLgdAssumptions.InputName.CostOfRecoveryHighCollateralValue : corName[i - 1],
                        Value = i == 0 ? DefaultLgdAssumptions.InputValue.CollateralValue : DefaultLgdAssumptions.InputValue.CostOfRecoveryHigh,
                        DataType = i == 0 ? DataTypeEnum.DoubleMoney : DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

            for (int i = 0; i < corLowKeys.Length; i++)
            {
                var corLow = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == corLowKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (corLow == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CostOfRecoveryLow,
                        Key = corLowKeys[i],
                        InputName = i == 0 ? DefaultLgdAssumptions.InputName.CostOfRecoveryHighCollateralValue : corName[i - 1],
                        Value = i == 0 ? DefaultLgdAssumptions.InputValue.CollateralValue : DefaultLgdAssumptions.InputValue.CostOfRecoveryLow,
                        DataType = i == 0 ? DataTypeEnum.DoubleMoney : DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateCollateralGrowth(FrameworkEnum framework, long ou)
        {
            string[] name = new string[] {DefaultLgdAssumptions.InputName.Debenture, DefaultLgdAssumptions.InputName.Cash, DefaultLgdAssumptions.InputName.Inventory,
                                          DefaultLgdAssumptions.InputName.PlantEquipment, DefaultLgdAssumptions.InputName.ResidentialProperty, DefaultLgdAssumptions.InputName.CommercialProperty,
                                          DefaultLgdAssumptions.InputName.Receivables, DefaultLgdAssumptions.InputName.Shares, DefaultLgdAssumptions.InputName.Vehicle};

            string[] bestValue = new string[] {DefaultLgdAssumptions.InputValue.CollateralGrowthBestDebenture, DefaultLgdAssumptions.InputValue.CollateralGrowthBestCash,
                                              DefaultLgdAssumptions.InputValue.CollateralGrowthBestInventory, DefaultLgdAssumptions.InputValue.CollateralGrowthBestPlantEquipment,
                                              DefaultLgdAssumptions.InputValue.CollateralGrowthBestResidentialProperty, DefaultLgdAssumptions.InputValue.CollateralGrowthBestCommercialProperty,
                                              DefaultLgdAssumptions.InputValue.CollateralGrowthBestReceivables, DefaultLgdAssumptions.InputValue.CollateralGrowthBestShares,
                                              DefaultLgdAssumptions.InputValue.CollateralGrowthBestVehicle};

            string[] bestKeys = new string[] {DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestCash,
                                              DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestPlantEquipment,
                                              DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestCommercialProperty,
                                              DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestShares,
                                              DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthBestVehicle};

            string[] optimisticKeys = new string[] {DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticCash,
                                                    DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticPlantEquipment,
                                                    DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticCommercialProperty,
                                                    DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticShares,
                                                    DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthOptimisticVehicle};

            string[] downturnKeys = new string[] {DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnCash,
                                                  DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnPlantEquipment,
                                                  DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnCommercialProperty,
                                                  DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnShares,
                                                  DefaultLgdAssumptions.LgdAssumptionKey.CollateralGrowthDownturnVehicle};

            //Best
            for (int i = 0; i < bestKeys.Length; i++)
            {
                var best = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == bestKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (best == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralGrowthBest,
                        Key = bestKeys[i],
                        InputName = name[i],
                        Value = bestValue[i],
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

            //Optimistic
            for (int i = 0; i < optimisticKeys.Length; i++)
            {
                var op = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == optimisticKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (op == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralGrowthOptimistic,
                        Key = optimisticKeys[i],
                        InputName = name[i],
                        Value = bestValue[i],
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

            //Downturn
            for (int i = 0; i < downturnKeys.Length; i++)
            {
                var downturn = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == downturnKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (downturn == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralGrowthDownturn,
                        Key = downturnKeys[i],
                        InputName = name[i],
                        Value = (Convert.ToDouble(bestValue[i]) * 0.92 - 0.08).ToString(),
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

        }

        private void CreateTTR(FrameworkEnum framework, long ou)
        {
            string[] keys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsCash,
                                           DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsPlantEquipment,
                                           DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsCommercialProperty,
                                           DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsShares,
                                           DefaultLgdAssumptions.LgdAssumptionKey.CollateralTypeTtrYearsVehicle};
            string[] name = new string[] {DefaultLgdAssumptions.InputName.Debenture, DefaultLgdAssumptions.InputName.Cash, DefaultLgdAssumptions.InputName.Inventory,
                                          DefaultLgdAssumptions.InputName.PlantEquipment, DefaultLgdAssumptions.InputName.ResidentialProperty, DefaultLgdAssumptions.InputName.CommercialProperty,
                                          DefaultLgdAssumptions.InputName.Receivables, DefaultLgdAssumptions.InputName.Shares, DefaultLgdAssumptions.InputName.Vehicle};
            string[] value = new string[] {DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsDebenture, DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsCash,
                                           DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsInventory, DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsPlantEquipment,
                                           DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsResidentialProperty, DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsCommercialProperty,
                                           DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsReceivables, DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsShares,
                                           DefaultLgdAssumptions.InputValue.CollateralTypeTtrYearsVehicle};

            for (int i = 0; i < keys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralTTR,
                        Key = keys[i],
                        InputName = name[i],
                        Value = value[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateHaircut(FrameworkEnum framework, long ou)
        {
            string[] keys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.HaircutDebenture, DefaultLgdAssumptions.LgdAssumptionKey.HaircutCash,
                                           DefaultLgdAssumptions.LgdAssumptionKey.HaircutInventory, DefaultLgdAssumptions.LgdAssumptionKey.HaircutPlantEquipment,
                                           DefaultLgdAssumptions.LgdAssumptionKey.HaircutResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.HaircutCommercialProperty,
                                           DefaultLgdAssumptions.LgdAssumptionKey.HaircutReceivables, DefaultLgdAssumptions.LgdAssumptionKey.HaircutShares,
                                           DefaultLgdAssumptions.LgdAssumptionKey.HaircutVehicle};
            string[] name = new string[] {DefaultLgdAssumptions.InputName.Debenture, DefaultLgdAssumptions.InputName.Cash, DefaultLgdAssumptions.InputName.Inventory,
                                          DefaultLgdAssumptions.InputName.PlantEquipment, DefaultLgdAssumptions.InputName.ResidentialProperty, DefaultLgdAssumptions.InputName.CommercialProperty,
                                          DefaultLgdAssumptions.InputName.Receivables, DefaultLgdAssumptions.InputName.Shares, DefaultLgdAssumptions.InputName.Vehicle};
            string[] value = new string[] {DefaultLgdAssumptions.InputValue.HaircutDebenture, DefaultLgdAssumptions.InputValue.HaircutCash,
                                           DefaultLgdAssumptions.InputValue.HaircutInventory, DefaultLgdAssumptions.InputValue.HaircutPlantEquipment,
                                           DefaultLgdAssumptions.InputValue.HaircutResidentialProperty, DefaultLgdAssumptions.InputValue.HaircutCommercialProperty,
                                           DefaultLgdAssumptions.InputValue.HaircutReceivables, DefaultLgdAssumptions.InputValue.HaircutShares,
                                           DefaultLgdAssumptions.InputValue.HaircutVehicle};

            for (int i = 0; i < keys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.Haircut,
                        Key = keys[i],
                        InputName = name[i],
                        Value = value[i],
                        DataType = DataTypeEnum.DoublePercentage,
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

        private void CreateCollateralProjection(FrameworkEnum framework, long ou)
        {
            string[] bestKeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestCash,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestPlantEquipment,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestCommercialProperty,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestShares,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionBestVehicle};
            string[] optimisticKeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticCash,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticPlantEquipment,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticCommercialProperty,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticShares,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionOptimisticVehicle};
            string[] downturnKeys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnDebenture, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnCash,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnInventory, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnPlantEquipment,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnResidentialProperty, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnCommercialProperty,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnReceivables, DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnShares,
                                               DefaultLgdAssumptions.LgdAssumptionKey.CollateralProjectionDownturnVehicle};

            string[] name = new string[] {DefaultLgdAssumptions.InputName.Debenture, DefaultLgdAssumptions.InputName.Cash, DefaultLgdAssumptions.InputName.Inventory,
                                          DefaultLgdAssumptions.InputName.PlantEquipment, DefaultLgdAssumptions.InputName.ResidentialProperty, DefaultLgdAssumptions.InputName.CommercialProperty,
                                          DefaultLgdAssumptions.InputName.Receivables, DefaultLgdAssumptions.InputName.Shares, DefaultLgdAssumptions.InputName.Vehicle};

            //Collateral Projection Best
            for (int i = 0; i < bestKeys.Length; i++)
            {
                var best = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == bestKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (best == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralProjectionBest,
                        Key = bestKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

            //Collateral Projection Optimistic
            for (int i = 0; i < optimisticKeys.Length; i++)
            {
                var op = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == optimisticKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (op == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralProjectionOptimistic,
                        Key = optimisticKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }

            //Collateral Projection Downturn
            for (int i = 0; i < downturnKeys.Length; i++)
            {
                var downturn = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == downturnKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (downturn == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.CollateralProjectionDownturn,
                        Key = downturnKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.DoublePercentage,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou,
                        Status = GeneralStatusEnum.Approved
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreatePdAssumption(FrameworkEnum framework, long ou)
        {
            string[] keys = new string[] { DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption1, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption2,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption3, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption4,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption6, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption7,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption8, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption9,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumption10, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumptionConsStage1,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumptionConsStage2, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumptionCommStage1,
                                           DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumptionCommStage2, DefaultLgdAssumptions.LgdAssumptionKey.PdGroupAssumptionExp};
            string[] name = new string[] { DefaultLgdAssumptions.InputName.PdGroupAssumption1, DefaultLgdAssumptions.InputName.PdGroupAssumption2,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumption3, DefaultLgdAssumptions.InputName.PdGroupAssumption4,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumption6, DefaultLgdAssumptions.InputName.PdGroupAssumption7,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumption8, DefaultLgdAssumptions.InputName.PdGroupAssumption9,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumption10, DefaultLgdAssumptions.InputName.PdGroupAssumptionConsStage1,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumptionConsStage2, DefaultLgdAssumptions.InputName.PdGroupAssumptionCommStage1,
                                           DefaultLgdAssumptions.InputName.PdGroupAssumptionCommStage2, DefaultLgdAssumptions.InputName.PdGroupAssumptionExp};
            string[] value = new string[] { DefaultLgdAssumptions.InputValue.PdGroupAssumption1, DefaultLgdAssumptions.InputValue.PdGroupAssumption2,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumption3, DefaultLgdAssumptions.InputValue.PdGroupAssumption4,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumption6, DefaultLgdAssumptions.InputValue.PdGroupAssumption7,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumption8, DefaultLgdAssumptions.InputValue.PdGroupAssumption9,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumption10, DefaultLgdAssumptions.InputValue.PdGroupAssumptionConsStage1,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumptionConsStage2, DefaultLgdAssumptions.InputValue.PdGroupAssumptionCommStage1,
                                           DefaultLgdAssumptions.InputValue.PdGroupAssumptionCommStage2, DefaultLgdAssumptions.InputValue.PdGroupAssumptionExp};

            for (int i = 0; i < keys.Length; i++)
            {
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionGroupEnum.PdAssumptions,
                        Key = keys[i],
                        InputName = name[i],
                        Value = value[i],
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

    }
}
