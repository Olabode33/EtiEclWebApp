using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdAssumption12MonthBase: AssumptionBase
    {
		[Required]
		public virtual int Credit { get; set; }
		public virtual double? PD { get; set; }
		public virtual string SnPMappingEtiCreditPolicy { get; set; }
		public virtual string SnPMappingBestFit { get; set; }
	}
}
