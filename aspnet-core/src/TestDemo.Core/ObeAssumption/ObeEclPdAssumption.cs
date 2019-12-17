using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclPdAssumptions")]
    [Audited]
    public class ObeEclPdAssumption : FullAuditedEntity<Guid> 
    {

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		
		public virtual PdInputAssumptionGroupEnum PdGroup { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual bool CanAffiliateEdit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		
		public virtual long OrganizationUnitId { get; set; }
		

		public virtual Guid ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}