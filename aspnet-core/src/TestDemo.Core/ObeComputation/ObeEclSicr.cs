using TestDemo.EclShared;
using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.ObeComputation
{
	[Table("ObeEclSicrs")]
    [Audited]
    public class ObeEclSicr : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual int ComputedSICR { get; set; }
		
		public virtual string OverrideSICR { get; set; }
		
		public virtual string OverrideComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid? ObeEclDataLoanBookId { get; set; }
		
        [ForeignKey("ObeEclDataLoanBookId")]
		public ObeEclDataLoanBook ObeEclDataLoanBookFk { get; set; }
		
    }
}