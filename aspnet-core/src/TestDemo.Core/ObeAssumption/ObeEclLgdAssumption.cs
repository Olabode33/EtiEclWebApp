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
	[Table("ObeEclLgdAssumptions")]
    [Audited]
    public class ObeEclLgdAssumption : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum DataType { get; set; }
		
		[Required]
		public virtual bool IsComputed { get; set; }
		
		public virtual LdgInputAssumptionEnum LgdGroup { get; set; }
		
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}