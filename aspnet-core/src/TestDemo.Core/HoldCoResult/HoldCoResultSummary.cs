using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.HoldCoResult
{
	[Table("HoldCoResultSummaries")]
    public class HoldCoResultSummary : FullAuditedEntity<Guid> 
    {

		public virtual double BestEstimateExposure { get; set; }
		
		public virtual double OptimisticExposure { get; set; }
		
		public virtual double DownturnExposure { get; set; }
		
		public virtual double BestEstimateTotal { get; set; }
		
		public virtual double OptimisticTotal { get; set; }
		
		public virtual string DownturnTotal { get; set; }
		
		public virtual double BestEstimateImpairmentRatio { get; set; }
		
		public virtual double OptimisticImpairmentRatio { get; set; }
		
		public virtual double DownturnImpairmentRatio { get; set; }
		
		public virtual double Exposure { get; set; }
		
		public virtual double Total { get; set; }
		
		public virtual double ImpairmentRatio { get; set; }
		
		public virtual bool Check { get; set; }
		
		public virtual string Diff { get; set; }
		
		public virtual Guid RegistrationId { get; set; }
		

    }
}