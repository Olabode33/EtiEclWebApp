using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.LoanImpairmentApprovals
{
	[Table("LoanImpairmentApprovals")]
    public class LoanImpairmentApproval : FullAuditedEntity<Guid> 
    {

		public virtual Guid RegisterId { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual long ReviewedByUserId { get; set; }
		
		public virtual DateTime ReviewedDate { get; set; }
		
		public virtual CalibrationStatusEnum Status { get; set; }
		

    }
}