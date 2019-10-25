using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.WholesaleComputation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEclSicrApprovals")]
    [Audited]
    public class WholesaleEclSicrApproval : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual DateTime? ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
		public virtual Guid? WholesaleEclSicrId { get; set; }
		
        [ForeignKey("WholesaleEclSicrId")]
		public WholesaleEclSicr WholesaleEclSicrFk { get; set; }
		
    }
}