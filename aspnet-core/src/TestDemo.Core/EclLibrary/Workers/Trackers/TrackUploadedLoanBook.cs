using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackUploadedLoanBook : Entity,  IMustHaveOrganizationUnit
    {
		public virtual Guid EclId { get; set; }
		public virtual long OrganizationUnitId { get; set; }

		public virtual string CustomerNo { get; set; }

		public virtual string AccountNo { get; set; }

		public virtual string ContractNo { get; set; }

		public virtual string CustomerName { get; set; }

		public virtual string SnapshotDate { get; set; }

		public virtual string Segment { get; set; }

		public virtual string Sector { get; set; }

		public virtual string Currency { get; set; }

		public virtual string ProductType { get; set; }

		public virtual string ProductMapping { get; set; }

		public virtual string SpecialisedLending { get; set; }

		public virtual string RatingModel { get; set; }

		public virtual string OriginalRating { get; set; }

		public virtual string CurrentRating { get; set; }

		public virtual string LifetimePD { get; set; }

		public virtual string Month12PD { get; set; }

		public virtual string DaysPastDue { get; set; }

		public virtual bool WatchlistIndicator { get; set; }

		public virtual string Classification { get; set; }

		public virtual string ImpairedDate { get; set; }

		public virtual string DefaultDate { get; set; }

		public virtual string CreditLimit { get; set; }

		public virtual string OriginalBalanceLCY { get; set; }

		public virtual string OutstandingBalanceLCY { get; set; }

		public virtual string OutstandingBalanceACY { get; set; }

		public virtual string ContractStartDate { get; set; }

		public virtual string ContractEndDate { get; set; }

		public virtual bool RestructureIndicator { get; set; }

		public virtual string RestructureRisk { get; set; }

		public virtual string RestructureType { get; set; }

		public virtual string RestructureStartDate { get; set; }

		public virtual string RestructureEndDate { get; set; }

		public virtual string PrincipalPaymentTermsOrigination { get; set; }

		public virtual string PPTOPeriod { get; set; }

		public virtual string InterestPaymentTermsOrigination { get; set; }

		public virtual string IPTOPeriod { get; set; }

		public virtual string PrincipalPaymentStructure { get; set; }

		public virtual string InterestPaymentStructure { get; set; }

		public virtual string InterestRateType { get; set; }

		public virtual string BaseRate { get; set; }

		public virtual string OriginationContractualInterestRate { get; set; }

		public virtual string IntroductoryPeriod { get; set; }

		public virtual string PostIPContractualInterestRate { get; set; }

		public virtual string CurrentContractualInterestRate { get; set; }

		public virtual string EIR { get; set; }

		public virtual string DebentureOMV { get; set; }

		public virtual string DebentureFSV { get; set; }

		public virtual string CashOMV { get; set; }

		public virtual string CashFSV { get; set; }

		public virtual string InventoryOMV { get; set; }

		public virtual string InventoryFSV { get; set; }

		public virtual string PlantEquipmentOMV { get; set; }

		public virtual string PlantEquipmentFSV { get; set; }

		public virtual string ResidentialPropertyOMV { get; set; }

		public virtual string ResidentialPropertyFSV { get; set; }

		public virtual string CommercialPropertyOMV { get; set; }

		public virtual string CommercialProperty { get; set; }

		public virtual string ReceivablesOMV { get; set; }

		public virtual string ReceivablesFSV { get; set; }

		public virtual string SharesOMV { get; set; }

		public virtual string SharesFSV { get; set; }

		public virtual string VehicleOMV { get; set; }

		public virtual string VehicleFSV { get; set; }

		public virtual string CureRate { get; set; }

		public virtual bool GuaranteeIndicator { get; set; }

		public virtual string GuarantorPD { get; set; }

		public virtual string GuarantorLGD { get; set; }

		public virtual string GuaranteeValue { get; set; }

		public virtual string GuaranteeLevel { get; set; }

		public virtual string ContractId { get; set; }
		public virtual string Exception { get; set; }

	}
}
