using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentHaircuts
{
	[Table("LoanImpairmentHaircuts")]
    public class LoanImpairmentHaircut : FullAuditedEntity<Guid> 
    {

		public virtual double CashRecovery { get; set; }
		
		public virtual double Property { get; set; }
		
		public virtual double Shares { get; set; }
		
		public virtual double LoanSale { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}