using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.RetailAssumption
{
	[Table("RetailEclAssumptionApprovals")]
    [Audited]
    public class RetailEclAssumptionApproval : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
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
		
		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}