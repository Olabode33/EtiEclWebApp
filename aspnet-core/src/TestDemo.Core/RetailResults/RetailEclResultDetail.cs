using TestDemo.Retail;
using TestDemo.RetailInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.RetailResults
{
	[Table("RetailEclResultDetails")]
    public class RetailEclResultDetail : FullAuditedEntity<Guid> , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string ContractID { get; set; }
		
		public virtual string AccountNo { get; set; }
		
		public virtual string CustomerNo { get; set; }
		
		public virtual string Segment { get; set; }
		
		public virtual string ProductType { get; set; }
		
		public virtual string Sector { get; set; }
		
		public virtual int? Stage { get; set; }
		
		public virtual double? OutstandingBalance { get; set; }
		
		public virtual double? PreOverrideEclBest { get; set; }
		
		public virtual double? PreOverrideEclOptimistic { get; set; }
		
		public virtual double? PreOverrideEclDownturn { get; set; }
		
		public virtual int? OverrideStage { get; set; }
		
		public virtual double? OverrideTTRYears { get; set; }
		
		public virtual double? OverrideFSV { get; set; }
		
		public virtual double? OverrideOverlay { get; set; }
		
		public virtual double? PostOverrideEclBest { get; set; }
		
		public virtual double? PostOverrideEclOptimistic { get; set; }
		
		public virtual double? PostOverrideEclDownturn { get; set; }
		
		public virtual double? PreOverrideImpairment { get; set; }
		
		public virtual double? PostOverrideImpairment { get; set; }
		

		public virtual Guid? RetailEclId { get; set; }
		
        [ForeignKey("RetailEclId")]
		public RetailEcl RetailEclFk { get; set; }
		
		public virtual Guid? RetailEclDataLoanBookId { get; set; }
		
        [ForeignKey("RetailEclDataLoanBookId")]
		public RetailEclDataLoanBook RetailEclDataLoanBookFk { get; set; }
		
    }
}