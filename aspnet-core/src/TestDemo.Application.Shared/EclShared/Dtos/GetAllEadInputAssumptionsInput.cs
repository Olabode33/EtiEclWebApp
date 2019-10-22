using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllEadInputAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string InputNameFilter { get; set; }



    }
}