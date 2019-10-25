using TestDemo.EclShared;
using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.RetailComputation
{
	[Table("RetailEclSicrs")]
    [Audited]
    public class RetailEclSicr : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual int ComputedSICR { get; set; }
		
		public virtual string OverrideSICR { get; set; }
		
		public virtual string OverrideComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid RetailEclDataLoanBookId { get; set; }
		
        [ForeignKey("RetailEclDataLoanBookId")]
		public RetailEclDataLoanBook RetailEclDataLoanBookFk { get; set; }
		
    }
}