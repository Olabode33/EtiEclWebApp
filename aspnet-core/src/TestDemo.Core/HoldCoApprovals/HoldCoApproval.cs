using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.HoldCoApprovals
{
	[Table("HoldCoApprovals")]
    public class HoldCoApproval : FullAuditedEntity 
    {

		public virtual Guid RegistrationId { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual long ReviewedByUserId { get; set; }
		
		public virtual DateTime ReviewedDate { get; set; }
		
		public virtual CalibrationStatusEnum Status { get; set; }
		

    }
}