using Abp.Application.Services.Dto;

namespace TestDemo.ObeInputs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}