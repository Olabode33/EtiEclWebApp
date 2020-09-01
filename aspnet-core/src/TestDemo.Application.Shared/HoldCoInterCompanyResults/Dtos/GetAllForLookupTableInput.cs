using Abp.Application.Services.Dto;

namespace TestDemo.HoldCoInterCompanyResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}