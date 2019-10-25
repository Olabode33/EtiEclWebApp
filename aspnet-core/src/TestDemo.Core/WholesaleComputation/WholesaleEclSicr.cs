using TestDemo.EclShared;
using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.WholesaleComputation
{
	[Table("WholesaleEclSicrs")]
    [Audited]
    public class WholesaleEclSicr : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        [Required]
		public virtual int ComputedSICR { get; set; }
		
		public virtual string OverrideSICR { get; set; }
		
		public virtual string OverrideComment { get; set; }
		
		public virtual GeneralStatusEnum Status { get; set; }
		

		public virtual Guid WholesaleEclDataLoanBookId { get; set; }
		
        [ForeignKey("WholesaleEclDataLoanBookId")]
		public WholesaleEclDataLoanBook WholesaleEclDataLoanBookFk { get; set; }
		
    }
}