using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentKeyParameters
{
	[Table("LoanImpairmentKeyParameters")]
    public class LoanImpairmentKeyParameter : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegisterId { get; set; }
		
		public virtual int Year { get; set; }
		
		public virtual double ExpectedCashFlow { get; set; }
		
		public virtual double RevisedCashFlow { get; set; }
		

    }
}