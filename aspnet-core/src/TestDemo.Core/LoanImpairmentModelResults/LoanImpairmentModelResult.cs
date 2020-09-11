using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentModelResults
{
	[Table("LoanImpairmentModelResults")]
    public class LoanImpairmentModelResult : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegisterId { get; set; }
		
		public virtual double BaseScenarioExposure { get; set; }

		public virtual double OptimisticScenarioExposure { get; set; }

		public virtual double DownturnScenarioExposure { get; set; }

		public virtual double ResultsExposure { get; set; }

		public virtual double BaseScenarioPreOverlay { get; set; }

		public virtual double OptimisticScenarioPreOverlay { get; set; }

		public virtual double DownturnScenarioPreOverlay { get; set; }

		public virtual double ResultPreOverlay { get; set; }

		public virtual double BaseScenarioOverrideImpact { get; set; }

		public virtual double OptimisticScenarioOverrideImpact { get; set; }

		public virtual double DownturnScenarioOverrideImpact { get; set; }

		public virtual double ResultOverrideImpact { get; set; }

		public virtual double BaseScenarioIPO { get; set; }

		public virtual double OptimisticScenarioIPO { get; set; }

		public virtual double DownturnScenarioIPO { get; set; }

		public virtual double ResultIPO { get; set; }

		public virtual double BaseScenarioOverlay { get; set; }

		public virtual double OptimisticScenarioOverlay { get; set; }

		public virtual double DownturnScenarioOverlay { get; set; }

		public virtual double ResultOverlay { get; set; }

		public virtual double BaseScenarioFinalImpairment { get; set; }

		public virtual double OptimisticScenarioFinalImpairment { get; set; }

		public virtual double DownturnScenarioFinalImpairment { get; set; }

		public virtual double ResultFinalImpairment { get; set; }

	}
}