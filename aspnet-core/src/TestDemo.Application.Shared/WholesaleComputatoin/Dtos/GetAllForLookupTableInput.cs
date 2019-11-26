using Abp.Application.Services.Dto;

namespace TestDemo.WholesaleComputatoin.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}