using TestDemo.EclShared;
using TestDemo.Retail;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailResults
{
	[Table("RetailEclResultSummaries")]
    public class RetailEclResultSummary : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual ResultSummaryTypeEnum SummaryType { get; set; }
		
		public virtual string Title { get; set; }
		
		public virtual double? PreOverrideExposure { get; set; }
		
		public virtual double? PreOverrideImpairment { get; set; }
		
		public virtual double? PreOverrideCoverageRatio { get; set; }
		
		public virtual double? PostOverrideExposure { get; set; }
		
		public virtual double? PostOverrideImpairment { get; set; }
		
		public virtual double? PostOverrideCoverageRatio { get; set; }
		

		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
    }
}