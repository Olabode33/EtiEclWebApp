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
	[Table("Assumptions")]
    [Audited]
    public class Assumption : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual AssumptionGroupEnum AssumptionGroup { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }

        public virtual FrameworkEnum Framework { get; set; }

        public virtual bool CanAffiliateEdit { get; set; }

        public long OrganizationUnitId { get; set; }

        public virtual GeneralStatusEnum Status { get; set; }
    }
}