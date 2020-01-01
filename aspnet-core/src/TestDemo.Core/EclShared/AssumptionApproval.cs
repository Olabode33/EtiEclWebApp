using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.EclShared
{
	[Table("AssumptionApprovals")]
    [Audited]
    public class AssumptionApproval : FullAuditedEntity<Guid> 
    {

		public virtual long OrganizationUnitId { get; set; }
		
		public virtual FrameworkEnum Framework { get; set; }
		
		public virtual AssumptionTypeEnum AssumptionType { get; set; }
		
		public virtual string AssumptionGroup { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string OldValue { get; set; }
		
		public virtual string NewValue { get; set; }
		
		public virtual DateTime? DateReviewed { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }

        public virtual Guid AssumptionId { get; set; }

        public virtual string AssumptionEntity { get; set; }

        public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
    }
}