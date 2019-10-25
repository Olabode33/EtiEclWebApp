using Abp.Application.Services.Dto;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}