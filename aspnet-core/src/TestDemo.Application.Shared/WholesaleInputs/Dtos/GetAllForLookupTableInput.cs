using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleInputs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}