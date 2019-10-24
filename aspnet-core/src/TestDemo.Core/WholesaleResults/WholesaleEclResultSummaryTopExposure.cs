using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.WholesaleResults
{
	[Table("WholesaleEclResultSummaryTopExposures")]
    public class WholesaleEclResultSummaryTopExposure : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual double? PreOverrideExposure { get; set; }
		
		public virtual double? PreOverrideImpairment { get; set; }
		
		public virtual double? PreOverrideCoverageRatio { get; set; }
		
		public virtual double? PostOverrideExposure { get; set; }
		
		public virtual double? PostOverrideImpairment { get; set; }
		
		public virtual double? PostOverrideCoverageRatio { get; set; }
		
		public virtual string ContractId { get; set; }
		

		public virtual Guid WholesaleEclId { get; set; }
		
        [ForeignKey("WholesaleEclId")]
		public WholesaleEcl WholesaleEclFk { get; set; }
		
		public virtual Guid? WholesaleEclDataLoanBookId { get; set; }
		
        [ForeignKey("WholesaleEclDataLoanBookId")]
		public WholesaleEclDataLoanBook WholesaleEclDataLoanBookFk { get; set; }
		
    }
}