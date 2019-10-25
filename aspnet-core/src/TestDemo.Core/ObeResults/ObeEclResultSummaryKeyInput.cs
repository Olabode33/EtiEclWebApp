using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.ObeResults
{
	[Table("ObeEclResultSummaryKeyInputs")]
    public class ObeEclResultSummaryKeyInput : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string PDGrouping { get; set; }
		
		public virtual double? Exposure { get; set; }
		
		public virtual double? Collateral { get; set; }
		
		public virtual double? UnsecuredPercentage { get; set; }
		
		public virtual double? PercentageOfBook { get; set; }
		
		public virtual double? Months6CummulativeBestPDs { get; set; }
		
		public virtual double? Months12CummulativeBestPDs { get; set; }
		
		public virtual double? Months24CummulativeBestPDs { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}