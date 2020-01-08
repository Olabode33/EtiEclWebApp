using TestDemo.EclShared;
using TestDemo.EclShared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.EclShared
{
	[Table("InvSecMacroEconomicAssumptions")]
    [Audited]
    public class InvSecMacroEconomicAssumption : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual int Month { get; set; }
		
		public virtual double BestValue { get; set; }
		
		public virtual double OptimisticValue { get; set; }
		
		public virtual double DownturnValue { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

    }
}