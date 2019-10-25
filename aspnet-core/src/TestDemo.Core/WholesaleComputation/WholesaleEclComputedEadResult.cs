using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEclComputedEadResults")]
    public class WholesaleEclComputedEadResult : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual string LifetimeEAD { get; set; }
		

		public virtual Guid? WholesaleEclDataLoanBookId { get; set; }
		
        [ForeignKey("WholesaleEclDataLoanBookId")]
		public WholesaleEclDataLoanBook WholesaleEclDataLoanBookFk { get; set; }
		
    }
}