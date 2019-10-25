using TestDemo.EclShared;
using TestDemo.RetailInputs;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.RetailInputs
{
	[Table("RetailEclUploadApprovals")]
    [Audited]
    public class RetailEclUploadApproval : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual DateTime? ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid RetailEclUploadId { get; set; }
		
        [ForeignKey("RetailEclUploadId")]
		public RetailEclUpload RetailEclUploadFk { get; set; }
		
		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
    }
}