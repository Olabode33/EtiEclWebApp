using Abp.Application.Services.Dto;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}