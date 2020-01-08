using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllInvSecMacroEconomicAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int StatusFilter { get; set; }
        


    }
}