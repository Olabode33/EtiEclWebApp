using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.Authorization.Users;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class EclAssumptionApprovalBase : FullAuditedEntity<Guid>, IMayHaveTenant, IMustHaveOrganizationUnit
    {
		public int? TenantId { get; set; }
		public virtual long OrganizationUnitId { get; set; }
		public virtual AssumptionTypeEnum AssumptionType { get; set; }
		public virtual string OldValue { get; set; }
		public virtual string NewValue { get; set; }
		public virtual DateTime? DateReviewed { get; set; }
		public virtual string ReviewComment { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		public virtual long? ReviewedByUserId { get; set; }
		[ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
	}
}
