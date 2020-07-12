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

namespace TestDemo.BatchEcls
{
	[Table("BatchEclApprovals")]
    [Audited]
    public class BatchEclApproval : FullAuditedEntity<Guid> , IMustHaveOrganizationUnit
    {
        public virtual long OrganizationUnitId { get; set; }
        public virtual DateTime? ReviewedDate { get; set; }
		public virtual string ReviewComment { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
		public virtual long? ReviewedByUserId { get; set; }
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		public virtual Guid? BatchEclId { get; set; }
		
        [ForeignKey("BatchEclId")]
		public BatchEcl BatchEclFk { get; set; }
		
    }
}