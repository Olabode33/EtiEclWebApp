using Abp.Application.Services.Dto;

namespace TestDemo.HoldCoApprovals.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}