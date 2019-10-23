using Abp.Application.Services.Dto;

namespace TestDemo.Wholesale.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}