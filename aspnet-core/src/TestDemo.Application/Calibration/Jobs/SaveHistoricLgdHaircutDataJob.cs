using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.CalibrationInput;
using TestDemo.EclShared.Dtos;

namespace TestDemo.Calibration.Jobs
{
    public class SaveHistoricLgdHaircutDataJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {

        private readonly IRepository<CalibrationLgdHairCut, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputLgdHairCut> _calibrationInputRepository;
        private readonly IRepository<CalibrationHistoryLgdHairCut> _calibrationHistoryRepository;

        public SaveHistoricLgdHaircutDataJob(
            IRepository<CalibrationLgdHairCut, Guid> calibrationRepository, 
            IRepository<CalibrationInputLgdHairCut> calibrationInputRepository, 
            IRepository<CalibrationHistoryLgdHairCut> calibrationHistoryRepository)
        {
            _calibrationRepository = calibrationRepository;
            _calibrationInputRepository = calibrationInputRepository;
            _calibrationHistoryRepository = calibrationHistoryRepository;
        }

        [UnitOfWork]
        public override void Execute(ImportCalibrationDataFromExcelJobArgs args)
        {
            var calibration = _calibrationRepository.FirstOrDefault(args.CalibrationId);
            var inputs = GetInputs(args);
            SaveHistoricData(inputs, calibration);
        }

        private List<CalibrationInputLgdHairCut> GetInputs(ImportCalibrationDataFromExcelJobArgs args)
        {
            var inputs = _calibrationInputRepository.GetAllList(e => e.CalibrationId == args.CalibrationId);

            return inputs;
        }

        private void SaveHistoricData(List<CalibrationInputLgdHairCut> inputs, CalibrationLgdHairCut calibration)
        {
            foreach (var item in inputs)
            {
                _calibrationHistoryRepository.Insert(new CalibrationHistoryLgdHairCut
                {
                    Account_No = item.Account_No,
                    AffiliateId = calibration.OrganizationUnitId,
                    Contract_No = item.Contract_No,
                    Customer_No = item.Customer_No,
                    ModelType = calibration.ModelType,
                    Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                    Snapshot_Date = item.Snapshot_Date,
                    Cash_FSV = item.Cash_FSV,
                    Cash_OMV = item.Cash_OMV,
                    Commercial_Property_FSV = item.Commercial_Property_FSV,
                    Commercial_Property_OMV = item.Commercial_Property_OMV,
                    Debenture_FSV = item.Debenture_FSV,
                    Debenture_OMV = item.Debenture_OMV,
                    Inventory_FSV = item.Inventory_FSV,
                    Inventory_OMV = item.Inventory_OMV,
                    Guarantee_Value = item.Guarantee_Value,
                    Period = item.Period,
                    Plant_And_Equipment_FSV = item.Plant_And_Equipment_FSV,
                    Plant_And_Equipment_OMV = item.Plant_And_Equipment_OMV,
                    Residential_Property_FSV = item.Residential_Property_FSV,
                    Receivables_FSV = item.Receivables_FSV,
                    Receivables_OMV = item.Receivables_OMV,
                    Residential_Property_OMV = item.Residential_Property_OMV,
                    Shares_FSV = item.Shares_FSV,
                    Shares_OMV = item.Shares_OMV,
                    Vehicle_FSV = item.Vehicle_FSV,
                    Vehicle_OMV = item.Vehicle_OMV
                });
            }
        }
    }
}
