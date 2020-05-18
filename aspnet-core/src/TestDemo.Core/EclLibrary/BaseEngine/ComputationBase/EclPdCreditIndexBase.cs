using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclPdCreditIndexBase: Entity<Guid>
    {
		public virtual int? ProjectionMonth { get; set; }
		public virtual double? BestEstimate { get; set; }
		public virtual double? Optimistic { get; set; }
		public virtual double? Downturn { get; set; }
	}
}
