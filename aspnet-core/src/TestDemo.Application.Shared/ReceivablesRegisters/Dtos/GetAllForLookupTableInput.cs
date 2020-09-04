using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesRegisters.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}