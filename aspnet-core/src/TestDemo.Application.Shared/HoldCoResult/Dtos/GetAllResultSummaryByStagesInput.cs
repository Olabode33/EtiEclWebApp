using Abp.Application.Services.Dto;
using System;

namespace TestDemo.HoldCoResult.Dtos
{
    public class GetAllResultSummaryByStagesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}