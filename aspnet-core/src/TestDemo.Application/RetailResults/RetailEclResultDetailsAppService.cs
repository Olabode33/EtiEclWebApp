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
using TestDemo.EclShared.Temp;

namespace TestDemo.RetailResults
{
    public class RetailEclResultDetailsAppService : TestDemoAppServiceBase
    {
		 private readonly IRepository<RetailEclFramworkReportDetail, Guid> _retailEclResultDetailRepository;
		 private readonly IRepository<RetailEcl,Guid> _lookup_retailEclRepository;
		 private readonly IRepository<RetailEclDataLoanBook,Guid> _lookup_retailEclDataLoanBookRepository;
		 

		  public RetailEclResultDetailsAppService(IRepository<RetailEclFramworkReportDetail, Guid> retailEclResultDetailRepository , IRepository<RetailEcl, Guid> lookup_retailEclRepository, IRepository<RetailEclDataLoanBook, Guid> lookup_retailEclDataLoanBookRepository) 
		  {
			_retailEclResultDetailRepository = retailEclResultDetailRepository;
			_lookup_retailEclRepository = lookup_retailEclRepository;
		_lookup_retailEclDataLoanBookRepository = lookup_retailEclDataLoanBookRepository;
		
		  }


        public async Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _retailEclResultDetailRepository.GetAll().Where(x => x.RetailEclId == input.Id);

            output.TotalExposure = await eclResult.SumAsync(x => x.Outstanding_Balance);
            output.PreOverrideImpairment = await eclResult.SumAsync(x => x.Impairment_ModelOutput);
            output.PreOverrideCoverageRatio = output.PreOverrideImpairment / output.TotalExposure;
            output.PostOverrideImpairment = await eclResult.SumAsync(x => x.Overrides_Impairment_Manual);
            output.PostOverrideCoverageRatio = output.PostOverrideImpairment / output.TotalExposure;

            return output;
        }

        public async Task<List<ViewEclResultDetailsDto>> GetTop20Exposure(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _retailEclResultDetailRepository.GetAll()
                                                            .Where(x => x.RetailEclId == input.Id)
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
                                                                    Impairment = e.Impairment_ModelOutput
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