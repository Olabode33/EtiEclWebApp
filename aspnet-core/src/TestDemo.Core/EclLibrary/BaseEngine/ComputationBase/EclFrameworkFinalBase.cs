using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclFrameworkFinalBase: Entity<Guid>
    {
		[Required]
		public virtual int EclMonth { get; set; }
		[Required]
		public virtual double MonthlyEclValue { get; set; }
		[Required]
		public virtual int Stage { get; set; }
		[Required]
		public virtual double FinalEclValue { get; set; }
		public virtual EclScenarioEnum Scenario { get; set; }
		public virtual string ContractId { get; set; }
	}
}
