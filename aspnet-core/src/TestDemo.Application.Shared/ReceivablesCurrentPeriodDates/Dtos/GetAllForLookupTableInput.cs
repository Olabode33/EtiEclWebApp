using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesCurrentPeriodDates.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}