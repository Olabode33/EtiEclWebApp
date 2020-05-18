using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclPdAssumptionMacroeconomicInputBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual string InputName { get; set; }
		public virtual double Value { get; set; }
		public virtual int MacroeconomicVariableId { get; set; }
		[ForeignKey("MacroeconomicVariableId")]
		public MacroeconomicVariable MacroeconomicVariable { get; set; }
	}
}
