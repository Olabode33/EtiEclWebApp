using Abp.Application.Services.Dto;
using System;

namespace TestDemo.HoldCoInterCompanyResults.Dtos
{
    public class GetAllHoldCoInterCompanyResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}