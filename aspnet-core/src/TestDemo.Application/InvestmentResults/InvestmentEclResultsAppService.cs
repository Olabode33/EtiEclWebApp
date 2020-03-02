using TestDemo.Retail;
using TestDemo.RetailInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.RetailResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.InvestmentComputation;

namespace TestDemo.InvestmentResults
{
    [AbpAuthorize(AppPermissions.Pages_EclView)]
    public class InvestmentEclResultsAppService : TestDemoAppServiceBase, IInvestmentEclResultsAppService
    {
        private readonly IRepository<InvestmentEclFinalResult, Guid> _investmentEclFinalResultRepository;
        private readonly IRepository<InvestmentEclFinalPostOverrideResult, Guid> _investmentEclFinalPostResultRepository;


        public InvestmentEclResultsAppService(
            IRepository<InvestmentEclFinalResult, Guid> investmentEclFinalResultRepository,
            IRepository<InvestmentEclFinalPostOverrideResult, Guid> investmentEclFinalPostResultRepository)
        {
            _investmentEclFinalResultRepository = investmentEclFinalResultRepository;
            _investmentEclFinalPostResultRepository = investmentEclFinalPostResultRepository;
        }

        public async Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _investmentEclFinalResultRepository.GetAll().Where(x => x.EclId == input.Id);
            var eclPostResult = _investmentEclFinalPostResultRepository.GetAll().Where(x => x.EclId == input.Id);

            output.TotalExposure = await eclResult.SumAsync(x => x.Exposure);
            output.PreOverrideImpairment = await eclResult.SumAsync(x => x.Impairment);
            output.PreOverrideCoverageRatio = output.PreOverrideImpairment / output.TotalExposure;

            output.PostOverrideImpairment = await (from r in eclResult
                                                   join p in eclPostResult on r.RecordId equals p.RecordId into p1
                                                   from p2 in p1.DefaultIfEmpty()
                                                   select new {
                                                        r.RecordId,
                                                        Impairment = p2 == null ? r.Impairment : p2.Impairment
                                                   }).SumAsync(x => x.Impairment);
            output.PostOverrideCoverageRatio = output.PostOverrideImpairment / output.TotalExposure;

            return output;
        }

        public async Task<List<ViewEclResultDetailsDto>> GetTop20Exposure(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclPreResult = _investmentEclFinalResultRepository.GetAll().Where(x => x.EclId == input.Id).OrderByDescending(x => x.Exposure).Take(10);
            var eclPostResult = _investmentEclFinalPostResultRepository.GetAll().Where(x => x.EclId == input.Id).OrderByDescending(x => x.Exposure).Take(10);

            var eclResult = from r in eclPreResult
                            join p in eclPostResult on r.RecordId equals p.RecordId into p1
                            from p2 in p1.DefaultIfEmpty()
                            select new ViewEclResultDetailsDto()
                            {
                                AccountNumber = r.AssetDescription,
                                Exposure = r.Exposure,
                                PreOverrideResult = new EclResultOverrideFigures { Stage = r.Stage, EclBest = r.BestValue, EclDownturn = r.DownturnValue, EclOptimistic = r.OptimisticValue, Impairment = r.Impairment },
                                PostOverrideResult = new EclResultOverrideFigures {
                                    Stage = p2 == null ? r.Stage : p2.Stage,
                                    EclBest = p2 == null ? r.BestValue : p2.BestValue, 
                                    EclDownturn = p2 == null ? r.DownturnValue : p2.DownturnValue, 
                                    EclOptimistic = p2 == null ? r.OptimisticValue : p2.OptimisticValue, 
                                    Impairment = p2 == null ? r.Impairment : p2.Impairment 
                                }
                            };


            return await eclResult.OrderByDescending(x => x.Exposure).ToListAsync();
        }
    }
}