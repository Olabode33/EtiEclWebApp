using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdAssumptionNplIndexBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual double Actual { get; set; }
		public virtual double Standardised { get; set; }
		public virtual double EtiNplSeries { get; set; }
	}
}
