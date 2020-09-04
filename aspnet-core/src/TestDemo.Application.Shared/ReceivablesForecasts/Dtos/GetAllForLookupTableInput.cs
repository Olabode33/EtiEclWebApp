using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesForecasts.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}