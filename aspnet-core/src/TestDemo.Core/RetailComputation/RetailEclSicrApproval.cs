using TestDemo.EclShared;
using TestDemo.RetailComputation;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.RetailComputation
{
	[Table("RetailEclSicrApprovals")]
    [Audited]
    public class RetailEclSicrApproval : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual DateTime? ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid? RetailEclSicrId { get; set; }
		
        [ForeignKey("RetailEclSicrId")]
		public RetailEclSicr RetailEclSicrFk { get; set; }
		
		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
    }
}