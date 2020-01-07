using Abp.Application.Services.Dto;
using System;

namespace TestDemo.EclConfig.Dtos
{
    public class GetAllEclConfigurationsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int DataTypeFilter { get; set; }



    }
}