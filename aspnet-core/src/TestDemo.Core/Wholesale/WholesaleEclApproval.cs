using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.Wholesale
{
	[Table("WholesaleEclApprovals")]
    [Audited]
    public class WholesaleEclApproval : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual DateTime? ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}