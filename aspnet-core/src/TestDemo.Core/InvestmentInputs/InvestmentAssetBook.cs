using TestDemo.InvestmentInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace TestDemo.InvestmentInputs
{
	[Table("InvestmentAssetBooks")]
    [Audited]
    public class InvestmentAssetBook : FullAuditedEntity<Guid> 
    {

		public virtual string AssetDescription { get; set; }
		
		public virtual string AssetType { get; set; }
		
		public virtual string CounterParty { get; set; }
		
		public virtual string SovereignDebt { get; set; }
		
		public virtual string RatingAgency { get; set; }
		
		public virtual string CreditRatingAtPurchaseDate { get; set; }
		
		public virtual string CurrentCreditRating { get; set; }
		
		public virtual double? NominalAmount { get; set; }
		
		public virtual string PrincipalAmortisation { get; set; }
		
		public virtual string RepaymentTerms { get; set; }
		
		public virtual double? CarryAmountNGAAP { get; set; }
		
		public virtual double? CarryingAmountIFRS { get; set; }
		
		public virtual double? Coupon { get; set; }
		
		public virtual double? Eir { get; set; }
		
		public virtual DateTime? PurchaseDate { get; set; }
		
		public virtual DateTime? IssueDate { get; set; }
		
		public virtual double? PurchasePrice { get; set; }
		
		public virtual DateTime? MaturityDate { get; set; }
		
		public virtual double? RedemptionPrice { get; set; }
		
		public virtual string BusinessModelClassification { get; set; }
		
		public virtual double? Ias39Impairment { get; set; }
		
		public virtual string PrudentialClassification { get; set; }
		
		public virtual string ForebearanceFlag { get; set; }
		

		public virtual Guid InvestmentEclUploadId { get; set; }
		
        [ForeignKey("InvestmentEclUploadId")]
		public InvestmentEclUpload InvestmentEclUploadFk { get; set; }
		
    }
}