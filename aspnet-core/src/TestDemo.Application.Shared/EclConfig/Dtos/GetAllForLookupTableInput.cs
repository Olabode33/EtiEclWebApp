using Abp.Application.Services.Dto;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}