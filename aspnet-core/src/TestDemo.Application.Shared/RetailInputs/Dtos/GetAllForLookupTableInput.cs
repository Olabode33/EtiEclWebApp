using Abp.Application.Services.Dto;

namespace TestDemo.RetailInputs.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}