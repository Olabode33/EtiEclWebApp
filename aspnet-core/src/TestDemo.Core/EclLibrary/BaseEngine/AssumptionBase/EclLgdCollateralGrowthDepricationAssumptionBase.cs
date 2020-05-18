using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclLgdCollateralGrowthDepricationAssumptionBase: AssumptionBase
    {
        public double? Debenture { get; set; }
        public double? Cash { get; set; }
        public double? Inventory { get; set; }
        public double? Plant_And_Equipment { get; set; }
        public double? Residential_Property { get; set; }
        public double? Commercial_Property { get; set; }
        public double? Receivables { get; set; }
        public double? Shares { get; set; }
        public double? Vehicle { get; set; }
        public CollateralProjectionType CollateralProjectionType { get; set; }
    }
}
