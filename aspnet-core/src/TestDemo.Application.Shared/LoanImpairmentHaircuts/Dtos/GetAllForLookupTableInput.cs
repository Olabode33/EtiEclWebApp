using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentHaircuts.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}