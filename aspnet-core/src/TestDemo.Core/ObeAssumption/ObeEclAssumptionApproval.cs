using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.OBE;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclAssumptionApprovals")]
    [Audited]
    public class ObeEclAssumptionApproval : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual AssumptionTypeEnum AssumptionType { get; set; }
		
		public virtual string OldValue { get; set; }
		
		public virtual string NewValue { get; set; }
		
		public virtual DateTime? DateReviewed { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
    }
}