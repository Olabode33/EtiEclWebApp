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
            //Wholesale
            CreateGeneralEadAssumption(FrameworkEnum.Wholesale);
            CreateWholesaleAddtionalEadAssumption();
            //Retail
            CreateGeneralEadAssumption(FrameworkEnum.Retail);
            //OBE
            CreateGeneralEadAssumption(FrameworkEnum.OBE);
        }

        private void CreateGeneralEadAssumption(FrameworkEnum framework)
        {
            //Conversion factor OBE
            var conversionFactorOBE = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.ConversionFactorOBE
                                                                                     && x.Framework == framework);
            if (conversionFactorOBE == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.General,
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
                                                                                     && x.Framework == framework);
            if (prepaymentFactor == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.General,
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

            //Credit Conversion Factor
            var ccfCorporate = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCorporate
                                                                                     && x.Framework == framework);
            if (ccfCorporate == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.CreditConversionFactors,
                    Key = DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCorporate,
                    InputName = DefaultEadAssumption.InputName.CreditConversionFactorCorporate,
                    Value = DefaultEadAssumption.InputValue.CreditConversionFactorCorporate,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var ccfCommercial = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCommercial
                                                                                     && x.Framework == framework);
            if (ccfCommercial == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.CreditConversionFactors,
                    Key = DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorCommercial,
                    InputName = DefaultEadAssumption.InputName.CreditConversionFactorCommercial,
                    Value = DefaultEadAssumption.InputValue.CreditConversionFactorCommercial,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var ccfConsumer = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorConsumer
                                                                                     && x.Framework == framework);
            if (ccfConsumer == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.CreditConversionFactors,
                    Key = DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorConsumer,
                    InputName = DefaultEadAssumption.InputName.CreditConversionFactorConsumer,
                    Value = DefaultEadAssumption.InputValue.CreditConversionFactorConsumer,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }
            var ccfObe = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorObe
                                                                                     && x.Framework == framework);
            if (ccfObe == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.CreditConversionFactors,
                    Key = DefaultEadAssumption.EadAssumptionKey.CreditConversionFactorObe,
                    InputName = DefaultEadAssumption.InputName.CreditConversionFactorObe,
                    Value = DefaultEadAssumption.InputValue.CreditConversionFactorObe,
                    Datatype = DataTypeEnum.DoublePercentage,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Variable Interest Rate Projection
            var virMpr = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjectionMpr
                                                                                     && x.Framework == framework);
            if (virMpr == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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
                    EadGroup = EadInputGroupEnum.ExchangeRateProjections,
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

        private void CreateWholesaleAddtionalEadAssumption()
        {
            var libor180 = _context.EadInputAssumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultEadAssumption.EadAssumptionKey.VariableInterestRateProjection180DaysLibor
                                                                                     && x.Framework == FrameworkEnum.Wholesale);
            if (libor180 == null)
            {
                _context.EadInputAssumptions.Add(new EadInputAssumption()
                {
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
                    EadGroup = EadInputGroupEnum.VariableInterestRateProjections,
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
