using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared.Importing.Dto
{
    [Serializable]
    public class SaveEclLoanbookDataFromExcelJobArgs
    {
        public ImportEclDataFromExcelJobArgs Args { get; set; }
        public List<ImportLoanbookDtoNew> Loanbook { get; set; }
    }

    public class ImportLoanbookDtoNew
    {
        public string CustomerNo { get; set; }
        public string AccountNo { get; set; }
        public string ContractNo { get; set; }
        public string CustomerName { get; set; }
        public string SnapshotDate { get; set; } // DateTimeValidation
        public string Segment { get; set; }
        public string Sector { get; set; }
        public string Currency { get; set; }
        public string ProductType { get; set; }
        public string ProductMapping { get; set; }
        public string SpecialisedLending { get; set; }
        public string RatingModel { get; set; }
        public string OriginalRating { get; set; } //int validation
        public string CurrentRating { get; set; } ////int validation
        public string LifetimePD { get; set; }
        public string Month12PD { get; set; }
        public string DaysPastDue { get; set; }
        public string WatchlistIndicator { get; set; }
        public string Classification { get; set; }
        public string ImpairedDate { get; set; }
        public string DefaultDate { get; set; }
        public string CreditLimit { get; set; }  //double validation
        public string OriginalBalanceLCY { get; set; } //double validation
        public string OutstandingBalanceLCY { get; set; } //double validation
        public string OutstandingBalanceACY { get; set; } //double validation
        public string ContractStartDate { get; set; }
        public string ContractEndDate { get; set; }
        public string RestructureIndicator { get; set; }
        public string RestructureRisk { get; set; }
        public string RestructureType { get; set; }
        public string RestructureStartDate { get; set; }
        public string RestructureEndDate { get; set; }
        public string PrincipalPaymentTermsOrigination { get; set; }
        public string PPTOPeriod { get; set; } //int validation
        public string InterestPaymentTermsOrigination { get; set; }
        public string IPTOPeriod { get; set; } //int validation
        public string PrincipalPaymentStructure { get; set; }
        public string InterestPaymentStructure { get; set; }
        public string InterestRateType { get; set; }
        public string BaseRate { get; set; }
        public string OriginationContractualInterestRate { get; set; }
        public string IntroductoryPeriod { get; set; } //int validation
        public string PostIPContractualInterestRate { get; set; } //double validation
        public string CurrentContractualInterestRate { get; set; } //double validation
        public string EIR { get; set; } //double validation
        public string DebentureOMV { get; set; } //double validation
        public string DebentureFSV { get; set; } //double validation
        public string CashOMV { get; set; } //double validation
        public string CashFSV { get; set; } //double validation
        public string InventoryOMV { get; set; } //double validation
        public string InventoryFSV { get; set; } //double validation
        public string PlantEquipmentOMV { get; set; } //double validation
        public string PlantEquipmentFSV { get; set; } //double validation
        public string ResidentialPropertyOMV { get; set; } //double validation
        public string ResidentialPropertyFSV { get; set; } //double validation
        public string CommercialPropertyOMV { get; set; } //double validation
        public string CommercialPropertyFSV { get; set; } //double validation
        public string ReceivablesOMV { get; set; } //double validation
        public string ReceivablesFSV { get; set; } //double validation
        public string SharesOMV { get; set; } //double validation
        public string SharesFSV { get; set; } //double validation
        public string VehicleOMV { get; set; } //double validation
        public string VehicleFSV { get; set; } //double validation
        public string CureRate { get; set; }//double validation
        public string GuaranteeIndicator { get; set; }
        public string GuarantorPD { get; set; }
        public string GuarantorLGD { get; set; } 
        public string GuaranteeValue { get; set; } //double validation
        public string GuaranteeLevel { get; set; } //double validation
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
