using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentKeyParameters.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}