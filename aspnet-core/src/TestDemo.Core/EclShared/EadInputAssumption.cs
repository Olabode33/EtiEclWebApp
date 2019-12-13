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
	[Table("EadInputAssumptions")]
    [Audited]
    public class EadInputAssumption : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {
		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum Datatype { get; set; }
        public virtual GeneralStatusEnum Status { get; set; }

        public virtual bool IsComputed { get; set; }

        public virtual bool CanAffiliateEdit { get; set; }

        public virtual EadInputAssumptionGroupEnum EadGroup { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }

        public virtual FrameworkEnum Framework { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}