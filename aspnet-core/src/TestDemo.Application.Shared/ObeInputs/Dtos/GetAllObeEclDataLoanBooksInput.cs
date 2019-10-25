using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetAllObeEclDataLoanBooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string CustomerNoFilter { get; set; }

		public string AccountNoFilter { get; set; }

		public string ContractNoFilter { get; set; }

		public string CustomerNameFilter { get; set; }

		public DateTime? MaxSnapshotDateFilter { get; set; }
		public DateTime? MinSnapshotDateFilter { get; set; }

		public string SegmentFilter { get; set; }

		public string SectorFilter { get; set; }

		public string CurrencyFilter { get; set; }

		public string ProductTypeFilter { get; set; }

		public string ProductMappingFilter { get; set; }

		public string SpecialisedLendingFilter { get; set; }

		public string RatingModelFilter { get; set; }

		public int? MaxOriginalRatingFilter { get; set; }
		public int? MinOriginalRatingFilter { get; set; }

		public int? MaxCurrentRatingFilter { get; set; }
		public int? MinCurrentRatingFilter { get; set; }

		public double? MaxLifetimePDFilter { get; set; }
		public double? MinLifetimePDFilter { get; set; }

		public double? MaxMonth12PDFilter { get; set; }
		public double? MinMonth12PDFilter { get; set; }

		public int? MaxDaysPastDueFilter { get; set; }
		public int? MinDaysPastDueFilter { get; set; }

		public int WatchlistIndicatorFilter { get; set; }

		public string ClassificationFilter { get; set; }

		public DateTime? MaxImpairedDateFilter { get; set; }
		public DateTime? MinImpairedDateFilter { get; set; }

		public DateTime? MaxDefaultDateFilter { get; set; }
		public DateTime? MinDefaultDateFilter { get; set; }

		public double? MaxCreditLimitFilter { get; set; }
		public double? MinCreditLimitFilter { get; set; }

		public double? MaxOriginalBalanceLCYFilter { get; set; }
		public double? MinOriginalBalanceLCYFilter { get; set; }

		public double? MaxOutstandingBalanceLCYFilter { get; set; }
		public double? MinOutstandingBalanceLCYFilter { get; set; }

		public double? MaxOutstandingBalanceACYFilter { get; set; }
		public double? MinOutstandingBalanceACYFilter { get; set; }

		public DateTime? MaxContractStartDateFilter { get; set; }
		public DateTime? MinContractStartDateFilter { get; set; }

		public DateTime? MaxContractEndDateFilter { get; set; }
		public DateTime? MinContractEndDateFilter { get; set; }

		public int RestructureIndicatorFilter { get; set; }

		public string RestructureRiskFilter { get; set; }

		public string RestructureTypeFilter { get; set; }

		public DateTime? MaxRestructureStartDateFilter { get; set; }
		public DateTime? MinRestructureStartDateFilter { get; set; }

		public DateTime? MaxRestructureEndDateFilter { get; set; }
		public DateTime? MinRestructureEndDateFilter { get; set; }

		public string PrincipalPaymentTermsOriginationFilter { get; set; }

		public int? MaxPPTOPeriodFilter { get; set; }
		public int? MinPPTOPeriodFilter { get; set; }

		public string InterestPaymentTermsOriginationFilter { get; set; }

		public int? MaxIPTOPeriodFilter { get; set; }
		public int? MinIPTOPeriodFilter { get; set; }

		public string PrincipalPaymentStructureFilter { get; set; }

		public string InterestPaymentStructureFilter { get; set; }

		public string InterestRateTypeFilter { get; set; }

		public string BaseRateFilter { get; set; }

		public string OriginationContractualInterestRateFilter { get; set; }

		public int? MaxIntroductoryPeriodFilter { get; set; }
		public int? MinIntroductoryPeriodFilter { get; set; }

		public double? MaxPostIPContractualInterestRateFilter { get; set; }
		public double? MinPostIPContractualInterestRateFilter { get; set; }

		public double? MaxCurrentContractualInterestRateFilter { get; set; }
		public double? MinCurrentContractualInterestRateFilter { get; set; }

		public double? MaxEIRFilter { get; set; }
		public double? MinEIRFilter { get; set; }

		public double? MaxDebentureOMVFilter { get; set; }
		public double? MinDebentureOMVFilter { get; set; }

		public double? MaxDebentureFSVFilter { get; set; }
		public double? MinDebentureFSVFilter { get; set; }

		public double? MaxCashOMVFilter { get; set; }
		public double? MinCashOMVFilter { get; set; }

		public double? MaxCashFSVFilter { get; set; }
		public double? MinCashFSVFilter { get; set; }

		public double? MaxInventoryOMVFilter { get; set; }
		public double? MinInventoryOMVFilter { get; set; }

		public double? MaxInventoryFSVFilter { get; set; }
		public double? MinInventoryFSVFilter { get; set; }

		public double? MaxPlantEquipmentOMVFilter { get; set; }
		public double? MinPlantEquipmentOMVFilter { get; set; }

		public double? MaxPlantEquipmentFSVFilter { get; set; }
		public double? MinPlantEquipmentFSVFilter { get; set; }

		public double? MaxResidentialPropertyOMVFilter { get; set; }
		public double? MinResidentialPropertyOMVFilter { get; set; }

		public double? MaxResidentialPropertyFSVFilter { get; set; }
		public double? MinResidentialPropertyFSVFilter { get; set; }

		public double? MaxCommercialPropertyOMVFilter { get; set; }
		public double? MinCommercialPropertyOMVFilter { get; set; }

		public double? MaxCommercialPropertyFilter { get; set; }
		public double? MinCommercialPropertyFilter { get; set; }

		public double? MaxReceivablesOMVFilter { get; set; }
		public double? MinReceivablesOMVFilter { get; set; }

		public double? MaxReceivablesFSVFilter { get; set; }
		public double? MinReceivablesFSVFilter { get; set; }

		public double? MaxSharesOMVFilter { get; set; }
		public double? MinSharesOMVFilter { get; set; }

		public double? MaxSharesFSVFilter { get; set; }
		public double? MinSharesFSVFilter { get; set; }

		public double? MaxVehicleOMVFilter { get; set; }
		public double? MinVehicleOMVFilter { get; set; }

		public double? MaxVehicleFSVFilter { get; set; }
		public double? MinVehicleFSVFilter { get; set; }

		public double? MaxCureRateFilter { get; set; }
		public double? MinCureRateFilter { get; set; }

		public int GuaranteeIndicatorFilter { get; set; }

		public string GuarantorPDFilter { get; set; }

		public string GuarantorLGDFilter { get; set; }

		public double? MaxGuaranteeValueFilter { get; set; }
		public double? MinGuaranteeValueFilter { get; set; }

		public double? MaxGuaranteeLevelFilter { get; set; }
		public double? MinGuaranteeLevelFilter { get; set; }

		public string ContractIdFilter { get; set; }


		 public string ObeEclUploadTenantIdFilter { get; set; }

		 
    }
}