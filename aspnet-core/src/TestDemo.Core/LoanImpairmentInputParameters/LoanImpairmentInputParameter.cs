using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentInputParameters
{
	[Table("LoanImpairmentInputParameters")]
    public class LoanImpairmentInputParameter : FullAuditedEntity<Guid> 
    {

		public virtual DateTime ReportingDate { get; set; }
		
		public virtual double CostOfCapital { get; set; }
		
		public virtual double LoanAmount { get; set; }
		
		public virtual Guid RegisterId { get; set; }
		

    }
}