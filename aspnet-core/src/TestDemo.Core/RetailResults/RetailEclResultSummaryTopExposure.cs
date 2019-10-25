using TestDemo.Retail;
using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.RetailResults
{
	[Table("RetailEclResultSummaryTopExposures")]
    public class RetailEclResultSummaryTopExposure : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual double? PreOverrideExposure { get; set; }
		
		public virtual double? PreOverrideImpairment { get; set; }
		
		public virtual double? PreOverrideCoverageRatio { get; set; }
		
		public virtual double? PostOverrideExposure { get; set; }
		
		public virtual double? PostOverrideImpairment { get; set; }
		
		public virtual double? PostOverrideCoverageRatio { get; set; }
		

		public virtual Guid RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
		public virtual Guid? RetailEclDataLoanBookId { get; set; }
		
        [ForeignKey("RetailEclDataLoanBookId")]
		public RetailEclDataLoanBook RetailEclDataLoanBookFk { get; set; }
		
    }
}