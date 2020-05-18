using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclLgdInputAssumptionUnsecuredRecoveryBase: AssumptionBase
    {
        public string Segment_Product_Type { get; set; }
        public double Cure_Rate { get; set; }
        public double Days_0 { get; set; }
    }
}
