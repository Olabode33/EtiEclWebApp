using Abp.Application.Services.Dto;

namespace TestDemo.Retail.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}