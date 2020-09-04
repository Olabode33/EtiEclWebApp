using Abp.Application.Services.Dto;

namespace TestDemo.ReceivablesApprovals.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}