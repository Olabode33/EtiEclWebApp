using Abp.Application.Services.Dto;

namespace TestDemo.HoldCoResult.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}