using Abp.Application.Services.Dto;

namespace TestDemo.RetailResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}