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
    public class SaveHistoricEadCcfSummaryDataJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {

        private readonly IRepository<CalibrationEadCcfSummary, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputEadCcfSummary> _calibrationInputRepository;
        private readonly IRepository<CalibrationHistoryEadCcfSummary> _calibrationHistoryRepository;

        public SaveHistoricEadCcfSummaryDataJob(
            IRepository<CalibrationEadCcfSummary, Guid> calibrationRepository, 
            IRepository<CalibrationInputEadCcfSummary> calibrationInputRepository, 
            IRepository<CalibrationHistoryEadCcfSummary> calibrationHistoryRepository)
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

        private List<CalibrationInputEadCcfSummary> GetInputs(ImportCalibrationDataFromExcelJobArgs args)
        {
            var inputs = _calibrationInputRepository.GetAllList(e => e.CalibrationId == args.CalibrationId);

            return inputs;
        }

        private void SaveHistoricData(List<CalibrationInputEadCcfSummary> inputs, CalibrationEadCcfSummary calibration)
        {
            foreach (var item in inputs)
            {
                _calibrationHistoryRepository.Insert(new CalibrationHistoryEadCcfSummary
                {
                    Account_No = item.Account_No,
                    AffiliateId = calibration.OrganizationUnitId,
                    Classification = item.Classification,
                    Contract_End_Date = item.Contract_End_Date,
                    Contract_Start_Date = item.Contract_Start_Date,
                    Customer_No = item.Customer_No,
                    ModelType = calibration.ModelType,
                    Snapshot_Date = item.Snapshot_Date,
                    Limit = item.Limit,
                    Outstanding_Balance = item.Outstanding_Balance,
                    Product_Type = item.Product_Type,
                    Settlement_Account = item.Settlement_Account,
                    Serial=item.Serial,                   
                    SourceType = EclShared.SourceTypeEnum.CalibrationUpload,
                    CalibrationSourceId = item.Id
                });
            }
        }
    }
}
