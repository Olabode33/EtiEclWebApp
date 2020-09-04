using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ReceivablesResults
{
	[Table("ReceivablesResults")]
    public class ReceivablesResult : Entity<Guid> 
    {

		public virtual double TotalExposure { get; set; }
		
		public virtual double TotalImpairment { get; set; }
		
		public virtual double AdditionalProvision { get; set; }
		
		public virtual double Coverage { get; set; }
		
		public virtual double OptimisticExposure { get; set; }
		
		public virtual double BaseExposure { get; set; }
		
		public virtual double DownturnExposure { get; set; }
		
		public virtual double ECLTotalExposure { get; set; }
		
		public virtual double OptimisticImpairment { get; set; }
		
		public virtual double BaseImpairment { get; set; }
		
		public virtual double DownturnImpairment { get; set; }
		
		public virtual double ECLTotalImpairment { get; set; }
		
		public virtual double OptimisticCoverageRatio { get; set; }
		
		public virtual double BaseCoverageRatio { get; set; }
		
		public virtual double DownturnCoverageRatio { get; set; }
		
		public virtual double TotalCoverageRatio { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}