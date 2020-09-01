using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.HoldCoInterCompanyResults
{
	[Table("HoldCoInterCompanyResults")]
    public class HoldCoInterCompanyResult : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegistrationId { get; set; }
		
		public virtual string AssetType { get; set; }
		
		public virtual string AssetDescription { get; set; }
		
		public virtual int Stage { get; set; }
		
		public virtual double OutstandingBalance { get; set; }
		
		public virtual double BestEstimate { get; set; }
		
		public virtual double Optimistic { get; set; }
		
		public virtual double Downturn { get; set; }
		
		public virtual double Impairment { get; set; }
		

    }
}