using TestDemo.ObeInputs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace TestDemo.ObeInputs
{
	[Table("ObeEclDataLoanBooks")]
    public class ObeEclDataLoanBook : FullAuditedEntity<Guid> , IMayHaveTenant, IMustHaveOrganizationUnit
    {
			public int? TenantId { get; set; }
        public virtual long OrganizationUnitId { get; set; }


        public virtual string CustomerNo { get; set; }
		
		public virtual string AccountNo { get; set; }
		
		public virtual string ContractNo { get; set; }
		
		public virtual string CustomerName { get; set; }
		
		public virtual DateTime? SnapshotDate { get; set; }
		
		public virtual string Segment { get; set; }
		
		public virtual string Sector { get; set; }
		
		public virtual string Currency { get; set; }
		
		public virtual string ProductType { get; set; }
		
		public virtual string ProductMapping { get; set; }
		
		public virtual string SpecialisedLending { get; set; }
		
		public virtual string RatingModel { get; set; }
		
		public virtual int? OriginalRating { get; set; }
		
		public virtual int? CurrentRating { get; set; }
		
		public virtual double? LifetimePD { get; set; }
		
		public virtual double? Month12PD { get; set; }
		
		public virtual int? DaysPastDue { get; set; }
		
		[Required]
		public virtual bool WatchlistIndicator { get; set; }
		
		public virtual string Classification { get; set; }
		
		public virtual DateTime? ImpairedDate { get; set; }
		
		public virtual DateTime? DefaultDate { get; set; }
		
		public virtual double? CreditLimit { get; set; }
		
		public virtual double? OriginalBalanceLCY { get; set; }
		
		public virtual double? OutstandingBalanceLCY { get; set; }
		
		public virtual double? OutstandingBalanceACY { get; set; }
		
		public virtual DateTime? ContractStartDate { get; set; }
		
		public virtual DateTime? ContractEndDate { get; set; }
		
		[Required]
		public virtual bool RestructureIndicator { get; set; }
		
		public virtual string RestructureRisk { get; set; }
		
		public virtual string RestructureType { get; set; }
		
		public virtual DateTime? RestructureStartDate { get; set; }
		
		public virtual DateTime? RestructureEndDate { get; set; }
		
		public virtual string PrincipalPaymentTermsOrigination { get; set; }
		
		public virtual int? PPTOPeriod { get; set; }
		
		public virtual string InterestPaymentTermsOrigination { get; set; }
		
		public virtual int? IPTOPeriod { get; set; }
		
		public virtual string PrincipalPaymentStructure { get; set; }
		
		public virtual string InterestPaymentStructure { get; set; }
		
		public virtual string InterestRateType { get; set; }
		
		public virtual string BaseRate { get; set; }
		
		public virtual string OriginationContractualInterestRate { get; set; }
		
		public virtual int? IntroductoryPeriod { get; set; }
		
		public virtual double? PostIPContractualInterestRate { get; set; }
		
		public virtual double? CurrentContractualInterestRate { get; set; }
		
		public virtual double? EIR { get; set; }
		
		public virtual double? DebentureOMV { get; set; }
		
		public virtual double? DebentureFSV { get; set; }
		
		public virtual double? CashOMV { get; set; }
		
		public virtual double? CashFSV { get; set; }
		
		public virtual double? InventoryOMV { get; set; }
		
		public virtual double? InventoryFSV { get; set; }
		
		public virtual double? PlantEquipmentOMV { get; set; }
		
		public virtual double? PlantEquipmentFSV { get; set; }
		
		public virtual double? ResidentialPropertyOMV { get; set; }
		
		public virtual double? ResidentialPropertyFSV { get; set; }
		
		public virtual double? CommercialPropertyOMV { get; set; }
		
		public virtual double? CommercialProperty { get; set; }
		
		public virtual double? ReceivablesOMV { get; set; }
		
		public virtual double? ReceivablesFSV { get; set; }
		
		public virtual double? SharesOMV { get; set; }
		
		public virtual double? SharesFSV { get; set; }
		
		public virtual double? VehicleOMV { get; set; }
		
		public virtual double? VehicleFSV { get; set; }
		
		public virtual double? CureRate { get; set; }
		
		[Required]
		public virtual bool GuaranteeIndicator { get; set; }
		
		public virtual string GuarantorPD { get; set; }
		
		public virtual string GuarantorLGD { get; set; }
		
		public virtual double? GuaranteeValue { get; set; }
		
		public virtual double? GuaranteeLevel { get; set; }
		
		public virtual string ContractId { get; set; }
		

		public virtual Guid? ObeEclUploadId { get; set; }
		
        [ForeignKey("ObeEclUploadId")]
		public ObeEclUpload ObeEclUploadFk { get; set; }
		
    }
}