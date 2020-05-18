using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclLgdCollateralProjectionBase: Entity<Guid>
    {
		public virtual int Month { get; set; }
		public virtual EclScenarioEnum CollateralProjectionType { get; set; }
		public virtual double Debenture { get; set; }
		public virtual double Cash { get; set; }
		public virtual double Inventory { get; set; }
		public virtual double Plant_And_Equipment { get; set; }
		public virtual double Residential_Property { get; set; }
		public virtual double Commercial_Property { get; set; }
		public virtual double Receivables { get; set; }
		public virtual double Shares { get; set; }
		public virtual double Vehicle { get; set; }

	}
}
