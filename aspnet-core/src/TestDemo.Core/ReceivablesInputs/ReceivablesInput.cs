using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ReceivablesInputs
{
	[Table("ReceivablesInputs")]
    public class ReceivablesInput : Entity<Guid> 
    {

		public virtual DateTime ReportingDate { get; set; }
		
		public virtual double ScenarioOptimistic { get; set; }
		
		public virtual int LossDefinition { get; set; }
		
		public virtual double LossRate { get; set; }
		
		public virtual bool FLIOverlay { get; set; }
		
		public virtual double OverlayOptimistic { get; set; }
		
		public virtual double OverlayBase { get; set; }
		
		public virtual double OverlayDownturn { get; set; }
		
		public virtual double InterceptCoefficient { get; set; }
		
		public virtual double IndexCoefficient { get; set; }
		
		public virtual double LossRateCoefficient { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		
		public virtual double ScenarioBase { get; set; }
		

    }
}