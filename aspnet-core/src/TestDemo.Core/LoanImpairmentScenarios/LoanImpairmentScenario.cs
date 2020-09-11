using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentScenarios
{
	[Table("LoanImpairmentScenarios")]
    public class LoanImpairmentScenario : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegisterId { get; set; }
		
		public virtual string ScenarioOption { get; set; }
		
		public virtual string ApplyOverridesBaseScenario { get; set; }
		
		public virtual string ApplyOverridesOptimisticScenario { get; set; }
		
		public virtual string ApplyOverridesDownturnScenario { get; set; }
		
		public virtual double BestScenarioOverridesValue { get; set; }
		
		public virtual double OptimisticScenarioOverridesValue { get; set; }
		
		public virtual double DownturnScenarioOverridesValue { get; set; }
		
		public virtual double BaseScenario { get; set; }
		
		public virtual double OptimisticScenario { get; set; }
		

    }
}