using Abp.Application.Services.Dto;

namespace TestDemo.OBE.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}