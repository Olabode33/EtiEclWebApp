using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclEadInputAssumptionBase: AssumptionBase
    {
		public virtual string Key { get; set; }
		public virtual string InputName { get; set; }
		public virtual string Value { get; set; }
		public virtual EadInputAssumptionGroupEnum EadGroup { get; set; }
	}
}
