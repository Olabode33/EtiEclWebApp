using Abp.Application.Services.Dto;
using System;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetAllInvestmentEclOverridesInput : PagedAndSortedResultRequestDto
    {
        public Guid EclId { get; set; }
        public string Filter { get; set; }
        public int StatusFilter { get; set; }
        public string InvestmentEclSicrAssetDescriptionFilter { get; set; }
    }
}