﻿
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoAssetBook.Dtos
{
    public class CreateOrEditAssetBookDto : EntityDto<Guid?>
    {

		public string Entity { get; set; }
		
		
		public string AssetDescription { get; set; }
		
		
		public string AssetType { get; set; }
		
		
		public string RatingAgency { get; set; }
		
		
		public string PurchaseDateCreditRating { get; set; }
		
		
		public string CurrentCreditRating { get; set; }
		
		
		public double NominalAmountACY { get; set; }
		
		
		public double NominalAmountLCY { get; set; }
		
		
		public string PrincipalAmortisation { get; set; }
		
		
		public string PrincipalRepaymentTerms { get; set; }
		
		
		public string InterestRepaymentTerms { get; set; }
		
		
		public double OutstandingBalanceACY { get; set; }
		
		
		public double OutstandingBalanceLCY { get; set; }
		
		
		public double Coupon { get; set; }
		
		
		public double EIR { get; set; }
		
		
		public string LoanOriginationDate { get; set; }
		
		
		public string LoanMaturityDate { get; set; }
		
		
		public int DaysPastDue { get; set; }
		
		
		public string PrudentialClassification { get; set; }
		
		
		public string ForebearanceFlag { get; set; }

		public Guid RegistrationId { get; set; }

	}
}