using Abp.Application.Services.Dto;

namespace TestDemo.Calibration.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}