using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailComputation
{
	[Table("RetailEclComputedEadResults")]
    public class RetailEclComputedEadResult : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string LifetimeEAD { get; set; }
		

		public virtual Guid? RetailEclDataLoanBookId { get; set; }
		
        [ForeignKey("RetailEclDataLoanBookId")]
		public RetailEclDataLoanBook RetailEclDataLoanBookFk { get; set; }
		
    }
}