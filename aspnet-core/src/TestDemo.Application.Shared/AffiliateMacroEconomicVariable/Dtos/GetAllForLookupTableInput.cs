using Abp.Application.Services.Dto;

namespace TestDemo.AffiliateMacroEconomicVariable.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}