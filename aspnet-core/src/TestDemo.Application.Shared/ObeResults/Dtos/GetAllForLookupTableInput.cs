using Abp.Application.Services.Dto;

namespace TestDemo.ObeResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}