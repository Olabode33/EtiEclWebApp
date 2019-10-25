using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.RetailResults
{
	[Table("RetailEclResultSummaryKeyInputs")]
    public class RetailEclResultSummaryKeyInput : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
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
		

		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}