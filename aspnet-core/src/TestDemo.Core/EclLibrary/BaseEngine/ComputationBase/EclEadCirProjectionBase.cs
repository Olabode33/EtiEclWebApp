using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclEadCirProjectionBase: Entity<Guid>
    {
		public virtual string CIR_GROUP { get; set; }
		public virtual int Month { get; set; }
		public virtual double Value { get; set; }
		public virtual double CIR_EFFECTIVE { get; set; }

	}
}
