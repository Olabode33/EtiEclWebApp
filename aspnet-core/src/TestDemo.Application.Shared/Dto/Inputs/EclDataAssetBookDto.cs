
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.Dto.Inputs
{
    public class EclDataAssetBookDto : EntityDto<Guid>
    {
		public string AssetDescription { get; set; }

		public string AssetType { get; set; }

		public string CounterParty { get; set; }

		public string SovereignDebt { get; set; }

		public string RatingAgency { get; set; }

		public string CreditRatingAtPurchaseDate { get; set; }

		public string CurrentCreditRating { get; set; }

		public double? NominalAmount { get; set; }

		public string PrincipalAmortisation { get; set; }

		public string RepaymentTerms { get; set; }

		public double? CarryAmountNGAAP { get; set; }

		public double? CarryingAmountIFRS { get; set; }

		public double? Coupon { get; set; }

		public double? Eir { get; set; }

		public DateTime? PurchaseDate { get; set; }

		public DateTime? IssueDate { get; set; }

		public double? PurchasePrice { get; set; }

		public DateTime? MaturityDate { get; set; }

		public double? RedemptionPrice { get; set; }

		public string BusinessModelClassification { get; set; }

		public double? Ias39Impairment { get; set; }

		public string PrudentialClassification { get; set; }

		public string ForebearanceFlag { get; set; }


		 public Guid InvestmentEclUploadId { get; set; }

		 
    }
}