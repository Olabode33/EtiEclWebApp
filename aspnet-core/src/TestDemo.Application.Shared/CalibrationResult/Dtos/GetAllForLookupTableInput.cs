using Abp.Application.Services.Dto;

namespace TestDemo.LgdCalibrationResult.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}