using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdSnPCummulativeDefaultRateBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual string Rating { get; set; }
		public virtual int? Years { get; set; }
		public virtual double? Value { get; set; }
	}
}
