using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeAssumption
{
	[Table("ObeEclPdSnPCummulativeDefaultRates")]
    [Audited]
    public class ObeEclPdSnPCummulativeDefaultRate : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Key { get; set; }
		
		public virtual string Rating { get; set; }
		
		public virtual int? Years { get; set; }
		
		public virtual double? Value { get; set; }
		
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}