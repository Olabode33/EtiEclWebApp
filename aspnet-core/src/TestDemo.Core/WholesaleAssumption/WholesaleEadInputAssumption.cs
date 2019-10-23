using TestDemo.EclShared;
using TestDemo.EclShared;
using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.WholesaleAssumption
{
	[Table("WholesaleEadInputAssumptions")]
    [Audited]
    public class WholesaleEclEadInputAssumption : Entity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Key { get; set; }
		
		public virtual string InputName { get; set; }
		
		public virtual string Value { get; set; }
		
		public virtual DataTypeEnum Datatype { get; set; }
		
		public virtual bool IsComputed { get; set; }
		
		public virtual EadInputGroupEnum EadGroup { get; set; }
		

		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}