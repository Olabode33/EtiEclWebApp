using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.ComputationBase
{
    public class EclOverrideApprovalBase : FullAuditedEntity<Guid>
    {
		public virtual Guid? EclOverrideId { get; set; }
		public virtual DateTime? ReviewDate { get; set; }
		public virtual string ReviewComment { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
		public virtual long? ReviewedByUserId { get; set; }
		[ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
	}
}
