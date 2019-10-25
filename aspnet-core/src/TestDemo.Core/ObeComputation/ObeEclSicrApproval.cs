using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.ObeComputation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.ObeComputation
{
	[Table("ObeEclSicrApprovals")]
    [Audited]
    public class ObeEclSicrApproval : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual DateTime? ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
		public virtual Guid? ObeEclSicrId { get; set; }
		
        [ForeignKey("ObeEclSicrId")]
		public ObeEclSicr ObeEclSicrFk { get; set; }
		
    }
}