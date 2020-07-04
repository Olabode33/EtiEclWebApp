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
    public class SaveHistoricPdCdDrDataJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {

        private readonly IRepository<CalibrationPdCrDr, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputPdCrDr> _calibrationInputRepository;
        private readonly IRepository<CalibrationHistoryPdCrDr> _calibrationHistoryRepository;

        public SaveHistoricPdCdDrDataJob(
            IRepository<CalibrationPdCrDr, Guid> calibrationRepository, 
            IRepository<CalibrationInputPdCrDr> calibrationInputRepository, 
            IRepository<CalibrationHistoryPdCrDr> calibrationHistoryRepository)
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

        private List<CalibrationInputPdCrDr> GetInputs(ImportCalibrationDataFromExcelJobArgs args)
        {
            var inputs = _calibrationInputRepository.GetAllList(e => e.CalibrationId == args.CalibrationId);

            return inputs;
        }

        private void SaveHistoricData(List<CalibrationInputPdCrDr> inputs, CalibrationPdCrDr calibration)
        {
            foreach (var item in inputs)
            {
                _calibrationHistoryRepository.Insert(new CalibrationHistoryPdCrDr
                {
                    Account_No = item.Account_No,
                    AffiliateId = calibration.OrganizationUnitId,
                    Classification = item.Classification,
                    Contract_End_Date = item.Contract_End_Date,
                    Contract_No = item.Contract_No,
                    Contract_Start_Date = item.Contract_Start_Date,
                    Customer_No = item.Customer_No,
                    ModelType = calibration.ModelType,
                    Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                    Current_Rating = item.Current_Rating,
                    Days_Past_Due = item.Days_Past_Due,
                    Product_Type = item.Product_Type,
                    RAPP_Date = item.RAPP_Date
                });
            }
        }
    }
}
