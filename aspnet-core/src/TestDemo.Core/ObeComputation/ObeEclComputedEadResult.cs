using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeComputation
{
	[Table("ObeEclComputedEadResults")]
    public class ObeEclComputedEadResult : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string LifetimeEAD { get; set; }
		

		public virtual Guid? ObeEclDataLoanBookId { get; set; }
		
        [ForeignKey("ObeEclDataLoanBookId")]
		public ObeEclDataLoanBook ObeEclDataLoanBookFk { get; set; }
		
    }
}