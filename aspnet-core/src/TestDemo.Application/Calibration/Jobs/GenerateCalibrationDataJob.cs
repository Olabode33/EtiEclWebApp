using Abp.BackgroundJobs;
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;
using TestDemo.EclShared;
using TestDemo.Storage;
using TestDemo.Notifications;
using Abp.Configuration;
using OfficeOpenXml;
using TestDemo.Dto;
using Abp.AspNetZeroCore.Net;
using Abp.Threading;
using Abp.Domain.Uow;
using TestDemo.EclShared.Emailer;
using Abp.Domain.Repositories;
using TestDemo.Authorization.Users;
using Abp.Organizations;
using TestDemo.Wholesale;
using TestDemo.Investment;
using TestDemo.OBE;
using TestDemo.Retail;
using System.IO;
using TestDemo.CalibrationInput;

namespace TestDemo.Calibration.Jobs
{
    public class GenerateCalibrationDataJob : BackgroundJob<GenerateCalibrationDataJobArgs>, ITransientDependency
    {


        private readonly IRepository<CalibrationHistoryEadBehaviouralTerms> _btHistoryRepository;
        private readonly IRepository<CalibrationHistoryEadCcfSummary> _ccfHistoryRepository;
        private readonly IRepository<CalibrationHistoryLgdRecoveryRate> _rrHistoryRepository;
        private readonly IRepository<CalibrationHistoryLgdHairCut> _hcHistoryRepository;
        private readonly IRepository<CalibrationHistoryPdCommsCons> _ccHistoryRepository;
        private readonly IRepository<CalibrationHistoryPdCrDr> _pdHistoryRepository;

        public GenerateCalibrationDataJob(IRepository<CalibrationHistoryEadBehaviouralTerms> btHistoryRepository,
            IRepository<CalibrationHistoryEadCcfSummary> ccfHistoryRepository,
            IRepository<CalibrationHistoryLgdRecoveryRate> rrHistoryRepository,
            IRepository<CalibrationHistoryLgdHairCut> hcHistoryRepository,
            IRepository<CalibrationHistoryPdCommsCons> ccHistoryRepository,
            IRepository<CalibrationHistoryPdCrDr> pdHistoryRepository)
        {

            _btHistoryRepository = btHistoryRepository;
            _ccfHistoryRepository = ccfHistoryRepository;
            _rrHistoryRepository = rrHistoryRepository;
            _hcHistoryRepository = hcHistoryRepository;
            _ccHistoryRepository = ccHistoryRepository;
            _pdHistoryRepository = pdHistoryRepository;
        }


        [UnitOfWork]
        public override void Execute(GenerateCalibrationDataJobArgs args)
        {

            var btData = new List<CalibrationHistoryEadBehaviouralTerms>();
            var ccfData = new List<CalibrationHistoryEadCcfSummary>();
            var hcData = new List<CalibrationHistoryLgdHairCut>();
            var rrData = new List<CalibrationHistoryLgdRecoveryRate>();
            var ccData = new List<CalibrationHistoryPdCommsCons>();
            var pdData = new List<CalibrationHistoryPdCrDr>();
            foreach (var r in args.LoanbookData)
            {
                int? yyyyMMStapShotDate = null;
                if (r.SnapshotDate != null && r.SnapshotDate != default)
                {
                    yyyyMMStapShotDate = Convert.ToInt32(r.SnapshotDate.Value.ToString("yyyyMM"));
                }
                int? currentRating = null;
                if (string.IsNullOrEmpty(r.CurrentRating))
                {
                    currentRating = int.Parse(r.CurrentRating);
                }
                var bt = new CalibrationHistoryEadBehaviouralTerms { Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Classification = r.Classification, Contract_End_Date = r.ContractEndDate, Contract_No = r.ContractNo, Contract_Start_Date = r.ContractStartDate, Customer_Name = r.CustomerName, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, ModelType = FrameworkEnum.Wholesale, Original_Balance_Lcy = r.OriginalBalanceLCY, Outstanding_Balance_Acy = r.OutstandingBalanceACY, Outstanding_Balance_Lcy = r.OutstandingBalanceLCY, Restructure_End_Date = r.RestructureEndDate, Restructure_Indicator = r.RestructureIndicator ? "1" : "0", Restructure_Start_Date = r.RestructureStartDate, Restructure_Type = r.RestructureType, Snapshot_Date = r.SnapshotDate, SourceType = SourceTypeEnum.Loanbook, SourceId = r.Id };
                _btHistoryRepository.InsertAsync(bt);

                var ccf = new CalibrationHistoryEadCcfSummary { Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Classification = r.Classification, Contract_End_Date = r.ContractEndDate, Contract_Start_Date = r.ContractStartDate, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, Limit = r.CreditLimit, ModelType = FrameworkEnum.Wholesale, Outstanding_Balance = r.OutstandingBalanceLCY, Product_Type = r.ProductType, Settlement_Account = r.AccountNo, Snapshot_Date = yyyyMMStapShotDate, SourceId = r.Id, SourceType = SourceTypeEnum.Loanbook };
                _ccfHistoryRepository.InsertAsync(ccf);

                var hc = new CalibrationHistoryLgdHairCut { Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Cash_FSV = r.CashFSV, Cash_OMV = r.CashOMV, Commercial_Property_FSV = r.CommercialProperty, Commercial_Property_OMV = r.CommercialPropertyOMV, Contract_No = r.ContractNo, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, Debenture_FSV = r.DebentureFSV, Debenture_OMV = r.DebentureFSV, Guarantee_Value = r.GuaranteeValue, Inventory_FSV = r.InventoryFSV, Inventory_OMV = r.InventoryOMV, ModelType = FrameworkEnum.Wholesale, Outstanding_Balance_Lcy = r.OutstandingBalanceLCY, Plant_And_Equipment_FSV = r.PlantEquipmentFSV, Plant_And_Equipment_OMV = r.PlantEquipmentOMV, Receivables_FSV = r.ReceivablesFSV, Receivables_OMV = r.ReceivablesOMV, Residential_Property_FSV = r.ResidentialPropertyFSV, Residential_Property_OMV = r.ResidentialPropertyOMV, Shares_FSV = r.SharesFSV, Shares_OMV = r.SharesOMV, Snapshot_Date = r.SnapshotDate, SourceId = r.Id, SourceType = SourceTypeEnum.Loanbook, Vehicle_FSV = r.VehicleFSV, Vehicle_OMV = r.VehicleOMV };
                _hcHistoryRepository.InsertAsync(hc);

                var rr = new CalibrationHistoryLgdRecoveryRate { Account_Name = r.CustomerName, Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Amount_Recovered = r.OriginalBalanceLCY ?? 0 - r.OutstandingBalanceLCY ?? 0, Classification = r.Classification, Contractual_Interest_Rate = r.CurrentContractualInterestRate, Contract_No = r.ContractNo, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, Date_Of_Recovery = r.ImpairedDate, Days_Past_Due = (int?)r.DaysPastDue, ModelType = FrameworkEnum.Wholesale, Outstanding_Balance_Lcy = r.OutstandingBalanceLCY, Default_Date = r.DefaultDate, Product_Type = r.ProductType, Segment = r.Segment, SourceId = r.Id, SourceType = SourceTypeEnum.Loanbook, Type_Of_Recovery = "CASH" };
                _rrHistoryRepository.InsertAsync(rr);

                var cc = new CalibrationHistoryPdCommsCons { Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Classification = r.Classification, Contract_End_Date = r.ContractStartDate, Contract_No = r.ContractNo, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, Days_Past_Due = (int?)r.DaysPastDue, ModelType = FrameworkEnum.Wholesale, Outstanding_Balance_Lcy = r.OutstandingBalanceLCY, Product_Type = r.ProductType, Segment = r.Segment, SourceId = r.Id, SourceType = SourceTypeEnum.Loanbook, Contract_Start_Date = r.ContractStartDate, Current_Rating = currentRating, Snapshot_Date = yyyyMMStapShotDate };
                _ccHistoryRepository.InsertAsync(cc);

                var pd = new CalibrationHistoryPdCrDr { Account_No = r.AccountNo, AffiliateId = r.OrganizationUnitId, Classification = r.Classification, Contract_End_Date = r.ContractStartDate, Contract_No = r.ContractNo, Customer_No = r.CustomerNo, DateCreated = DateTime.Now, Days_Past_Due = (int?)r.DaysPastDue, ModelType = FrameworkEnum.Wholesale, Outstanding_Balance_Lcy = r.OutstandingBalanceLCY, Product_Type = r.ProductType, Segment = r.Segment, SourceId = r.Id, SourceType = SourceTypeEnum.Loanbook, Contract_Start_Date = r.ContractStartDate, Current_Rating = r.CurrentRating, RAPP_Date = yyyyMMStapShotDate };
                _pdHistoryRepository.InsertAsync(pd);

            }

        }


    }
}
