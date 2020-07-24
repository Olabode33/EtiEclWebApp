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

namespace TestDemo.EclShared.Jobs
{
    public class EraseEclJob : BackgroundJob<EraserJobArgs>, ITransientDependency
    {
        private readonly IEclCustomRepository _customRepository;

        public EraseEclJob(IEclCustomRepository customRepository)
        {
            _customRepository = customRepository;
        }

        [UnitOfWork]
        public override void Execute(EraserJobArgs args)
        {
            switch (args.EraseType)
            {
                case EclShared.TrackTypeEnum.Retail:
                case EclShared.TrackTypeEnum.Wholesale:
                case EclShared.TrackTypeEnum.Obe:
                    EraseOthers(args);
                    break;
                case EclShared.TrackTypeEnum.Investment:
                    EraseInvestment(args);
                    break;
                default:
                    break;
            }
        }

        [UnitOfWork]
        private void EraseOthers(EraserJobArgs args)
        {
            EraseAssumptions(args);
            EraseInputData(args);
            EraseComputation(args);
            EraseResults(args);
        }

        private void EraseAssumptions(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclFrameworkAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclEadAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclLgdAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumption12Months, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumptionMacroeconomicInputs, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumptionMacroeconomicProjections, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumptionNonInternalModels, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumptionNplIndexes, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclPdSnPCummulativeDefaultRates, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdAssumptionNonInternalModels, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
        }

        private void EraseInputData(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclDataLoanBooks, args.EraseType.ToString() + DbHelperConst.COL_EclUploadId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclDataPaymentSchedules, args.EraseType.ToString() + DbHelperConst.COL_EclUploadId, args.GuidId.ToString()));
        }
        private void EraseComputation(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EadCirProjections, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EadEirProjections, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EadInputs, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EadLifetimeProjections, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_LGDAccountData, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_LGDCollateral, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_LgdCollateralProjection, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_LgdCollateralTypeDatas, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PDCreditIndex, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdLifetimeBests, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdLifetimeDownturns, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdLifetimeOptimistics, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdMappings, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdRedefaultLifetimeBests, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdRedefaultLifetimeDownturns, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_PdRedefaultLifetimeOptimistics, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
        }
        private void EraseResults(EraserJobArgs args)
        {
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_ECLFrameworkFinal, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_ECLFrameworkFinalOverride, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(args.EraseType.ToString() + DbHelperConst.TB_SUFFIX_EclFramworkReportDetail, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
        }

        [UnitOfWork]
        private void EraseInvestment(EraserJobArgs args)
        {

            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_EadInputAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_LgdInputAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_PdFitchDefaultRates, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_PdInputAssumptions, args.EraseType.ToString() + DbHelperConst.COL_EclId, args.GuidId.ToString()));

            AsyncHelper.RunSync(() => _customRepository.DeleteExistingRecordsCustomInvestmentAssetBooks(args.GuidId.ToString()));

            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_DiscountFactor, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_EadInputs, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_EadLifetimes, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_PdLifetime, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_Sicr, DbHelperConst.COL_EclId, args.GuidId.ToString()));

            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_MonthlyResults, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_MonthlyPostOverrideResults, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_FinalResult, DbHelperConst.COL_EclId, args.GuidId.ToString()));
            AsyncHelper.RunSync(() => _customRepository.DeleteExistingInputRecords(DbHelperConst.TB_INVSEC_FinalPostOverrideResults, DbHelperConst.COL_EclId, args.GuidId.ToString()));
        }

    }
}
