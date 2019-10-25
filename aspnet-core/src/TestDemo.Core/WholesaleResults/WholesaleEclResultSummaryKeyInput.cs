using TestDemo.Wholesale;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.WholesaleResults
{
	[Table("WholesaleEclResultSummaryKeyInputs")]
    public class WholesaleEclResultSummaryKeyInput : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual string PDGrouping { get; set; }
		
		public virtual double? Exposure { get; set; }
		
		public virtual double? Collateral { get; set; }
		
		public virtual double? UnsecuredPercentage { get; set; }
		
		public virtual double? PercentageOfBook { get; set; }
		
		public virtual double? Months6CummulativeBestPDs { get; set; }
		
		public virtual double? Months12CummulativeBestPDs { get; set; }
		
		public virtual double? Months24CummulativeBestPDs { get; set; }
		

		public virtual Guid? WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
    }
}