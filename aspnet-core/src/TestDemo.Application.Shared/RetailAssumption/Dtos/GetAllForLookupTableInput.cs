using Abp.Application.Services.Dto;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}