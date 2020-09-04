using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}