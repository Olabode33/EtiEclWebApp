using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EclLibrary.BaseEngine.AssumptionBase
{
    public class AssumptionBase : FullAuditedEntity<Guid>, IMustHaveOrganizationUnit
    {
        public virtual long OrganizationUnitId { get; set; }
		public virtual DataTypeEnum DataType { get; set; }
		[Required]
		public virtual bool IsComputed { get; set; }
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		public virtual bool CanAffiliateEdit { get; set; }
		public virtual GeneralStatusEnum Status { get; set; }
	}
}
