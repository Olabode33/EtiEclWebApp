using Abp.Application.Services.Dto;

namespace TestDemo.GeneralCalibrationResult.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}