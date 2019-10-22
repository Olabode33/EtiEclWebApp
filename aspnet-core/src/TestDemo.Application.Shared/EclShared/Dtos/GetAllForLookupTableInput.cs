using Abp.Application.Services.Dto;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}