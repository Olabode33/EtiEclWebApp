using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentModelResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}