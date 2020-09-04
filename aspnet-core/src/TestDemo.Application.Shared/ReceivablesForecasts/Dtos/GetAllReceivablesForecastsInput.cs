using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ReceivablesForecasts.Dtos
{
    public class GetAllReceivablesForecastsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}