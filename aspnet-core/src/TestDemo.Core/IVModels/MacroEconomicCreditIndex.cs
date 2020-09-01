using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.IVModels
{
	[Table("MacroEconomicCreditIndices")]
    public class MacroEconomicCreditIndex : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegistrationId { get; set; }
		
		public virtual int Month { get; set; }
		
		public virtual double BestEstimate { get; set; }
		
		public virtual double Optimistic { get; set; }
		
		public virtual double Downturn { get; set; }
		

    }
}