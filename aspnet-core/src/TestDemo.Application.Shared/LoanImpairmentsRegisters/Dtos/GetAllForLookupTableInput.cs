using Abp.Application.Services.Dto;

namespace TestDemo.LoanImpairmentsRegisters.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}