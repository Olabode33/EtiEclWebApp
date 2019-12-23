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
            var temp = _context.AffiliateAssumptions.Select(x => x.Id).ToArray();
            long[] ous = _context.OrganizationUnits.IgnoreQueryFilters().Select(x => x.Id).ToArray();

            foreach (long ou in ous)
            {
                Create(ou);
            }
        }


        public void Create(long ou)
        {
            //Wholesale
            CreateDefaultAssumption(FrameworkEnum.Wholesale, ou);
            CreateCreditRatingRank(FrameworkEnum.Wholesale, ou);
            //Retail
            CreateDefaultAssumption(FrameworkEnum.Retail, ou);
            CreateCreditRatingRank(FrameworkEnum.Retail, ou);
            //OBE
            CreateDefaultAssumption(FrameworkEnum.OBE, ou);
            CreateCreditRatingRank(FrameworkEnum.OBE, ou);
        }

        private void CreateDefaultAssumption(FrameworkEnum framework, long ou)
        {
            string[] keys = new string[] { DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditIndexThreshold, DefaultPortfolioAssumption.FrameworkAssumptionKey.BestScenarioLikelihood,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.OptimisticScenarioLikelihood, DefaultPortfolioAssumption.FrameworkAssumptionKey.DownturnScenarioLikelihood,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.AbsoluteCreditQualityCriteria, DefaultPortfolioAssumption.FrameworkAssumptionKey.AbsoluteCreditQualityThreshold,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.RelativeCreditQualityCriteria, DefaultPortfolioAssumption.FrameworkAssumptionKey.RelativeCreditQualityThreshold,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRankLowHighRisk, DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRankLowRisk,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRankHighRisk, DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingDefaultIndicator,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.UseWatchlistIndicator, DefaultPortfolioAssumption.FrameworkAssumptionKey.UseRestructureIndicator,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.ForwardTransitionStage1to2, DefaultPortfolioAssumption.FrameworkAssumptionKey.ForwardTransitionStage2to3,
                                           DefaultPortfolioAssumption.FrameworkAssumptionKey.BackwardTransitionsStage2to1, DefaultPortfolioAssumption.FrameworkAssumptionKey.BackwardTransitionsStage3to2};

            string[] names = new string[] { DefaultPortfolioAssumption.InputName.CreditIndexThreshold, DefaultPortfolioAssumption.InputName.BestScenarioLikelihood,
                                           DefaultPortfolioAssumption.InputName.OptimisticScenarioLikelihood, DefaultPortfolioAssumption.InputName.DownturnScenarioLikelihood,
                                           DefaultPortfolioAssumption.InputName.AbsoluteCreditQualityCriteria, DefaultPortfolioAssumption.InputName.AbsoluteCreditQualityThreshold,
                                           DefaultPortfolioAssumption.InputName.RelativeCreditQualityCriteria, DefaultPortfolioAssumption.InputName.RelativeCreditQualityThreshold,
                                           DefaultPortfolioAssumption.InputName.CreditRatingRankLowHighRisk, DefaultPortfolioAssumption.InputName.CreditRatingRankLowRisk,
                                           DefaultPortfolioAssumption.InputName.CreditRatingRankHighRisk, DefaultPortfolioAssumption.InputName.CreditRatingDefaultIndicator,
                                           DefaultPortfolioAssumption.InputName.UseWatchlistIndicator, DefaultPortfolioAssumption.InputName.UseRestructureIndicator,
                                           DefaultPortfolioAssumption.InputName.ForwardTransitionStage1to2, DefaultPortfolioAssumption.InputName.ForwardTransitionStage2to3,
                                           DefaultPortfolioAssumption.InputName.BackwardTransitionsStage2to1, DefaultPortfolioAssumption.InputName.BackwardTransitionsStage3to2 };

            string[] values = new string[] { DefaultPortfolioAssumption.InputValue.CreditIndexThreshold, DefaultPortfolioAssumption.InputValue.BestScenarioLikelihood,
                                           DefaultPortfolioAssumption.InputValue.OptimisticScenarioLikelihood, DefaultPortfolioAssumption.InputValue.DownturnScenarioLikelihood,
                                           DefaultPortfolioAssumption.InputValue.AbsoluteCreditQualityCriteria, DefaultPortfolioAssumption.InputValue.AbsoluteCreditQualityThreshold,
                                           DefaultPortfolioAssumption.InputValue.RelativeCreditQualityCriteria, DefaultPortfolioAssumption.InputValue.RelativeCreditQualityThreshold,
                                           DefaultPortfolioAssumption.InputValue.CreditRatingRankLowHighRisk, DefaultPortfolioAssumption.InputValue.CreditRatingRankLowRisk,
                                           DefaultPortfolioAssumption.InputValue.CreditRatingRankHighRisk, DefaultPortfolioAssumption.InputValue.CreditRatingDefaultIndicator,
                                           DefaultPortfolioAssumption.InputValue.UseWatchlistIndicator, DefaultPortfolioAssumption.InputValue.UseRestructureIndicator,
                                           DefaultPortfolioAssumption.InputValue.ForwardTransitionStage1to2, DefaultPortfolioAssumption.InputValue.ForwardTransitionStage2to3,
                                           DefaultPortfolioAssumption.InputValue.BackwardTransitionsStage2to1, DefaultPortfolioAssumption.InputValue.BackwardTransitionsStage3to2 };
            DataTypeEnum[] dataTypes = new DataTypeEnum[] { DataTypeEnum.Double, DataTypeEnum.DoublePercentage, DataTypeEnum.DoublePercentage, DataTypeEnum.DoublePercentage,
                                                            DataTypeEnum.String, DataTypeEnum.DoublePercentage, DataTypeEnum.String, DataTypeEnum.DoublePercentage,
                                                            DataTypeEnum.Int, DataTypeEnum.Int, DataTypeEnum.Int, DataTypeEnum.Int, DataTypeEnum.String, DataTypeEnum.String,
                                                            DataTypeEnum.Int, DataTypeEnum.Int, DataTypeEnum.Int, DataTypeEnum.Int};
            AssumptionGroupEnum[] groupEnums = new AssumptionGroupEnum[] { AssumptionGroupEnum.ScenarioInputs, AssumptionGroupEnum.ScenarioInputs, AssumptionGroupEnum.ScenarioInputs, AssumptionGroupEnum.ScenarioInputs,
                                                                           AssumptionGroupEnum.AbsoluteCreditQuality, AssumptionGroupEnum.AbsoluteCreditQuality,
                                                                           AssumptionGroupEnum.RelativeCreditQuality, AssumptionGroupEnum.RelativeCreditQuality,
                                                                           AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions,
                                                                           AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions, AssumptionGroupEnum.ForwardTransitions,
                                                                           AssumptionGroupEnum.BackwardTransitions, AssumptionGroupEnum.BackwardTransitions};

            for (int i = 0; i < keys.Length; i++)
            {
                var c = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == keys[i] && x.Framework == framework && x.OrganizationUnitId == ou);
                if (c == null)
                {
                    _context.Assumptions.Add(new Assumption()
                    {
                        AssumptionGroup = groupEnums[i],
                        Key = keys[i],
                        InputName = names[i],
                        Value = values[i],
                        DataType = dataTypes[i],
                        Framework = framework,
                        IsComputed = false,
                        RequiresGroupApproval = true,
                        CanAffiliateEdit = false,
                        OrganizationUnitId = ou
                    });
                    _context.SaveChanges();
                }
            }
            

        }

        private void CreateCreditRatingRank(FrameworkEnum framework, long ou)
        {
            int maxCreditRating = 10;
            int rankCount = 1;

            for (int rating = 1; rating <= maxCreditRating; rating++)
            {
                if (rating >= 2 && rating <= 6)
                {
                    //Plus
                    var rankPlus = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework && x.OrganizationUnitId == ou);
                    if (rankPlus == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString() + "+",
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                            CanAffiliateEdit = false,
                            OrganizationUnitId = ou
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }

                    //Norm
                    var rankNorm = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework);
                    if (rankNorm == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString(),
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                            CanAffiliateEdit = false,
                            OrganizationUnitId = ou
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }

                    //Minus
                    var rankMinus = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                              && x.Framework == framework);
                    if (rankMinus == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString() + "-",
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                            CanAffiliateEdit = false,
                            OrganizationUnitId = ou
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }
                }
                else
                {
                    var rank = _context.Assumptions.IgnoreQueryFilters().FirstOrDefault(x => x.Key == DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString()
                                                                                          && x.Framework == framework);
                    if (rank == null)
                    {
                        _context.Assumptions.Add(new Assumption()
                        {
                            AssumptionGroup = AssumptionGroupEnum.CreditRatingRank,
                            Key = DefaultPortfolioAssumption.FrameworkAssumptionKey.CreditRatingRank + rankCount.ToString(),
                            InputName = DefaultPortfolioAssumption.InputName.CreditRatingRank + rankCount.ToString(),
                            Value = rating.ToString(),
                            DataType = DataTypeEnum.String,
                            Framework = framework,
                            IsComputed = false,
                            RequiresGroupApproval = true,
                            CanAffiliateEdit = false,
                            OrganizationUnitId = ou
                        });
                        _context.SaveChanges();
                        rankCount += 1;
                    }
                }
            }
        }
    }
}
