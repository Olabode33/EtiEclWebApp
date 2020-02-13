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


        public InvestmentEclResultsAppService(
            IRepository<InvestmentEclFinalResult, Guid> investmentEclFinalResultRepository)
        {
            _investmentEclFinalResultRepository = investmentEclFinalResultRepository;
        }

        public async Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _investmentEclFinalResultRepository.GetAll().Where(x => x.EclId == input.Id);

            output.TotalExposure = await eclResult.SumAsync(x => x.Exposure);
            output.PreOverrideImpairment = await eclResult.SumAsync(x => x.Impairment);
            output.PreOverrideCoverageRatio = output.PreOverrideImpairment / output.TotalExposure;

            output.PostOverrideImpairment = await eclResult.SumAsync(x => x.Impairment);
            output.PostOverrideCoverageRatio = output.PostOverrideImpairment / output.TotalExposure;

            return output;
        }

        public async Task<List<ViewEclResultDetailsDto>> GetTop20Exposure(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _investmentEclFinalResultRepository.GetAll()
                                                               .Where(x => x.EclId == input.Id)
                                                               .OrderByDescending(x => x.Exposure)
                                                               .Take(10)
                                                               .Select(x => new ViewEclResultDetailsDto()
                                                                {
                                                                    AccountNumber = x.AssetDescription,
                                                                    Exposure = x.Exposure,
                                                                    Staging = x.Stage,
                                                                    PreOverrideResult = new EclResultOverrideFigures { EclBest = x.BestValue, EclDownturn = x.DownturnValue, EclOptimistic = x.OptimisticValue, Impairment = x.Impairment },
                                                                    PostOverrideResult = new EclResultOverrideFigures { EclBest = x.BestValue, EclDownturn = x.DownturnValue, EclOptimistic = x.OptimisticValue, Impairment = x.Impairment }
                                                                })
                                                               .OrderByDescending(x => x.Exposure)
                                                               .ToListAsync();

            return await eclResult;
        }
    }
}