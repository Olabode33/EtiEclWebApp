using TestDemo.EclShared;
using TestDemo.Authorization.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.Investment
{
	[Table("InvestmentEcls")]
    [Audited]
    public class InvestmentEcl : FullAuditedEntity<Guid> , IMustHaveOrganizationUnit
    {

		public virtual DateTime ReportingDate { get; set; }
		
		public virtual DateTime? ClosedDate { get; set; }
		
		public virtual bool IsApproved { get; set; }
		
		public virtual EclStatusEnum Status { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

		public virtual long? ClosedByUserId { get; set; }
		
        [ForeignKey("ClosedByUserId")]
		public User ClosedByUserFk { get; set; }
		
    }
}