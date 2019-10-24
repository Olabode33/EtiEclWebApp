using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Wholesale;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.WholesaleAssumption
{
	[Table("WholesaleEclAssumptionApprovals")]
    [Audited]
    public class WholesaleEclAssumptionApproval : FullAuditedEntity<Guid> 
    {

		public virtual AssumptionTypeEnum AssumptionType { get; set; }
		
		public virtual Guid AssumptionId { get; set; }
		
		public virtual string OldValue { get; set; }
		
		public virtual string NewValue { get; set; }
		
		public virtual DateTime? DateReviewed { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }

		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
    }
}