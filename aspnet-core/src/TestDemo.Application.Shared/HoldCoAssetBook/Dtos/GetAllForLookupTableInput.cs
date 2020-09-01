using Abp.Application.Services.Dto;

namespace TestDemo.HoldCoAssetBook.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}