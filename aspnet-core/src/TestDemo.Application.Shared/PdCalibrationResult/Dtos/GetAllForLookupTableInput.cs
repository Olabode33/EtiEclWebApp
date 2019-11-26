using Abp.Application.Services.Dto;

namespace TestDemo.PdCalibrationResult.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}