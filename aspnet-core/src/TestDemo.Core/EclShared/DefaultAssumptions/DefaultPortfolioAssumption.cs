using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.DefaultAssumptions
{
    public static class DefaultPortfolioAssumption
    {
        public static class AssumptionKey
        {
            public const string CreditIndexThreshold = "CreditIndexThresholdforDownturnRecoveries";
            public const string BestScenarioLikelihood = "BestEstimateScenarioLikelihood";
            public const string OptimisticScenarioLikelihood = "OptimisticScenarioLikelihood";
            public const string DownturnScenarioLikelihood = "DownturnScenarioLikelihood";
            public const string AbsoluteCreditQualityCriteria = "AbsoluteCreditQualityCriteria";
            public const string AbsoluteCreditQualityThreshold = "AbsoluteCreditQualityThreshold";
            public const string RelativeCreditQualityCriteria = "RelativeCreditQualityCriteria";
            public const string RelativeCreditQualityThreshold = "RelativeCreditQualityThreshold";
            public const string CreditRatingRankLowHighRisk = "CreditRatingRankLowHighRisk";
            public const string CreditRatingRankLowRisk = "CreditRatingRankNotchesLowRisk";
            public const string CreditRatingRankHighRisk = "CreditRatingRankNotchesHighRisk";
            public const string CreditRatingDefaultIndicator = "CreditRatingDefaultIndicator";
            public const string UseWatchlistIndicator = "UseWatchlistIndicator";
            public const string UseRestructureIndicator = "UseRestructureIndicator?";
            public const string ForwardTransitionStage1to2 = "ForwardTransitionsStage1to2";
            public const string ForwardTransitionStage2to3 = "ForwardTransitionsStage2toStage3";
            public const string BackwardTransitionsStage2to1 = "BackwardTransitionsProbationPeriodStage2to1";
            public const string BackwardTransitionsStage3to2 = "BackwardTransitionsProbationPeriodStage3to2";
            public const string CreditRatingRank = "CreditRatingRank";
        }

        public static class InputName
        {
            public const string CreditIndexThreshold = "Credit Index Threshold for Downturn Recoveries";
            public const string BestScenarioLikelihood = "Best Estimate Scenario(Likelihood)";
            public const string OptimisticScenarioLikelihood = "Optimistic Scenario(Likelihood)";
            public const string DownturnScenarioLikelihood = "Downturn Scenario(Likelihood)";
            public const string AbsoluteCreditQualityCriteria = "Absolute Credit Quality Criteria";
            public const string AbsoluteCreditQualityThreshold = "Absolute Credit Quality Threshold";
            public const string RelativeCreditQualityCriteria = "Relative Credit Quality Criteria";
            public const string RelativeCreditQualityThreshold = "Relative Credit Quality Threshold";
            public const string CreditRatingRankLowHighRisk = "Credit Rating Rank (Low/High Risk)";
            public const string CreditRatingRankLowRisk = "Credit Rating Rank (Notches) - Low Risk";
            public const string CreditRatingRankHighRisk = "Credit Rating Rank (Notches) - High Risk";
            public const string CreditRatingDefaultIndicator = "Credit Rating Default Indicator";
            public const string UseWatchlistIndicator = "Use Watchlist Indicator?";
            public const string UseRestructureIndicator = "Use Restructure Indicator?";
            public const string ForwardTransitionStage1to2 = "Forward Transitions Stage 1 -> Stage 2 (dpd)";
            public const string ForwardTransitionStage2to3 = "Forward Transitions Stage 2 -> Stage 3 (dpd)";
            public const string BackwardTransitionsStage2to1 = "Backward Transitions (Probation Period) Stage 2 -> Stage 1 (days)";
            public const string BackwardTransitionsStage3to2 = "Backward Transitions (Probation Period) Stage 3 -> Stage 2 (days)";
            public const string CreditRatingRank = "Credit Rating Rank ";
        }

        public static class InputValue
        {
            public const string CreditIndexThreshold = "1.96";
            public const string BestScenarioLikelihood = "0.50";
            public const string OptimisticScenarioLikelihood = "0.28";
            public const string DownturnScenarioLikelihood = "0.22";
            public const string AbsoluteCreditQualityCriteria = "None";
            public const string AbsoluteCreditQualityThreshold = "0.05";
            public const string RelativeCreditQualityCriteria = "None";
            public const string RelativeCreditQualityThreshold = "0.05";
            public const string CreditRatingRankLowHighRisk = "14";
            public const string CreditRatingRankLowRisk = "5";
            public const string CreditRatingRankHighRisk = "3";
            public const string CreditRatingDefaultIndicator = "20";
            public const string UseWatchlistIndicator = "No";
            public const string UseRestructureIndicator = "Yes";
            public const string ForwardTransitionStage1to2 = "30";
            public const string ForwardTransitionStage2to3 = "90";
            public const string BackwardTransitionsStage2to1 = "90";
            public const string BackwardTransitionsStage3to2 = "90";
        }
    }
}
