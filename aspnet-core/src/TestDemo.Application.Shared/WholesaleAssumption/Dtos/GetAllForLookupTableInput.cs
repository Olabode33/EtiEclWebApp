using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}