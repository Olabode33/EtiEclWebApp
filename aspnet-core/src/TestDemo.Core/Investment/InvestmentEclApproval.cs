using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using TestDemo.Investment;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.Investment
{
	[Table("InvestmentEclApprovals")]
    [Audited]
    public class InvestmentEclApproval : FullAuditedEntity<Guid> 
    {

		public virtual long OrganizationUnitId { get; set; }
		
		public virtual DateTime ReviewedDate { get; set; }
		
		public virtual string ReviewComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual long? ReviewedByUserId { get; set; }
		
        [ForeignKey("ReviewedByUserId")]
		public User ReviewedByUserFk { get; set; }
		
		public virtual Guid InvestmentEclId { get; set; }
		
        [ForeignKey("InvestmentEclId")]
		public InvestmentEcl InvestmentEclFk { get; set; }
		
    }
}