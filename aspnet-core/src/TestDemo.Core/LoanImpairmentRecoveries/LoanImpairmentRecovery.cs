using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentRecoveries
{
	[Table("LoanImpairmentRecoveries")]
    public class LoanImpairmentRecovery : FullAuditedEntity<Guid> 
    {

		public virtual string Recovery { get; set; }
		
		public virtual double CashRecovery { get; set; }
		
		public virtual double Property { get; set; }
		
		public virtual double Shares { get; set; }
		
		public virtual double LoanSale { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}