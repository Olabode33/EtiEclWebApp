using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclShared;
using TestDemo.EclShared.DefaultAssumptions;
using TestDemo.EntityFrameworkCore;

namespace TestDemo.Migrations.Seed.DefaultAsusmption.EadInput
{
    public class DefaultEadAssumptionBuilder
    {
        private readonly TestDemoDbContext _context;

        public DefaultEadAssumptionBuilder(TestDemoDbContext context)
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
            //Wholesale
            CreateGeneralEadAssumption(FrameworkEnum.Wholesale, ou);
            CreateWholesaleAddtionalEadAssumption();
            //Retail
            CreateGeneralEadAssumption(FrameworkEnum.Retail, ou);
            //OBE
            CreateGeneralEadAssumption(FrameworkEnum.OBE, ou);
        }

        private void CreateGeneralEadAssumption(FrameworkEnum framework, long ou)
        {
            CreateCreditConversionFactor(framework, ou);

            //Conversion factor OBE
            var conversionFactorOBE = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ConversionFactorOBE
                                                                                                         && x.Framework == framework && x.OrganizationUnitId == ou);
            if (conversionFactorOBE == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.General,
                    Key = DefaultEadAssumption.EadAssumptionKey.ConversionFactorOBE,
                    InputName = DefaultEadAssumption.InputName.ConversionFactorOBE,
                    Value = DefaultEadAssumption.InputValue.ConversionFactorOBE,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Prepayment Factor
            var prepaymentFactor = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.PrePaymentFactor
                                                                                                      && x.Framework == framework && x.OrganizationUnitId == ou);
            if (prepaymentFactor == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.General,
                    Key = DefaultEadAssumption.EadAssumptionKey.PrePaymentFactor,
                    InputName = DefaultEadAssumption.InputName.PrePaymentFactor,
                    Value = DefaultEadAssumption.InputValue.PrePaymentFactor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }


            //Variable Interest Rate Projection
            var virMpr = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionMpr
                                                                                     && x.Framework == framework && x.OrganizationUnitId == ou);
            if (virMpr == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionMpr,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjectionMpr,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjectionMpr,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Exchange Rate Projection
            var xrateNGN = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionNgn
                                                                                     && x.Framework == framework);
            if (xrateNGN == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionNgn,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionNgn,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionNgn,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateUSD = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionUsd
                                                                                     && x.Framework == framework);
            if (xrateUSD == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionUsd,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionUsd,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionUsd,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateXoF = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionXoF
                                                                                     && x.Framework == framework);
            if (xrateXoF == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionXoF,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionXoF,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionXoF,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateCAD = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionCad
                                                                                     && x.Framework == framework);
            if (xrateCAD == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionCad,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionCad,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionCad,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateEUR = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionEur
                                                                                     && x.Framework == framework);
            if (xrateEUR == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionEur,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionEur,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionEur,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateGBP = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionGbp
                                                                                     && x.Framework == framework);
            if (xrateGBP == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionGbp,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionGbp,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionGbp,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateJPY = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionJpy
                                                                                     && x.Framework == framework);
            if (xrateJPY == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionJpy,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionJpy,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionJpy,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateKES = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionKes
                                                                                     && x.Framework == framework);
            if (xrateKES == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionKes,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionKes,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionKes,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var xrateZAR = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionZar
                                                                                     && x.Framework == framework);
            if (xrateZAR == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.ExchangeRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.ExchangeRateProjectionZar,
                    InputName = DefaultEadAssumption.InputName.ExchangeRateProjectionZar,
                    Value = DefaultEadAssumption.InputValue.ExchangeRateProjectionZar,
                    Datatype = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }

        private void CreateCreditConversionFactor(FrameworkEnum framework, long ou)
        {
            string[] ccfKeys = { DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCorporate, DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCommercial,
                                 DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorConsumer, DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorObe};
            string[] ccfNames = { DefaultEadAssumption.InputName.CreditConversionFactorCorporate, DefaultEadAssumption.InputName.CreditConversionFactorCommercial,
                                  DefaultEadAssumption.InputName.CreditConversionFactorConsumer, DefaultEadAssumption.InputName.CreditConversionFactorObe};
            string[] ccfValues = { DefaultEadAssumption.InputValue.CreditConversionFactorCorporate, DefaultEadAssumption.InputValue.CreditConversionFactorCommercial,
                                   DefaultEadAssumption.InputValue.CreditConversionFactorConsumer, DefaultEadAssumption.InputValue.CreditConversionFactorObe};

            //Credit Conversion Factor
            for (int i = 0; i < ccfKeys.Length; i++)
            {
                var ccf = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == ccfKeys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (ccf == null)
                {
                    _context.EadInputAssumptions.Add(new EadInputAssumption()
                    {
                        EadGroup = EadInputAssumptionGroupEnum.CreditConversionFactors,
                        Key = ccfKeys[i],
                        InputName = ccfNames[i],
                        Value = ccfValues[i],
                        Datatype = DataTypeEnum.DoublePercentage,
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

        private void CreateWholesaleAddtionalEadAssumption()
        {
            var libor180 = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection180DaysLibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (libor180 == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection180DaysLibor,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjection180DaysLibor,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjection180DaysLibor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = FrameworkEnum.Wholesale,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var libor3 = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection3MonthsLibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (libor3 == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection3MonthsLibor,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjection3MonthsLibor,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjection3MonthsLibor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = FrameworkEnum.Wholesale,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var libor90 = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection90DaysLibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (libor90 == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection90DaysLibor,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjection90DaysLibor,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjection90DaysLibor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = FrameworkEnum.Wholesale,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var nibor = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionAveragePreceding90DaysNibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (nibor == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionAveragePreceding90DaysNibor,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjectionAveragePreceding90DaysNibor,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjectionAveragePreceding90DaysNibor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = FrameworkEnum.Wholesale,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            var libor = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionLibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (libor == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputAssumptionGroupEnum.VariableInterestRateProjections,
                    Key = DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionLibor,
                    InputName = DefaultEadAssumption.InputName.VariableInterestRateProjectionLibor,
                    Value = DefaultEadAssumption.InputValue.VariableInterestRateProjectionLibor,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = FrameworkEnum.Wholesale,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
        }
    }
}
