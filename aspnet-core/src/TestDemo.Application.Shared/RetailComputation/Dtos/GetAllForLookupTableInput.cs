using Abp.Application.Services.Dto;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}