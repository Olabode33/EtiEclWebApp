using Abp.Application.Services.Dto;

namespace TestDemo.Investment.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}