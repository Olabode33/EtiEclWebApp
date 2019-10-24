using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}