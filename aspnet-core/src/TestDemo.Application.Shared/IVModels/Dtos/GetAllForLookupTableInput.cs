using Abp.Application.Services.Dto;

namespace TestDemo.IVModels.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}