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
    public class SaveHistoricEadBehaviouralDataJob : BackgroundJob<ImportCalibrationDataFromExcelJobArgs>, ITransientDependency
    {

        private readonly IRepository<CalibrationEadBehaviouralTerm, Guid> _calibrationRepository;
        private readonly IRepository<CalibrationInputEadBehaviouralTerms> _calibrationInputRepository;
        private readonly IRepository<CalibrationHistoryEadBehaviouralTerms> _calibrationHistoryRepository;

        public SaveHistoricEadBehaviouralDataJob(
            IRepository<CalibrationEadBehaviouralTerm, Guid> calibrationRepository, 
            IRepository<CalibrationInputEadBehaviouralTerms> calibrationInputRepository, 
            IRepository<CalibrationHistoryEadBehaviouralTerms> calibrationHistoryRepository)
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

        private List<CalibrationInputEadBehaviouralTerms> GetInputs(ImportCalibrationDataFromExcelJobArgs args)
        {
            var inputs = _calibrationInputRepository.GetAllList(e => e.CalibrationId == args.CalibrationId);

            return inputs;
        }

        private void SaveHistoricData(List<CalibrationInputEadBehaviouralTerms> inputs, CalibrationEadBehaviouralTerm calibration)
        {
            foreach (var item in inputs)
            {
                _calibrationHistoryRepository.Insert(new CalibrationHistoryEadBehaviouralTerms
                {
                    Account_No = item.Account_No,
                    AffiliateId = calibration.OrganizationUnitId,
                    Classification = item.Classification,
                    Contract_End_Date = item.Contract_End_Date,
                    Contract_No = item.Contract_No,
                    Contract_Start_Date = item.Contract_Start_Date,
                    Customer_Name = item.Customer_Name,
                    Customer_No = item.Customer_No,
                    ModelType = calibration.ModelType,
                    Original_Balance_Lcy = item.Original_Balance_Lcy,
                    Outstanding_Balance_Acy = item.Outstanding_Balance_Acy,
                    Outstanding_Balance_Lcy = item.Outstanding_Balance_Lcy,
                    Restructure_End_Date = item.Restructure_End_Date,
                    Serial = item.Serial,
                    Restructure_Indicator = item.Restructure_Indicator,
                    Restructure_Start_Date = item.Restructure_Start_Date,
                    Restructure_Type = item.Restructure_Type,
                    Snapshot_Date = item.Snapshot_Date,
                    SourceType = EclShared.SourceTypeEnum.CalibrationUpload,
                    CalibrationSourceId = item.Id
                });
            }
        }
    }
}
