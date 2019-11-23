using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDemo.EclShared;
using TestDemo.EclShared.DefaultAssumptions;
using TestDemo.EntityFrameworkCore;

namespace TestDemo.Migrations.Seed.DefaultAsusmption.Portfolio
{
    public class DefaultAssumptionBuilder
    {
        private readonly TestDemoDbContext _context;

        public DefaultAssumptionBuilder(TestDemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //Wholesale
            CreateDefaultAssumption(FrameworkEnum.Wholesale);
            CreateCreditRatingRank(FrameworkEnum.Wholesale);
            //Retail
            CreateDefaultAssumption(FrameworkEnum.Retail);
            CreateCreditRatingRank(FrameworkEnum.Retail);
            //OBE
            CreateDefaultAssumption(FrameworkEnum.OBE);
            CreateCreditRatingRank(FrameworkEnum.OBE);
        }

        private void CreateDefaultAssumption(FrameworkEnum framework)
        {
            //Credit Index Threshold for Downturn Recoveries
            var creditIndexThreshold = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditIndexThreshold
                                                                                                  && x.Framework == framework);
            if(creditIndexThreshold == null)
            {
                _context.Assumptions.Add(new Assumption() {
                    AssumptionGroup = AssumptionGroupEnum.ScenarioInputs,
                    Key = DefaultPortfolioAssumption.AssumptionKey.CreditIndexThreshold,
                    InputName = DefaultPortfolioAssumption.InputName.CreditIndexThreshold,
                    Value = DefaultPortfolioAssumption.InputValue.CreditIndexThreshold,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Best Estimate Scenario(Likelihood)
            var bestScenarioLikelihood = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.BestScenarioLikelihood
                                                                                                    && x.Framework == framework);
            if (bestScenarioLikelihood == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ScenarioInputs,
                    Key = DefaultPortfolioAssumption.AssumptionKey.BestScenarioLikelihood,
                    InputName = DefaultPortfolioAssumption.InputName.BestScenarioLikelihood,
                    Value = DefaultPortfolioAssumption.InputValue.BestScenarioLikelihood,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Optimistic Scenario(Likelihood)
            var optimisticScenarioLikelihood = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.OptimisticScenarioLikelihood
                                                                                                          && x.Framework == framework);
            if (optimisticScenarioLikelihood == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ScenarioInputs,
                    Key = DefaultPortfolioAssumption.AssumptionKey.OptimisticScenarioLikelihood,
                    InputName = DefaultPortfolioAssumption.InputName.OptimisticScenarioLikelihood,
                    Value = DefaultPortfolioAssumption.InputValue.OptimisticScenarioLikelihood,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Downturn Estimate Scenario(Likelihood)
            var downturnScenarioLikelihood = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.DownturnScenarioLikelihood
                                                                                                        && x.Framework == framework);
            if (bestScenarioLikelihood == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ScenarioInputs,
                    Key = DefaultPortfolioAssumption.AssumptionKey.DownturnScenarioLikelihood,
                    InputName = DefaultPortfolioAssumption.InputName.DownturnScenarioLikelihood,
                    Value = DefaultPortfolioAssumption.InputValue.DownturnScenarioLikelihood,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Absolute Credit Quality Criteria
            var absoluteCreditQualityCriteria = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.AbsoluteCreditQualityCriteria
                                                                                                           && x.Framework == framework);
            if (absoluteCreditQualityCriteria == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.AbsoluteCreditQuality,
                    Key = DefaultPortfolioAssumption.AssumptionKey.AbsoluteCreditQualityCriteria,
                    InputName = DefaultPortfolioAssumption.InputName.AbsoluteCreditQualityCriteria,
                    Value = DefaultPortfolioAssumption.InputValue.AbsoluteCreditQualityCriteria,
                    DataType = DataTypeEnum.String,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Absolute Credit Quality Threshold
            var absoluteCreditQualityThreshold = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.AbsoluteCreditQualityThreshold
                                                                                                            && x.Framework == framework);
            if (absoluteCreditQualityThreshold == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.AbsoluteCreditQuality,
                    Key = DefaultPortfolioAssumption.AssumptionKey.AbsoluteCreditQualityThreshold,
                    InputName = DefaultPortfolioAssumption.InputName.AbsoluteCreditQualityThreshold,
                    Value = DefaultPortfolioAssumption.InputValue.AbsoluteCreditQualityThreshold,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Relative Credit Quality Criteria
            var relativeCreditQualityCriteria = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.RelativeCreditQualityCriteria
                                                                                                           && x.Framework == framework);
            if (relativeCreditQualityCriteria == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.RelativeCreditQuality,
                    Key = DefaultPortfolioAssumption.AssumptionKey.RelativeCreditQualityCriteria,
                    InputName = DefaultPortfolioAssumption.InputName.RelativeCreditQualityCriteria,
                    Value = DefaultPortfolioAssumption.InputValue.RelativeCreditQualityCriteria,
                    DataType = DataTypeEnum.String,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Relative Credit Quality Threshold
            var relativeCreditQualityThreshold = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.RelativeCreditQualityThreshold
                                                                                                            && x.Framework == framework);
            if (relativeCreditQualityThreshold == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.RelativeCreditQuality,
                    Key = DefaultPortfolioAssumption.AssumptionKey.RelativeCreditQualityThreshold,
                    InputName = DefaultPortfolioAssumption.InputName.RelativeCreditQualityThreshold,
                    Value = DefaultPortfolioAssumption.InputValue.RelativeCreditQualityThreshold,
                    DataType = DataTypeEnum.Double,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Credit Rating Rank Low / High Risk
            var creditRatingRankLowHighRisk = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankLowHighRisk
                                                                                                         && x.Framework == framework);
            if (creditRatingRankLowHighRisk == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankLowHighRisk,
                    InputName = DefaultPortfolioAssumption.InputName.CreditRatingRankLowHighRisk,
                    Value = DefaultPortfolioAssumption.InputValue.CreditRatingRankLowHighRisk,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Credit Rating Rank Low Risk
            var creditRatingRankLowRisk = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankLowRisk
                                                                                                     && x.Framework == framework);
            if (creditRatingRankLowRisk == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankLowRisk,
                    InputName = DefaultPortfolioAssumption.InputName.CreditRatingRankLowRisk,
                    Value = DefaultPortfolioAssumption.InputValue.CreditRatingRankLowRisk,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Credit Rating Rank High Risk
            var creditRatingRankHighRisk = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankHighRisk
                                                                                                      && x.Framework == framework);
            if (creditRatingRankHighRisk == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRankHighRisk,
                    InputName = DefaultPortfolioAssumption.InputName.CreditRatingRankHighRisk,
                    Value = DefaultPortfolioAssumption.InputValue.CreditRatingRankHighRisk,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Credit Rating Default Indicator
            var creditRatingDefaultIndicator = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingDefaultIndicator
                                                                                                          && x.Framework == framework);
            if (creditRatingDefaultIndicator == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingDefaultIndicator,
                    InputName = DefaultPortfolioAssumption.InputName.CreditRatingDefaultIndicator,
                    Value = DefaultPortfolioAssumption.InputValue.CreditRatingDefaultIndicator,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Use Watchlist Indicator
            var useWatchlistIndicator = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.UseWatchlistIndicator
                                                                                                   && x.Framework == framework);
            if (useWatchlistIndicator == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.UseWatchlistIndicator,
                    InputName = DefaultPortfolioAssumption.InputName.UseWatchlistIndicator,
                    Value = DefaultPortfolioAssumption.InputValue.UseWatchlistIndicator,
                    DataType = DataTypeEnum.String,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Use Restructure Indicator
            var useRestructureIndicator = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.UseRestructureIndicator
                                                                                                     && x.Framework == framework);
            if (useRestructureIndicator == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.UseRestructureIndicator,
                    InputName = DefaultPortfolioAssumption.InputName.UseRestructureIndicator,
                    Value = DefaultPortfolioAssumption.InputValue.UseRestructureIndicator,
                    DataType = DataTypeEnum.String,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Forward Transition Stage 1 to Stage 2
            var stage1to2 = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.ForwardTransitionStage1to2
                                                                                       && x.Framework == framework);
            if (stage1to2 == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.ForwardTransitionStage1to2,
                    InputName = DefaultPortfolioAssumption.InputName.ForwardTransitionStage1to2,
                    Value = DefaultPortfolioAssumption.InputValue.ForwardTransitionStage1to2,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Forward Transition Stage 2 to Stage 3
            var stage2to3 = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.ForwardTransitionStage2to3
                                                                                       && x.Framework == framework);
            if (stage2to3 == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.ForwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.ForwardTransitionStage2to3,
                    InputName = DefaultPortfolioAssumption.InputName.ForwardTransitionStage2to3,
                    Value = DefaultPortfolioAssumption.InputValue.ForwardTransitionStage2to3,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Backward Transition Stage 2 to Stage 1
            var stage2to1 = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.BackwardTransitionsStage2to1
                                                                                       && x.Framework == framework);
            if (stage2to1 == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.BackwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.BackwardTransitionsStage2to1,
                    InputName = DefaultPortfolioAssumption.InputName.BackwardTransitionsStage2to1,
                    Value = DefaultPortfolioAssumption.InputValue.BackwardTransitionsStage2to1,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

            //Backward Transition Stage 3 to Stage 2
            var stage3to2 = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.BackwardTransitionsStage3to2
                                                                                       && x.Framework == framework);
            if (stage3to2 == null)
            {
                _context.Assumptions.Add(new Assumption()
                {
                    AssumptionGroup = AssumptionGroupEnum.BackwardTransitions,
                    Key = DefaultPortfolioAssumption.AssumptionKey.BackwardTransitionsStage3to2,
                    InputName = DefaultPortfolioAssumption.InputName.BackwardTransitionsStage3to2,
                    Value = DefaultPortfolioAssumption.InputValue.BackwardTransitionsStage3to2,
                    DataType = DataTypeEnum.Int,
                    Framework = framework,
                    IsComputed = false,
                    RequiresGroupApproval = true,
                });
                _context.SaveChanges();
            }

        }

        private void CreateCreditRatingRank(FrameworkEnum framework)
        {
            int maxCreditRating = 10;
            int rankCount = 1;

            for (int rating = 1; rating <= maxCreditRating; rating++)
            {
                if (rating >= 2 && rating <= 6)
                {
                    //Plus
                    var rankPlus = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework);
                    if (rankPlus == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString() + "+",
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }

                    //Norm
                    var rankNorm = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework);
                    if (rankNorm == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString(),
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }

                    //Minus
                    var rankMinus = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework);
                    if (rankMinus == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString() + "-",
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }
                }
                else
                {
                    var rank = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                          && x.Framework == framework);
                    if (rank == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.AssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString(),
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }
                }
            }
        }
    }
}
