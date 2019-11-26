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
            //Retail
            CreateRetailUnsecuredRecoveryCureRate();
            CreateRetailUnsecuredRecoveryTimeInDefault();
            CreateCostOfRecovery(FrameworkEnum.Retail);
            CreateCollateralGrowth(FrameworkEnum.Retail);
            CreateTTR(FrameworkEnum.Retail);
            CreateHaircut(FrameworkEnum.Retail);
            CreateCollateralProjection(FrameworkEnum.Retail);

            //Wholesale
            CreateWholesaleUnsecuredRecoveryCureRate();
            CreateWholesaleUnsecuredRecoveryTimeInDefault();
            CreateCostOfRecovery(FrameworkEnum.Wholesale);
            CreateCollateralGrowth(FrameworkEnum.Wholesale);
            CreateTTR(FrameworkEnum.Wholesale);
            CreateHaircut(FrameworkEnum.Wholesale);
            CreateCollateralProjection(FrameworkEnum.Wholesale);

            //OBE
            CreateObeUnsecuredRecoveryCureRate();
            CreateObeUnsecuredRecoveryTimeInDefault();
            CreateCostOfRecovery(FrameworkEnum.OBE);
            CreateCollateralGrowth(FrameworkEnum.OBE);
            CreateTTR(FrameworkEnum.OBE);
            CreateHaircut(FrameworkEnum.OBE);
            CreateCollateralProjection(FrameworkEnum.OBE);
        }

        private void CreateRetailUnsecuredRecoveryCureRate()
        {
            FrameworkEnum framework = FrameworkEnum.Retail;

            var customerCard = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardCureRate
                                                                                     && x.Framework == framework);
            if (customerCard == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerCard,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseCureRate
                                                                                     && x.Framework == framework);
            if (customerLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerLease,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanCureRate
                                                                                     && x.Framework == framework);
            if (customerLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerLoan,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageCureRate
                                                                                     && x.Framework == framework);
            if (customerMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerMortgage,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdCureRate
                                                                                     && x.Framework == framework);
            if (customerOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOd,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateWholesaleUnsecuredRecoveryCureRate()
        {
            FrameworkEnum framework = FrameworkEnum.Wholesale;

            var commercialCard = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardCureRate
                                                                                     && x.Framework == framework);
            if (commercialCard == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialCard,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseCureRate
                                                                                     && x.Framework == framework);
            if (commercialLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLease,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanCureRate
                                                                                     && x.Framework == framework);
            if (commercialLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLoan,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageCureRate
                                                                                     && x.Framework == framework);
            if (commercialMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialMortgage,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdCureRate
                                                                                     && x.Framework == framework);
            if (commercialOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialOd,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var corporateLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseCureRate
                                                                                     && x.Framework == framework);
            if (corporateLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLease,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanCureRate
                                                                                     && x.Framework == framework);
            if (corporateLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLoan,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageCureRate
                                                                                     && x.Framework == framework);
            if (corporateMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateMortgage,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdCureRate
                                                                                     && x.Framework == framework);
            if (corporateOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateOd,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateObeUnsecuredRecoveryCureRate()
        {
            FrameworkEnum framework = FrameworkEnum.OBE;

            //Commercial
            var commercialAdPayment = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeCureRate
                                                                                     && x.Framework == framework);
            if (commercialAdPayment == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialAdvancePaymentGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialBidBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondCureRate
                                                                                     && x.Framework == framework);
            if (commercialBidBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialBidBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialCustomBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondCureRate
                                                                                     && x.Framework == framework);
            if (commercialCustomBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialCustomsBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLoC = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditCureRate
                                                                                     && x.Framework == framework);
            if (commercialLoC == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLetterOfCredit,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeCureRate
                                                                                     && x.Framework == framework);
            if (commercialObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialPBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondCureRate
                                                                                     && x.Framework == framework);
            if (commercialPBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialTBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondCureRate
                                                                                     && x.Framework == framework);
            if (commercialTBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Customer
            var customerObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeCureRate
                                                                                     && x.Framework == framework);
            if (customerObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Corporate
            var corpAdPayment = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeCureRate
                                                                                     && x.Framework == framework);
            if (corpAdPayment == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateAdvancePaymentGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpBidBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondCureRate
                                                                                     && x.Framework == framework);
            if (corpBidBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateBidBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpCustomBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondCureRate
                                                                                     && x.Framework == framework);
            if (corpCustomBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateCustomsBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpLoC = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditCureRate
                                                                                     && x.Framework == framework);
            if (corpLoC == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLetterOfCredit,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeCureRate
                                                                                     && x.Framework == framework);
            if (corpObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporateOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpPBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondCureRate
                                                                                     && x.Framework == framework);
            if (corpPBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpTBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondCureRate
                                                                                     && x.Framework == framework);
            if (corpTBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondCureRate,
                    InputName = DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.WholesaleObeCureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateRetailUnsecuredRecoveryTimeInDefault()
        {
            FrameworkEnum framework = FrameworkEnum.Retail;

            var customerCard = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerCard == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerCardBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerCard,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLeaseBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerLease,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerLoanBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerLoan,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerMortgageBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerMortgage,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var customerOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOdBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOd,
                    Value = DefaultLgdAssumptions.InputValue.CureRate,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateWholesaleUnsecuredRecoveryTimeInDefault()
        {
            FrameworkEnum framework = FrameworkEnum.Wholesale;

            var commercialCard = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialCard == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCardBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialCard,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLeaseBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLease,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLoanBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLoan,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialMortgageBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialMortgage,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOdBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialOd,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var corporateLease = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corporateLease == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLeaseBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLease,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateLoan = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corporateLoan == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLoanBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLoan,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateMortgage = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corporateMortgage == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateMortgageBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateMortgage,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corporateOd = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corporateOd == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOdBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateOd,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateObeUnsecuredRecoveryTimeInDefault()
        {
            FrameworkEnum framework = FrameworkEnum.OBE;

            //Commercial
            var commercialAdPayment = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialAdPayment == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialAdvancePaymentGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialAdvancePaymentGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialBidBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialBidBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialBidBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialBidBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialCustomBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialCustomBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialCustomsBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialCustomsBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialLoC = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialLoC == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialLetterOfCreditTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialLetterOfCredit,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialOtherBondGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialPBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialPBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialPerformanceBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var commercialTBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (commercialTBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCommercialTenderBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CommercialPerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Customer
            var customerObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeTimeInDefault
                                                                                     && x.Framework == framework);
            if (customerObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCustomerOtherBondsGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CustomerOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Corporate
            var corpAdPayment = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpAdPayment == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateAdvancePaymentGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateAdvancePaymentGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpBidBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpBidBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateBidBondBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateBidBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpCustomBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpCustomBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateCustomsBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateCustomsBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpLoC = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditBestTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpLoC == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateLetterOfCreditBestTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateLetterOfCredit,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpObg = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpObg == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateOtherBondGuaranteeTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporateOtherBondGuarantee,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpPBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpPBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporatePerformanceBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var corpTBond = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondTimeInDefault
                                                                                     && x.Framework == framework);
            if (corpTBond == null)
            {
                _context.LgdAssumption.Add(new LgdInputAssumption()
                {
                    LgdGroup = LdgInputAssumptionEnum.UnsecuredRecoveries,
                    Key = DefaultLgdAssumptions.LgdAssumptionKey.UnsecuredRecoveriesCorporateTenderBondTimeInDefault,
                    InputName = DefaultLgdAssumptions.InputName.CorporatePerformanceBond,
                    Value = DefaultLgdAssumptions.InputValue.TimeInDefault,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = true,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateCostOfRecovery(FrameworkEnum framework)
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
                var corHigh = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == corHighKeys[i]
                                                                                     && x.Framework == framework);
                if (corHigh == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CostOfRecoveryHigh,
                        Key = corHighKeys[i],
                        InputName = i == 0 ? DefaultLgdAssumptions.InputName.CostOfRecoveryHighCollateralValue : corName[i - 1],
                        Value = i == 0 ? DefaultLgdAssumptions.InputValue.CollateralValue : DefaultLgdAssumptions.InputValue.CostOfRecoveryHigh,
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

            for (int i = 0; i < corLowKeys.Length; i++)
            {
                var corLow = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == corLowKeys[i]
                                                                                     && x.Framework == framework);
                if (corLow == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CostOfRecoveryLow,
                        Key = corLowKeys[i],
                        InputName = i == 0 ? DefaultLgdAssumptions.InputName.CostOfRecoveryHighCollateralValue : corName[i - 1],
                        Value = i == 0 ? DefaultLgdAssumptions.InputValue.CollateralValue : DefaultLgdAssumptions.InputValue.CostOfRecoveryLow,
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateCollateralGrowth(FrameworkEnum framework)
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
                var best = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == bestKeys[i] && x.Framework == framework);
                if (best == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralGrowthBest,
                        Key = bestKeys[i],
                        InputName = name[i],
                        Value = bestValue[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

            //Optimistic
            for (int i = 0; i < optimisticKeys.Length; i++)
            {
                var op = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == optimisticKeys[i] && x.Framework == framework);
                if (op == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralGrowthOptimistic,
                        Key = optimisticKeys[i],
                        InputName = name[i],
                        Value = bestValue[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

            //Downturn
            for (int i = 0; i < downturnKeys.Length; i++)
            {
                var downturn = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == downturnKeys[i] && x.Framework == framework);
                if (downturn == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralGrowthDownturn,
                        Key = downturnKeys[i],
                        InputName = name[i],
                        Value = (Convert.ToDouble(bestValue[i]) * 0.92 - 0.08).ToString(),
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

        }

        private void CreateTTR(FrameworkEnum framework)
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
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralTTR,
                        Key = keys[i],
                        InputName = name[i],
                        Value = value[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateHaircut(FrameworkEnum framework)
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
                var ttr = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework);
                if (ttr == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.Haircut,
                        Key = keys[i],
                        InputName = name[i],
                        Value = value[i],
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = true,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }
        }

        private void CreateCollateralProjection(FrameworkEnum framework)
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
                var best = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == bestKeys[i] && x.Framework == framework);
                if (best == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralProjectionBest,
                        Key = bestKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

            //Collateral Projection Optimistic
            for (int i = 0; i < optimisticKeys.Length; i++)
            {
                var op = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == optimisticKeys[i] && x.Framework == framework);
                if (op == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralProjectionOptimistic,
                        Key = optimisticKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }

            //Collateral Projection Downturn
            for (int i = 0; i < downturnKeys.Length; i++)
            {
                var downturn = _context.LgdAssumption.IgnoreQueryFilters().FirstOrDefault(x => x.Key == downturnKeys[i] && x.Framework == framework);
                if (downturn == null)
                {
                    _context.LgdAssumption.Add(new LgdInputAssumption()
                    {
                        LgdGroup = LdgInputAssumptionEnum.CollateralProjectionDownturn,
                        Key = downturnKeys[i],
                        InputName = name[i],
                        Value = "1.0",
                        DataType = DataTypeEnum.Double,
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
