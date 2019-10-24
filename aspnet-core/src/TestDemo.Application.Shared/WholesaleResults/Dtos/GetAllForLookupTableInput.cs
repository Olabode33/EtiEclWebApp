using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}