using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentRecoveries.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}