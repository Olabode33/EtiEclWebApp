using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.InvestmentComputation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.InvestmentComputation
{
	[Table("InvestmentEclOverrideApprovals")]
    [Audited]
    public class InvestmentEclOverrideApproval : FullAuditedEntity<Guid> 
    {

		public virtual DateTime? ReviewDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
		public virtual Guid? InvestmentEclOverrideId { get; set; }
		
        [ForeignKey("InvestmentEclOverrideId")]
		public InvestmentEclOverride InvestmentEclOverrideFk { get; set; }
		
    }
}