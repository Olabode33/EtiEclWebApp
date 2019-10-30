using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.Retail
{
	[Table("RetailEcls")]
    [Audited]
    public class RetailEcl : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        [Required]
		public virtual DateTime ReportingDate { get; set; }
		
		public virtual DateTime? ClosedDate { get; set; }
		
		[Required]
		public virtual bool IsApproved { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ClosedByUserId { get; set; }
		
        [ForeignKey("ClosedByUserId")]
		public User ClosedByUserFk { get; set; }
		
    }
}