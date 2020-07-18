using TestDemo.OBE;
using TestDemo.ObeInputs;


using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using TestDemo.ObeResults.Dtos;
using TestDemo.Dto;
using Abp.Application.Services.Dto;
using TestDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using TestDemo.EclShared.Temp;

namespace TestDemo.ObeResults
{
    public class ObeEclResultDetailsAppService : TestDemoAppServiceBase
    {
		 private readonly IRepository<ObeEclFramworkReportDetail, Guid> _obeEclResultDetailRepository;
		 private readonly IRepository<ObeEcl,Guid> _lookup_obeEclRepository;
		 private readonly IRepository<ObeEclDataLoanBook,Guid> _lookup_obeEclDataLoanBookRepository;
		 

		  public ObeEclResultDetailsAppService(IRepository<ObeEclFramworkReportDetail, Guid> obeEclResultDetailRepository , IRepository<ObeEcl, Guid> lookup_obeEclRepository, IRepository<ObeEclDataLoanBook, Guid> lookup_obeEclDataLoanBookRepository) 
		  {
			_obeEclResultDetailRepository = obeEclResultDetailRepository;
			_lookup_obeEclRepository = lookup_obeEclRepository;
		_lookup_obeEclDataLoanBookRepository = lookup_obeEclDataLoanBookRepository;
		
		  }

        public async Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input)
        {
            ViewEclResultSummaryDto output = new ViewEclResultSummaryDto();
            var eclResult = _obeEclResultDetailRepository.GetAll().Where(x => x.ObeEclId == input.Id);

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
            var eclResult = _obeEclResultDetailRepository.GetAll()
                                                            .Where(x => x.ObeEclId == input.Id)
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
                                                                PreOverrideResult = new EclResultOverrideFigures { 
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