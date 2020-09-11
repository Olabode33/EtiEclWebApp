using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentScenarios.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}