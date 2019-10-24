using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.WholesaleAssumption
{
	[Table("WholesaleEclPdAssumption12Months")]
    [Audited]
    public class WholesaleEclPdAssumption12Month : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual int Credit { get; set; }
		
		public virtual double? PD { get; set; }
		
		public virtual string SnPMappingEtiCreditPolicy { get; set; }
		
		public virtual string SnPMappingBestFit { get; set; }
		
		public virtual bool RequiresGroupApproval { get; set; }
		

		public virtual Guid? WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}