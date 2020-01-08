using Abp.Application.Services.Dto;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}