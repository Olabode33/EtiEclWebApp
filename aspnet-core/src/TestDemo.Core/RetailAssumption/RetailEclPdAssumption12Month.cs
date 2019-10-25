using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;
using Abp.Organizations;

namespace TestDemo.RetailAssumption
{
	[Table("RetailEclPdAssumption12Months")]
    [Audited]
    public class RetailEclPdAssumption12Month : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        [Required]
		public virtual int Credit { get; set; }
		
		public virtual double? PD { get; set; }
		
		public virtual string SnPMappingEtiCreditPolicy { get; set; }
		
		public virtual string SnPMappingBestFit { get; set; }
		
		[Required]
		public virtual bool RequiresGroupApproval { get; set; }
		

		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}