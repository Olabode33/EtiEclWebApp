using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.CalibrationInput;
using TestDemo.Common;
using TestDemo.EclShared.Dtos;
using TestDemo.InvestmentComputation;

namespace TestDemo.Calibration.Jobs
{
    public class EraseCalibrationJob : BackgroundJob<EraserJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _customRepository;

        public EraseCalibrationJob(IEclCustomRepository customRepository)
        {
            _customRepository = customRepository;
        }

        [UnitOfWork]
        public override void Execute(EraserJobArgs args)
        {
            switch (args.EraseType)
            {
                case EclShared.TrackTypeEnum.CalibrateBehaviouralTerm:
                    EraseBehavioralTerm(args);
                    break;
                case EclShared.TrackTypeEnum.CalibrateCcfSummary:
                    EraseCcfSummary(args);
                    break;
                case EclShared.TrackTypeEnum.CalibrateHaircut:
                    EraseHaircut(args);
                    break;
                case EclShared.TrackTypeEnum.CalibrateRecoveryRate:
                    EraseRecoveryRate(args);
                    break;
                case EclShared.TrackTypeEnum.CalibratePdCrDr:
                    ErasePdCrDr(args);
                    break;
                case EclShared.TrackTypeEnum.MacroAnalysis:
                    EraseMacro(args);
                    break;

                default:
                    break;
            }
        }

        [UnitOfWork]
        private void EraseBehavioralTerm(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputBehaviouralTerm, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultBehaviouralTerm, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
        }

        [UnitOfWork]
        private void EraseCcfSummary(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputCcfSummary, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultCcfSummary, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
        }

        [UnitOfWork]
        private void EraseHaircut(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputHaircut, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultHaircut, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultHaircutSummary, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
        }

        [UnitOfWork]
        private void EraseRecoveryRate(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputRecoveryRate, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultRecoveryRate, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
        }

        [UnitOfWork]
        private void ErasePdCrDr(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputPdCrDr, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultPd12Months, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationResultPd12MonthsSummary, DbHelperConst.COL_CalibrationId, args.GuidId.ToString()));
            
        }

        [UnitOfWork]
        private void EraseMacro(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_CalibrationInputMacroeconomicData, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultCorMat, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultIndexData, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultPrincipalComponent, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultPrincipalComponentSummary, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultSelectedMacroVariables, DbHelperConst.COL_MacroId, args.IntId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_MacroResultStatistics, DbHelperConst.COL_MacroId, args.IntId.ToString()));
        }
    }
}
