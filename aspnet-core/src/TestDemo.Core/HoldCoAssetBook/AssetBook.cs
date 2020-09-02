using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace TestDemo.HoldCoAssetBook
{
	[Table("AssetBooks")]
    public class AssetBook : FullAuditedEntity<Guid> 
    {

		public virtual string Entity { get; set; }
		
		public virtual string AssetDescription { get; set; }
		
		public virtual string AssetType { get; set; }
		
		public virtual string RatingAgency { get; set; }
		
		public virtual string PurchaseDateCreditRating { get; set; }
		
		public virtual string CurrentCreditRating { get; set; }
		
		public virtual double NominalAmountACY { get; set; }
		
		public virtual double NominalAmountLCY { get; set; }
		
		public virtual string PrincipalAmortisation { get; set; }
		
		public virtual string PrincipalRepaymentTerms { get; set; }
		
		public virtual string InterestRepaymentTerms { get; set; }
		
		public virtual double OutstandingBalanceACY { get; set; }
		
		public virtual double OutstandingBalanceLCY { get; set; }
		
		public virtual double Coupon { get; set; }
		
		public virtual double EIR { get; set; }
		
		public virtual string LoanOriginationDate { get; set; }
		
		public virtual string LoanMaturityDate { get; set; }
		
		public virtual int DaysPastDue { get; set; }
		
		public virtual string PrudentialClassification { get; set; }
		
		public virtual string ForebearanceFlag { get; set; }
		
		public virtual Guid RegistrationId { get; set; }
		

    }
}