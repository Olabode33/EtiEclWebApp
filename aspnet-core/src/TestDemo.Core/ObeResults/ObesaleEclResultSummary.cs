using TestDemo.EclShared;
using TestDemo.OBE;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.ObeResults
{
	[Table("ObesaleEclResultSummaries")]
    public class ObesaleEclResultSummary : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual ResultSummaryTypeEnum SummaryType { get; set; }
		
		public virtual string Title { get; set; }
		
		public virtual double? PreOverrideExposure { get; set; }
		
		public virtual double? PreOverrideImpairment { get; set; }
		
		public virtual double? PreOverrideCoverageRatio { get; set; }
		
		public virtual double? PostOverrideExposure { get; set; }
		
		public virtual double? PostOverrideImpairment { get; set; }
		
		public virtual double? PostOverrideCoverageRatio { get; set; }
		

		public virtual Guid? ObeEclId { get; set; }
		
        [ForeignKey("ObeEclId")]
		public ObeEcl ObeEclFk { get; set; }
		
    }
}