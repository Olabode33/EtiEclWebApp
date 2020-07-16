using TestDemo.Wholesale;
using TestDemo.WholesaleInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.WholesaleResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclShared.Temp;

namespace TestDemo.WholesaleResults
{
    public class WholesaleEclResultDetailsAppService : TestDemoAppServiceBase
    {
		 private readonly IRepository<WholesaleEclFramworkReportDetail, Guid> _wholesaleEclResultDetailRepository;
		 private readonly IRepository<WholesaleEcl,Guid> _lookup_wholesaleEclRepository;
		 private readonly IRepository<WholesaleEclDataLoanBook,Guid> _lookup_wholesaleEclDataLoanBookRepository;
		 

		  public WholesaleEclResultDetailsAppService(IRepository<WholesaleEclFramworkReportDetail, Guid> wholesaleEclResultDetailRepository , IRepository<WholesaleEcl, Guid> lookup_wholesaleEclRepository, IRepository<WholesaleEclDataLoanBook, Guid> lookup_wholesaleEclDataLoanBookRepository) 
		  {
			_wholesaleEclResultDetailRepository = wholesaleEclResultDetailRepository;
			_lookup_wholesaleEclRepository = lookup_wholesaleEclRepository;
		_lookup_wholesaleEclDataLoanBookRepository = lookup_wholesaleEclDataLoanBookRepository;
		
		  }

        public async Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _wholesaleEclResultDetailRepository.GetAll().Where(x => x.WholesaleEclId == input.Id);

            output.TotalExposure = await eclResult.SumAsync(x => x.Outstanding_Balance);
            output.PreOverrideImpairment = await eclResult.SumAsync(x => StaticEclGenerator.RandomImpairment(x.Outstanding_Balance, x.Impairment_ModelOutput, x.Stage)); ///TODO Temp hack to display results);
            output.PreOverrideCoverageRatio = output.PreOverrideImpairment / output.TotalExposure;
            output.PostOverrideImpairment = await eclResult.SumAsync(x => x.Overrides_Impairment_Manual);
            output.PostOverrideCoverageRatio = output.PostOverrideImpairment / output.TotalExposure;

            return output;
        }

        public async Task<List<ViewEclResultDetailsDto>> GetTop20Exposure(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _wholesaleEclResultDetailRepository.GetAll()
                                                            .Where(x => x.WholesaleEclId == input.Id)
                                                            .OrderByDescending(x => x.Outstanding_Balance).Take(10)
                                                            .Select(e => new ViewEclResultDetailsDto()
                                                            {
                                                                AccountNumber = e.AccountNo,
                                                                ContractId = e.ContractNo,
                                                                CustomerNumber = e.CustomerNo,
                                                                ProductType = e.ProductType,
                                                                Sector = e.Sector,
                                                                Segment = e.Segment,
                                                                Exposure = e.Outstanding_Balance,
                                                                PreOverrideResult = new EclResultOverrideFigures
                                                                {
                                                                    Stage = e.Stage,
                                                                    EclBest = e.ECL_Best_Estimate,
                                                                    EclDownturn = e.ECL_Downturn,
                                                                    EclOptimistic = e.ECL_Optimistic,
                                                                    Impairment = StaticEclGenerator.RandomImpairment(e.Outstanding_Balance, e.Impairment_ModelOutput, e.Stage) ///TODO Temp hack to display results
                                                                },
                                                                PostOverrideResult = new EclResultOverrideFigures
                                                                {
                                                                    Stage = e.Overrides_Stage,
                                                                    EclBest = e.Overrides_ECL_Best_Estimate,
                                                                    EclDownturn = e.Overrides_ECL_Downturn,
                                                                    EclOptimistic = e.Overrides_ECL_Optimistic,
                                                                    Impairment = e.Overrides_Impairment_Manual
                                                                }
                                                            });



            return await eclResult.OrderByDescending(x => x.Exposure).ToListAsync();
        }
    }
}