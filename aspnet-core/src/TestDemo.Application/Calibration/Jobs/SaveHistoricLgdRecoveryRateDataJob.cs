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
    public class SaveHistoricLgdRecoveryRateDataJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {

        private readonly IRepository<CalibrationLgdRecoveryRate, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputLgdRecoveryRate> _calibrationInputRepository;
        private readonly IRepository<CalibrationHistoryLgdRecoveryRate> _calibrationHistoryRepository;

        public SaveHistoricLgdRecoveryRateDataJob(
            IRepository<CalibrationLgdRecoveryRate, Guid> calibrationRepository, 
            IRepository<CalibrationInputLgdRecoveryRate> calibrationInputRepository, 
            IRepository<CalibrationHistoryLgdRecoveryRate> calibrationHistoryRepository)
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

        private List<CalibrationInputLgdRecoveryRate> GetInputs(ImportCalibrationDataFromExcelJobArgs args)
        {
            var inputs = _calibrationInputRepository.GetAllList(e => e.CalibrationId == args.CalibrationId);

            return inputs;
        }

        private void SaveHistoricData(List<CalibrationInputLgdRecoveryRate> inputs, CalibrationLgdRecoveryRate calibration)
        {
            foreach (var item in inputs)
            {
                _calibrationHistoryRepository.Insert(new CalibrationHistoryLgdRecoveryRate
                {
                    Account_No = item.Account_No,
                    AffiliateId = calibration.OrganizationUnitId,
                    Classification = item.Classification,
                    Contract_No = item.Contract_No,
                    Customer_No = item.Customer_No,
                    ModelType = calibration.ModelType,
                    Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                    Account_Name = item.Account_Name,
                    Amount_Recovered = item.Amount_Recovered,
                    Contractual_Interest_Rate = item.Contractual_Interest_Rate,
                    Date_Of_Recovery = item.Date_Of_Recovery,
                    Days_Past_Due = item.Days_Past_Due,
                    Default_Date = item.Default_Date,
                    Product_Type = item.Product_Type,
                    Segment = item.Segment,
                    Type_Of_Recovery = item.Type_Of_Recovery,
                    Serial=item.Serial,
                    CalibrationSourceId = item.Id
                });
            }
        }
    }
}
