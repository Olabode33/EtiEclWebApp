using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDemo.Dto;

namespace TestDemo.InvestmentResults
{
    public interface IInvestmentEclResultsAppService
    {
        Task<ViewEclResultSummaryDto> GetResultSummary(EntityDto<Guid> input);
        Task<List<ViewEclResultDetailsDto>> GetTop20Exposure(EntityDto<Guid> input);
    }
}