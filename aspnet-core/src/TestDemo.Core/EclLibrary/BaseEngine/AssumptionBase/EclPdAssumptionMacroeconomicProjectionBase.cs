using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdAssumptionMacroeconomicProjectionBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string InputName { get; set; }
		public virtual double BestValue { get; set; }
		public virtual double OptimisticValue { get; set; }
		public virtual double DownturnValue { get; set; }
		public virtual int MacroeconomicVariableId { get; set; }
		[ForeignKey("MacroeconomicVariableId")]
		public MacroeconomicVariable MacroeconomicVariable { get; set; }
	}
}
