using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdAssumptionNonInternalModelBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual int Month { get; set; }
		public virtual string PdGroup { get; set; }
		public virtual double MarginalDefaultRate { get; set; }
		public virtual double CummulativeSurvival { get; set; }
	}
}
