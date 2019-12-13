using EclEngine.BaseEclEngine.PdInput;
using System.Data;
using System.Linq;
using EclEngine.Utils;
using System;

namespace EclEngine.BaseEclEngine.Framework.Calculations
{
    public class SicrWorkings
    {
        PdInputResult _pdInput = new PdInputResult();

        public void Run()
        {
            DataTable dataTable = ComputeStageClassification();
            string stop = "Ma te";
        }
        public DataTable ComputeStageClassification()
        {
            DataTable stageClassification = new DataTable();
            stageClassification.Columns.Add(StageClassificationColumns.ContractId, typeof(string));
            stageClassification.Columns.Add(StageClassificationColumns.Stage, typeof(int));

            DataTable loanbook = GetLoanBookData();
            DataTable sicrInput = GetSicrInputResult();
            DataTable assumption = GetImpairmentAssumptionsData();
            DataTable pdMapping = GetPdMappingResult();

            foreach (DataRow row in sicrInput.Rows)
            {
                DataRow loanbookRecord = loanbook.AsEnumerable().FirstOrDefault(x => x.Field<string>(LoanBookColumns.ContractID) == row.Field<string>(SicrInputsColumns.ContractId));
                DataRow pdMappingRecord = pdMapping.AsEnumerable().FirstOrDefault(x => x.Field<string>(LoanBookColumns.ContractID) == row.Field<string>(SicrInputsColumns.ContractId));

                DataRow newRow = stageClassification.NewRow();
                newRow[StageClassificationColumns.ContractId] = row.Field<string>(SicrInputsColumns.ContractId);
                newRow[StageClassificationColumns.Stage] = ComputeStage(row, loanbookRecord, assumption, pdMappingRecord.Field<string>(PdMappingColumns.PdGroup));

                stageClassification.Rows.Add(newRow);
            }


            return stageClassification;
        }

        protected int ComputeStage(DataRow sicrInputRecord, DataRow loanBookRecord, DataTable assumption, string pdMapping)
        {
            int pdAbsoluteScore = ComputePdAbsoluteScore(sicrInputRecord, loanBookRecord, assumption);
            int pdRelativeScore = ComputePdRelativeScore(sicrInputRecord, loanBookRecord, assumption);
            int creditRatingScore = ComputeCreditRatingScore(loanBookRecord, assumption);
            int watchlistScore = ComputeWatchlistIndicatorScore(loanBookRecord, assumption);
            int restructureScore = ComputeRestructureIndicatorScore(loanBookRecord, assumption);
            int forwardScore = ComputeForwardScore(sicrInputRecord, loanBookRecord, assumption);
            int backwardScore = ComputeBackwardScore(sicrInputRecord, assumption);
            int expDefault = ComputeExpDefaultScore(pdMapping);

            int maxScore1 = Math.Max(pdAbsoluteScore, pdRelativeScore);
            int maxScore2 = Math.Max(creditRatingScore, watchlistScore);
            int maxScore3 = Math.Max(forwardScore, backwardScore);
            int maxScore4 = Math.Max(restructureScore, expDefault);

            int maxScore5 = Math.Max(maxScore1, maxScore2);
            int maxScore6 = Math.Max(maxScore3, maxScore4);

            return Math.Max(maxScore5, maxScore6);
        }
        protected int ComputeExpDefaultScore(string pdMapping)
        {
            return pdMapping == "EXP" ? 3 : 0;
        }
        protected int ComputeBackwardScore(DataRow sicrInputRecord, DataTable assumption)
        {
            double stage2to1Backward = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.BackwardTransitionsStage2to1));
            double stage3to2Backward = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.BackwardTransitionsStage3to2));
            long stage1Transition = sicrInputRecord.Field<long>(SicrInputsColumns.Stage1Transition);
            long stage2Transition = sicrInputRecord.Field<long>(SicrInputsColumns.Stage2Transition);

            if (stage2Transition < stage3to2Backward && stage2Transition != 0)
            {
                return 3;
            }
            else
            {
                if ((stage1Transition < stage2to1Backward && stage1Transition != 0) || (stage2Transition < stage3to2Backward + stage2to1Backward && stage2Transition != 0))
                {
                    return 2;
                } 
                else
                {
                    return 1;
                }
            }
        }
        protected int ComputeForwardScore(DataRow sicrInputRecord, DataRow loanbookRecord, DataTable assumption)
        {

            long currentRating = loanbookRecord.Field<long>(LoanBookColumns.CurrentRating);
            double currentCreditRankRating = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRank + currentRating.ToString()).Replace("-", "").Replace("+", ""));
            double stage2to3creditRating = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingDefaultIndicator));
            double stage1to2Forward = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.ForwardTransitionStage1to1));
            double stage2to3Forward = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.ForwardTransitionStage2to3));
            long daysPastDue = sicrInputRecord.Field<long>(SicrInputsColumns.DaysPastDue);

            if (currentCreditRankRating < stage2to3creditRating)
            {
                return daysPastDue < stage1to2Forward ? 1 : (daysPastDue > stage2to3Forward ? 3 : 2);
            }
            else
            {
                return 3;
            }
        }
        protected int ComputeRestructureIndicatorScore(DataRow loabbookRecord, DataTable assumption)
        {
            string useRestructureIndicator = GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.UseRestructureIndicator);
            if (useRestructureIndicator.ToLower() == "yes")
            {
                return loabbookRecord.Field<bool>(LoanBookColumns.RestructureIndicator) 
                        && loabbookRecord.Field<string>(LoanBookColumns.RestructureRisk).ToLower() == "yes" ? 2 : 1;
            }
            else
            {
                return 1;
            }
        }
        protected int ComputeWatchlistIndicatorScore(DataRow loabbookRecord, DataTable assumption)
        {
            string useWatchlistIndicator = GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.UseWatchlistIndicator);
            if(useWatchlistIndicator.ToLower() == "yes")
            {
                return loabbookRecord.Field<bool>(LoanBookColumns.WatchlistIndicator) ? 2 : 1;
            } 
            else
            {
                return 1;
            }
        }
        protected int ComputeCreditRatingScore(DataRow loanbookRecord, DataTable assumption)
        {
            double stage2to3CreditRating = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.ForwardTransitionStage2to3));
            double lowHighRiskThreshold = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRankLowHighRisk));
            double normalRiskThreshold = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRankLowRisk));
            double highRiskThreshold = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRankHighRisk));
            long? currentRating = loanbookRecord.Field<long?>(LoanBookColumns.CurrentRating);
            long? originalRating = loanbookRecord.Field<long?>(LoanBookColumns.OriginalRating);

            double currentCreditRankRating = string.IsNullOrWhiteSpace(currentRating.ToString()) ? 1 : Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRank + currentRating.ToString()).Replace("-", "").Replace("+", ""));
            double originalCreditRankRating =  string.IsNullOrWhiteSpace(originalRating.ToString()) ? 1 : Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.CreditRatingRank + originalRating.ToString()).Replace("-", "").Replace("+", ""));

            if (currentCreditRankRating >= stage2to3CreditRating)
            {
                return 3;
            }
            else
            {
                if (currentCreditRankRating <= lowHighRiskThreshold)
                {
                    return currentCreditRankRating - originalCreditRankRating > normalRiskThreshold ? 2 : 1;
                }
                else
                {
                    return currentCreditRankRating - originalCreditRankRating > highRiskThreshold ? 2 : 1;
                }
            }


        }
        protected int ComputePdRelativeScore(DataRow sicrInputRow, DataRow loanBookRecord, DataTable assumption)
        {
            string relativeType = GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.RelativeCreditQualityCriteria);
            double relativeThreshold = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.RelativeCreditQualityThreshold));

            switch (relativeType)
            {
                case TempEclData.CreditQualityCriteriaNone:
                    return 0;

                case TempEclData.CreditQualityCriteria12MonthPd:
                    double sicr12MonthPd = sicrInputRow.Field<double>(SicrInputsColumns.Pd12Month);
                    double loan12MonthPd = loanBookRecord.Field<double>(LoanBookColumns.Month12PD);

                    return ((sicr12MonthPd / loan12MonthPd) - 1 > relativeThreshold) ? 2 : 1;

                default:
                    double sicrLifetimePd = sicrInputRow.Field<double>(SicrInputsColumns.LifetimePd);
                    double loanLifetimePd = loanBookRecord.Field<double>(LoanBookColumns.LifetimePD);

                    return ((sicrLifetimePd / loanLifetimePd) - 1 > relativeThreshold) ? 2 : 1;
            }
        }
        protected int ComputePdAbsoluteScore(DataRow sicrInputRow, DataRow loanBookRecord, DataTable assumption)
        {
            string absoluteType = GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.AbsoluteCreditQualityCriteria);
            double absoluteThreshold = Convert.ToDouble(GetImpairmentAssumptionValue(assumption, ImpairmentRowKeys.AbsoluteCreditQualityThreshold));

            switch (absoluteType)
            {
                case TempEclData.CreditQualityCriteriaNone:
                    return 0;

                case TempEclData.CreditQualityCriteria12MonthPd:
                    double sicr12MonthPd = sicrInputRow.Field<double>(SicrInputsColumns.Pd12Month);
                    double loan12MonthPd = loanBookRecord.Field<double>(LoanBookColumns.Month12PD);

                    return ((sicr12MonthPd - loan12MonthPd) > absoluteThreshold) ? 2 : 1;

                default:
                    double sicrLifetimePd = sicrInputRow.Field<double>(SicrInputsColumns.LifetimePd);
                    double loanLifetimePd = loanBookRecord.Field<double>(LoanBookColumns.LifetimePD);

                    return ((sicrLifetimePd - loanLifetimePd) > absoluteThreshold) ? 2 : 1;
            }
        }
        protected string GetImpairmentAssumptionValue(DataTable assumptions, string assumptionKey)
        {
            return assumptions.AsEnumerable()
                              .FirstOrDefault(x => x.Field<string>(ImpairmentRowKeys.ColumnAssumption) == assumptionKey)
                              .Field<string>(ImpairmentRowKeys.ColumnValue);
        }
        protected DataTable GetPdMappingResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInput.GetPdMapping());
        }
        protected DataTable GetSicrInputResult()
        {
            return JsonUtil.DeserializeToDatatable(_pdInput.GetSicrInputs());
        }
        protected DataTable GetImpairmentAssumptionsData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetImpairmentAssumptionsData());
        }
        protected DataTable GetLoanBookData()
        {
            return JsonUtil.DeserializeToDatatable(DbUtil.GetLoanbookData());
        }
    }
}
