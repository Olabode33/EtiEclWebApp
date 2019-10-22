using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAllLgdAssumptionUnsecuredRecoveriesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string InputNameFilter { get; set; }



    }
}