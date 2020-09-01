using Abp.Application.Services.Dto;
using System;

namespace TestDemo.HoldCoResult.Dtos
{
    public class GetAllHoldCoResultSummariesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}