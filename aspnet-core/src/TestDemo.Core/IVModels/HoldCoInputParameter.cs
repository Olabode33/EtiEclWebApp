using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.IVModels
{
	[Table("HoldCoInputParameters")]
    public class HoldCoInputParameter : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegistrationId { get; set; }
		
		public virtual DateTime ValuationDate { get; set; }
		
		public virtual double Optimistic { get; set; }
		
		public virtual double BestEstimate { get; set; }
		
		public virtual double Downturn { get; set; }
		
		public virtual string AssumedRating { get; set; }
		
		public virtual string DefaultLoanRating { get; set; }
		
		public virtual double RecoveryRate { get; set; }
		
		public virtual DateTime AssumedStartDate { get; set; }
		
		public virtual DateTime AssumedMaturityDate { get; set; }
		

    }
}